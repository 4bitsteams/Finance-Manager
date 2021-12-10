using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Category
    {
        private string _id;
        private string _name;
        private List<MoneyChange> _changes;
        private string _imageSource;

        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string Name 
        {
            set { this._name = value; }
            get { return this._name; } 
        }
        public string ImageSource
        {
            set { this._imageSource = value; }
            get { return this._imageSource; }
        }

        public List<MoneyChange> MoneyChanges
        {
            get { return this._changes; }
            set { this._changes = value; }
        }

        public double TotalExpencesImpact
        {
            get
            {
                double totalExpencesImpact = 0;
                foreach (MoneyChange change in MoneyChanges)
                {
                    if (change.Type == ChangeType.Expenses)
                    {
                        totalExpencesImpact += change.Impact;
                    }
                }

                return totalExpencesImpact;
            }
        }

        public double TotalIncomesImpact
        {
            get
            {
                double totalIncomesImpact = 0;
                foreach (MoneyChange change in MoneyChanges)
                {
                    if (change.Type == ChangeType.Income)
                    {
                        totalIncomesImpact += change.Impact;
                    }
                }

                return totalIncomesImpact;
            }
        }

        public Category()
        {
            this._changes = new List<MoneyChange>();
        }

        public Category(string Id, string Name, string ImageSource, List<MoneyChange> MoneyChanges)
        {
            this.Id = Id;
            this.Name = Name;
            this.ImageSource = ImageSource;
            this.MoneyChanges = MoneyChanges;
        }

        public void AddMoneyChange(MoneyChange change)
        {
            if (!this.MoneyChanges.Contains(change))
            {
                this.MoneyChanges.Add(change);
            }
        }

        public void RemoveMoneyChange(MoneyChange change)
        {
            this.MoneyChanges.Remove(change);
        }
    }
}
