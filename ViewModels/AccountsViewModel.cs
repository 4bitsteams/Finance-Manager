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
        private AccountViewModel? _selectedAccount;

        public AccountViewModel SelectedAccount
        {
            get 
            { 
                return this._selectedAccount; 
            }
            set
            {
                this.EditAccountView.Editable = value;
                this._selectedAccount = value;
                OnPropertyChange(nameof(Accounts));
                if (this._selectedAccount != null)
                {
                    this.IsEditAble = true;
                }
                else
                {
                    this.IsEditAble = false;
                }
                OnPropertyChange();
            }
        }

        private bool _isEditAble;

        public bool IsEditAble
        {
            get
            {
                return this._isEditAble;
            }

            set
            {
                this._isEditAble = value;
                OnPropertyChange();
            }
        }

        public ObservableCollection<AccountViewModel> Accounts { get; set; }
        public double TotalBalance
        { 
            get
            {
                double totalBalance = 0;
                if (Accounts != null && Accounts.Count > 0)
                {
                    foreach (AccountViewModel account in Accounts)
                    {
                        if (account.ToCount)
                        {
                            totalBalance += account.Balance;
                        }
                    }
                }

                return totalBalance;
            }
        }

        public AccountEditViewModel EditAccountView { get; set; }

        public RelayCommand EditAccountCommand { get; set; }

        public RelayCommand AddAccountCommand { get; set; }

        public RelayCommand DeleteAccountCommand { get; set; }

        public AccountsViewModel()
        {
            this.AccountsLoad();
            EditAccountView = new AccountEditViewModel();
            EditAccountCommand = new RelayCommand(e => EditAccountView.IsVisible = !EditAccountView.IsVisible);
            AddAccountCommand = new RelayCommand(e =>
            {
                AccountViewModel account = new AccountViewModel(new Account()
                {
                    Name = "New account",
                    Balance = 0
                });
                account.PropertyChanged += AccountsChanged;
                this.Accounts.Add(account);
                this.SelectedAccount = this.Accounts[^1];
                Service.AddAccount(this.SelectedAccount.Account);
                this.EditAccountView.IsVisible = true;
            });
            DeleteAccountCommand = new RelayCommand(e =>
            {
                Service.RemoveAccount(this.SelectedAccount.Account);
                this.Accounts.Remove(this.SelectedAccount);
                this.OnPropertyChange(nameof(this.TotalBalance));

            });

        }

        public void AccountsLoad()
        {
            if(this.Accounts != null)
            {
                this.Accounts.Clear();
            }
            else
            {
                this.Accounts = new ObservableCollection<AccountViewModel>();
            }
            foreach (var account in Service.Accounts)
            {
                AccountViewModel acc = new AccountViewModel(account);
                acc.PropertyChanged += AccountsChanged;
                Accounts.Add(new AccountViewModel(account));
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

        private void AccountsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AccountViewModel.Balance)
                || e.PropertyName == nameof(AccountViewModel.ToCount)) 
            {
                OnPropertyChange(nameof(this.TotalBalance));
            }
        }
    }
}
