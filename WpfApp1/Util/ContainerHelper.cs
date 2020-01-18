using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Extras.DynamicProxy;

namespace WpfApp1
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