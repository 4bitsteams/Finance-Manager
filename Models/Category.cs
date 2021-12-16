using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FinanceManager.Models
{
    public class Category
    {
        private int _id;
        private string _name;
        private Color _color;
        private List<MoneyChange> _changes;
        private string _imageSource;
        private ChangeType _type;

        public int Id
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

        public ChangeType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }

        public byte A
        {
            get
            {
                return this._color.A;
            }
            set
            {
                this._color.A = value;
            }
        }

        public byte R
        {
            get
            {
                return this._color.R;
            }
            set
            {
                this._color.R = value;
            }
        }

        public byte B
        {
            get
            {
                return this._color.B;
            }
            set
            {
                this._color.B = value;
            }
        }

        public byte G
        {
            get
            {
                return this._color.G;
            }
            set
            {
                this._color.G = value;
            }
        }

        [NotMapped]
        public Color Color
        {
            get { return this._color; }
            set { this._color = value; }
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
            this._name = "New Category";
            this._changes = new List<MoneyChange>();
            this.ImageSource = "some source";
            this.Color = Colors.White;
            this.Type = ChangeType.Expenses;
        }

        public Category(string Name, string ImageSource, ChangeType type, List<MoneyChange> MoneyChanges = null)
        {
            this.Name = Name;
            this.ImageSource = ImageSource;
            this.MoneyChanges = MoneyChanges;
            this.Type = type;
            this.Color= Colors.White;
            if (MoneyChanges == null)
            {
                MoneyChanges = new List<MoneyChange>();
            }

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
