using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Core;
using FinanceManager.Models;

namespace FinanceManager.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        private MoneyChangeViewModel? _selectedmoneyChange;
        private ObservableCollection<CategoryViewModel> _categories;
        private ObservableCollection<CategoryViewModel> _expances;
        private bool _incomesRadioButtonIsChecked;
        private bool _expencesRadioButtonIsChecked;

        public ObservableCollection<CategoryViewModel> Expences 
        { 
            get
            {
                return this._expances;
            }

            set
            {
                this._expances = value;
            }
        }

        public ObservableCollection<CategoryViewModel> Incomes { get; set; }

        public MoneyChangeEditViewModel MoneyChangeEditViewModel { get; set; }

        public ObservableCollection<CategoryViewModel> ShownMoneyChanges
        {
            get
            {
                return this._categories;
            }
            set
            {
                this._categories = value;
                OnPropertyChange();
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
                this.MoneyChangeEditViewModel.MoneyChange = this._selectedmoneyChange;
                OnPropertyChange();
            }
        }

        public RelayCommand ExpencesCommand { get; set; }

        public RelayCommand IncomesCommand { get; set; }

        public bool IncomesRadioButtonIsChecked
        {
            get
            {
                return this._incomesRadioButtonIsChecked;
            }
            set
            {
                this._incomesRadioButtonIsChecked = value;
                OnPropertyChange();
            }
        }

        public bool ExpenxesRadioButtonIsChecked
        {
            get
            {
                return this._expencesRadioButtonIsChecked;
            }
            set
            {
                this._expencesRadioButtonIsChecked = value;
                OnPropertyChange();
            }
        }

        public InformationViewModel()
        {
            MoneyChangeEditViewModel = new MoneyChangeEditViewModel();
            this.ExpenscesLoad();
            Expences[0].MoneyChanges.Add(new MoneyChangeViewModel(new MoneyChange(50, Service.Accounts[0], DateTime.Now, ChangeType.Expenses, "Some description", Expences[0].Category)));
            Incomes = new ObservableCollection<CategoryViewModel>();
            ExpencesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Expences;
            });
            IncomesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Incomes;
            });
            this.ShownMoneyChanges = Expences;
            ExpenxesRadioButtonIsChecked = true;
        }

        private void CategoryViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is CategoryViewModel category)
            {
                switch (e.PropertyName)
                {
                    case nameof(CategoryViewModel.SelectedMoneyChange):
                        if (category.SelectedMoneyChange != null)
                        {
                            this.SelectedMoneyChange = category.SelectedMoneyChange;
                            foreach (CategoryViewModel anotherCategory in this.ShownMoneyChanges)
                            {
                                if (anotherCategory != category && anotherCategory.SelectedMoneyChange != null)
                                {
                                    anotherCategory.SelectedMoneyChange = null;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void ExpenscesLoad()
        {
            if(this.Expences != null)
            {
                this.Expences.Clear();
            }
            else
            {
                this.Expences = new ObservableCollection<CategoryViewModel>();
            }
            foreach (var category in Service.Categories)
            {
                
                //if (category.MoneyChanges.Count > 0)
                {
                    this.Expences.Add(new CategoryViewModel(category));
                    this.Expences[^1].PropertyChanged += CategoryViewModelPropertyChanged;
                }
            }
        }
    }
}
