using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public class PropertyView : ViewBase
    {
        public static readonly DependencyProperty ItemTemplateProperty =
            ItemsControl.ItemTemplateProperty.AddOwner( typeof(PropertyView) );

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue( ItemTemplateProperty );
            set => SetValue( ItemTemplateProperty, value );
        }

        protected override object DefaultStyleKey =>
            new ComponentResourceKey( GetType(), "myViewDSK" );
    }
}