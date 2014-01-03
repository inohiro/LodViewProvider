using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LodViewProvider {

	class SelectClauseFinder : ExpressionVisitor {

		static readonly string selectMethodName = "Select";
		readonly List<MethodCallExpression> selectExpressions = new List<MethodCallExpression>();

		internal MethodCallExpression[] GetAllSelections( Expression expression ) {
			Visit( expression );
			return selectExpressions.ToArray();
		}

		protected override Expression VisitMethodCall( MethodCallExpression expression ) {
			if ( selectMethodName == expression.Method.Name && expression.Arguments.Count() == 2 ) {
				selectExpressions.Add( expression );
			}

			Visit( expression.Arguments[0] );
			return expression;
		}
	}
}
