using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using NLog;
using IContainer = Autofac.IContainer;

namespace WpfApp1
{
    public class ProxyGenerationHook : IProxyGenerationHook
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public void NonProxyableMemberNotification(
            Type       type,
            MemberInfo memberInfo
        )
        {
        }

        public bool ShouldInterceptMethod(
            Type       type,
            MethodInfo memberInfo
        )
        {
            return memberInfo.Name.StartsWith(
                                              "get_", StringComparison.Ordinal
                                             );
        }

        public void MethodsInspected()
        {
        }

        public void NonVirtualMemberNotification(
            Type       type,
            MemberInfo memberInfo
        )
        {
        }
    }

    public class MyInterceptor : IInterceptor
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public void Intercept(
            IInvocation invocation
        )
        {
            invocation.Proceed();
            if ( invocation.Method.Name.StartsWith( "get_" ) )
            {
                Logger.Debug( invocation.Method.Name );
            }
        }
    }

    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public App()
        {
            SetupContainer();
        }

        public XMenuItem XMenuItemProxy { get; set; }


        public IContainer AppContainer { get; set; }

        private XMenuItem CreateDynamixProxy()
        {
            throw new NotImplementedException();
            // nop;
            var q = new ProxyGenerationOptions( new ProxyGenerationHook() );
            XMenuItemProxy =
                Generator.CreateClassProxy < XMenuItem >( new MyInterceptor() );
            return XMenuItemProxy;
        }

        private void OpenWindowExecuted(
            object                  sender,
            ExecutedRoutedEventArgs e
        )
        {
            Logger.Info( $"{sender} {e.Parameter}" );
        }

        private void SetupContainer()
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
            AppContainer = builder.Build();
        }

        private void Application_Startup(
            object           sender,
            StartupEventArgs e
        )
        {
            if ( e.Args.Any() )
            {
                var windowName = e.Args[0];
                var xaml       = windowName + ".xaml";
                var converter  = TypeDescriptor.GetConverter( typeof(Uri) );
                if ( converter.CanConvertFrom( typeof(string) ) )
                {
                    StartupUri = (Uri)converter.ConvertFrom( xaml );
                    Logger.Debug( "Startup URI is {startupUri}", StartupUri );
                }
            }

            Dispatcher.BeginInvoke(
                                   DispatcherPriority.Send,
                                   (DispatcherOperationCallback)delegate

                                                                {
                                                                    var windows
                                                                        = AppContainer
                                                                           .Resolve
                                                                            < IEnumerable
                                                                                < Lazy
                                                                                    < Window
                                                                                    > >
                                                                            >();
                                                                    windows
                                                                       .Select(
                                                                               (
                                                                                   lazy,
                                                                                   i
                                                                               ) =>
                                                                               {
                                                                                   var
                                                                                       cmdBinding
                                                                                           = new
                                                                                               CommandBinding(
                                                                                                              MyAppCommands
                                                                                                                 .OpenWindow,
                                                                                                              OpenWindowExecuted
                                                                                                             );
                                                                                   CommandManager
                                                                                      .RegisterClassCommandBinding(
                                                                                                                   typeof
                                                                                                                   (Window
                                                                                                                   ),
                                                                                                                   cmdBinding
                                                                                                                  );
                                                                                   return
                                                                                       true;
                                                                               }
                                                                              );
                                                                    var
                                                                        menuItemList
                                                                            = AppContainer
                                                                               .Resolve
                                                                                < MenuItemList
                                                                                >();

                                                                    Resources
                                                                            ["MyMenuItemList"]
                                                                        = menuItemList;

                                                                    var
                                                                        mainWindow
                                                                            = new
                                                                                MainWindow();
                                                                    mainWindow
                                                                       .Show();
                                                                    return null;
                                                                }, null
                                  );
        }
    }
}