using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Net;
using System.IO;

namespace LodViewProvider {

	/// <summary>
	/// Execute several operations to LOD
	/// </summary>
	public class LodViewExecute {

		internal static string RequestToLod( Request request, RequestProcessor requestProcessor ) {
			// TODO: create Logger

			string response = String.Empty;
			string httpStatus = String.Empty;
			WebClient client = new WebClient();

			try {
				// TODO: Check Full URL
				string fullUrl = request.FullURL();
				Stream stream = client.OpenRead( fullUrl );

				// TODO: Consider Async Access
				// client.OpenReadAsync( new Uri( request.FullURL()));

				StreamReader streamReader = new StreamReader( stream );
				response = streamReader.ReadToEnd();
			}
			catch ( WebException webex ) {
				// TODO: Logger.log( webex );
				throw webex;
			}

			// TODO: should check the status code
			return response;
		}

		private static WebRequest createWebRequest( Request request ) {
			Uri requestUri = new Uri( request.FullURL() );
			return HttpWebRequest.Create( requestUri );
			// return WebRequest.Create( requestUri );
		}

		internal static void dispose() {
			throw new NotImplementedException();
		}
	}
}
