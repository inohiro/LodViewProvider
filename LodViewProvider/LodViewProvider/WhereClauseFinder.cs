using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LodViewProvider {

	internal class WhereClauseFinder : ExpressionVisitor {

		static readonly string[] whereMethodNames = { "Where", "Single", "First" };
		readonly List<MethodCallExpression> whereExpressions = new List<MethodCallExpression>();

		public MethodCallExpression[] GetAllWheres( Expression expression ) {
			Visit( expression );
			return whereExpressions.ToArray();
		}

		protected override Expression VisitMethodCall( MethodCallExpression expression ) {
			if ( whereMethodNames.Contains( expression.Method.Name ) && expression.Arguments.Count == 2 ) {
				whereExpressions.Add( expression );
			}

			Visit( expression.Arguments[0] );
			return expression;
		}
	}
}
