using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {

	public class Request {

		public string ViewURL { get; private set; }
		public QueryParameter Parameter { get; private set; }

		public Request( string viewUrl ) {
			ViewURL = viewUrl;
			Parameter = new QueryParameter();
		}

		public Request( string viewUrl, QueryParameter parameter ) {
			ViewURL = viewUrl;
			Parameter = parameter;
		}

		public string FullURL () {
			string queryString = Parameter.QueryString();
			// TODO: Check the URL adn QueryString are valid or not

			return String.Format( "{0}?{1}", ViewURL, queryString );
		}
	}
}
