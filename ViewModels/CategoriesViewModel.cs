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
    public class CategoriesViewModel : ViewModelBase
    {
        private CategoryViewModel _selectedCategory;
        private ObservableCollection<CategoryViewModel> _incomes;
        private ObservableCollection<CategoryViewModel> _expances;
        private bool _incomesRadioButtonIsChecked;
        private bool _expencesRadioButtonIsChecked;

        public CategoryViewModel SelectedCategory
        {
            get 
            { 
                return _selectedCategory;
            }

            set 
            { 
                this._selectedCategory = value;
                this.CategoryEditViewModel.EditableCategory = this._selectedCategory;
                OnPropertyChange();
            }
        }

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

        public ObservableCollection<CategoryViewModel> Incomes
        {
            get
            {
                return this._incomes;
            }

            set
            {
                this._incomes = value;
            }
        }

        public ObservableCollection<CategoryViewModel> ShownCategories { get; set; }

        public RelayCommand ExpencesCommand { get; set; }

        public RelayCommand IncomesCommand { get; set; }

        public CategoryEditViewModel CategoryEditViewModel { get; set; }

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

        public CategoriesViewModel()
        {
            this.Incomes = new ObservableCollection<CategoryViewModel>();
            this.CategoryEditViewModel = new CategoryEditViewModel();
            this.CategoryEditViewModel.PropertyChanged += CategoryAdded;
            this.CategoriesLoad();
            this.ShownCategories = this.Expences;
            this.ExpencesCommand = new RelayCommand(o =>
            {
                this.ShownCategories = this.Expences;
            });
            this.IncomesCommand = new RelayCommand(o =>
            {
                this.ShownCategories = this.Incomes;
            });
            this.IncomesRadioButtonIsChecked = true;
        }

        private void CategoryAdded(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CategoryEditViewModel.EditableCategory):
                    if(this.SelectedCategory != this.CategoryEditViewModel.EditableCategory)
                    {
                        this.ShownCategories.Add(this.CategoryEditViewModel.EditableCategory);
                    }
                    break;
                default:
                    break;
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
                this.Expences.Add(new CategoryViewModel(category));
            }
        }

        public void AccountsUpdate()
        {
            if (this.Accounts == null)
            {
                this.Accounts = new ObservableCollection<AccountViewModel>();
                this.AccountsLoad();
            }
            else
            {
                foreach (var account in Service.Accounts)
                {
                    bool flag = true;
                    foreach (var exAcc in this.Accounts)
                    {
                        if (exAcc.Account == account)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        AccountViewModel acc = new AccountViewModel(account);
                        acc.PropertyChanged += AccountsChanged;
                        Accounts.Add(new AccountViewModel(account));
                    }
                }
            }
        }
    }
}
