using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Models;
using FinanceManager.Core;

namespace FinanceManager.ViewModels
{
    public class AccountEditViewModel : ViewModelBase
    {
        private AccountViewModel? _editable;

        private bool _isVisible;

        public bool IsVisible
        {
            get { return this._isVisible; }
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

        public AccountViewModel? Editable 
        { 
            get 
            { 
                return this._editable; 
            }
            set 
            {
                this._editable = value;
                if (this._editable != null)
                {
                    OnPropertyChange(nameof(this.Name));
                    OnPropertyChange(nameof(this.Balance));
                    OnPropertyChange(nameof(this.ToCount));
                }
                else
                {
                    this.IsVisible = false;
                }
            }
        }

        public string Name
        {
            get 
            {
                if (this._editable != null)
                {
                    return this._editable.Name;
                }
                else
                {
                    IsVisible = false;
                    return string.Empty;
                }
            }
            set
            {
                if (this._editable != null)
                {
                    this._editable.Name = value;
                }
                else
                {
                    this.IsVisible = false;
                }
                OnPropertyChange();
            }
        }

        public double Balance
        {
            get 
            {
                if (this._editable != null)
                {
                    return _editable.Balance;
                }
                else
                {
                    this.IsVisible = false;
                    return 0d;
                }
            }
            set
            {
                if (this._editable != null)
                {
                    this._editable.Balance = value;
                }
                else
                {
                    this.IsVisible = false;
                }
                OnPropertyChange();
            }
        }

        public bool ToCount
        {
            get
            {
                if (this._editable != null)
                {
                    return this._editable.ToCount;
                }
                else
                {
                    this.IsVisible = false;
                    return false;
                }
            }
            set
            {
                if (this._editable != null)
                {
                    this._editable.ToCount = value;
                }
                else
                {
                    this.IsVisible = false;
                }
                OnPropertyChange();
            }
        }

        public AccountEditViewModel()
        {

        }
    }
}
