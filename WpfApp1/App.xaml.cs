using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Autofac;
using Castle.DynamicProxy;
using Microsoft.Scripting.Actions;
using SharpShell.Interop;
using Shell32;
using Vanara.Windows.Shell;
using HWND = Vanara.PInvoke.HWND;
using IContainer = Autofac.IContainer;

namespace WpfApp1
{
    public class ProxyGenerationHook : IProxyGenerationHook
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo memberInfo)
        {
            return memberInfo.Name.StartsWith("get_", StringComparison.Ordinal);
        }

        public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        public void MethodsInspected()
        {
        }
    }
    public class MyInterceptor : IInterceptor
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (invocation.Method.Name.StartsWith("get_"))
            {
                Logger.Debug(invocation.Method.Name);

            }
        }
    }
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public App()
        {
            SetupContainer();
        }

        private XMenuItem CreateDynamixProxy()
        {
            // nop;
            var q = new ProxyGenerationOptions(new ProxyGenerationHook());
            XMenuItemProxy = Generator.CreateClassProxy<XMenuItem>(new MyInterceptor());
            return XMenuItemProxy;
        }

        public XMenuItem XMenuItemProxy { get; set; }

        private void OpenWindowExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Logger.Info($"{sender} {e.Parameter}");
        }


        public IContainer AppContainer { get; set; }
        private void SetupContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<SystemParametersControl>().As<ISettingsPanel>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(
                    t => typeof(Window).IsAssignableFrom(t)).As<Window>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(
                    t => typeof(ITopLevelMenu).IsAssignableFrom(t)).As<ITopLevelMenu>();
            builder.Register(c => CreateDynamixProxy());
            builder.RegisterType<MenuItemList>();
            AppContainer = builder.Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Any())
            {
                var windowName = e.Args[0];
                var xaml = windowName + ".xaml";
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Uri));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    StartupUri = (Uri) converter.ConvertFrom(xaml);
                    Logger.Debug("Startup URI is {startupUri}", StartupUri);
                }
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Send,
                (DispatcherOperationCallback) delegate(object unused)

                {
                    IEnumerable<Lazy<Window>> windows = AppContainer.Resolve<IEnumerable<Lazy<Window>>>();
                    windows.Select((lazy, i) =>
                    {
                        var cmdBinding = new CommandBinding(MyAppCommands.OpenWindow, OpenWindowExecuted);
                        CommandManager.RegisterClassCommandBinding(typeof(Window), cmdBinding);
                        return true;
                    });
                    var menuItemList = AppContainer.Resolve<MenuItemList>();

                    Resources["MyMenuItemList"] = menuItemList;

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    return null;
                }, null);
        }
    }
}