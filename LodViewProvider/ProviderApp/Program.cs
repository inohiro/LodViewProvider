using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LodViewProvider;

namespace ProviderApp {
	class Program {
		static void Main( string[] args ) {

			string viewUrl = "http://lodviewwebapp.herokuapp.com/test/1/";
			var context = new LodViewContext( viewUrl ).Resource;

			var values = from resource in context
						 where resource.Values["names"] == "inohiro"
						 select resource;

			var values2 = context.Where( r => r.Values["name"] == "inohiro" );
			var values3 = context.Max( e => e.Values["age"] );

			// var avg = context.Average()
			// var min = context.Min()
			// var max = context.Max()
			// var count = context.Count()
			// var sum = context.Sum


			// var distinct = context.Distinct()

			foreach ( var v in values3 ) {
				Console.WriteLine( v );
			}

			Console.ReadKey();

			/*
			 * var max = context.Max( e => e["age"] );
			 * var min = context.Max( e => e["age"] );
			 * var avg = context.Average( e => e["age"] );
			 * var count = context.Count( e => e["name"] == "inohiro" );
			 */
		}
	}
}
