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
							condition = createSingleSelectionFunctionFromBinaryExpression( lambdaExpression ); // Select a variable with condition
						} break;
						case ExpressionType.New: {
							condition = createMultipleSelectionFunction( lambdaExpression ); // Select many variables
						} break;
						case ExpressionType.Call: {
							condition = createSingleSelectionFunction( lambdaExpression ); // Select a variable
						}break;
						case ExpressionType.Parameter: {
							condition = new All(); // Select all with no condition
						}break;
						default: {
							condition = createSingleSelectionFunctionFromBinaryExpression( lambdaExpression ); // Select a variable with condition
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

		private MultipleSelection createMultipleSelectionFunction( LambdaExpression lambdaExpression ) {
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

		private SingleSelection createSingleSelectionFunction( LambdaExpression lambdaExpression ) {
			MethodCallExpression mCallExp = null;
			string variable = null;

			try {
				mCallExp = lambdaExpression.Body as MethodCallExpression;
				variable = mCallExp.Arguments[0].ToString();
				if ( mCallExp.NodeType == ExpressionType.Call ) {
					var variableExpression = mCallExp.Arguments[0] as MethodCallExpression;
					if ( variableExpression != null ) {
						variable = variableExpression.Arguments[0].ToString();
					}
				}
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			return new SingleSelection( variable );
		}

		private SingleSelection createSingleSelectionFunctionFromBinaryExpression( LambdaExpression lambdaExpression ) {
			var tuple = castBinaryExpression( lambdaExpression );
			return new SingleSelection( tuple.Item1, tuple.Item2, tuple.Item4 );
		}

		private Tuple<string, string, string, string> castBinaryExpression( LambdaExpression lambdaExpression ) {
			BinaryExpression binExp = null;
			MethodCallExpression mCallExp = null;
			string variable = null;

			try {
				binExp = lambdaExpression.Body as BinaryExpression;
				mCallExp = binExp.Left as MethodCallExpression;
				variable = mCallExp.Arguments[0].ToString();
				if ( mCallExp.NodeType == ExpressionType.Call ) {
					var variableExpression = mCallExp.Arguments[0] as MethodCallExpression;
					if ( variableExpression != null ) {
						variable = variableExpression.Arguments[0].ToString();
					}
				}
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			var left = binExp.Left as MethodCallExpression;
			Tuple<string, string, string, string> conditionTuple = new Tuple<string, string, string, string>(
				variable,
				binExp.Right.ToString(),
				binExp.Right.Type.ToString(),
				detectOperator( binExp.NodeType ) );

			return conditionTuple;
		}

		private Aggregation createCountFunction( LambdaExpression lambdaExpression ) {
			var tuple = castBinaryExpression( lambdaExpression );
			return new Aggregation( tuple.Item1, AggregationType.Count );
		}

		private Filter createFilterForSelection( LambdaExpression lambdaExpression ) {
			var tuple = castBinaryExpression( lambdaExpression );
			return new Filter( tuple.Item1, tuple.Item2, tuple.Item4, FilterType.Normal, tuple.Item3 );
		}

		private Aggregation createAggregationForAggregationFunction( LambdaExpression lambdaExpression, AggregationType aggType ) {
			MethodCallExpression mCallExp = null;
			string variable = null;

			try {
				mCallExp = lambdaExpression.Body as MethodCallExpression;
				variable = mCallExp.Arguments[0].ToString();
				if ( mCallExp.NodeType == ExpressionType.Call ) {
					var variableExpression = mCallExp.Arguments[0] as MethodCallExpression;
					if ( variableExpression != null ) {
						variable = variableExpression.Arguments[0].ToString();
					}
				}
			}
			catch ( InvalidCastException icex ) {
				throw icex;
			}

			return new Aggregation( variable, aggType );
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
