using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	public enum TargetMethodType {
		Selection,
		Projection,
		Aggregation
	}

	public class RequestProcessor {

		public RequestProcessor() {
		}
		
		public IRequestable GetParameters( LambdaExpression lambdaExpression, TargetMethodType methodType, AggregationType aggType = AggregationType.Min ) {
			IRequestable condition = null;

			switch ( methodType ) {
				case TargetMethodType.Projection: {
					switch ( lambdaExpression.Body.NodeType ) {
						case ExpressionType.Equal: {
							condition = createProjectionFunction( lambdaExpression ); // Select a variable
						} break;
						case ExpressionType.New: {
							condition = createManyProjectionFunction( lambdaExpression ); // Select many variables
						}break;
						default: {
							condition = createProjectionFunction( lambdaExpression ); // Select a variable
						} break;
					}
				}break;
				case TargetMethodType.Selection: {
					condition = createFilterForSelection( lambdaExpression );
				} break;
				case TargetMethodType.Aggregation: {
					switch ( aggType ) {
						case AggregationType.Count: {
							condition = createCountFunction( lambdaExpression );
						} break;
						default: {
							condition = createAggregationForAggregationFunction( lambdaExpression, aggType );
						} break;
					}
				} break;
			}

			return condition;
		}

		private IRequestable createManyProjectionFunction( LambdaExpression lambdaExpression ) {
			NewExpression newExp = null;

			try {
				newExp = lambdaExpression.Body as NewExpression;
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			List<SingleSelection> singleSelections = new List<SingleSelection>();
			foreach ( var arg in newExp.Arguments ) {
				var mcall = arg as MethodCallExpression;
				singleSelections.Add( new SingleSelection( mcall.Arguments[0].ToString() ) );
			}

			return new MultipleSelection( singleSelections );
		}

		private SingleSelection createProjectionFunction( LambdaExpression lambdaExpression ) {
			var tuple = castBinaryExpression( lambdaExpression );
			return new SingleSelection( tuple.Item1, tuple.Item2, tuple.Item3 );
		}

		private Tuple<string, string, string> castBinaryExpression( LambdaExpression lambdaExpression ) {
			BinaryExpression binExp = null;

			try {
				binExp = lambdaExpression.Body as BinaryExpression;
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			var left = binExp.Left as MethodCallExpression;
			Tuple<string, string, string> conditionTuple = new Tuple<string, string, string>(
				left.Arguments[0].ToString(),
				binExp.Right.ToString(),
				detectOperator( binExp.NodeType ) );

			return conditionTuple;
		}

		private Aggregation createCountFunction( LambdaExpression lambdaExpression ) {
			var tuple = castBinaryExpression( lambdaExpression );
			return new Aggregation( tuple.Item1, AggregationType.Count );
		}

		private Filter createFilterForSelection( LambdaExpression lambdaExpression ) {
			var tuple = castBinaryExpression( lambdaExpression );
			return new Filter( tuple.Item1, tuple.Item2, tuple.Item3 );
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
				} break;
				case ExpressionType.NotEqual: {
					oper = "!=";
				}break;
				default: {
					throw new InvalidOperationException();
				} break;
			}
			return oper;
		}

		public string QueryString { get; set; }

		public string Result { get; set; }

		internal Request CreateRequest( string ViewURI, List<IRequestable> conditions ) {
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
