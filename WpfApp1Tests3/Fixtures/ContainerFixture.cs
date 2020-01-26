﻿#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// ContainerFixture.cs
// 
// 2020-01-19-5:59 PM
// 
// ---

#endregion

using System;
using System.Threading.Tasks ;
using Autofac;
using JetBrains.Annotations;
using NLog;
using WpfApp1.Util;
using Xunit ;

namespace WpfApp1Tests3.Fixtures
{
    [ UsedImplicitly ]
    public class ContainerFixture : IAsyncLifetime
    {
	    private static readonly Logger Logger =
		    LogManager.GetCurrentClassLogger();

        private readonly ILifetimeScope _container;

        public ILifetimeScope LifetimeScope { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ContainerFixture()
        {
	        IContainer container ;
	        _container = ContainerHelper.SetupContainer( out container );
            LifetimeScope = _container.BeginLifetimeScope();
        }

       

        /// <summary>
        /// Called immediately after the class has been created, before it is used.
        /// </summary>
        public Task InitializeAsync (  ) { return Task.CompletedTask ; }


        /// <summary>
        /// Called when an object is no longer needed. Called just before <see cref="M:System.IDisposable.Dispose" />
        /// if the class also implements that.
        /// </summary>
        public Task DisposeAsync (  )
        {
	        LifetimeScope?.Dispose();
	        _container?.Dispose();
	        return Task.CompletedTask ;
        }
    }
}
