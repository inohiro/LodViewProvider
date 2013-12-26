using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	/// <summary>
	/// Execute several operations to LOD
	/// </summary>
	public class LodViewExecute {

		internal static object Execute( Expression expression, bool isEnumerable, string viewUrl ) {

			/*
			var queryableResources = RequestToLod( viewUrl );
			var treeCopier = new ExpressionTreeModifier( queryableResources );
			var newExpressionTree = treeCopier.Visit( expression );
			*/

			InnermostWhereFinder whereFinder = new InnermostWhereFinder();
			MethodCallExpression whereExpression = whereFinder.GetInnermostWhere( expression );
			LambdaExpression lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression ) ( whereExpression.Arguments[1] ) ).Operand;

			// IMA KOKO

			if ( isEnumerable ) {
				return queryableResources.Provider.CreateQuery( newExpressionTree );
			}
			else {
				return queryableResources.Provider.Execute( newExpressionTree );
			}
		}

		private static IQueryable<Resource> RequestToLod( string viewUrl ) {
			var list = new List<Resource>();
			var hash = new Dictionary<string, string>();
			hash.Add( "name", "inohiro" );
			hash.Add( "viewUrl", viewUrl );
			list.Add( new Resource( "hello", hash ) );

			return list.AsQueryable();
		}
	}
}
