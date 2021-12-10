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
        private CategoryViewModel _category;

        public MoneyChange MoneyChange
        {
            get 
            { 
                return _moneyChange; 
            }

            set 
            { 
                this._moneyChange = value;
                OnPropertyChange(nameof(this.Impact));
                OnPropertyChange(nameof(this.Account));
                OnPropertyChange(nameof(this.Date));
                OnPropertyChange(nameof(this.Description));
                OnPropertyChange(nameof(this.Category));
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
                return new AccountViewModel(this.MoneyChange.Account);
            }

            set
            {
                this.MoneyChange.Account = (value as AccountViewModel).Account;
                OnPropertyChange();
            }
        }

        public DateTime Date
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

        public CategoryViewModel Category
        {
            get
            {
                return new CategoryViewModel(this.MoneyChange.Category);
            }

            set
            {
                this.MoneyChange.Category = (value as CategoryViewModel).Category;
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
            this.MoneyChange.Impact = default;
            this.MoneyChange.Account = new Account();
            this.MoneyChange.Category = new Category();
            this.MoneyChange.Description = string.Empty;
            this.PropertyChanged += DB_SaveChanges;
        }

        public MoneyChangeViewModel(MoneyChange change) : this()
        {
            this.MoneyChange = change;
        }

        private void DB_SaveChanges(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Service.SaveChanges();
        }
    }
}
