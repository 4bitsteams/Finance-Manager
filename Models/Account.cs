using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Account
    {
        private string _name;
        private double _balance;
        private bool _count;
        private List<MoneyChange> _influenceMoneyChanges;

        public int Id { get; set; }
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
        public double Balance 
        { 
            set 
            {
                this._balance = value;
            }
            get 
            { 
                return this._balance; 
            } 
        }
        public bool ToCount
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

        public List<MoneyChange> InfluenceMoneyChanges
        {
            get
            {
                return this._influenceMoneyChanges;
            }

            set
            {
                foreach (MoneyChange change in this._influenceMoneyChanges)
                {
                    RemoveMoneyChange(change);
                }

                foreach (MoneyChange change in value)
                {
                    AddMoneyChange(change);
                }
            }
        }


        public Account() 
        {
            this.Id = default;
            this._name = string.Empty;
            this._balance = default;
            this._count = false;
            this._influenceMoneyChanges = new List<MoneyChange>();
        }

        public Account (string Name, double Balance, bool ToCount, List<MoneyChange> InfluenceMoneyChanges = null)
        {
            this.Name = Name;
            this.Balance = Balance;
            this.ToCount = ToCount;

            if (InfluenceMoneyChanges == null)
            { 
                InfluenceMoneyChanges = new List<MoneyChange>(); 
            }

            this.InfluenceMoneyChanges = InfluenceMoneyChanges;
        }

        public void AddMoneyChange(MoneyChange change)
        {
            if (!this.InfluenceMoneyChanges.Contains(change))
            {
                this.InfluenceMoneyChanges.Add(change);
                if (change.Type is ChangeType.Expenses)
                {
                    this.Balance -= change.Impact;
                }
                else
                {
                    this.Balance += change.Impact;
                }
            }
        }

        public void RemoveMoneyChange(MoneyChange change)
        {
            this.InfluenceMoneyChanges.Remove(change);
            if (change.Type is ChangeType.Income)
            {
                this.Balance -= change.Impact;
            }
            else
            {
                this.Balance += change.Impact;
            }
        }
    }
}
