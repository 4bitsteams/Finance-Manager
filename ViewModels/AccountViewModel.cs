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
                OnPropertyChange(nameof(this.Amount));
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

        public float Amount
        {
            get { return _account.Amount; }
            set
            {
                this._account.Amount = value;
                OnPropertyChange();
            }
        }

        public bool ToCount
        {
            get
            {
                return this._account.Count;
            }

            set
            {
                this._account.Count = value;
                OnPropertyChange();
            }
        }

        public AccountViewModel() { }

        public AccountViewModel(Account account)
        {
            this.Account = account;
        }
    }
}
