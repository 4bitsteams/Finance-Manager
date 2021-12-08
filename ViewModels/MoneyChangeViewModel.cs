using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Core;
using FinanceManager.Models;

namespace FinanceManager.ViewModels
{
    public class MoneyChangeViewModel : ViewModelBase
    {
        private MoneyChange _moneyChange;

        private AccountViewModel _account;

        public MoneyChange MoneyChange
        {
            get 
            { 
                return _moneyChange; 
            }

            set 
            { 
                this._moneyChange = value;
                this._account = new AccountViewModel(this._moneyChange.Account);
                OnPropertyChange(nameof(this.Impact));
                OnPropertyChange(nameof(this.Account));
                OnPropertyChange(nameof(this.Date));
                OnPropertyChange(nameof(this.Description));
                OnPropertyChange(nameof(this.Type));
            }
        }

        public double Impact
        {
            get
            {
                return this._moneyChange.Impact;
            }

            set
            {
                this._moneyChange.Impact = value;
                OnPropertyChange();
            }
        }

        public AccountViewModel Account
        {
            get
            {
                return this._account;
            }

            set
            {
                this._account = value;
                this.MoneyChange.Account = this._account.Account;
                OnPropertyChange();
            }
        }

        public DateOnly Date
        {
            get
            {
                return this._moneyChange.Date;
            }

            set
            {
                this._moneyChange.Date = value;
                OnPropertyChange();
            }
        }

        public string Description
        {
            get
            {
                return this._moneyChange.Description;
            }

            set
            {
                this._moneyChange.Description = value;
                OnPropertyChange();
            }
        }

        public ChangeType Type
        {
            get
            {
                return this._moneyChange.Type;
            }
            set
            {
                this._moneyChange.Type = value;
                OnPropertyChange();
            }
        }

        public MoneyChangeViewModel()
        {
            this._moneyChange = new MoneyChange();
            this.MoneyChange.Impact = 15;
            this.MoneyChange.Account = new Account("poof", 25, true);
            this.MoneyChange.Description = "A lot of description A lot of description A lot of description A lot of description A lot of description" +
                "A lot of description A lot of description A lot of description A lot of description A lot of description" +
                "A lot of description A lot of description A lot of description A lot of description A lot of description" +
                "A lot of description A lot of description A lot of description A lot of description A lot of description";
        }

        public MoneyChangeViewModel(MoneyChange change)
        {
            this.MoneyChange = change;
        }
    }
}
