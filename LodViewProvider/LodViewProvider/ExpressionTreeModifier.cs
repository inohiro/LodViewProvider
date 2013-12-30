﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LodViewProvider {

	internal class ExpressionTreeModifier : ExpressionVisitor {

		private IQueryable<Resource> queryableResources;

		internal ExpressionTreeModifier( IQueryable<Resource> resources ) {
			this.queryableResources = resources;
		}

		//protected override Expression VisitConstant( ConstantExpression node ) {
		//    if ( node.Type == typeof( Resource ) ) {
		//        return Expression.Constant( this.queryableResources );
		//    }
		//    else {
		//        return node;
		//    }
		//}

		protected override Expression VisitConstant( ConstantExpression c ) {
			if ( c.Type.Name == "LodViewQueryable`1" ) {
				return Expression.Constant( queryableResources );
			}
			return c;
		}

		internal Expression CopyAndModify( Expression expression ) {
			return Visit( expression );
		}
	}
}
