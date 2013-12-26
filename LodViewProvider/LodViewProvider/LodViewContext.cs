using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {
	public class LodViewContext {
		public LodViewContext( string viewUri ) {
			ViewUri = viewUri;
		}

		public string ViewUri { get; private set; }
		
	}
}
