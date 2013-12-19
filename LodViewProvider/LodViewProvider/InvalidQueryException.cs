using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {
	class InvalidQueryException : Exception {
		public InvalidQueryException( string e ) {
		}
	}
}
