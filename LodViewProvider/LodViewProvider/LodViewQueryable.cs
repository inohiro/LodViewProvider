using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects;

namespace LodViewProvider {

	/// <summary>
	/// Object which is queried
	/// </summary>
	public class LodViewQueryable : IQueryable<Resource> { // IOrderedQueryable<Resource> {

		public string ViewUrl { get; private set; }
		public IQueryProvider Provider { get; private set; }
		public Expression Expression { get; private set; }

		public LodViewQueryable( LodViewContext context, string viewUrl ) {
			ViewUrl = viewUrl;
			Provider = new LodViewQueryProvider();
			Expression = Expression.Constant( this );
			( ( LodViewQueryProvider ) Provider ).Context = context;
		}

		internal LodViewQueryable( LodViewQueryProvider provider, Expression expression ) {
			// TODO: Error processing is required
			Provider = provider;
			Expression = expression;
		}

		public IEnumerator<Resource> GetEnumerator() {
			return Provider.Execute<IEnumerable<Resource>>( Expression ).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return ( Provider.Execute<IEnumerable>( Expression ) ).GetEnumerator();
		}

		public Type ElementType {
			get { return typeof( Resource ); }
		}
	}
}
