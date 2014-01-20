using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

using ExpressionViewer;

namespace LodViewProvider {

	/// <summary>
	/// LodView LINQ Provider
	/// </summary>
	public class LodViewQueryProvider : IQueryProvider {

		public LodViewContext Context { get; set; }

		public IQueryable<TElement> CreateQuery<TElement>( Expression expression ) {
			return new LodViewQueryable<TElement>( this, expression );
		}

		public IQueryable CreateQuery( Expression expression ) {
			Type elementType = TypeSystem.GetElementType( expression.Type );
			try {
				return( IQueryable) Activator.CreateInstance( 
					typeof( LodViewQueryable<>).MakeGenericType( elementType ),
					new object[] { this, expression } );
			}
			catch ( Exception ex ) {
				throw ex.InnerException;
			}
		}

public TResult Execute<TResult>( Expression expression ) {
			// ( ( Expression ) expression ).Show();

			bool isEnumerable = typeof( TResult ).Name == "IEnumerable`1" || typeof( TResult ).Name == "IEnumerable";
			Type resultType = new MethodCallExpressionTypeFinder().GetGenericType( expression );
			var genericArguments = new[] { resultType };

			var methodInfo = Context.GetType().GetMethod( "Execute", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
			MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod( genericArguments );

			try {
				var result = ( TResult ) genericMethodInfo.Invoke( Context, new object[] { expression, isEnumerable } );
				return result;
			}
			catch ( Exception ex ) {
				if ( ex.InnerException != null ) {
					throw ex.InnerException;
				}
				throw;
			}
		}

		public object Execute( Expression expression ) {
			Type elementType = TypeSystem.GetElementType( expression.Type );
			return GetType().GetMethod( "Execute", new[] { elementType } ).Invoke( this, new object[] { expression } );
		}
	}
}