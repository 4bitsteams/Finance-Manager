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
        private int _id;
        private string _name;
        private double _balance;
        private bool _count;

        [Key]
        public int Id { get { return _id; } }
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


        public Account() 
        {
            this._id = default;
            this._name = string.Empty;
            this._balance = default;
            this._count = false;
        }

        public Account (string Name, double Balance, bool ToCount)
        {
            this._id = default;
            this.Name = Name;
            this.Balance = Balance;
            this.ToCount = ToCount;

        }
    }
}
