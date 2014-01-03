using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Newtonsoft.Json;

namespace LodViewProvider {

	public class QueryParameter {

		public List<IRequestable> Conditions { get; private set; }

		public QueryParameter()
			: this( new List<IRequestable>() ) { }

		public QueryParameter( List<IRequestable> conditions ) {
			Conditions = conditions;
		}

		public string CreateQueryString () {
			string serializedFilters = JsonConvert.SerializeObject( Conditions );

			// StringBuilder strb = new StringBuilder();
			// Conditions.ForEach( c => strb.Append( c.ToString() ) );
			// string serializedFilters = strb.ToString();

			string encodedQuery = HttpUtility.UrlEncode( serializedFilters );
			// TODO: Consider empty query string
			return String.Format( "query={0}", encodedQuery );
		}
	}
}
