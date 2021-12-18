using System.Windows.Controls;

namespace FinanceManager.Views
{
    /// <summary>
    /// Логика взаимодействия для MoneyChangeEditView.xaml
    /// </summary>
    public partial class MoneyChangeEditView : UserControl
    {
        public MoneyChangeEditView()
        {
            InitializeComponent();
            ImpactText.LostFocus += ImpactText_LostFocus;
        }

        private void ImpactText_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                if(text.Text == string.Empty)
                {
                    text.Text = "0.0";
                    text.Focus();
                }
            }
        }
    }
}
