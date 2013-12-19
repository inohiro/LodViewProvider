using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LodViewProvider {

	public class QueryableLodTuple<T> : IOrderedQueryable<T> {

		public IQueryProvider Provider { get; private set; }
		public Expression Expression { get; private set; }

		public QueryableLodTuple() {
			Provider = new ViewedLodTupleProvider();
			Expression = Expression.Constant( this );
		}

		public QueryableLodTuple( ViewedLodTupleProvider provider, Expression expression ) {
			Provider = provider;
			Expression = expression;
		}

		public Type ElementType {
			get { return typeof( T ); }
		}

		public IEnumerator<T> GetEnumerator() {
			return ( Provider.Execute<IEnumerable<T>>( Expression ) ).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return ( Provider.Execute<System.Collections.IEnumerable>( Expression ) ).GetEnumerator();
		}
	}

	///// <summary>
	///// http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx
	///// から取ってきたソース。
	///// これくらい、標準で提供してくれてもいいのに。
	///// http://ufcpp.net/study/csharp/source/Query.cs
	///// </summary>
	//public class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable {
	//    QueryProvider provider;
	//    Expression expression;

	//    public Query( QueryProvider provider ) {
	//        if( provider == null ) {
	//            throw new ArgumentNullException( "provider" );
	//        }
	//        this.provider = provider;
	//        this.expression = Expression.Constant( this );
	//    }

	//    public Query( QueryProvider provider, Expression expression ) {
	//        if( provider == null ) {
	//            throw new ArgumentNullException( "provider" );
	//        }

	//        if( expression == null ) {
	//            throw new ArgumentNullException( "expression" );
	//        }

	//        if( !typeof( IQueryable<T> ).IsAssignableFrom( expression.Type ) ) {
	//            throw new ArgumentOutOfRangeException( "expression" );
	//        }

	//        this.provider = provider;
	//        this.expression = expression;
	//    }

	//    Expression IQueryable.Expression {
	//        get { return this.expression; }
	//    }

	//    Type IQueryable.ElementType {
	//        get { return typeof( T ); }
	//    }

	//    IQueryProvider IQueryable.Provider {
	//        get { return this.provider; }
	//    }

	//    public IEnumerator<T> GetEnumerator() {
	//        return ( ( IEnumerable<T> )this.provider.Execute( this.expression ) ).GetEnumerator();
	//    }

	//    IEnumerator IEnumerable.GetEnumerator() {
	//        return ( ( IEnumerable )this.provider.Execute( this.expression ) ).GetEnumerator();
	//    }

	//    public override string ToString() {
	//        return this.provider.GetQueryText( this.expression );
	//    }
	//}

	//public abstract class QueryProvider : IQueryProvider {
	//    protected QueryProvider() {
	//    }

	//    IQueryable<S> IQueryProvider.CreateQuery<S>( Expression expression ) {
	//        return new Query<S>( this, expression );
	//    }

	//    IQueryable IQueryProvider.CreateQuery( Expression expression ) {
	//        Type elementType = TypeSystem.GetElementType( expression.Type );

	//        try {
	//            return ( IQueryable )Activator.CreateInstance( typeof( Query<> ).MakeGenericType( elementType ), new object[] { this, expression } );
	//        }
	//        catch( TargetInvocationException tie ) {
	//            throw tie.InnerException;
	//        }
	//    }

	//    S IQueryProvider.Execute<S>( Expression expression ) {
	//        return ( S )this.Execute( expression );
	//    }

	//    object IQueryProvider.Execute( Expression expression ) {
	//        return this.Execute( expression );
	//    }

	//    public abstract string GetQueryText( Expression expression );
	//    public abstract object Execute( Expression expression );
	//}

	//internal static class TypeSystem {
	//    internal static Type GetElementType( Type seqType ) {
	//        Type ienum = FindIEnumerable( seqType );
	//        if( ienum == null ) return seqType;
	//        return ienum.GetGenericArguments()[0];
	//    }

	//    private static Type FindIEnumerable( Type seqType ) {
	//        if( seqType == null || seqType == typeof( string ) )
	//            return null;

	//        if( seqType.IsArray )
	//            return typeof( IEnumerable<> ).MakeGenericType( seqType.GetElementType() );

	//        if( seqType.IsGenericType ) {
	//            foreach( Type arg in seqType.GetGenericArguments() ) {
	//                Type ienum = typeof( IEnumerable<> ).MakeGenericType( arg );

	//                if( ienum.IsAssignableFrom( seqType ) ) {
	//                    return ienum;
	//                }
	//            }
	//        }

	//        Type[] ifaces = seqType.GetInterfaces();

	//        if( ifaces != null && ifaces.Length > 0 ) {
	//            foreach( Type iface in ifaces ) {
	//                Type ienum = FindIEnumerable( iface );
	//                if( ienum != null ) return ienum;
	//            }
	//        }

	//        if( seqType.BaseType != null && seqType.BaseType != typeof( object ) ) {
	//            return FindIEnumerable( seqType.BaseType );
	//        }
	//        return null;
	//    }
	//}
}