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
		/// Target View URI (API)
		/// </summary>
		public string ViewURL { get; set; }

		/// <summary>
		/// Set of condition
		/// </summary>
		public Dictionary<string, string> GetParameters( LambdaExpression lambdaExpression ) {
			// set parameters
			return new Dictionary<string, string>();
		}

		public string QueryString { get; set; }

		public string BuildURL( Dictionary<string, string> parameters ) {
			return "";
		}

		/// <summary>
		/// Result
		/// </summary>
		public string Result { get; set; }

		
	}
}
