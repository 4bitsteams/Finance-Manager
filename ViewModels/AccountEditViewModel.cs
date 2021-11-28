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
        private AccountViewModel _editable;

        private bool _isVisible;

        public bool IsVisible
        {
            get { return this._isVisible; }
            set
            {
                this._isVisible = value;
                OnPropertyChange();
            }
        }

        public AccountViewModel Editable 
        { 
            get 
            { 
                return this._editable; 
            }
            set 
            { 
                this._editable = value;
                OnPropertyChange(nameof(this.Name));
                OnPropertyChange(nameof(this.Amount));
            }
        }

        public string Name
        {
            get { return this._editable.Name; }
            set
            {
                this._editable.Name = value;
                OnPropertyChange();
            }
        }

        public float Amount
        {
            get { return _editable.Amount; }
            set
            {
                this._editable.Amount = value;
                OnPropertyChange();
            }
        }

        public AccountEditViewModel()
        {

        }
    }
}
