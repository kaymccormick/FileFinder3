﻿using System ;
using System.Diagnostics ;
using System.IO ;
using System.Reflection ;
using System.Runtime.CompilerServices ;
using Castle.DynamicProxy ;
using NLog ;

namespace Common.Logging
{
	public class LoggerProxyHelper
	{
		public delegate void LogMethod (
			string                      message
		  , [ CallerFilePath ]   string callerFilePath   = ""
		  , [ CallerMemberName ] string callerMemberName = ""
		) ;

		/// <summary>
		///     Initializes a new instance of the
		///     <see
		///         cref="T:System.Object" />
		///     class.
		/// </summary>
		public LoggerProxyHelper ( ProxyGenerator generator ) { Generator = generator ; }

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public LoggerProxyHelper ( ProxyGenerator generator , LogMethod logMethod )
		{
			Generator    = generator ;
			UseLogMethod = logMethod ;
		}

		public ProxyGenerator Generator { get ; }

		public LogMethod UseLogMethod { get ; set ; }

		public LogFactory CreateLogFactory ( LogFactory logFactory )
		{
			if ( logFactory == null )
			{
				logFactory = LogManager.LogFactory ;
			}

			var opts = new ProxyGenerationOptions ( new LoggerFactoryHook ( UseLogMethod ) ) ;
			opts.Initialize();
			var proxy = ( LogFactory ) Generator.CreateClassProxyWithTarget (
			                                                                 logFactory.GetType ( )
			                                                               , Type.EmptyTypes
			                                                               , logFactory
			                                                               , opts
			                                                               , new
				                                                                 LogFactoryInterceptor (
				                                                                                        Generator
				                                                                                      , UseLogMethod
				                                                                                       )
			                                                                ) ;
			return proxy ;
		}

		public static Logger GetCurrentClassLogger ( [ CallerFilePath ] string path = null )
		{
			var name = "default" ;
			if ( path != null )
			{
				name = Path.GetFileNameWithoutExtension ( path ) ;
			}

			var myLogFactory = ( LogManager.Configuration as CodeConfiguration ).LogFactory as MyLogFactory ;
			if ( myLogFactory == null )
			{
				Debug.WriteLine ( "no log factory of my type" ) ;
				throw new NotImplementedException();
			}

			if ( myLogFactory.GetDoLogMessage() != null )
			{
				Debug.WriteLine ( myLogFactory.GetDoLogMessage().ToString ( ) ) ;
			}
			else
			{
				Debug.WriteLine ( "no dologmessage" ) ;
			}

			var logger =
				myLogFactory
			   .GetLogger ( name ) ;
			return logger ;
		}
	}

	public class LoggerFactoryHook : IProxyGenerationHook
	{
		private readonly LoggerProxyHelper.LogMethod _useLogMethod ;

		public LoggerFactoryHook ( LoggerProxyHelper.LogMethod useLogMethod )
		{
			_useLogMethod = useLogMethod ;
		}

		/// <summary>
		///     Invoked by the generation process to notify that the whole process has
		///     completed.
		/// </summary>
		public void MethodsInspected ( ) { }

		/// <summary>
		///     Invoked by the generation process to notify that a member was not marked as
		///     virtual.
		/// </summary>
		/// <param name="type">The type which declares the non-virtual member.</param>
		/// <param name="memberInfo">The non-virtual member.</param>
		/// <remarks>
		///     This method gives an opportunity to inspect any non-proxyable member of a
		///     type that has
		///     been requested to be proxied, and if appropriate - throw an exception to
		///     notify the caller.
		/// </remarks>
		public void NonProxyableMemberNotification ( Type type , MemberInfo memberInfo )
		{
			// _useLogMethod ( $"Unproxyable: {memberInfo.Name}" );

		}

		/// <summary>
		///     Invoked by the generation process to determine if the specified method
		///     should be proxied.
		/// </summary>
		/// <param name="type">The type which declares the given method.</param>
		/// <param name="methodInfo">The method to inspect.</param>
		/// <returns>True if the given method should be proxied; false otherwise.</returns>
		public bool ShouldInterceptMethod ( Type type , MethodInfo methodInfo )
		{
			_useLogMethod ( $"{type}.{methodInfo.Name}" ) ;
			if ( methodInfo.Name == "ToString" )
			{
				return false ;
			}

			return true ;
		}
	}

	public class MyLogFactory : LogFactory
	{
		private readonly LoggerProxyHelper.LogMethod _doLogMessage ;

		public virtual LoggerProxyHelper.LogMethod GetDoLogMessage ( ) { return _doLogMessage ; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NLog.LogFactory" /> class.
		/// </summary>
		public MyLogFactory ( ) {
		}

		public MyLogFactory ( LoggerProxyHelper.LogMethod doLogMessage )
		{
			_doLogMessage = doLogMessage ;
		}

		public new Logger GetLogger ( string name )
		{
			var logger = base.GetLogger ( name ) ;
			if ( GetDoLogMessage() != null )
			{
				GetDoLogMessage()( $"{name} = {logger}" ) ;
			}
			else
			{
				Debug.WriteLine ( "oops" ) ;
			}

			return logger ;
		}
	}
}