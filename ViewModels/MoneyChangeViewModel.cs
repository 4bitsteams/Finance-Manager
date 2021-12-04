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

        public MoneyChange MoneyChange
        {
            get 
            { 
                return _moneyChange; 
            }

            set 
            { 
                this._moneyChange = value; 
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

        public Account Account
        {
            get
            {
                return this._moneyChange.Account;
            }

            set
            {
                this._moneyChange.Account = value;
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

        public Category Category
        {
            get
            {
                return this._moneyChange.Category;
            }

            set
            {
                this._moneyChange.Category = value;
                OnPropertyChange();
            }
        }

        public MoneyChangeViewModel()
        {
            this._moneyChange = new MoneyChange();
            this.MoneyChange.Account = new Account("poof", 25, true);
            this.MoneyChange.Category = new Category();
            this.MoneyChange.Category.Name = "name";
            this.MoneyChange.Description = "A lot of description A lot of description A lot of description A lot of description A lot of description" +
                "A lot of description A lot of description A lot of description A lot of description A lot of description" +
                "A lot of description A lot of description A lot of description A lot of description A lot of description" +
                "A lot of description A lot of description A lot of description A lot of description A lot of description";
        }
    }
}
