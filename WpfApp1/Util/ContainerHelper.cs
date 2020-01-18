using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Extras.DynamicProxy;
using WpfApp1.Controls;
using WpfApp1.Interfaces;
using WpfApp1.Menus;

namespace WpfApp1.Util
{
    public static class ContainerHelper
    {
        public static IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType < SystemParametersControl >()
                   .As < ISettingsPanel >();
            builder.RegisterAssemblyTypes( Assembly.GetExecutingAssembly() )
                   .Where(
                          t => typeof(Window).IsAssignableFrom( t )
                         ).As < Window >();
            builder.RegisterAssemblyTypes( Assembly.GetExecutingAssembly() )
                   .Where(
                          t => typeof(ITopLevelMenu).IsAssignableFrom( t )
                         ).As < ITopLevelMenu >();
            //builder.Register(c => CreateDynamixProxy());
            builder.RegisterType < MenuItemList >().EnableClassInterceptors();
            builder.Register( C => new MyInterceptor() );
            builder.RegisterType < XMenuItem >().EnableClassInterceptors();
            return builder.Build();
        }
    }
}