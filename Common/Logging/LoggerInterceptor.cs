using System ;
using System.Linq ;
using Castle.DynamicProxy ;
using NLog ;

namespace Common.Logging
{
	public class LoggerInterceptor : IInterceptor
	{
		public ProxyGenerator Generator { get ; }

		public LoggerProxyHelper.LogMethod UseLogMethod { get ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public LoggerInterceptor ( ) {
		}

		public LoggerInterceptor ( ProxyGenerator generator , LoggerProxyHelper.LogMethod useLogMethod )
		{
			Generator = generator ;
			UseLogMethod = useLogMethod ;
		}

		public void Intercept ( IInvocation invocation )
		{
			UseLogMethod ( $"{invocation.Method.Name} on {invocation.InvocationTarget}" ) ;
			var enumerable = LogLevel.AllLevels.Select ( level => level.Name == invocation.Method.Name ) ;
			if ( enumerable.Any()) 
			{
				var level = enumerable.First ( ) ;
				var @params = new Type[ invocation.Arguments.Length + 1 ] ;
				var args = new object[ invocation.Arguments.Length  + 1 ] ;
				int i = 1 ;
				foreach ( var arg in invocation.Arguments )
				{
					@params[ i ] = arg.GetType ( ) ;
					args[ i ]    = arg ;
					var @select = invocation.Method.GetParameters ( )
					                        .Select ( ( info , i1 ) => info.Name == "message" ) ;
					if ( @select.Any ( ) )
					{
						args[ i ] = "hello " + arg.ToString ( ) ;
					}
					i            = i + 1 ;
				}

				@params[ 0 ] = typeof ( LogLevel ) ;
				args[ 0 ]    = level ;

				//LogBuilder b = new LogBuilder(invocation.InvocationTarget as ILogger);
				var method = invocation.TargetType.GetMethod ( "Log" , @params ) ;
				var result = method.Invoke ( invocation.InvocationTarget,  args ) ;
				
			}
		}

	}
}