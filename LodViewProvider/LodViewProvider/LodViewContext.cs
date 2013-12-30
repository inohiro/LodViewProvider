﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Net;
using System.Reflection;

using LodViewProvider.LinqToTwitter;

namespace LodViewProvider {

	/// <summary>
	/// Context for using LINQ
	/// </summary>
	public class LodViewContext {

		public string RawResult { get; private set; }
		public string ViewURI { get; private set; }

		public LodViewContext( string viewUri ) {
			ViewURI = viewUri;
			LodViewExecutor = new LodViewExecute();
		}

		public LodViewContext( string viewUri, LodViewExecute executor ) {
			ViewURI = viewUri;
			// LodViewExecutor = executor;
		}

		internal LodViewExecute LodViewExecutor { get; private set; }

		public LodViewQueryable<Resource> Resource {
			get {
				return new LodViewQueryable<Resource>( this, ViewURI );
			}
		}

		public virtual object Execute<T>( Expression expression, bool isEnumerable ) {
			// var requestProcessor = createRequestProcessor( expression );
			var requestProcessor = new RequestProcessor();
			var conditions = getRequestParameters( expression, requestProcessor );

			Request request = requestProcessor.CreateRequest( ViewURI, conditions );
			string result = LodViewExecute.RequestToLod( request, requestProcessor );

			// var queryableList = requestProcessor.ProcessResult( result );
			var queryableResources = requestProcessor.ProcessResult( result ).AsQueryable();

			var treeCopier = new ExpressionTreeModifier( queryableResources );
			Expression newExpressionTree = treeCopier.CopyAndModify( expression );

			if ( isEnumerable ) {
				return queryableResources.Provider.CreateQuery( newExpressionTree );
			}

			return queryableResources.Provider.Execute( newExpressionTree );
		}

		private List<Condition> getRequestParameters( Expression expression, RequestProcessor requestProcessor ) {
			var conditions = new List<Condition>();

			var whereExpressions = new WhereClauseFinder().GetAllWheres( expression );
			foreach ( var whereExpression in whereExpressions ) {
				var lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression ) ( whereExpression.Arguments[1] ) ).Operand;
				lambdaExpression = ( LambdaExpression ) Evaluator.PartialEval( lambdaExpression );

				var filter = requestProcessor.GetParameters( lambdaExpression );
				conditions.Add( filter );
			}

			// TODO: Aggregate Function { 'sum', 'avg', 'count', 'min', 'max' }
			// http://www.w3.org/TR/sparql11-query/#sparqlAlgebra

			// set method type by myself like : where, aggregation, ...

			var aggFunctions = new AggregateFunctionFinder().GetAllAggFunctions( expression );
			foreach( var aggFunction in aggFunctions ) {
				var lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression) ( aggFunction.Arguments[1] ) ).Operand;
				lambdaExpression = ( LambdaExpression ) Evaluator.PartialEval( lambdaExpression );

				var aggregation = requestProcessor.GetParameters( lambdaExpression );
				conditions.Add( aggregation );
			}

			return conditions;
		}
	}
}
