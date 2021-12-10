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
        private ApplicationContext _dB;

        private MoneyChangeViewModel? _selectedmoneyChange;
        private ObservableCollection<CategoryViewModel> _categories;
        private bool _incomesRadioButtonIsChecked;
        private bool _expencesRadioButtonIsChecked;

        public ObservableCollection<CategoryViewModel> Expences { get; set; }

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
            this._dB = new ApplicationContext();
            MoneyChangeEditViewModel = new MoneyChangeEditViewModel();
            Expences = new ObservableCollection<CategoryViewModel>();
            Incomes = new ObservableCollection<CategoryViewModel>();
            Category cat = new Category();
            cat.Name = "Category";
            cat.AddMoneyChange(new MoneyChange(15, 48 , new Account("Account", 25, true), DateTime.Now, ChangeType.Expenses, "Some description", cat));
            cat.AddMoneyChange(new MoneyChange(15, 36.15, new Account("Account", 25, true), DateTime.Now, ChangeType.Expenses, "Some description", cat));
            Expences.Add(new CategoryViewModel(cat));
            Expences.Add(new CategoryViewModel(cat));
            Expences.Add(new CategoryViewModel(cat));
            Expences[^1].PropertyChanged += CategoryViewModelPropertyChanged;
            Expences[^2].PropertyChanged += CategoryViewModelPropertyChanged;
            Expences[^3].PropertyChanged += CategoryViewModelPropertyChanged;
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
    }
}
