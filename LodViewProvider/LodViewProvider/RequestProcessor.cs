using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	public enum TargetMethodType {
		Selection,
		Aggregation
	}

	public class RequestProcessor {

		public RequestProcessor() {
		}
		
		public ICondition GetParameters( LambdaExpression lambdaExpression, TargetMethodType methodType, AggregationType aggType = AggregationType.Min ) {
			ICondition condition = null;

			switch ( methodType ) {
				case TargetMethodType.Selection: {
					condition = createFilterForSelection( lambdaExpression );
				} break;
				case TargetMethodType.Aggregation: {
					condition = createAggregationForAggregationFunction( lambdaExpression, aggType );
				} break;
			}

			return condition;
		}

		private Filter createFilterForSelection( LambdaExpression lambdaExpression ) {
			BinaryExpression binExp = null;

			try {
				binExp = lambdaExpression.Body as BinaryExpression;
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			var left = binExp.Left as MethodCallExpression;
			string leftValue = left.Arguments[0].ToString();
			string rightValue = binExp.Right.ToString();
			string oper = detectOperator( binExp.NodeType );

			return new Filter( leftValue, rightValue, oper );
		}

		private Aggregation createAggregationForAggregationFunction( LambdaExpression lambdaExpression, AggregationType aggType ) {
			MethodCallExpression mCallExp = null;

			try {
				mCallExp = lambdaExpression.Body as MethodCallExpression;
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			return new Aggregation( mCallExp.Arguments[0].ToString(), aggType );
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

		public string Result { get; set; }

		internal Request CreateRequest( string ViewURI, List<ICondition> conditions ) {
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
