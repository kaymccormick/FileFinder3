﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Menus
{
    internal class MenuTemplateSelector : ItemContainerTemplateSelector
    {
        public override DataTemplate SelectTemplate(
            object       item,
            ItemsControl parentItemsControl
        )
        {
            throw new NotImplementedException();
        }
    }
}