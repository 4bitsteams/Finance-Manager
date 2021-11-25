using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Core;

namespace FinanceManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AccountsViewCommand { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                Set(ref _currentView, value);
            }
        }

        public HomeViewModel HomeView { get; set; }
        public AccountsViewModel AccountsView { get; set; }

        public MainViewModel()
        {
            this.HomeView = new HomeViewModel();
            this.AccountsView = new AccountsViewModel();
            this.CurrentView = HomeView;
            this.HomeViewCommand = new RelayCommand(
                o =>
                {
                    this.CurrentView = this.HomeView;
                }
            );
            this.AccountsViewCommand = new RelayCommand(o =>
            {
                this.CurrentView = this.AccountsView;
            }
            );

        }
    }
}
