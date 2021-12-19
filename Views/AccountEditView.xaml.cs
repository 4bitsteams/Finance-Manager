using FinanceManager.Core;
using FinanceManager.Models;
using FinanceManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FinanceManager.Views
{
    /// <summary>
    /// Логика взаимодействия для AccountEditView.xaml
    /// </summary>
    public partial class AccountEditView : UserControl
    {
        public AccountEditView()
        {
            InitializeComponent();
            BalanceTextBox.LostFocus += BalanceTextBox_LostFocus;
            SaveToExelButton.Click += SaveButton_Click;
        }

        private void BalanceTextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                if (text.Text == string.Empty)
                {
                    text.Text = "0.0";
                    text.Focus();
                }
            }
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is AccountEditViewModel accountEditViewModel
                && accountEditViewModel.Editable != null)
            {
                Account account = accountEditViewModel.Editable.Account;
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.FileName = account.Name + "Information"; // Default file name
                dialog.DefaultExt = ".xlsx"; // Default file extension
                dialog.Filter = "Text documents (.xlsx)|*.xlsx"; // Filter files by extension

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    string strResult = Service.AccountToExel(account, dialog.FileName, StartDatePicker.Value, EndDatePicker.Value);
                    if (strResult != null)
                    {
                        MessageBox.Show(strResult);
                    }
                }
            }
        }
    }
}
