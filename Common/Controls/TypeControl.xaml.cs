using System ;
using System.CodeDom ;
using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Documents ;
using System.Windows.Markup ;
using System.Windows.Navigation ;
using AppShared ;
using Microsoft.CSharp ;
using NLog ;

namespace Common.Controls
{
	/// <summary>
	/// Interaction logic for TypeControl.xaml
	/// </summary>
	public partial class TypeControl : UserControl
	{
		private static Logger Logger =
			LogManager.GetCurrentClassLogger ( ) ;

		public static readonly DependencyProperty RenderedTypeProperty = App.RenderedTypeProperty ;

		public static readonly DependencyProperty TargetNameProperty =
			DependencyProperty.Register (
			                             "TargetName"
			                           , typeof ( string )
			                           , typeof ( TypeControl )
			                           , new PropertyMetadata ( default ( string ) )
			                            ) ;

		public static readonly DependencyProperty TargetProperty =
			DependencyProperty.Register (
			                             "Target"
			                           , typeof ( Frame )
			                           , typeof ( TypeControl )
			                           , new PropertyMetadata ( default ( Frame ) )
			                            ) ;

		public static readonly DependencyProperty DetailedProperty =
			DependencyProperty.Register (
			                             "Detailed"
			                           , typeof ( bool )
			                           , typeof ( TypeControl )
			                           , new PropertyMetadata ( default ( bool ) )
			                            ) ;

		public static readonly DependencyProperty TargetDetailedProperty =
			DependencyProperty.Register (
			                             "TargetDetailed"
			                           , typeof ( bool )
			                           , typeof ( TypeControl )
			                           , new PropertyMetadata ( default ( bool ) )
			                            ) ;

		public Type RenderedType
		{
			get => GetValue ( RenderedTypeProperty ) as Type ;
			set => SetValue ( RenderedTypeProperty , value ) ;
		}

		public string TargetName
		{
			get => ( string ) GetValue ( TargetNameProperty ) ;
			set => SetValue ( TargetNameProperty , value ) ;
		}

		public Frame Target
		{
			get => ( Frame ) GetValue ( TargetProperty ) ;
			set => SetValue ( TargetProperty , value ) ;
		}

		public bool Detailed
		{
			get => ( bool ) GetValue ( DetailedProperty ) ;
			set => SetValue ( DetailedProperty , value ) ;
		}

		public bool TargetDetailed
		{
			get => ( bool ) GetValue ( TargetDetailedProperty ) ;
			set => SetValue ( TargetDetailedProperty , value ) ;
		}


		public event RoutedPropertyChangedEventHandler < Type > RenderedTypeChanged
		{
			add => AddHandler ( App.RenderedTypeChangedEvent , value ) ;
			remove => RemoveHandler ( App.RenderedTypeChangedEvent , value ) ;
		}

		public TypeControl ( )
		{
			RenderedTypeChanged +=
				new RoutedPropertyChangedEventHandler < Type > ( OnRenderedTypeChanged ) ;
			InitializeComponent ( ) ;
			PopulateControl ( GetValue ( RenderedTypeProperty ) as Type ) ;
		}

		private void OnRenderedTypeChanged (
			object                                  sender
		  , RoutedPropertyChangedEventArgs < Type > e
		)
		{
			PopulateControl ( e.NewValue ) ;
		}

		private void PopulateControl ( Type myType )
		{
			IAddChild addChild ;
			if ( Detailed )
			{
				var paragraph = new Paragraph ( ) ;
				flowDocument = new FlowDocument ( paragraph ) ;
				var reader = new FlowDocumentReader { Document = flowDocument } ;
				addChild = paragraph ;
				Content  = reader ;
			}
			else
			{
				addChild = new TextBlock ( ) ;
				Content  = addChild ;

				// Container.Children.Clear();
				// Container.Children.Add ( block ) ;
			}

			// Viewer.Document.Blocks.Clear();
			// doc.Blocks.Clear();
			// Container.Children.Clear();
			if ( myType == null )
			{
				return ;
			}

			// TextBlock b = new TextBlock();
			var mainInline = new Span ( ) ;

			if ( Detailed )
			{
				var elem = new List ( ) { MarkerStyle = TextMarkerStyle.None} ;

				var baseType = myType.BaseType ;
				while ( baseType != null )
				{
					var paragraph = new Paragraph ( ) ;
					var listItem = new ListItem ( paragraph ) ;

					GenerateControlsForType ( baseType , paragraph , false ) ;
					elem.ListItems.Add ( listItem ) ;
					//Container.Children.Insert ( 0 , new TextBlock ( new Hyperlink()) ( baseType.Name ) ) ) ;
					baseType = baseType.BaseType ;
				}

				flowDocument.Blocks.InsertBefore ( flowDocument.Blocks.FirstBlock , elem ) ;
			}

			var p = new Span ( ) ;
			GenerateControlsForType ( myType , p , true ) ;
			addChild.AddChild ( p ) ;
			// Viewer.Document.Blocks.Add ( block ) ;
			// Container.Children.Add ( ) ;
		}

