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
        private MoneyChangeViewModel? _selectedmoneyChange;
        private ObservableCollection<CategoryViewModel> _categories;
        private ObservableCollection<CategoryViewModel> _incomes;
        private ObservableCollection<CategoryViewModel> _expances;
        private bool _incomesRadioButtonIsChecked;
        private bool _expencesRadioButtonIsChecked;
        private DateTime _startDate;
        private DateTime _endDate;

        public ObservableCollection<CategoryViewModel> Expences 
        { 
            get
            {
                return this._expances;
            }

            set
            {
                this._expances = value;
            }
        }

        public DateTime From
        {
            get { return this._startDate; }
            set 
            { 
                this._startDate = value;
                OnPropertyChange();
                CategoriesLoad();
            }
        }

        public DateTime To
        {
            get { return this._endDate; }
            set
            {
                this._endDate = value;
                OnPropertyChange();
                CategoriesLoad();
            }
        }

        public DateTime CurrentDate
        {
            get => DateTime.Now.Date;
        }

        public ObservableCollection<CategoryViewModel> Incomes 
        { 
            get
            {
                return this._incomes;
            }
            set
            {
                this._incomes = value;
                OnPropertyChange();
            }
        }

        public MoneyChangeEditViewModel MoneyChangeEditViewModel { get; set; }

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

        public MoneyChangeViewModel? SelectedMoneyChange
        {
            get 
            { 
                return this._selectedmoneyChange; 
            }

            set 
            { 
                this._selectedmoneyChange = value;
                if (this.MoneyChangeEditViewModel.MoneyChange != this._selectedmoneyChange)
                {
                    this.MoneyChangeEditViewModel.MoneyChange = this._selectedmoneyChange;
                }
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
            this.To = DateTime.Now.Date;
            this.From = DateTime.Now.Date;
            this.From = this.From.AddDays(-7);
            this.MoneyChangeEditViewModel = new MoneyChangeEditViewModel();
            this.MoneyChangeEditViewModel.PropertyChanged += MoneyChangeEditViewModelChandged;
            this.CategoriesLoad();
            this.ExpencesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Expences;
                this.MoneyChangeEditViewModel.CurrentType = ChangeType.Expenses;
            });
            this.IncomesCommand = new RelayCommand(o =>
            {
                this.ShownMoneyChanges = Incomes;
                this.MoneyChangeEditViewModel.CurrentType = ChangeType.Income;
            });
            this.ShownMoneyChanges = this.Expences;
            this.ExpenxesRadioButtonIsChecked = true;
        }

        private void MoneyChangeEditViewModelChandged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MoneyChangeEditViewModel.Category):
                    if (this.MoneyChangeEditViewModel.MoneyChange != this.SelectedMoneyChange)
                    {
                        this._selectedmoneyChange = null;
                        OnPropertyChange(nameof(this.SelectedMoneyChange));
                    }
                    MoneyChangeViewModel buffer = this.SelectedMoneyChange;
                    this.CategoriesLoad();
                    this.SelectedMoneyChange = buffer;
                    break;
                case nameof(MoneyChangeEditViewModel.MoneyChange):
                    if (this.SelectedMoneyChange != MoneyChangeEditViewModel.MoneyChange)
                    {
                        this.SelectedMoneyChange = MoneyChangeEditViewModel.MoneyChange;
                        CategoriesLoad();
                    }
                    break;
                default:
                    break;
            }
        }

        private void CategoryViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is CategoryViewModel category)
            {
                switch (e.PropertyName)
                {
                    case nameof(CategoryViewModel.SelectedMoneyChange):
                        if (category.SelectedMoneyChange != null)
                        {
                            this.SelectedMoneyChange = category.SelectedMoneyChange;
                            foreach (CategoryViewModel anotherCategory in this.ShownMoneyChanges)
                            {
                                if (anotherCategory != category && anotherCategory.SelectedMoneyChange != null)
                                {
                                    anotherCategory.SelectedMoneyChange = null;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void CategoriesLoad()
        {
            if (this.Expences != null)
            {
                this.Expences.Clear();
            }
            else
            {
                this.Expences = new ObservableCollection<CategoryViewModel>();
            }

            if (this.Incomes != null)
            {
                this.Incomes.Clear();
            }
            else
            {
                this.Incomes = new ObservableCollection<CategoryViewModel>();
            }
            foreach (var category in Service.Categories)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel(category);
                categoryViewModel.PropertyChanged += CategoryViewModelPropertyChanged;
                foreach(MoneyChangeViewModel changeViewModel in categoryViewModel.MoneyChanges)
                {
                    if(changeViewModel.Date.Date > this.To || changeViewModel.Date.Date < this.From)
                    {
                        changeViewModel.ShouldBeCounted = false;
                    }
                    else
                    {
                        changeViewModel.ShouldBeCounted = true;
                    }
                }
                if (category.Type == ChangeType.Income)
                {
                    this.Incomes.Add(categoryViewModel);
                }
                else if (category.Type == ChangeType.Expenses)
                {
                    this.Expences.Add(categoryViewModel);
                }
            }

            this.NormilizeCategories();
            OnPropertyChange(nameof(this.ShownMoneyChanges));
        }

        public void NormilizeCategories()
        {
            for(int i = 0; i < this.Expences.Count; i++)
            {
                if (this.Expences[i].MoneyChanges.Count == 0)
                {
                    this.Expences.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < this.Incomes.Count; i++)
            {
                if (this.Incomes[i].MoneyChanges.Count == 0)
                {
                    this.Incomes.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
