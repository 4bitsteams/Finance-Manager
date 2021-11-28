using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Core;
using FinanceManager.Models;

namespace FinanceManager.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private AccountViewModel _selectedAccount;

        public AccountViewModel SelectedAccount
        {
            get { return this._selectedAccount; }
            set
            {
               EditAccountView.Editable = value;
                this._selectedAccount = value;
            }
        }

        public ObservableCollection<AccountViewModel> Accounts { get; set; }
        public float TotalAmount
        { 
            get
            {
                float totalAmount = 0;
                if (Accounts != null && Accounts.Count > 0)
                {
                    foreach (AccountViewModel account in Accounts)
                    {
                        if (account.ToCount)
                        {
                            totalAmount += account.Amount;
                        }
                    }
                }

                return totalAmount;
            }
        }

        public AccountEditViewModel EditAccountView { get; set; }

        public RelayCommand EditAccountCommand { get; set; }

        public AccountsViewModel()
        {
            this.Accounts = new ObservableCollection<AccountViewModel>()
            {
                new AccountViewModel(new Account() { Name = "Cash"}),
                new AccountViewModel(new Account() { Name ="Credit"})
            };
            EditAccountView = new AccountEditViewModel();
            EditAccountCommand = new RelayCommand(e => EditAccountView.IsVisible = !EditAccountView.IsVisible);
            this.SelectedAccount = Accounts[0];
            this.SelectedAccount.Amount += 15;
            this.SelectedAccount.ToCount = true;
            Accounts[1].ToCount = true;
            foreach (AccountViewModel account in Accounts)
            {
                account.PropertyChanged += AccountsChanged;
            }
        }

        private void AccountsChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChange(nameof(this.TotalAmount));
        }
    }
}
