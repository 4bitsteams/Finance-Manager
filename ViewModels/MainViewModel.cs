using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Core;

namespace FinanceManager.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AccountsViewCommand { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChange();
            }
        }

        public HomeViewModel HomeView { get; set; }
        public AccountsViewModel AccountsView { get; set; }

        public MainViewModel()
        {
            HomeView = new HomeViewModel();
            AccountsView = new AccountsViewModel();
            HomeViewCommand = new RelayCommand(o =>
            CurrentView = HomeView
            );
            AccountsViewCommand = new RelayCommand(o =>

            CurrentView = AccountsView
            );
            CurrentView = HomeView;
        }
    }
}
