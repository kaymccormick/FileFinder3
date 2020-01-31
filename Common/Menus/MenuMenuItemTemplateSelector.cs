using System.Linq ;
using System.Windows ;
using System.Windows.Controls ;
using AppShared.Interfaces ;
using Common.Logging ;
using NLog ;

namespace Common.Menus
{
	public class MenuMenuItemTemplateSelector : DataTemplateSelector

	{
		public AppLogger AppLogger { get ; set ; }

		public ILogger Logger
		{
			get => AppLogger.LoggerInstance ;
			set => AppLogger.LoggerInstance = value ;
		}


		public override DataTemplate SelectTemplate ( object item , DependencyObject container )
		{
			var menuItem = container as MenuItem ;
			var cp = container as ContentPresenter ;
			if ( cp != null )
			{
				Logger.Debug ( "contentpresenter" ) ;
				return base.SelectTemplate ( item , container ) ;
			}
			// if ( menuItem == null )
			// {
			//     Logger.Warn( $"container is not a menuitem {container.GetType()}" );
			//     return base.SelectTemplate( item, container );
			// }

			var source = item as IMenuItem ;
			if ( source == null )
			{
				Logger.Warn ( "item is not a IMenuItem" ) ;
				return base.SelectTemplate ( item , container ) ;
			}

			Logger.Info ( $"menuItem is {menuItem}" ) ;
			Logger.Debug ( $"args are {item} {container}" ) ;
			Logger.Debug ( $"item type is {item.GetType ( )}" ) ;
			var r = container as FrameworkElement ;
			if ( item is IMenuItem x )
			{
				Logger.Debug ( "item is IMenuItem" ) ;
				if ( x.Children.Any ( ) )
				{
					var key = "Menu_ItemTemplateChildren" ;
					Logger.Info ( $"Selecting template {key} for {container}" ) ;
					var dataTemplate = r.FindResource ( key ) as DataTemplate ;
					Logger.Debug ( $"returning {key} {dataTemplate.DataTemplateKey}" ) ;
#if writexaml
                        var sw = new StringWriter();
                        XamlWriter.Save( dataTemplate, sw );
                        Logger.Trace( sw.ToString() );
#endif
					return dataTemplate ;
				}

				else
				{
					var key = "Menu_ItemTemplateNoChildren" ;

					Logger.Info ( $"Selecting template {key} for {container}" ) ;

					var dataTemplate = menuItem.FindResource ( key ) as DataTemplate ;
					Logger.Debug ( $"returning {key} {dataTemplate.DataTemplateKey}" ) ;
#if writexaml
                        var sw = new StringWriter();
                        XamlWriter.Save( dataTemplate, sw );
                        Logger.Trace( sw.ToString() );
#endif
					return dataTemplate ;
				}
			}

			Logger.Debug ( "Returning result from base method" ) ;
			return base.SelectTemplate ( item , container ) ;
		}
	}
}