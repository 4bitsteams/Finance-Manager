﻿using FinanceManager.Core;
using FinanceManager.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace FinanceManager.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private Category _category;

        private MoneyChangeViewModel? _selectedmoneyChange;

        private ObservableCollection<MoneyChangeViewModel> _moneyChanges;

        public Category Category
        {
            get
            {
                return _category;
            }

            set
            {
                this._category = value;
                OnPropertyChange(nameof(this.Name));
                OnPropertyChange(nameof(this.ImageSource));
                OnPropertyChange(nameof(this.Color));
                this.MoneyChangesLoad();

            }
        }

        public MoneyChangeViewModel? SelectedMoneyChange
        {
            get
            {
                return this._selectedmoneyChange;
            }

            set
            {
                this._selectedmoneyChange = value;
                OnPropertyChange();
            }
        }

        public ObservableCollection<MoneyChangeViewModel> MoneyChanges
        {
            get
            {
                return this._moneyChanges;
            }
            set
            {
                this._moneyChanges = value;
                OnPropertyChange();
            }
        }

        public string Name
        {
            get
            {
                return this._category.Name;
            }

            set
            {
                this._category.Name = value;
                OnPropertyChange("DbChanged");
                OnPropertyChange();
            }
        }

        public string ImageSource
        {
            get
            {
                return this._category.ImageSource;
            }

            set
            {
                this._category.ImageSource = value;
                OnPropertyChange("DbChanged");
                OnPropertyChange();
            }
        }

        public Color Color
        {
            get
            {
                return this._category.Color;
            }

            set
            {
                this._category.Color = value;
                OnPropertyChange("DbChanged");
                OnPropertyChange();
            }
        }

        public SolidColorBrush ColorBrush
        {
            get
            {
                return this._category.ColorBrush;
            }
        }

        public ChangeType Type
        {
            get
            {
                return this._category.Type;
            }

            set
            {
                this._category.Type = value;
                OnPropertyChange();
            }
        }

        public double TotalImpact
        {
            get
            {
                return this._category.TotalImpact;
            }
        }

        public double TotalExpencesImpact
        {
            get
            {
                return this._category.TotalExpencesImpact;
            }
        }

        public double TotalIncomesImpact
        {
            get
            {
                return this._category.TotalIncomesImpact;
            }
        }

        public CategoryViewModel()
        {
            this._category = new Category();
            this.PropertyChanged += DB_SaveChanges;
        }

        public CategoryViewModel(Category category) : this()
        {
            this.Category = category;
        }

        private void MoneyChangeImpactChange(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MoneyChangeViewModel.Impact):
                    OnPropertyChange(nameof(this.TotalExpencesImpact));
                    OnPropertyChange(nameof(this.TotalIncomesImpact));
                    break;
                default:
                    break;
            }
        }

        public void AddMoneyChange(MoneyChangeViewModel moneyChange)
        {
            this.Category.AddMoneyChange(moneyChange.MoneyChange);
            this.MoneyChanges.Add(moneyChange);
            OnPropertyChange(nameof(this.MoneyChanges));
        }

        private void DB_SaveChanges(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DbChanged")
            {
                Service.SaveChanges();
            }
        }


        private void MoneyChangesLoad()
        {
            if (this._category != null)
            {
                this._moneyChanges = new ObservableCollection<MoneyChangeViewModel>();
                foreach (MoneyChange change in this._category.MoneyChanges)
                {
                    MoneyChangeViewModel moneyChange = new MoneyChangeViewModel(change);
                    moneyChange.PropertyChanged -= MoneyChangeImpactChange;
                    moneyChange.PropertyChanged += MoneyChangeImpactChange;
                    this.MoneyChanges.Add(moneyChange);
                }
            }
            OnPropertyChange(nameof(this.MoneyChanges));
        }

        public override bool Equals(object? obj)
        {
            if (obj is CategoryViewModel cat)
            {
                if (this.Category == cat.Category)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
