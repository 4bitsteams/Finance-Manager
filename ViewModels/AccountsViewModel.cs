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
        private Account _selectedAccount;

        public Account SelectedAccount
        {
            get { return this._selectedAccount; }
            set
            {
               Set(ref _selectedAccount, value);
            }
        }

        public ObservableCollection<Account> Accounts { get; set; }
        public float TotalAmount
        { 
            get
            {
                float totalAmount = 0;
                if (Accounts != null && Accounts.Count > 0)
                {
                    foreach (Account account in Accounts)
                    {
                        if (account.Count)
                        {
                            totalAmount += account.Amount;
                        }
                    }
                }

                return totalAmount;
            } 
        }

        public AccountsViewModel()
        {
            this.Accounts = new ObservableCollection<Account>()
            {
                new Account() { Name = "Cash"},
                new Account() { Name ="Credit"}
            };
            this.SelectedAccount = Accounts[0];
        }
    }
}
