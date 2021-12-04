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
        private ObservableCollection<MoneyChangeViewModel> _shownMoneyChanges;
        private bool _incomesRadioButtonIsChecked;
        private bool _expencesRadioButtonIsChecked;

        private ObservableCollection<MoneyChangeViewModel> Expences { get; set; }

        private ObservableCollection<MoneyChangeViewModel> Incomes { get; set; }

        public ObservableCollection<MoneyChangeViewModel> ShownMoneyChanges
        {
            get
            {
                return this._shownMoneyChanges;
            }
            set
            {
                this._shownMoneyChanges = value;
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
            Expences = new ObservableCollection<MoneyChangeViewModel>();
            Incomes = new ObservableCollection<MoneyChangeViewModel>();
            Expences.Add(new MoneyChangeViewModel());
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
