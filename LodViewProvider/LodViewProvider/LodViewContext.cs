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
			var parameters = getRequestParameters( expression, requestProcessor );

			Request request = requestProcessor.CreateRequest( ViewURI, parameters );
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

		// private List<Filter> getRequestParameters( Expression expression, RequestProcessor requestProcessor ) {
		private Dictionary<string, string> getRequestParameters( Expression expression, RequestProcessor requestProcessor ) {
			var parameters = new Dictionary<string, string>();
			var filters = new List<Filter>();

			var whereExpressions = new WhereClauseFinder().GetAllWheres( expression );
			foreach ( var whereExpression in whereExpressions ) {
				var lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression ) ( whereExpression.Arguments[1] ) ).Operand;
				lambdaExpression = ( LambdaExpression ) Evaluator.PartialEval( lambdaExpression );

				var filter = requestProcessor.GetParameters( lambdaExpression );
				//foreach ( var newParameter in newParameters ) {
				//    if ( !parameters.ContainsKey( newParameter.Key ) ) {
				//        parameters.Add( newParameter.Key, newParameter.Value );
				//    }
				// }
			}

			return parameters;
		}
	}
}
