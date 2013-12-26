using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	public class LodViewQueryProvider : IQueryProvider {

		public LodViewContext Context { get; set; }

		private readonly string viewUrl;

		public LodViewQueryProvider( string viewUrl ) {
			this.viewUrl = viewUrl;
		}

		public IQueryable<TElement> CreateQuery<TElement>( Expression expression ) {
			return ( IQueryable<TElement> ) new LodViewExecute( this, expression );
		}

		public IQueryable CreateQuery( Expression expression ) {
			return new LodViewExecute( this, expression );
		}

		public TResult Execute<TResult>( Expression expression ) {
			bool isEnumerable = ( typeof( TResult ).Name == "IEnumerable`1" );
			return ( TResult ) LodViewExecute.Execute( expression, isEnumerable, viewUrl );
		}

		public object Execute( Expression expression ) {
			throw new NotImplementedException();
		}
	}
}