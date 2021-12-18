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
    }
}
