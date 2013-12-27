using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;

using Newtonsoft.Json;

namespace LodViewProvider {

	public class QueryParameter {

		public QueryParameter() {
			Filters = new Dictionary<string, string>();
		}

		public QueryParameter( Dictionary<string,string> filters ) {
			Filters = filters;
		}

		public Dictionary<string, string> Filters { get; private set; }

		public string QueryString () {
			string serializedFilters = JsonConvert.SerializeObject( Filters );
			string encodedQuery = HttpUtility.UrlEncode( serializedFilters );
			return String.Format( "query={0}", serializedFilters );
		}
	}
}
