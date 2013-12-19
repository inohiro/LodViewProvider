using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LodViewProvider;

namespace ProviderApp {
	class Program {
		static void Main( string[] args ) {
			var provider = LodViewProvider.LodViewProvider.CreateQueryable<int>();

			var query = provider.Select( e => e == 1 );
			Console.WriteLine( query.Expression );

			Console.ReadKey();
		}
	}
}
