using System;
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
		}

		internal LodViewExecute LodViewExecutor { get; private set; }

		public LodViewQueryable<Resource> Resource {
			get {
				return new LodViewQueryable<Resource>( this, ViewURI );
			}
		}

		public virtual object Execute<T>( Expression expression, bool isEnumerable ) {
			var requestProcessor = new RequestProcessor();
			var conditions = getRequestParameters( expression, requestProcessor );

			Request request = requestProcessor.CreateRequest( ViewURI, conditions );
			string result = LodViewExecute.RequestToLod( request, requestProcessor );

			var queryableResources = requestProcessor.ProcessResult( result ).AsQueryable();
			var treeCopier = new ExpressionTreeModifier( queryableResources );
			Expression newExpressionTree = treeCopier.CopyAndModify( expression );

			if ( isEnumerable ) {
				return queryableResources.Provider.CreateQuery( newExpressionTree );
			}

			return queryableResources.Provider.Execute( newExpressionTree );
		}

		private List<IRequestable> getRequestParameters( Expression expression, RequestProcessor requestProcessor ) {
			var conditions = new List<IRequestable>();

			//
			// 'Select' Expression
			//

			var selectExpressions = new SelectClauseFinder().GetAllSelections( expression );
			foreach ( var selectExpression in selectExpressions ) {
				var lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression ) ( selectExpression.Arguments[1] ) ).Operand;
				lambdaExpression = ( LambdaExpression ) Evaluator.PartialEval( lambdaExpression );

				var selection = requestProcessor.GetParameters( lambdaExpression, TargetMethodType.Projection );
				conditions.Add( selection );
			}

			//
			// 'Where' Expression
			//

			var whereExpressions = new WhereClauseFinder().GetAllWheres( expression );
			foreach ( var whereExpression in whereExpressions ) {
				var lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression ) ( whereExpression.Arguments[1] ) ).Operand;
				lambdaExpression = ( LambdaExpression ) Evaluator.PartialEval( lambdaExpression );

				var filter = requestProcessor.GetParameters( lambdaExpression, TargetMethodType.Selection );
				conditions.Add( filter );
			}

			//
			// Aggregation Expressions
			//

			var aggFunctions = new AggregateFunctionFinder().GetAllTarget( expression );
			foreach( Tuple<MethodCallExpression, AggregationType> aggFunction in aggFunctions ) {
				var lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression) ( aggFunction.Item1.Arguments[1] ) ).Operand;
				lambdaExpression = ( LambdaExpression ) Evaluator.PartialEval( lambdaExpression );

				var aggregation = requestProcessor.GetParameters( lambdaExpression, TargetMethodType.Aggregation, aggFunction.Item2 );
				conditions.Add( aggregation );
			}

			return conditions;
		}
	}
}
