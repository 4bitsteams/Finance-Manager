using FinanceManager.Core;
using FinanceManager.Models;

namespace FinanceManager.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private Account _account;

        public Account Account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
                OnPropertyChange(nameof(this.Name));
                OnPropertyChange(nameof(this.Balance));
                OnPropertyChange(nameof(this.ToCount));
            }
        }

        public string Name
        {
            get { return this._account.Name; }
            set
            {
                this._account.Name = value;
                OnPropertyChange();
            }
        }

        public double Balance
        {
            get { return _account.Balance; }
            set
            {
                this._account.Balance = value;
                OnPropertyChange();
            }
        }

        public bool ToCount
        {
            get
            {
                return this._account.ToCount;
            }

            set
            {
                this._account.ToCount = value;
                OnPropertyChange();
            }
        }

        public AccountViewModel() 
        {
            this._account = new Account();
            this.PropertyChanged += DB_SaveChanges;
        }

        public AccountViewModel(Account account) : this()
        {
            this.Account = account;
        }

        private void DB_SaveChanges(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Service.SaveChanges();
        }
    }
}
