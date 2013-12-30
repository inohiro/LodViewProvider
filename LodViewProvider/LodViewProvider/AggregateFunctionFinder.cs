using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LodViewProvider {

	internal class AggregateFunctionFinder : ExpressionVisitor {

		static readonly string[] aggMethodNames = { "Max", "Min", "Sum", "Average", "Count" };
		readonly List<MethodCallExpression> aggMethodExpressions = new List<MethodCallExpression>();

		public MethodCallExpression[] GetAllAggFunctions( Expression expression ) {
			Visit( expression );
			return aggMethodExpressions.ToArray();
		}

		protected override Expression VisitMethodCall( MethodCallExpression expression ) {
			if ( aggMethodNames.Contains( expression.Method.Name ) && expression.Arguments.Count == 2 ) { // ???
				aggMethodExpressions.Add( expression );
			}

			Visit( expression.Arguments[0] );
			return expression;
		}

	}
}
