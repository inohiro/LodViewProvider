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

			/*
			 * 
			 * Projection: Select
			 * 
			 */

			var values = from resource in context
						 where resource.Values["names"] == "inohiro"
						 select resource;

			var values1 = context.Select( e => e.Values["name"] );
			// var result1 = values1.ToArray();

			var values2 = context.Select( e => e.Values["name"] == "inohiro" );
			// var result2 = values2.ToArray();

			var values3 = context.Select( e => new Tuple<string, string>( e.Values["strig"], e.Values["age"] ) );
			// var result3 = values3.ToArray();

			var values4 = context
				.Where( e => Int32.Parse( e.Values["age"] ) <= 30 )
				.Select( e => new { Name = e.Values["name"], Age = e.Values["age"] } );
			// var result4 = values4.ToArray();

			var values5 = from resource in context
						  where Int32.Parse( resource.Values["age"] ) <= 30
						  select new { Name = resource.Values["name"], Age = resource.Values["age"] };
			// var result5 = values5.ToArray();

			/*
			 * 
			 * Aggregation: Min, Max, Sum, Count, Average
			 * 
			 */

			// var avg = context.Average( e => Int32.Parse( e.Values["age"] ) ); // Variable has a problem
			// var avg2 = context.Where( e => e.Values["name"] == "inohiro" ).Average( e => Int32.Parse( e.Values["age"] ) ); // No Problem

			// var count = context.Count( e => e.Values["name"] == "inohiro" ); // No Problem
			// var count2 = context.Where( e => e.Values["name"] == "inohiro" ).Count(); // Request parameter will be empty
			// var count3 = context.Where( e => e.Values["name"] == "inohiro" ).Select( e => e.Values["name"] ); // Request parameter will be empty

			// var sum = context.Sum( e => Int32.Parse( e.Values["age"] ) ); // Variable has a problem
			// var sum2 = context.Select( e => Int32.Parse( e.Values["age"] ) ).Count(); // Request parameter will be empty
			// var sum3 = context.Where( e => e.Values["name"] == "inohiro" ).Sum( e => Int32.Parse( e.Values["age"] ) ); // Variable has a problem

			// var max = context.Max( e => e.Values["age"] ); // No problem
			// var max2 = context.Select( e => e.Values["age"] ).Max(); // Request parameter will be empty
			// var max3 = context.Where( e => e.Values["name"] == "inohiro" ).Max( e => e.Values["age"] ); // No problem

			// var min = context.Min( e => e.Values["age"] ); // No problem
			// var min2 = context.Select( e => e.Values["age"] ).Min(); // Request Parameter will be empty
			// var min3 = context.Where( e => e.Values["name"] == "inohiro" ).Min( e => e.Values["age"] ); // No problem

			// var values2 = context.Where( r => r.Values["name"] == "inohiro" );
			// var distinctedNames = context.Select( e => e.Values["name"] == "inohiro" ).Distinct();
			// var distinctedNames2 = context.Where( e => e.Values["name"] == "inohiro" ).Distinct(); // No Problem
			// var distinctedNames3 = context.Distinct( e => e.Values["name"] == "inohiro" ); // Need to support IEqualityComparable interface

			//foreach ( var v in values ) {
			//    Console.WriteLine( v );
			//}

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
