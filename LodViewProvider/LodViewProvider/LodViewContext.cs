using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {

	/// <summary>
	/// Context for using LINQ
	/// </summary>
	public class LodViewContext {

		public LodViewContext() : this( "dummy view url" ) { }

		public LodViewContext( string viewUri ) {
			ViewUri = viewUri;
		}

		public string ViewUri { get; private set; }
		
	}
}
