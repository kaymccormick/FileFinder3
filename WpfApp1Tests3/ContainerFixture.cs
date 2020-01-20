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
    internal class ContainerFixture : IDisposable
    {
        private IContainer _container;
        private ILifetimeScope _lifetimeScope;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ContainerFixture()
        {
            _container = ContainerHelper.SetupContainer();
            _lifetimeScope = _container.BeginLifetimeScope();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _lifetimeScope?.Dispose();
            _container?.Dispose();
        }
    }
}