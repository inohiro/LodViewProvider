using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LodViewProvider {

	internal class AggregateFunctionFinder : ExpressionVisitor {

		static readonly string[] aggMethodNames = { "Min", "Max", "Sum", "Count", "Average" };
		readonly List<Tuple<MethodCallExpression, AggregationType>> aggMethodExpressions = new List<Tuple<MethodCallExpression, AggregationType>>();

		protected override Expression VisitMethodCall( MethodCallExpression expression ) {
			if ( aggMethodNames.Contains( expression.Method.Name ) && expression.Arguments.Count == 2 ) {
				int index = Array.IndexOf( aggMethodNames, expression.Method.Name );
				AggregationType aggType = detectAggregationType( index );
				aggMethodExpressions.Add( new Tuple<MethodCallExpression,AggregationType>( expression, aggType ) );
			}

			Visit( expression.Arguments[0] );
			return expression;
		}

		public List<Tuple<MethodCallExpression, AggregationType>> GetAllTarget( Expression expression ) {
			Visit( expression );
			return aggMethodExpressions;
		}

		private AggregationType detectAggregationType( int index ) {
			string aggMethodName = aggMethodNames[index];

			AggregationType aggType;
			try {
				if ( Enum.IsDefined( typeof( AggregationType ), aggMethodName ) ) {
					aggType = ( AggregationType ) Enum.Parse( typeof( AggregationType ), aggMethodName );
				}
				else {
					throw new InvalidAggregationTypeException();
				}
			}
			catch ( ArgumentException argex ) {
				throw argex;
			}

			return aggType;
		}
	}
}