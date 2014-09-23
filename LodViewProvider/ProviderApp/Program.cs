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

			const int viewBaseUrlPort = 4567;
			const string viewBaseHost = "192.168.58.1";
			string viewBaseUrl = String.Format( "http://{0}:{1}/", viewBaseHost, viewBaseUrlPort.ToString() );
			string viewUrl = viewBaseUrl + "exp/1/";

			Console.WriteLine( viewUrl );
			//
			// Initialize Contxts
			//

			var context = new LodViewContext( viewUrl ).Resource;
			var dicontext = new LodViewContext( viewUrl ).Dictionary;
			var jcontext = new LodViewContext( viewUrl ).JTokens;
			var stringlistcont = new LodViewContext( viewUrl ).StringList;

			//
			// For notation in paper
			//

			var aaabb = from resource in dicontext
					  where resource["object"] == "Capital"
					  select resource;
			var result = aaabb.ToList();
			result.ForEach( e => {
				var subject = e["subject"];
				var obj = e["object"];
				Console.WriteLine( "-----------------------------------------" );
				Console.WriteLine( "subject: {0}, obj: {1}", subject, obj );
			} );
			// Console.WriteLine(result);

			var bbb = from resource in dicontext
					  where resource["labname"] == "北川データ工学研究室"
					  select resource["first"];
			var bbbresult = bbb.ToList();
			// var aaae = dicontext.Where( e => Int32.Parse( e["age"] ) > 40 ).OrderBy( e => e["name"] ).Select( e => e ).ToList();
			// var bbbe = dicontext.Where( e => e["labname"] == "北川データ工学研究室" ).Select( e => e["first"] );

			//
			// for Experiment
			//

			// var sel_avg = dicontext.Where( e => Int32.Parse( e["value"] ) < 400 ).Average( e => Int32.Parse( e["value"] ) ).ToString();
			// var sel_pro = dicontext.Where( e => Int32.Parse( e["value"] ) <= 400 ).Select( e => e ).ToList();
			// var sel_pro = dicontext.Where( e => Int32.Parse( e["value"] ) <= 400 ).Select( e => e["value"] ).ToList();

			var aaaaa = dicontext.Where( e => Int32.Parse( e["value"] ) < 400 ).Select( e => new { Value = e["value"], Label = e["label"] } ).ToList();

			// var sel_pro = dicontext.Where( e => Int32.Parse( e["value"] ) <= 400 ).OrderBy( e => e["value"] ).Select( e => e["label"] ).ToList();
			// => Projection after OrderBy will be ignored (optional)

			// var orderby = dicontext.OrderBy( e => Int32.Parse( e["value"] ) ).ToList();
			// var min = dicontext.Min( e => Int32.Parse( e["value"] ) ).ToString();
			// var max = dicontext.Max( e => Int32.Parse( e["value"] ) ).ToString();
			// var avg = dicontext.Average( e => Int32.Parse( e["value"] ) ).ToString();

			Console.ReadKey();

			var avgpa = from student in dicontext
						group student by student["position"] into positions
						select new {
							Position = positions.Key,
							Gpa = positions.Average( e => Int32.Parse( e["gpa"] ) )
						};

			var gpa = from student in dicontext
					  group student by student["labname"] into lab
					  select new {
						  Lab = lab.Key,
						  Gpa = lab.Average( e => Int32.Parse( e["gpa"] ) ) };
			var gparesult = gpa.ToList();

			var h = dicontext.Select( e => e["name"] );
			// var hresult = h.ToList();

			var q = dicontext.Where( e => e["first"] == "Hiroyuki" );
			// var qresult = q.ToList();

			var v = dicontext.Where( e => e["first"] == "Hiroyuki" ).Select( e => e["labname"] );
			var vresult = v.ToList();

			var w = dicontext.Where( e => Int32.Parse( e["age"] ) <= 20 ).Select( e => e["name"] );
			var z = from resource in dicontext
					where resource["name"] == "Hiroyuki"
					select resource["labname"];
			// var labnames = z.ToList();

			var a = dicontext
				.Where( e => Int32.Parse( e["age"] ) <= 20 )
				.Select( e => new {
					Name = e["name"],
					Age = e["age"]
				} );
			// var aresult = a.ToList();

			var profs = from resource in dicontext
						group resource by resource["pname"] into pnames
						select new {
							Pname = pnames.Key,
							Sum = pnames.Sum( e => Int32.Parse( e["pop"] ) )
						};
			// var profsresult = profs.ToList();

			// var bbb = dicontext.Select( e => e[""])

			var ccc = dicontext.GroupBy( e => e["pname"] )
				.Select( e => new {
					Pname = e.Key,
					pop = e.Select( p => p["pop"] )
				} );
			// var cccresult = ccc.ToList();

			var aab = from r in dicontext
					  where Int16.Parse( r["age"] ) <= 1000
					  group r by r["pname"] into rs
					  select new {
						  Pname = rs.Key,
						  Age = rs.Average( e => Int32.Parse( e["age"] ) ) 
					  };
			var aabresult = aab.ToList();

			var aaa = dicontext
				.Where( e => Int32.Parse( e["pop"] ) <= 10000 )
				.GroupBy( e => e["pname"] )
				.Select( e => new {
					Pname = e.Key,
					Sum = e.Sum( p => Int32.Parse( p["pop"] ) ) } )
				.Where( e => e.Sum >= 10000 )
				.OrderByDescending( e => e.Sum ); // OrderBy, OrderByDescending
