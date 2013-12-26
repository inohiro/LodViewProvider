using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Net;
using System.Reflection;

namespace LodViewProvider {

	/// <summary>
	/// Context for using LINQ
	/// </summary>
	public class LodViewContext {

		public string RawResult { get; private set; }
		public string ViewURI { get; private set; }
		
		public LodViewContext( string viewUri, LodViewExecute executor ) {
			ViewURI = viewUri;
			LodViewExecutor = executor;
		}
		
		internal LodViewExecute LodViewExecutor { get; private set; }
		public LodViewQueryable LodView() {
			return new LodViewQueryable( this, ViewURI );
		}

		public virtual object Execute( Expression expression, bool isEnumerable ) {
			// var requestProcessor = createRequestProcessor( expression );
			var requestProcessor = new RequestProcessor();
			var parameters = getRequestParameters( expression, requestProcessor );
		}

		private object getRequestParameters( Expression expression, RequestProcessor requestProcessor ) {
			var parameters = new Dictionary<string, string>();

			var whereExpressions = new WhereClauseFinder().GetAllWheres( expression );
			foreach ( var whereExpression in whereExpressions ) {
				var lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression ) ( whereExpression.Arguments[1] ) ).Operand;
				lambdaExpression = ( LambdaExpression ) Evaluator. // IMA KOKO

				// lambdaExpression = requestProcessor.GetParameters( lambdaExpression );
				foreach ( var newParameter in newparameters ) {
					
				}
			}
		}
	}
}
