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
                OnPropertyChange();
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
                OnPropertyChange();
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
                    OnPropertyChange(nameof(this.Account));
                    OnPropertyChange(nameof(this.Category));
                    OnPropertyChange(nameof(this.Date));
                    OnPropertyChange(nameof(this.Description));
                    OnPropertyChange(nameof(this.Impact));
                    OnPropertyChange();
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

        public ObservableCollection<CategoryViewModel> AvailibleCategories { get; set; }

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

        public ObservableCollection<AccountViewModel> AvailibleAccounts { get; set; }

        public RelayCommand EditCommand { get; set; }

        public MoneyChangeEditViewModel()
        {
            this.MoneyChange = new();
            this.IsVisible = false;
            this.EditCommand = new RelayCommand(o =>
            {
                this.IsEditable = true;
            });
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
                    OnPropertyChange(nameof(this.Description));
                    break;
                case nameof(MoneyChangeViewModel.Category):
                    OnPropertyChange(nameof(this.Category));
                    break;
                case nameof(MoneyChangeViewModel.Date):
                    OnPropertyChange(nameof(this.Date));
                    break;
                case nameof(MoneyChangeViewModel.Impact):
                    OnPropertyChange(nameof(this.Impact));
                    break;
                case nameof(MoneyChangeViewModel.Account):
                    OnPropertyChange(nameof(this.Account));
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
    }
}
