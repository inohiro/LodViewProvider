using System;
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
	}
}
