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
using System.Windows.Shapes;
using Vanara.PInvoke;
using WpfApp1.Attributes;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [WindowMetadata("Main Window")]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Vanara.PInvoke.User32.SetWindowLong( Vanara.PInvoke.User32.GetActiveWindow(), User32.WindowLongFlags.GWL_EXSTYLE )
        }

    }
}
