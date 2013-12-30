using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LodViewProvider {

	public enum FilterType {
		Regex,     // Not supported yet (http://stackoverflow.com/questions/17711822/linq-regex-for-whole-word-search )
		Exists,    // Not supported yet
		NotExists, // Not supported yet
		Normal
	}

	public class Filter {

		public string Left { get; private set; }
		public string Right { get; private set; }
		public string Operator { get; private set; }
		public FilterType FilterType { get; private set; }

		public Filter( string left, string right, string oper )
			: this( left, right, oper, FilterType.Normal ) {}

		public Filter( string left, string right, string oper, FilterType filterType ) {
			Left = left.Trim( '\"' );
			Right = right.Trim( '\"' );
			Operator = oper;
			FilterType = filterType;
		}

		public override string ToString() {
			var str = new StringBuilder();
			str.Append( "{" );
			str.Append( String.Format( "\"type\":\"{0}\",", FilterType.ToString() ) );
			str.Append( String.Format( "\"var\":\"{0}\",", Left ) );
			str.Append( String.Format( "\"operator\":\"{0}\",", Operator  ));
			str.Append( String.Format( "\"condition\":\"{0}\"", Right ) );
			str.Append( "}" );
			return str.ToString();
		}
	}
}
