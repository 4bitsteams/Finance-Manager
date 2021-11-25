using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class MoneyTransaction
    {
        private string _description;
        private string _categoryID;
        private float _impact;
        private DateTime _performDate;

        public string Description
        {
            set
            {
                this._description = value;
            }
            get
            {
                return this._description;
            }
        }
        public string CategoryID
        {
            get
            {
                return this._categoryID;
            }
        }
        public float Impact
        {
            set { this._impact = value; }
            get { return this._impact; }
        }
        public DateTime PerformDate
        { 
            set { this._performDate = value; }
            get { return this._performDate; } 
        }

        public MoneyTransaction() 
        {
            this._description = string.Empty;
            this._categoryID = string.Empty;
        }

        public void Perform(string accountID)
        {
            //need toget account by its ID
            Account account = new Account();
            account.Amount += this.Impact;
            if(this.PerformDate == default)
            {
                this.PerformDate = DateTime.Today;
            }

            account.TransactionsHistory.Add(this);

        }
    }
}
