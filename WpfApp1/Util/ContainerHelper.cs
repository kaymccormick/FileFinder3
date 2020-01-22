using System;
using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Extras.AttributeMetadata;
using Autofac.Extras.DynamicProxy;
using NLog;
using WpfApp1.Controls;
using WpfApp1.Interfaces;
using WpfApp1.Menus;

namespace WpfApp1.Util
{
    public static class ContainerHelper
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public static IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AttributedMetadataModule>();
            builder.RegisterType < SystemParametersControl >()
                   .As < ISettingsPanel >();
            var executingAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes( executingAssembly )
                   .Where(
                          delegate(
	                          Type t
                          ) {
	                          var isAssignableFrom = typeof(Window).IsAssignableFrom( t );
	                          Logger.Debug( $"{t} is assignable from ${isAssignableFrom}" );
	                          return isAssignableFrom;
                          } ).As < Window >()
	            ;builder.RegisterAssemblyTypes( executingAssembly )
	                    .Where(
	                           t => typeof(ITopLevelMenu).IsAssignableFrom( t )
	                          ).As < ITopLevelMenu >();
            
            //builder.Register(c => CreateDynamixProxy());
            //builder.Register( C => new MyInterceptor() );
            builder.RegisterType < MyInterceptor >().AsSelf();
            //builder.Register(context => context.  )
            builder.RegisterType < LogFactory >().AsSelf();
            //builder.RegisterType <>().As <ILoggingEntity>();
            builder.RegisterType < MenuItemList >().AsImplementedInterfaces()
                   .WithMetadata < ResourceMetadata >( m => m.For( rn => rn.ResourceName, "MenuItemList" ) )
                   .PreserveExistingDefaults() // As<INotifyCollectionChanged>().As<INotifyPropertyChanged>().As<>()
                   .EnableInterfaceInterceptors().InterceptedBy( typeof(MyInterceptor) );
            builder.RegisterType < XMenuItem >().AsImplementedInterfaces().PreserveExistingDefaults()
                   .EnableInterfaceInterceptors().InterceptedBy( typeof(MyInterceptor) );
            builder.RegisterBuildCallback( container => Logger.Info( "Container built." ) );

            return builder.Build();
        }
    }
}