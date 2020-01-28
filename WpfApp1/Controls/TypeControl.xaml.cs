using System;
using System.CodeDom ;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup ;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppShared ;
using Microsoft.CSharp ;
using Microsoft.Scripting.Utils ;
using NLog ;
using PostSharp.Reflection ;

namespace WpfApp1.Controls
{
	/// <summary>
	/// Interaction logic for TypeControl.xaml
	/// </summary>
	public partial class TypeControl : UserControl
	{
		private static Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
		public static readonly DependencyProperty RenderedTypeProperty =
			App.RenderedTypeProperty ;

		public Type RenderedType
		{
			get { return GetValue ( RenderedTypeProperty ) as Type ; }
			set { SetValue (  RenderedTypeProperty , value  ) ; }
		}

		public event RoutedPropertyChangedEventHandler <Type> RenderedTypeChanged {
			add { AddHandler ( App.RenderedTypeChangedEvent , value ) ; }
			remove { RemoveHandler ( App.RenderedTypeChangedEvent , value ) ; }

		}
		public TypeControl ()
		{
			RenderedTypeChanged += new RoutedPropertyChangedEventHandler <Type> (OnRenderedTypeChanged);
			InitializeComponent ( );
			PopulateControl(GetValue(RenderedTypeProperty) as Type);
		}

		private void OnRenderedTypeChanged (
			object                                  sender
		  , RoutedPropertyChangedEventArgs < Type > e
		)
		{
			PopulateControl (e.NewValue ) ;
		}

		private void PopulateControl ( Type myType)
		{
			Container.Children.Clear();
			if(myType == null)
			{
				return ;
			}
			TextBlock b = new TextBlock();
			var mainInline = new Span();

			GenerateControlsForType ( myType, b ) ;
			Container.Children.Add ( b ) ;

		}

		private void GenerateControlsForType ( Type myType, IAddChild addChild )
		{
			var name = NameForType ( myType ) ;
			var hyperLink = new Hyperlink ( new Run ( myType.Name ) ) ;
			Uri.TryCreate ( "obj://" +Uri.EscapeUriString(myType.Name) , UriKind.Absolute , out Uri res ) ;

			hyperLink.NavigateUri = res ;
			// hyperLink.Command          = MyAppCommands.VisitTypeCommand ;
			// hyperLink.CommandParameter = myType ;
			hyperLink.ToolTip = new ToolTip ( ) { Content = ToopTipContent ( myType ) } ;

			hyperLink.RequestNavigate += HyperLinkOnRequestNavigate;
			addChild.AddChild( hyperLink ) ;
			if ( myType.IsGenericType )
			{
				addChild.AddText ( "<" ) ;
				int i = 0 ;
				foreach ( var arg in myType.GenericTypeArguments )
				{
					GenerateControlsForType ( arg , addChild ) ;
					if ( i < myType.GenericTypeArguments.Length )
					{
						addChild.AddText(", ");
					}
				}

				addChild.AddText ( ">" ) ;
			}
		}

		private object ToopTipContent ( Type myType , StackPanel pp = null)
		{
			CSharpCodeProvider provider = new CSharpCodeProvider();
			var codeTypeReference = new CodeTypeReference(myType) ;
			var q = codeTypeReference;
			var toopTipContent = new TextBlock ( )
			                     {
				                     Text   = provider.GetTypeOutput ( q ) , FontSize = 20
				                   //, Margin = new Thickness ( 15 )
				                    ,
			                     } ;
			if ( pp == null )
			{
				pp = new StackPanel ( ) { Orientation = Orientation.Vertical } ;
			}
			pp.Children.Insert ( 0 , toopTipContent ) ;
			var @base = myType.BaseType ;
			if(@base != null)
				ToopTipContent ( @base , pp ) ;
			return pp ;
		}

		private string NameForType ( Type myType )
		{
			CSharpCodeProvider provider = new CSharpCodeProvider();
			if ( myType.IsGenericType )
			{
				Type type = myType.GetGenericTypeDefinition ( ) ;
				myType = type ;

			}

			var codeTypeReference = new CodeTypeReference(myType) ;
			var q = codeTypeReference;
			//myType.GetGenericTypeParameters()
			return provider.GetTypeOutput ( q ) ;
			// return myType.IsGenericType ? myType.GetGenericTypeDefinition ( ).Name : myType.Name ;
		}

		private void HyperLinkOnRequestNavigate ( object sender , RequestNavigateEventArgs e )
		{
			Logger.Debug ( $"{nameof(HyperLinkOnRequestNavigate)}: Uri={e.Uri}");
			ContentElement uie = ( ContentElement ) sender ;
			var navigationService = NavigationService.GetNavigationService ( uie ) ;
			if ( navigationService != null )
			{
				navigationService.Navigate (
				                            new TypeControl ( )
				                            {
					                            RenderedType =
						                            uie.GetValue (
						                                          App.RenderedTypeProperty
						                                         ) as Type
				                            }
				                           ) ;
				e.Handled = true ;
			}
			else
			{
                Logger.Info("find other way to navigate type");

			}

		}
	}
}
