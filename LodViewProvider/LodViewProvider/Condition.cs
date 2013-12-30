using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {

	public abstract class Condition {

		public string Left;
		public string Right;
		public string Operator;

		public override string ToString() {
			return base.ToString();
		}

	}
}
