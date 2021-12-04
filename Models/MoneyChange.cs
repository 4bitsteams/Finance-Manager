using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public enum ChangeType
    {
        Expenses,
        Income
    };

    public class MoneyChange
    {
        private double _impact;
        private Account _account;
        private DateOnly _date;
        private Category _category;
        private ChangeType _type;
        private string _description;

        public double Impact 
        {
            get
            {
                return this._impact;
            }

            set
            {
                this._impact = value;
            }
        }

        public Account Account
        {
            get
            {
                return this._account;
            }

            set
            {
                this._account = value;
            }
        }

        public DateOnly Date
        {
            get 
            { 
                return this._date; 
            }

            set
            {
                this._date = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }

            set 
            { 
                this._description = value; 
            }
        }

        public Category Category
        {
            get
            {
                return this._category;
            }

            set
            {
                this._category = value;
            }
        }

        public ChangeType Type { get; set; }

        public MoneyChange() 
        {
            this._impact = 0;
            this._account = null;
            this._date = DateOnly.FromDateTime(DateTime.Now);
            this._category = null;
            this._description = null;
        }
    }
}