		public FlowDocument flowDocument { get ; set ; }

		private void GenerateControlsForType ( Type myType , IAddChild addChild , bool toolTip )
		{
			// TextBlock tb = new TextBlock();
			// var old = addChild ;
			// addChild = tb ;

			var name = NameForType ( myType ) ;
			var hyperLink = new Hyperlink ( new Run ( myType.Name ) ) ;
			Uri.TryCreate (
			               "obj://" + Uri.EscapeUriString ( myType.Name )
			             , UriKind.Absolute
			             , out var res
			              ) ;

			hyperLink.NavigateUri = res ;
			// hyperLink.Command          = MyAppCommands.VisitTypeCommand ;
			// hyperLink.CommandParameter = myType ;
			if ( toolTip )
			{
				hyperLink.ToolTip = new ToolTip ( ) { Content = ToopTipContent ( myType ) } ;
			}

			hyperLink.RequestNavigate += HyperLinkOnRequestNavigate ;
			addChild.AddChild ( hyperLink ) ;
			if ( myType.IsGenericType )
			{
				addChild.AddText ( "<" ) ;
				var i = 0 ;
				foreach ( var arg in myType.GenericTypeArguments )
				{
					GenerateControlsForType ( arg , addChild , true ) ;
					if ( i < myType.GenericTypeArguments.Length )
					{
						addChild.AddText ( ", " ) ;
					}
				}

				addChild.AddText ( ">" ) ;
			}

			//old.AddChild ( tb ) ;
		}

		private object ToopTipContent ( Type myType , StackPanel pp = null )
		{
			var provider = new CSharpCodeProvider ( ) ;
			var codeTypeReference = new CodeTypeReference ( myType ) ;
			var q = codeTypeReference ;
			var toopTipContent = new TextBlock ( )
			                     {
				                     Text     = provider.GetTypeOutput ( q )
				                   , FontSize = 20
				                     //, Margin = new Thickness ( 15 )
				                    ,
			                     } ;
			if ( pp == null )
			{
				pp = new StackPanel ( ) { Orientation = Orientation.Vertical } ;
			}

			pp.Children.Insert ( 0 , toopTipContent ) ;
			var @base = myType.BaseType ;
			if ( @base != null )
			{
				ToopTipContent ( @base , pp ) ;
			}

			return pp ;
		}

		private string NameForType ( Type myType )
		{
			var provider = new CSharpCodeProvider ( ) ;
			if ( myType.IsGenericType )
			{
				var type = myType.GetGenericTypeDefinition ( ) ;
				myType = type ;
			}

			var codeTypeReference = new CodeTypeReference ( myType ) ;
			var q = codeTypeReference ;
			//myType.GetGenericTypeParameters()
			return provider.GetTypeOutput ( q ) ;
			// return myType.IsGenericType ? myType.GetGenericTypeDefinition ( ).Name : myType.Name ;
		}

		private void HyperLinkOnRequestNavigate ( object sender , RequestNavigateEventArgs e )
		{
			if ( UsePageFunction )
			{
				var p = new PageFunction1 ( ) ;
			}
			Logger.Debug ( $"{nameof ( HyperLinkOnRequestNavigate )}: Uri={e.Uri}" ) ;
			var uie = ( ContentElement ) sender ;
			try
			{
				var navigationService = Target != null
					                        ? NavigationService.GetNavigationService (
					                                                                  Target.Content
						                                                                  as
						                                                                  DependencyObject
					                                                                 )
					                        : NavigationService.GetNavigationService ( this ) ;

				if ( navigationService != null )
				{
					var targetDetailed = Detailed || TargetDetailed ;
					var value = uie.GetValue ( App.RenderedTypeProperty ) as Type ;
					var typeControl2 = new TypeControl2 ( ) ;
					typeControl2.SetValue(App.RenderedTypeProperty, value);
					if ( ! navigationService.Navigate (
					                                   typeControl2
					                                 , new NavState ( )
					                                   {
						                                   Detailed     = targetDetailed
						                                 , RenderedType = value
					                                   }
					                                  ) )
					{
						Logger.Error ( "nav cancelled" ) ;
					}
					e.Handled = true ;
				}
				else
				{
					Logger.Info ( "find other way to navigate type" ) ;
				}
			}
			catch ( Exception ex )
			{
				Logger.Warn ( ex , ex.Message ) ;
			}
		}

		public bool UsePageFunction { get ; set ; }
	}

	internal class NavState
	{
		public bool Detailed { get ; set ; }

		public Type RenderedType { get ; set ; }
	}
}