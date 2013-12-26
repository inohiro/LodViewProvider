using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	internal class MethodCallExpressionTypeFinder : ExpressionVisitor {
		private Type genericType;

		public Type GetGenericType( Expression expression ) {
			Visit( expression );
			return genericType;
		}

		protected override Expression VisitMethodCall( MethodCallExpression expression ) {
			if ( expression.Arguments.Count > 0 ) {
				genericType = expression.Method.GetGenericArguments()[0];
			}

			Visit( expression.Arguments[0] );
			return expression;
		}

	}

}
