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
    public class MoneyChangeEditViewModel : ViewModelBase
    {
        private MoneyChangeViewModel? _moneyChange;
        private ObservableCollection<CategoryViewModel> _availibleCategories;
        private ObservableCollection<AccountViewModel> _availibleAccounts;
        private string _supposedDescription;
        private double _supposedImpact;
        private DateTime _supposedDate;
        private AccountViewModel? _supposedAccount;
        private CategoryViewModel? _supposedCategory;
        private bool _isEditable;
        private bool _isVisible;
        private ChangeType _currentType;

        public ChangeType CurrentType
        {
            get { return _currentType; }
            set 
            { 
                _currentType = value;
                OnPropertyChange();
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }

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
                    this.SupposedDescription = this._moneyChange.Description;
                    this.SupposedImpact = this._moneyChange.Impact;
                    this.SupposedDate = this._moneyChange.Date;
                    this.SupposedAccount = this._moneyChange.Account;
                    this.SupposedCategory = this._moneyChange.Category;
                }
                this.OnPropertyChange();
            }
        }

        public bool SaveAbility
        {
            get
            {
                return this.SaveAbilityCheck();
            }
        }

        public string SupposedDescription
        {
            get
            {
                return this._supposedDescription;
            }

            set
            {
                this._supposedDescription = value;
                OnPropertyChange();
            }
        }

        public double SupposedImpact
        {
            get
            {
                return this._supposedImpact;
            }

            set
            {
                this._supposedImpact = value;
                OnPropertyChange();
            }
        }

        public DateTime SupposedDate
        {
            get
            {
                return this._supposedDate;
            }

            set
            {
                this._supposedDate = value;
                OnPropertyChange();
            }
        }

        public AccountViewModel? SupposedAccount
        {
            get
            {
                return this._supposedAccount;
            }

            set
            {
                this._supposedAccount = value;
                OnPropertyChange();
            }
        }

        public CategoryViewModel? SupposedCategory
        {
            get
            {
                return this._supposedCategory;
            }

            set
            {
                this._supposedCategory = value;
                OnPropertyChange();
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

        public ObservableCollection<CategoryViewModel> AvailibleCategories
        {
            get
            {
                return this._availibleCategories;
            }
            set
            {
                this._availibleCategories = value;
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

        public RelayCommand RemoveCommand { get; set; }

        public RelayCommand AddMoneyChangeCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }

        public MoneyChangeEditViewModel()
        {
            this.PropertyChanged += SupposedPropertiesChanged;
            this.MoneyChange = new();
            this.IsVisible = false;
            this.EditCommand = new RelayCommand(o =>
            {
                this.IsEditable = !this.IsEditable;
            });
            this.AddMoneyChangeCommand = new RelayCommand(o =>
            {
                this.MoneyChange = null;
                this.IsVisible = true;
                this.IsEditable = true;
                this.SupposedDescription = "New money change";
                this.SupposedImpact = default;
                this.SupposedDate = DateTime.Now;
                this.SupposedAccount = null;
                this.SupposedCategory = null;
            });
            this.RemoveCommand = new RelayCommand(o =>
            {
                Service.RemoveMoneyChange(this.MoneyChange.MoneyChange);
                this.MoneyChange.Account.RemoveMoneyChange(this.MoneyChange);
                this.MoneyChange = null;
            });
            this.SaveCommand = new RelayCommand(o =>
            {
                SaveFromSupposed();
            });
            AvailibleAccountsLoad();
            AvailibleCategoriesLoad();
        }

        public MoneyChangeEditViewModel(MoneyChangeViewModel moneyChange) : this()
        {
            this.MoneyChange = moneyChange;
        }

        private void SupposedPropertiesChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SupposedDescription):
                    OnPropertyChange(nameof(this.SaveAbility));
                    break;
                case nameof(this.SupposedImpact):
                    OnPropertyChange(nameof(this.SaveAbility));
                    break;
                case nameof(this.SupposedDate):
                    OnPropertyChange(nameof(this.SaveAbility));
                    break;
                case nameof(this.SupposedAccount):
                    OnPropertyChange(nameof(this.SaveAbility));
                    break;
                case nameof(this.SupposedCategory):
                    OnPropertyChange(nameof(this.SaveAbility));
                    break;
                case nameof(this.MoneyChange):
                    OnPropertyChange(nameof(this.SaveAbility));
                    break;
                case nameof(this.CurrentType):
                    AvailibleCategoriesLoad();
                    break;
                default:
                    break;
            }
        }
            private void MoneyChangeEditPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MoneyChangeViewModel.Category):
                    this.OnPropertyChange(nameof(this.Category));
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

        private void SaveFromSupposed()
        {
            if (this._moneyChange == null)
            {
                this._moneyChange = new MoneyChangeViewModel();
                this._moneyChange.PropertyChanged += MoneyChangeEditPropertyChanged;
            }
            this._moneyChange.Description = this.SupposedDescription;
            this._moneyChange.Impact = this.SupposedImpact;
            this._moneyChange.Date = this.SupposedDate;
            this._moneyChange.Type = this.CurrentType;
            this._moneyChange.Account = this.SupposedAccount;
            this._moneyChange.Category = this.SupposedCategory;

            Service.SaveChanges();
        }

        private bool SaveAbilityCheck()
        {
            if (this.SupposedDescription != null 
                && this.SupposedDescription != string.Empty
                && this.SupposedAccount != null
                && this.SupposedCategory != null)
            {
                if (this._moneyChange == null
                    || this.SupposedDescription != this._moneyChange.Description
                    || this.SupposedImpact != this._moneyChange.Impact
                    || this.SupposedDate.Date != this._moneyChange.Date.Date
                    || !this.SupposedAccount.Equals(this._moneyChange.Account)
                    || !this.SupposedCategory.Equals(this._moneyChange.Category))
                {
                    return true;
                }
            }
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
                if (category.Type == this.CurrentType)
                {
                    this._availibleCategories.Add(new CategoryViewModel(category));
                }
            }
            OnPropertyChange(nameof(this.AvailibleCategories));
        }

        
    }
}
