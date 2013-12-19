using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {
	public abstract class Resource { // Json
		public Resource( string key ) {
			Key = key;
		}

		public Resource( string key, Dictionary<string, string> values ) {
			Key = key;
			Values = values;
		}

		public string Key { get; private set; }
		public Dictionary<string, string> Values { get; private set; }
	}
}
