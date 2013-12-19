using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	public class ViewedLodProvider : IQueryProvider {

		private readonly string viewUrl;

		public ViewedLodProvider( string viewUrl ) {
			this.viewUrl = viewUrl;
		}

		public IQueryable<TElement> CreateQuery<TElement>( Expression expression ) {
			return ( IQueryable<TElement> ) new ViewedLodContext( this, expression );
		}

		public IQueryable CreateQuery( Expression expression ) {
			return new ViewedLodContext( this, expression );
		}

		public TResult Execute<TResult>( Expression expression ) {
			var isEnumerable = ( typeof( TResult ).Name == "IEnumerable`1" );
			return ( TResult ) ViewedLodQueryContext.Execute( expression, isEnumerable, viewUrl );
		}

		public object Execute( Expression expression ) {
			throw new NotImplementedException();
		}
	}
}