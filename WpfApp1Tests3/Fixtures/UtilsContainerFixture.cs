﻿using System;
using Autofac;
using WpfApp1Tests3.Utils;

namespace WpfApp1Tests3.Fixtures
{
    public class UtilsContainerFixture : IDisposable
    {
        private IContainer _container;
        private ILifetimeScope _scope;
        public IComponentContext Container { get => _scope;  }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UtilsContainerFixture()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType< InfoContext >().AsSelf();
            containerBuilder.RegisterType < WpfTests.MyServices >().As < IMyServices >();
            containerBuilder.RegisterType < MyServicesFixture >().AsSelf ();
            _container = containerBuilder.Build();
            _scope = _container.BeginLifetimeScope();

        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}