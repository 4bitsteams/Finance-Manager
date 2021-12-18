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
        public RelayCommand InformationViewCommand { get; set; }
        public RelayCommand CategoriesViewCommand { get; set; }

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
        public InformationViewModel InformationView { get; set; }
        public CategoriesViewModel CategoriesView { get; set; }

        public MainViewModel()
        {
            this.HomeView = new HomeViewModel();
            this.AccountsView = new AccountsViewModel();
            this.InformationView = new InformationViewModel();
            this.CategoriesView = new CategoriesViewModel();
            this.HomeViewCommand = new RelayCommand(o =>
            {
                this.CurrentView = this.HomeView;
            }
            );
            this.AccountsViewCommand = new RelayCommand(o =>
            {
                this.AccountsView.AccountsUpdate();
                this.CurrentView = this.AccountsView;
            }
            );
            this.InformationViewCommand = new RelayCommand(o =>
            {
                this.InformationView.SelectedMoneyChange = null;
                this.InformationView.CategoriesLoad();
                this.CurrentView = this.InformationView;
            });
            this.CategoriesViewCommand = new RelayCommand(o =>
            {
                this.CurrentView = this.CategoriesView;
            });
            CurrentView = HomeView;
        }
    }
}
