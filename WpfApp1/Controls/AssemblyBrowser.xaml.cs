﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.AttachedProperties ;

namespace WpfApp1.Controls
{
	/// <summary>
	/// Interaction logic for AssemblyBrowser.xaml
	/// </summary>
	public partial class AssemblyBrowser : UserControl
	{
		public static readonly DependencyProperty AssemblyListPrroperty =
			AppProperties.AssemblyListProperty ;
		private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
		public AssemblyBrowser ()
		{
			InitializeComponent ( );
		}

		private void CommandBinding_OnExecuted ( object sender , ExecutedRoutedEventArgs e )
		{
			Logger.Debug ( "hello" ) ;
			CollectionView view = ( e.Parameter as CollectionView ) ;

			Logger.Debug ( $"{e.Parameter}" ) ;
		}

		private void UIElement_OnPreviewMouseDown ( object sender , MouseButtonEventArgs e )
		{
			Logger.Debug ( "preview mouse down" ) ;
		}
	}
}
