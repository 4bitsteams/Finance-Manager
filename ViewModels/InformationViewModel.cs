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
            this.MoneyChangeEditViewModel = new MoneyChangeEditViewModel();
            this.MoneyChangeEditViewModel.PropertyChanged += MoneyChangeEditViewModelChandged;
            this.CategoriesLoad();
            this.ExpencesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Expences;
            });
            this.IncomesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Incomes;
            });
            this.ShownMoneyChanges = this.Expences;
            this.ExpenxesRadioButtonIsChecked = true;
        }

        private void MoneyChangeEditViewModelChandged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MoneyChangeEditViewModel.SelectedCategory):
                    if (this.MoneyChangeEditViewModel.MoneyChange != this.SelectedMoneyChange)
                    {
                        this._selectedmoneyChange = null;
                        OnPropertyChange(nameof(this.SelectedMoneyChange));
                    }
                    break;
                default:
                    break;
            }
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

        public void CategoriesLoad()
        {
            if (this.Expences != null)
            {
                this.Expences.Clear();
            }
            else
            {
                this.Expences = new ObservableCollection<CategoryViewModel>();
            }

            if (this.Incomes != null)
            {
                this.Incomes.Clear();
            }
            else
            {
                this.Incomes = new ObservableCollection<CategoryViewModel>();
            }
            foreach (var category in Service.Categories)
            {
                if (category.Type == ChangeType.Income)
                {
                    CategoryViewModel categoryViewModel = new CategoryViewModel(category);
                    categoryViewModel.PropertyChanged += CategoryViewModelPropertyChanged;
                    this.Incomes.Add(categoryViewModel);
                }
                else if (category.Type == ChangeType.Expenses)
                {
                    CategoryViewModel categoryViewModel = new CategoryViewModel(category);
                    categoryViewModel.PropertyChanged += CategoryViewModelPropertyChanged;
                    this.Expences.Add(categoryViewModel);
                }
            }

            this.NormilizeCategories();
        }

        public void NormilizeCategories()
        {
            for(int i = 0; i < this.Expences.Count; i++)
            {
                if (this.Expences[i].MoneyChanges.Count == 0)
                {
                    this.Expences.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < this.Incomes.Count; i++)
            {
                if (this.Incomes[i].MoneyChanges.Count == 0)
                {
                    this.Incomes.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
