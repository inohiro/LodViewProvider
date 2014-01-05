using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LodViewProvider;

namespace ProviderApp {
	class Program {
		static void Main( string[] args ) {

			// string viewUrl = "http://lodviewwebapp.herokuapp.com/test/1/";

			const string viewBaseUrl = "http://172.16.225.1:4567/"; // Network Setting: NAT
			string viewUrl = viewBaseUrl + "api/fixed/3/";

			Console.WriteLine( viewUrl );

			var context = new LodViewContext( viewUrl ).Resource;
			var dicontext = new LodViewContext( viewUrl ).Dictionary;
			var jcontext = new LodViewContext( viewUrl ).JTokens;
			var stringlistcont = new LodViewContext( viewUrl ).StringList;


//			var groupby = from resouce in dicontext
//						  group resouce by resouce["Affiliation"];
			// var groupby = dicontext.GroupBy( e => e["Affiliation"] ).Where( e => e.Count() < 30 );
			var gby = dicontext.Where( e => e["names"] == "inohiro" )
				.Where( e => Int32.Parse( e["age"] ) <= 20 )
				.GroupBy( e => e["Affiliation"] )
				.Where( e => e.Count() < 30 );
			// var gbresult = gby.ToList();


			var ob = dicontext.OrderBy( e => e["age"].Count() );
			var obresult = ob.ToList();
			var wob = dicontext.Where( e => e["name"] == "inohiro" ).GroupBy( e => e["Affiliation"] );
			// var wobresult = wob.ToArray();

			var rob = dicontext.OrderByDescending( e => e["age"] );
//			var orderby = from resource in dicontext
//						  orderby Int32.Parse( resource["age"] )
//						  select resource;
			// var obresult = rob.ToList();

			var gbob = dicontext.Where( e => e["name"] == "inohiro" )
				.GroupBy( e => e["Affiliation"] )
				.Where( e => e.Count() < 30 )
				.OrderBy( e => e.Count() );
			var gbobresult = gbob.ToList();

			var obgb = dicontext.Where( e => e["name"] == "inohiro" )
				.GroupBy( e => e["Affiliation"] )
				.OrderBy( e => e.Count() );

			// var avggb = dicontext.Average( e => Int32.Parse( e["age"] ) ); // we can not write following query?

			// PREFIX : <http://books.example/>
			// SELECT (SUM(?lprice) AS ?totalPrice)
			// WHERE {
			//   ?org :affiliates ?auth .
			//   ?auth :writesBook ?book .
			//   ?book :price ?lprice .
			// }
			// GROUP BY ?org
			// HAVING (SUM(?lprice) > 10)

			var hoge = dicontext.Average( e => Int32.Parse( e["price"] ) );
			// var fuga = dicontext.GroupBy( e => e["org"]).Where( e => 



			// var query = context.Select( e => e.Values["subject"] );
			// var result = query.ToList();
			// result.ForEach( e => Console.WriteLine( e ) );

			var query = dicontext.Select( e => e["subject"] == "hoge" );
			// var hoge = query.ToArray(); // fine, but expressions are evaluated in double


//			foreach ( var a in query ) {
//				Console.WriteLine( a );
//			}

			// query.ToList().ForEach( e => Console.WriteLine( e ) );

			// var query = context.Select( e => e.Values["subject"] == "inohiro" );
			// var query = context.Select( e => e.Values["subject"] );
			// var result = query.ToList();

			// var query = stringlistcont.Select( e => e );
			// var result = query.ToList();
			// result.ForEach( e => Console.WriteLine( e ) );  // like a Tuple

			// var count = context.Select( e => e.Values["subject"] ).Count();
			// var count_result = context.ToString();
			// var count_subject = context.Count( e => e.Values["subjec"] );
			// var count_result = count_subject.ToString();

			/*
			 * 
			 * Projection: Select
			 * 
			 */

			#region Selection

			/*
			 *  JSON.net (JToken)
			 */

			var jquery = jcontext.Select( e => e.SelectToken( "subject" ) ); // selection to result of API Access
			// jquery.ToList().ForEach( e => Console.WriteLine( e.Last.Last.ToString() ) );

			var all = from resource in context select resource;
			// var allresult = all.ToArray();

			var values = from resource in context
						 where resource.Values["names"] == "inohiro"
						 select resource;
			// var valuesresult = values.ToArray();

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

			#endregion

			/*
			 * 
			 * Aggregation: Min, Max, Sum, Count, Average
			 * 
			 */

			#region Aggregation

			#region Average

			// var avg = context.Average( e => Int32.Parse( e.Values["age"] ) );
			// var avgresult = avg.ToString();

			// var avg2 = context.Where( e => e.Values["name"] == "inohiro" ).Average( e => Int32.Parse( e.Values["age"] ) );
			// var avg2result = avg2.ToString();

			#endregion

			#region Count

			// var dicount = dicontext.Count( e => e["subject"] != "" );
			// Console.WriteLine( dicount.ToString() );

			// var count = context.Count( e => e.Values["name"] == "inohiro" );
			// var countresult = count.ToString();

			var dicount = dicontext.Where( e => e["subject"] != "" ).Count();
			Console.WriteLine( dicount.ToString() );

			// var count2 = context.Where( e => e.Values["name"] == "inohiro" ).Count(); // Count() function will be executed at LINQ World after got result, I think...
			// var count2result = count2.ToString();

			// var count3 = context.Where( e => e.Values["name"] == "inohiro" ).Select( e => e.Values["name"] );
			// var count3result = count3.ToArray();

			#endregion

			#region Sum

			// var sum = context.Sum( e => Int32.Parse( e.Values["age"] ) );
			// var sumresult = sum.ToString();

			// var sum2 = context.Select( e => Int32.Parse( e.Values["age"] ) ).Count(); // Count() will be executed in LINQ World, I think...
			// var sum2result = sum2.ToString();

			// var sum3 = context.Where( e => e.Values["name"] == "inohiro" ).Sum( e => Int32.Parse( e.Values["age"] ) );
			// var sum3result = sum3.ToString();

			#endregion

			#region Max

			// var max = context.Max( e => e.Values["age"] );
			// var max = dicontext.Max( e => e["age"] );
			//var maxresult = max.ToArray();

			// var max2 = context.Select( e => e.Values["age"] ).Max(); // Max() will be executed in LINQ World, I think...
			// var max2result = max2.ToArray();

			// var max3 = context.Where( e => Int32.Parse( e.Values["age"] ) <= 30 ).Max();
			// var max3result = max3.ToString();

			// var max4 = context.Where( e => e.Values["name"] == "inohiro" ).Max( e => e.Values["age"] );
			// var max4result = max3.ToArray();

			#endregion

			#region Min

			// var min = context.Min( e => e.Values["age"] ); // No problem
			// var minresult = min.ToArray();

			// var min2 = context.Select( e => e.Values["age"] ).Min(); // Request Parameter will be empty
			// var min2result = min2.ToArray();

			// var min3 = context.Where( e => Int32.Parse( e.Values["age"] ) >= 30 ).Min();
			//var min3result = min3.ToString();

			// var min4 = context.Where( e => e.Values["name"] == "inohiro" ).Min( e => e.Values["age"] ); // No problem
			// var min4result = min4.ToArray();

			#endregion

			// var values2 = context.Where( r => r.Values["name"] == "inohiro" );
			// var distinctedNames = context.Select( e => e.Values["name"] == "inohiro" ).Distinct();
			// var distinctedNames2 = context.Where( e => e.Values["name"] == "inohiro" ).Distinct(); // No Problem
			// var distinctedNames3 = context.Distinct( e => e.Values["name"] == "inohiro" ); // Need to support IEqualityComparable interface

			#endregion

			Console.ReadKey();
		}
	}
}
