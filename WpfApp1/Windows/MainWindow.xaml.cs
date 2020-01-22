using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NLog;
using Vanara.PInvoke;
using WpfApp1.AttachedProperties;
using WpfApp1.Attributes;
using WpfApp1.Interfaces;
using WpfApp1.Menus;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [WindowMetadata("Main Window")]
    public partial class MainWindow : Window
    {
	    private static readonly Logger Logger =
		    LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            AddHandler(AppProperties.MenuItemListCollectionViewChangedEvent, new RoutedPropertyChangedEventHandler <ICollectionView>(OnMenuItemListCollectionViewChanged)  );
            //Vanara.PInvoke.User32.SetWindowLong( Vanara.PInvoke.User32.GetActiveWindow(), User32.WindowLongFlags.GWL_EXSTYLE )
        }

        private void OnMenuItemListCollectionViewChanged(object sender, RoutedPropertyChangedEventArgs<ICollectionView> e)
        {
	        if ( e.Source == this )
	        {
		        var menu = this.Template.FindName( "appMenu", this ) as Menu;
		        var c = e.NewValue as ICollectionView;

		        foreach ( IMenuItem menuItem in c )
		        {
			        menu.Items.Add( MenuHelper.MakeMenuItem( menuItem ) );
		        }

		        Logger.Warn(e.RoutedEvent.Name + " changed");
		        DumpRoutedPropertyChangedEventArgs<ICollectionView>(e);
		        e.Handled = true;
	        }
	        else
	        {
                Logger.Warn($"ignoring {e.RoutedEvent.Name} from {e.Source}" );
	        }
        }

        private void DumpRoutedPropertyChangedEventArgs < T >(
	        RoutedPropertyChangedEventArgs < T > routedPropertyChangedEventArgs
        )
        {
	        Logger.Debug(routedPropertyChangedEventArgs.OldValue);
	        Logger.Debug(routedPropertyChangedEventArgs.NewValue);
	        Logger.Debug(routedPropertyChangedEventArgs.RoutedEvent);
            Logger.Debug(routedPropertyChangedEventArgs.Source);
        }
    }
}
