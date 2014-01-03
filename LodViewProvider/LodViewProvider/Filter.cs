using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LodViewProvider {

	public enum FilterType {
		Regex     = 0, // Not supported yet (http://stackoverflow.com/questions/17711822/linq-regex-for-whole-word-search )
		Exists    = 1, // Not supported yet
		NotExists = 2, // Not supported yet
		Normal    = 3,
	}

	public class Filter : IRequestable {

		public string Left { get; private set; }
		public string Right { get; private set; }
		public string Operator { get; private set; }
		public FilterType FilterType { get; private set; }
		public string ConditionType { get; private set; }

		public Filter( string left, string right, string oper)
			: this( left, right, oper, FilterType.Normal, "string" ) {}

		public Filter( string left, string right, string oper, FilterType filterType, string conditionType ) {
			Left = left.Trim( '\"' );
			Right = right.Trim( '\"' );
			Operator = oper;
			FilterType = filterType;
			ConditionType = conditionType;
		}

		public override string ToString() {
			var str = new StringBuilder();
			str.Append( "{" );
			str.Append( String.Format( "type:{0},", FilterType.ToString() ) );
			str.Append( String.Format( "var:{0},", Left ) );
			str.Append( String.Format( "operator:{0},", Operator  ));
			str.Append( String.Format( "condition:{0}", Right ) );
			str.Append( "}" );
			return str.ToString();
		}
	}
}
