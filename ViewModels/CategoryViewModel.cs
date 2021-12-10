using FinanceManager.Core;
using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                this.MoneyChangesLoad();
                OnPropertyChange(nameof(this.Name));
                OnPropertyChange(nameof(this.ImageSource));
                OnPropertyChange(nameof(this.MoneyChanges));
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
                OnPropertyChange();
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
            this.Category = new Category();
            this.MoneyChangesLoad();
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
                    break;
                default:
                    break;
            }
        }


        private void DB_SaveChanges(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Service.SaveChanges();
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
        }
    }
}
