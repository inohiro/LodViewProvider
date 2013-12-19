using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {
	class LodViewProvider : QueryProvider {
		public override string GetQueryText( System.Linq.Expressions.Expression expression ) {
			return string.Empty;
		}

		public override object Execute( System.Linq.Expressions.Expression expression ) {
			return null;
		}

		public static IQueryable<T> CreateQueryable<T>() {
			return new Query<T>( new LodViewProvider() );
		}
	}
}
