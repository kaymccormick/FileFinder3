using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Markup ;

namespace Common
{
	[ContentProperty(nameof(InnerPanel))]
	public class CustomPanel : Panel, IAddChild
	{
		private int _currentRow ;
		private int _currentColumn ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Controls.Panel" /> class.</summary>
		public CustomPanel ( )
		{
			InnerPanel = new Grid ( ) ;
			InnerPanel.ColumnDefinitions.Add (
			                                  new ColumnDefinition ( ) { Width = GridLength.Auto }
			                                 ) ;
			InnerPanel.ColumnDefinitions.Add (
			                                  new ColumnDefinition ( ) { Width = GridLength.Auto }
			                                 ) ;
			InnerPanel.RowDefinitions.Add ( new RowDefinition ( ) { Height = GridLength.Auto } ) ;
		}

		public Grid InnerPanel { get ; set ; }

		void IAddChild.AddChild ( object value )
		{
		
			var uiElement = ( UIElement ) value ;
			uiElement.SetValue ( Grid.RowProperty , _currentRow ) ;
			uiElement.SetValue(Grid.ColumnProperty, _currentColumn);
			InnerPanel.Children.Add ( uiElement ) ;
			_currentColumn += 1 ;
			if ( _currentColumn % InnerPanel.ColumnDefinitions.Count == 0 )
			{
				_currentColumn = 0 ;
				_currentRow += 1 ;
				AddRow ( ) ;
			}
			this.Children.Add ( uiElement ) ;
		}

		private void AddRow ( )
		{
			InnerPanel.RowDefinitions.Add ( new RowDefinition ( ) { Height = GridLength.Auto } ) ;
		}
	}
}