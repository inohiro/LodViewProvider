using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects;

namespace LodViewProvider {

	public class ViewedLodResourceContext : IOrderedQueryable<Resource> {

		public string ViewUrl { get; private set; }
		public IQueryProvider Provider { get; private set; }
		public Expression Expression { get; private set; }

		public ViewedLodResourceContext( string viewUrl ) {
			ViewUrl = viewUrl;
		}

		internal ViewedLodResourceContext( IQueryProvider provider, Expression expression ) {
			Provider = provider;
			Expression = expression;
		}

		public IEnumerator<Resource> GetEnumerator() {
			return Provider.Execute<IEnumerable<Resource>>( Expression ).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			// return Provider.Execute<System.Collections.IEnumerable>( Expression ).GetEnumerator();
			return GetEnumerator();
		}

		public Type ElementType {
			get { return typeof( Resource ); }
		}
	}
}
