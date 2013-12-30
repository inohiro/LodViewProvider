using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;

using Newtonsoft.Json;

namespace LodViewProvider {

	public class QueryParameter {

		public List<Filter> Filters { get; private set; }

		public QueryParameter() {
			Filters = new List<Filter>();
		}

		public QueryParameter( List<Filter> filters ) {
			Filters = filters;
		}

		public string CreateQueryString () {
			string serializedFilters = JsonConvert.SerializeObject( Filters );
			string encodedQuery = HttpUtility.UrlEncode( serializedFilters );
			// TODO: Consider empty query string
			return String.Format( "query={0}", encodedQuery );
		}
	}
}
