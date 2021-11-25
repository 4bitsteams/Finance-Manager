using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Account
    {
        private string _id;
        private string _name;
        private float _amount;
        private bool _count;
        private List<MoneyTransaction> _transactionsHistory;

        public string Id { get { return _id; } }
        public string Name 
        { 
            set 
            { 
                this._name = value; 
            }
            get 
            { 
                return this._name; 
            } 
        }
        public float Amount 
        { 
            set 
            {
                this._amount = value;
            }
            get 
            { 
                return this._amount; 
            } 
        }
        public bool Count
        {
            set
            {
                this._count = value;
            }
            get 
            { 
                return this._count; 
            }
        }
        public List<MoneyTransaction> TransactionsHistory
        {
            get { return this._transactionsHistory; }
        }

        public Account() 
        {
            this._id = string.Empty;
            this._name = string.Empty;
            this._amount = default;
            this._transactionsHistory = new List<MoneyTransaction>();
        }
    }
}
