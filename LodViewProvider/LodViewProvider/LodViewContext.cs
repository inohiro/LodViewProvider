using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects;

namespace LodViewProvider {

	public class LodViewContext : ObjectContext {

		public string ViewUrl { get; private set; }

		public LodViewContext( string viewUrl ) : base ( viewUrl ) {
			ViewUrl = viewUrl;
		}
	}

	//public class LodViewContext : DataContext {

	//    public string ViewUrl { get; private set; }

	//    public LodViewContext() {}
	//    public LodViewContext( string viewUrl ) {
	//        ViewUrl = viewUrl;
	//    }
	//}

	//public class ViewedLodQueryContext {

	//    internal static object Execute( Expression expression, bool isEnumerable ) {
	//        if ( !IsQueryOverDataSource( expression ) ) {
	//            throw new InvalidProgramException( "No query over the data source was specified" );
	//        }

	//        //InnermostWhereFinder WhereFinder = new InnermostWhereFinder();
	//        //MethodCallExpression whereExpression = WhereFinder.GetInnermostWhere( expression );
	//        //LambdaExpression lambdaExpression = ( LambdaExpression ) ( ( UnaryExpression ) ( whereExpression.Arguments[1] ) ).Operand;

	//        //lambdaExpression = ( LambdaExpression ) Evaluator.PartialEval( lambdaExpression );

	//        //LocationFinder locFinder = new LocationFinder( lambdaExpression.Body );
	//        //List<string> locations = locFinder.Locations;
	//        //if ( locations.Count == 0 ) {
	//        //    throw new InvalidQueryException( "You must specify at least one place name in your query" );
	//        //}

	//        if ( isEnumerable ) {
	//            // CreateQuery
	//        }
	//        else {
	//            // Execute
	//        }
	//        return new object();
	//    }

	//    private static bool IsQueryOverDataSource( Expression expression ) {
	//        return ( expression is MethodCallExpression );
	//    }
	//}
}
