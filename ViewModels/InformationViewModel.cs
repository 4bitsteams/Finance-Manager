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

        public ObservableCollection<MoneyChangeViewModel> Expences { get; set; }

        public ObservableCollection<MoneyChangeViewModel> Incomes { get; set; }

        public InformationViewModel()
        {
            this._dB = new ApplicationContext();
            Expences = new ObservableCollection<MoneyChangeViewModel>();
            Incomes = new ObservableCollection<MoneyChangeViewModel>();
            
        }

    }
}
