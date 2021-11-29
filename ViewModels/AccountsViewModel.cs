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
               this.EditAccountView.Editable = value;
               this._selectedAccount = value;
                OnPropertyChange(nameof(Accounts));
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

        public RelayCommand AddAccountCommand { get; set; }

        public RelayCommand DeleteAccountCommand { get; set; }

        public AccountsViewModel()
        {
            this.Accounts = new ObservableCollection<AccountViewModel>()
            {
                new AccountViewModel(new Account() { Name = "Cash"}),
                new AccountViewModel(new Account() { Name ="Credit"})
            };
            EditAccountView = new AccountEditViewModel();
            EditAccountCommand = new RelayCommand(e => EditAccountView.IsVisible = !EditAccountView.IsVisible);
            AddAccountCommand = new RelayCommand(e =>
            {
               AccountViewModel account = new AccountViewModel(new Account()
               {
                   Name = "New account",
                   Amount = 0
               });
               account.PropertyChanged += AccountsChanged;
               Accounts.Add(account);
            });
            DeleteAccountCommand = new RelayCommand(e =>
            {
                this.Accounts.Remove(this.SelectedAccount);
                this.SelectedAccount = null;
                this.OnPropertyChange(nameof(this.TotalAmount));
            });
            this.SelectedAccount = Accounts[1];
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
            if (e.PropertyName == nameof(AccountViewModel.Amount)
                || e.PropertyName == nameof(AccountViewModel.ToCount)) 
            {
                OnPropertyChange(nameof(this.TotalAmount));
            }
        }
    }
}
