using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	public class ViewedLodTupleProvider : IQueryProvider {

		public IQueryable CreateQuery( Expression expression ) {
			Type elementType = TypeSystem.GetElementType( expression.Type );
			try {
				return ( IQueryable ) Activator.CreateInstance(
					typeof( QueryableLodTuple<> ).MakeGenericType( elementType ),
					new object[] { this, expression } );
			}
			catch ( System.Reflection.TargetInvocationException tie ) { throw tie.InnerException; }
		}

		public IQueryable<TResult> CreateQuery<TResult>( Expression expression ) {
			return new QueryableLodTuple<TResult>( this, expression );
		}

		public object Execute( Expression expression ) {
			return ViewedLodQueryContext.Execute( expression, false );
		}

		public TResult Execute<TResult>( Expression expression ) {
			bool isEnumerable = ( typeof( TResult ).Name == "IEnumerable`1" );
			return ( TResult ) ViewedLodQueryContext.Execute( expression, isEnumerable );
		}
	}
}