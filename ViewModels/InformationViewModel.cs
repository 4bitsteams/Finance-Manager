using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Core;
using FinanceManager.Models;

namespace FinanceManager.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        private ApplicationContext _dB;
        private ObservableCollection<CategoryViewModel> _categories;
        private bool _incomesRadioButtonIsChecked;
        private bool _expencesRadioButtonIsChecked;

        private ObservableCollection<CategoryViewModel> Expences { get; set; }

        private ObservableCollection<CategoryViewModel> Incomes { get; set; }

        public ObservableCollection<CategoryViewModel> ShownMoneyChanges
        {
            get
            {
                return this._categories;
            }
            set
            {
                this._categories = value;
                OnPropertyChange();
            }
        }

        public RelayCommand ExpencesCommand { get; set; }

        public RelayCommand IncomesCommand { get; set; }

        public bool IncomesRadioButtonIsChecked
        {
            get
            {
                return this._incomesRadioButtonIsChecked;
            }
            set
            {
                this._incomesRadioButtonIsChecked = value;
                OnPropertyChange();
            }
        }

        public bool ExpenxesRadioButtonIsChecked
        {
            get
            {
                return this._expencesRadioButtonIsChecked;
            }
            set
            {
                this._expencesRadioButtonIsChecked = value;
                OnPropertyChange();
            }
        }

        public InformationViewModel()
        {
            this._dB = new ApplicationContext();
            Expences = new ObservableCollection<CategoryViewModel>();
            Incomes = new ObservableCollection<CategoryViewModel>();
            Category cat = new Category();
            cat.Name = "Category";
            cat.MoneyChanges.Add(new MoneyChange(15, 48 , new Account("Account", 25, true), DateOnly.FromDateTime(DateTime.Now), ChangeType.Expenses, "Some description" ));
            Expences.Add(new CategoryViewModel(cat));
            ExpencesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Expences;
            });
            IncomesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Incomes;
            });
            this.ShownMoneyChanges = Expences;
            ExpenxesRadioButtonIsChecked = true;
        }

    }
}
