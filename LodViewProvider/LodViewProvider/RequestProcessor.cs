using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LodViewProvider {

	public class RequestProcessor {

		public RequestProcessor() {
		}

		/// <summary>
		/// Set of condition
		/// </summary>
		public Dictionary<string, string> GetParameters( LambdaExpression lambdaExpression ) {
			// set parameters
			return new Dictionary<string, string>();
		}

		public string QueryString { get; set; }

		/// <summary>
		/// Result
		/// </summary>
		public string Result { get; set; }

		internal Request CreateRequest( string ViewURI, Dictionary<string, string> parameters ) {
			QueryParameter queryParameter = new QueryParameter( parameters );
			Request request = new Request( ViewURI, queryParameter );
			return request;
		}

		internal List<Resource> ProcessResult( string result ) {
			throw new NotImplementedException();
		}
	}
}
