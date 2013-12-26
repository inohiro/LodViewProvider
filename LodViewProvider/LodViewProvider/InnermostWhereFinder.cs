using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace LodViewProvider {

	internal class InnermostWhereFinder : ExpressionVisitor {

		private MethodCallExpression innermostWhereExperssion;

		public MethodCallExpression GetInnermostWhere( Expression expression ) {
			Visit( expression );
			return innermostWhereExperssion;
		}

		protected override Expression VisitMethodCall( MethodCallExpression expression ) {
			if ( expression.Method.Name == "Where" ) {
				innermostWhereExperssion = expression;
			}

			Visit( expression.Arguments[0] );
			return expression;
		}
	}
}