//			     Where( e => e.Sum >= 10000 ); // Having
			// var aaaresult = aaa.ToList();

			var popsum = from resouce in dicontext
						 select new {
							 Name = resouce["name"],
							 Age = resouce["age"]
						 }; //=> no problem
			// var ppresult = popsum.ToList();

//			var groupby = from resouce in dicontext
//						  group resouce by resouce["Affiliation"];
			// var groupby = dicontext.GroupBy( e => e["Affiliation"] ).Where( e => e.Count() < 30 );
			var gby = dicontext.Where( e => e["names"] == "inohiro" )
				.Where( e => Int32.Parse( e["age"] ) <= 20 )
				.GroupBy( e => e["Affiliation"] )
				.Where( e => e.Count() < 30 );
			// var gbresult = gby.ToList();


			//var hogehgoe = from resource in dicontext
			//            orderby resource["age"]
			//            group resource by resource["labname"] into lab
			//            select new { LabName = lab };

			//var ag = from resouce in dicontext
			//         group resouce by resouce["labname"] into lab
			//         select new {
			//             Lab = lab.Key,
			//             Avg = lab.Average( e => e["age"] )
			//         };

			// var aaa = dicontext.GroupBy( e => e["labname"] );


			var ob = dicontext.OrderBy( e => e["age"].Count() );
			// var obresult = ob.ToList();
			var wgb = dicontext.Where( e => e["name"] == "inohiro" ).GroupBy( e => e["Affiliation"] );
			// var wgbresult = wgb.ToArray();

			var wob = dicontext.Where( e => e["name"] == "inohiro" ).OrderBy( e => e["age"] );
			var wobresult = wob.ToList();

			var rob = dicontext.OrderByDescending( e => e["age"] );
//			var orderby = from resource in dicontext
//						  orderby Int32.Parse( resource["age"] )
//						  select resource;
			// var obresult = rob.ToList();

			var gbob = dicontext.Where( e => e["name"] == "inohiro" )
				.GroupBy( e => e["Affiliation"] )
				.Where( e => e.Count() < 30 );
//				.OrderBy( e => e.Count() );
			var gbobresult = gbob.ToList();

			var obgb = dicontext.Where( e => e["name"] == "inohiro" )
				.GroupBy( e => e["Affiliation"] )
				.OrderBy( e => e.Count() );
			// var obgbresult = obgb.ToList();

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

			#region Projection

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
