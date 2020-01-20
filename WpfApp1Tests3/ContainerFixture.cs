#region header

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
using Autofac;
using WpfApp1.Util;

namespace WpfApp1Tests3
{
    public class ContainerFixture : IDisposable
    {
        private IContainer _container;

        public ILifetimeScope LifetimeScope { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ContainerFixture()
        {
            _container = ContainerHelper.SetupContainer();
            LifetimeScope = _container.BeginLifetimeScope();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            LifetimeScope?.Dispose();
            _container?.Dispose();
        }
    }
}