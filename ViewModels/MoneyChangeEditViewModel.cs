using FinanceManager.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.ViewModels
{
    public class MoneyChangeEditViewModel : ViewModelBase
    {
        private MoneyChangeViewModel? _moneyChange;
        private CategoryViewModel? _selectedCategory;
        private AccountViewModel? _selectedAccount;
        private ObservableCollection<CategoryViewModel> _availibleCategories;
        private ObservableCollection<AccountViewModel> _availibleAccounts;
        private bool _isEditable;
        private bool _isVisible;

        public bool IsEditable
        {
            get
            {
                return this._isEditable;
            }

            set
            {
                if (this.IsVisible == false)
                {
                    this._isEditable = false;
                }
                else
                {
                    this._isEditable = value;
                }
                this.OnPropertyChange();
            }
        }

        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                this._isVisible = value;
                if (value == false)
                {
                    this.IsEditable = false;
                }
                this.OnPropertyChange();
            }
        }

        public MoneyChangeViewModel? MoneyChange
        {
            get 
            {
                return this._moneyChange;
            }

            set
            {
                this._moneyChange = value;
                if (this.VisibilityCheck())
                {
                    this._moneyChange.PropertyChanged -= MoneyChangeEditPropertyChanged;
                    this._moneyChange.PropertyChanged += MoneyChangeEditPropertyChanged;
                    this.IsVisible = true;
                    this._selectedAccount = this._moneyChange.Account;
                    this.OnPropertyChange(nameof(this.SelectedAccount));
                    this.OnPropertyChange(nameof(this.Account));
                    this.OnPropertyChange(nameof(this.Category));
                    this.OnPropertyChange(nameof(this.Date));
                    this.OnPropertyChange(nameof(this.Description));
                    this.OnPropertyChange(nameof(this.Impact));
                    this.OnPropertyChange();
                }
            }
        }

        public string Description
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return this._moneyChange.Description;
                }

                return string.Empty;
            }

            set
            {
                if (this.VisibilityCheck())
                {
                    this._moneyChange.Description = value;
                }
            }
        }

        public double Impact
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return this._moneyChange.Impact;
                }

                return 0d;
            }

            set
            {
                if (this.VisibilityCheck())
                {
                    this._moneyChange.Impact = value;
                }
            }
        }

        public CategoryViewModel Category
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return this._moneyChange.Category;
                }

                return new CategoryViewModel();
            }

            set
            {
                if (this.VisibilityCheck())
                {
                    this._moneyChange.Category = value;
                }
            }
        }

        public CategoryViewModel? SelectedCategory
        {
            get
            {
                return this._selectedCategory;
            }

            set
            {
                this._selectedCategory = value;
                this.OnPropertyChange();
            }
        }

        public ObservableCollection<CategoryViewModel> AvailibleCategories
        {
            get
            {
                AvailibleCategoriesLoad();
                return this._availibleCategories;
            }
            set
            {
                this._availibleCategories = value;
                this.OnPropertyChange();
            }
        }

        public DateTime Date
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return this._moneyChange.Date;
                }

                return DateTime.Now;
            }

            set
            {
                if (this.VisibilityCheck())
                {
                    this._moneyChange.Date = value;
                }
            }
        }

        public AccountViewModel Account
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return this._moneyChange.Account;
                }

                return new AccountViewModel();
            }

            set
            {
                if (this.VisibilityCheck())
                {
                    this._moneyChange.Account = value;
                }
            }
        }

        public AccountViewModel? SelectedAccount
        {
            get
            {
                return this._selectedAccount;
            }

            set
            {
                this._selectedAccount = value;
                this.Account = this._selectedAccount;
                this.OnPropertyChange();
            }
        }

        public ObservableCollection<AccountViewModel> AvailibleAccounts
        {
            get
            {
                AvailibleAccountsLoad();
                return this._availibleAccounts;
            }
            set
            {
                this._availibleAccounts = value;
                this.OnPropertyChange();
            }
        }

        public RelayCommand EditCommand { get; set; }

        public MoneyChangeEditViewModel()
        {
            this.MoneyChange = new();
            this.IsVisible = false;
            this.EditCommand = new RelayCommand(o =>
            {
                this.IsEditable = true;
            });
            AvailibleAccountsLoad();
            AvailibleCategoriesLoad();
        }

        public MoneyChangeEditViewModel(MoneyChangeViewModel moneyChange) : this()
        {
            this.MoneyChange = moneyChange;
        }

        private void MoneyChangeEditPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MoneyChangeViewModel.Description):
                    this.OnPropertyChange(nameof(this.Description));
                    break;
                case nameof(MoneyChangeViewModel.Category):
                    this.OnPropertyChange(nameof(this.Category));
                    break;
                case nameof(MoneyChangeViewModel.Date):
                    this.OnPropertyChange(nameof(this.Date));
                    break;
                case nameof(MoneyChangeViewModel.Impact):
                    this.OnPropertyChange(nameof(this.Impact));
                    break;
                case nameof(MoneyChangeViewModel.Account):
                    this.OnPropertyChange(nameof(this.Account));
                    break;
                default:
                    break;
            }
        }

        private bool VisibilityCheck()
        {
            if(this._moneyChange != null)
            {
                return true;
            }

            this.IsVisible = false;
            return false;
        }

        private void AvailibleAccountsLoad()
        {
            if (this._availibleAccounts != null)
            {
                this._availibleAccounts.Clear();
            }
            else
            {
                this._availibleAccounts = new ObservableCollection<AccountViewModel>();
            }
            foreach(var account in Service.Accounts)
            {
                this._availibleAccounts.Add(new AccountViewModel(account));
            }
        }

        private void AvailibleCategoriesLoad()
        {
            if (this._availibleCategories != null)
            {
                this._availibleCategories.Clear();
            }
            else
            {
                this._availibleCategories = new ObservableCollection<CategoryViewModel>();
            }
            foreach (var category in Service.Categories)
            {
                this._availibleCategories.Add(new CategoryViewModel(category));
            }
        }
    }
}
