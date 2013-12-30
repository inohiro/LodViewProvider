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

			// var values = context.Select( e => e.Values["name"] == "inohiro" );
			var values = from resource in context
// 						 from resouce2 in context2
						 where resource.Values["names"] == "inohiro" //  && resouce2.Values["names"]
						 select resource;

			var values2 = context.Where( r => r.Values["name"] == "inohiro" );

			// var expression = context.Expression;

			foreach ( var v in values2 ) {
				Console.WriteLine( v );
			}

			Console.ReadKey();
		}

		/*
		 * Aggregate Functions
		 * セレクティびてぃの異なるクエリを幾つか実行してみる
		 * 　　LINQで処理する，APIで処理する，SPARQL Endpoint で処理する
		 * コスト式を考える
		 */

		static void Image () {

			/* example.json

			"People": [
			  "Person1": {
			    "name": "Hiroyuki Inoue",
			    "age": "24",
			    "status": "student",
			  },
			  "Person2": { ... },
			]
			*/
			/*

			string viewLod = @"domain.net/person_view";
			var provider = new ViewedLod( viewUrl );

			var names = provider.Select( "Name" );
			foreach( var name in names ) {
				Console.WriteLine( name );
			}

			var young = provider.Where( person => person["Age"] <= "40" );
			foreach( var person in young ) {
				Console.WriteLine( person );
			}

			var students = provider.Where( person => person["status"] == "student" );
			foreach( var student in students ) {
				Console.WriteLine( student );
			}

			*/
		}
	}
}
