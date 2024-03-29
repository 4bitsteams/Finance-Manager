﻿using System.Windows;
using System.Windows.Input;

namespace FinanceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTQ3ODQ1QDMxMzkyZTMzMmUzMGRuaXFBbWx3cVNqajVCQnY0SmJic2dZMVJuWk1yNG9Vb1RUeDI4b0VLSWM9");
            InitializeComponent();
        }

        private void WindowMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Exit_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                SystemCommands.MaximizeWindow(this);
            }
            else
            {
                SystemCommands.RestoreWindow(this);
            }
        }
    }
}
