using FinanceManager.Core;
using System;

namespace FinanceManager.ViewModels
{
    public class AccountEditViewModel : ViewModelBase
    {
        private AccountViewModel? _editable;
        private DateTime _startDate;
        private DateTime _endDate;

        private bool _isVisible;

        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                if (this._editable != null)
                {
                    this._isVisible = value;
                }
                else
                {
                    this._isVisible = false;
                }
                OnPropertyChange();
            }
        }

        public DateTime CurrentDate
        {
            get { return DateTime.Now.Date; }
        }

        public DateTime StartDate
        {
            get { return this._startDate;}
            set { this._startDate = value; OnPropertyChange(); }
        }

        public DateTime EndDate
        {
            get { return this._endDate; }
            set { this._endDate = value; OnPropertyChange(); }
        }

        public AccountViewModel? Editable
        {
            get
            {
                return this._editable;
            }
            set
            {
                this._editable = value;
                if (this.VisibilityCheck())
                {
                    OnPropertyChange(nameof(this.Name));
                    OnPropertyChange(nameof(this.Balance));
                    OnPropertyChange(nameof(this.ToCount));
                    OnPropertyChange(nameof(this.SaveAbility));
                    this._editable.PropertyChanged -= PropertyChanged;
                    this._editable.PropertyChanged += PropertyChanged;
                }
            }
        }

        public bool SaveAbility
        {
            get
            {
                if (VisibilityCheck())
                {
                    return this.Editable.Account.InfluenceMoneyChanges.Count > 0;
                }
                
                return false;
            }
        }

        public string Name
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return this._editable.Name;
                }

                return string.Empty;
            }
            set
            {
                if (this.VisibilityCheck())
                {
                    if(value == string.Empty)
                    {
                        value = "The Account without name";
                    }
                    this._editable.Name = value;
                }
                OnPropertyChange();
            }
        }

        public double Balance
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return _editable.Balance;
                }

                return 0d;
            }
            set
            {
                if (this.VisibilityCheck())
                {
                    this._editable.Balance = value;
                }
            }
        }

        public bool ToCount
        {
            get
            {
                if (this.VisibilityCheck())
                {
                    return this._editable.ToCount;
                }

                return false;
            }
            set
            {
                if (this.VisibilityCheck())
                {
                    this._editable.ToCount = value;
                }
            }
        }

        public AccountEditViewModel()
        {
            DatesLoading();
        }

        public AccountEditViewModel(AccountViewModel account) : this()
        {
            this.Editable = account;
        }

        private void PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AccountViewModel.Balance):
                    OnPropertyChange(nameof(this.Balance));
                    break;
                case nameof(AccountViewModel.ToCount):
                    OnPropertyChange(nameof(this.ToCount));
                    break;
                case nameof(AccountViewModel.Name):
                    OnPropertyChange(nameof(this.Name));
                    break;
                default:
                    break;
            }
        }

        private bool VisibilityCheck()
        {
            if (this._editable != null)
            {
                return true;
            }

            this.IsVisible = false;
            return false;
        }

        private void DatesLoading()
        {
            this.StartDate = DateTime.Now.Date.AddDays(1 - DateTime.Now.Date.Day);
            this.EndDate = DateTime.Now.Date;
        }
    }
}
