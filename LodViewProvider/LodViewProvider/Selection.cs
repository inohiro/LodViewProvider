using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {

	public class SingleSelection : IRequestable {

		public string Variable { get; private set; }
		public string Condition { get; private set; }
		public string Operator { get; private set; }
		public FilterType FilterType { get; private set; }

		public SingleSelection( string variable, string condition = "", string oper = "" ) {
			Variable = variable.Trim( '\"' );
			Condition = condition.Trim( '\"' );
			Operator = oper;
			FilterType = LodViewProvider.FilterType.SingleSelection;
		}

		public override string ToString() {
			var strb = new StringBuilder();
			strb.Append( "{" );
			strb.Append( String.Format( "t:{0},", "cond" ) );
			strb.Append( String.Format( "v:{0},", Variable ) );
			strb.Append( String.Format( "op:{0},", Operator ) );
			strb.Append( String.Format( "c:{0}", Condition ) );
			strb.Append( "}" );
			return strb.ToString();
		}
	}

	public class MultipleSelection : IRequestable	{
		public FilterType FilterType { get; private set; }
		public List<SingleSelection> Variables { get; private set; }

		public MultipleSelection()
			: this( new List<SingleSelection>() ) {}

		public MultipleSelection( List<SingleSelection> variables ) {
			Variables = variables;
			FilterType = LodViewProvider.FilterType.MultipleSelection;
		}

		public override string ToString() {
			StringBuilder strb = new StringBuilder();
			Variables.ForEach( s => strb.Append( s.ToString() ) );
			return strb.ToString();
		}
	}

	public class All : IRequestable {
		public FilterType FilterType { get; private set; }
		public All() {
			FilterType = LodViewProvider.FilterType.All;
		}
	}
}
