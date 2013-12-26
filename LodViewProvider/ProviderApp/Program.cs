using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LodViewProvider;

namespace ProviderApp {
	class Program {
		static void Main( string[] args ) {
			Console.WriteLine( "Hello, World" );

			var context = new LodViewQueryable( "this is URL of View" );
			var values = context.Select( e => e.Values["name"] == "inohiro" );
			var expression = context.Expression;

			Console.WriteLine( values.ToString() );

			Console.WriteLine( expression );
			Console.ReadKey();
		}

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
