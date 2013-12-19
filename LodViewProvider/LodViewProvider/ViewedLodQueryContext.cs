using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {
	public class ViewedLodQueryContext {
		internal static object Execute( Expression expression, bool isEnumerable, string viewUrl ) {
			// preliminary
			if ( isEnumerable ) {
			}
			else {
			}
			throw new NotImplementedException();
		}

		private static IQueryable<Resource> RequestToLod( string viewUrl ) {
			throw new NotImplementedException();
			// http://www.jacopretorius.net/2010/01/implementing-a-custom-linq-provider.html
		}
	}
}
