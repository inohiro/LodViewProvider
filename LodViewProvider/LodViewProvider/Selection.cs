using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {

	public enum SelectionType {
		Single = 0,
		Multiple = 1,
		All = 2
	}

	public class SingleSelection : IRequestable {

		public string Variable { get; private set; }
		public string Condition { get; private set; }
		public string Operator { get; private set; }
		public SelectionType SelectionType { get; private set; }

		public SingleSelection( string variable, string condition = "", string oper = "" ) {
			Variable = variable.Trim( '\"' );
			Condition = condition.Trim( '\"' );
			Operator = oper;
			SelectionType = LodViewProvider.SelectionType.Single;
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
		public SelectionType SelectionType { get; private set; }
		public List<SingleSelection> Variables { get; private set; }

		public MultipleSelection()
			: this( new List<SingleSelection>() ) {}

		public MultipleSelection( List<SingleSelection> variables ) {
			Variables = variables;
			SelectionType = LodViewProvider.SelectionType.Multiple;
		}

		public override string ToString() {
			StringBuilder strb = new StringBuilder();
			Variables.ForEach( s => strb.Append( s.ToString() ) );
			return strb.ToString();
		}
	}

	public class All : IRequestable {
		public SelectionType SelectionType { get; private set; }
		public All() {
			SelectionType = LodViewProvider.SelectionType.All;
		}
	}
}
