using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	public class RequestProcessor {

		public RequestProcessor() {
		}
		
		/// <summary>
		/// Set of condition
		/// </summary>
		public Filter GetParameters( LambdaExpression lambdaExpression ) {
			BinaryExpression binExp = null;

			var unary = lambdaExpression.Body as MethodCallExpression;


			try {
				binExp = lambdaExpression.Body as BinaryExpression;
			}
			catch ( InvalidCastException icxp ) {
				throw icxp;
			}

			MethodCallExpression left = null;

			try {
				left = binExp.Left as MethodCallExpression;
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			string leftValue = left.Arguments[0].ToString();
			string rightValue = binExp.Right.ToString();
			string oper = detectOperator( binExp.NodeType );

			return new Filter( leftValue, rightValue, oper );
		}

		private string detectOperator( ExpressionType expressionType ) {
			string oper = "";
			switch ( expressionType ) {
				case ExpressionType.Equal: {
					oper = "=";
				} break;
				case ExpressionType.GreaterThan: {
					oper = ">";
				} break;
				case ExpressionType.GreaterThanOrEqual: {
					oper = ">=";
				} break;
				case ExpressionType.LessThan: {
					oper = "<";
				} break;
				case ExpressionType.LessThanOrEqual: {
					oper = "<=";
				}break;
				default: {
					throw new InvalidOperationException();
				}break;
			}
			return oper;
		}

		public string QueryString { get; set; }

		/// <summary>
		/// Result
		/// </summary>
		public string Result { get; set; }

		internal Request CreateRequest( string ViewURI, List<Condition> conditions ) {
			QueryParameter queryParameter = new QueryParameter( conditions );
			Request request = new Request( ViewURI, queryParameter );
			return request;
		}

		internal List<Resource> ProcessResult( string result ) {
			Console.WriteLine( result );
			return new List<Resource>();
		}
	}
}
