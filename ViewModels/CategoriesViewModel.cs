using FinanceManager.Core;
using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.ViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        private CategoryViewModel _selectedCategory;
        private ObservableCollection<CategoryViewModel> _shownCategories;
        private ObservableCollection<CategoryViewModel> _incomes;
        private ObservableCollection<CategoryViewModel> _expances;
        private bool _incomesRadioButtonIsChecked;
        private bool _expencesRadioButtonIsChecked;

        public CategoryViewModel SelectedCategory
        {
            get 
            { 
                return _selectedCategory;
            }

            set 
            { 
                this._selectedCategory = value;
                this.CategoryEditViewModel.EditableCategory = this._selectedCategory;
                OnPropertyChange();
            }
        }

        public ObservableCollection<CategoryViewModel> Expences
        {
            get
            {
                return this._expances;
            }

            set
            {
                this._expances = value;
                OnPropertyChange();
            }
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

        public ObservableCollection<CategoryViewModel> ShownCategories 
        {
            get
            {
                return this._shownCategories;
            }

            set
            {
                this._shownCategories = value;
                OnPropertyChange();
            }
        }

        public RelayCommand ExpencesCommand { get; set; }

        public RelayCommand IncomesCommand { get; set; }

        public CategoryEditViewModel CategoryEditViewModel { get; set; }

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

        public CategoriesViewModel()
        {
            this.Incomes = new ObservableCollection<CategoryViewModel>();
            this.CategoryEditViewModel = new CategoryEditViewModel();
            this.CategoryEditViewModel.PropertyChanged += CategoryAdded;
            this.CategoriesLoad();
            this.ExpencesCommand = new RelayCommand(o =>
            {
                this.ShownCategories = this.Expences;
                this.ExpenxesRadioButtonIsChecked = true;
                this.CategoryEditViewModel.CurrentType = ChangeType.Expenses;
            });
            this.IncomesCommand = new RelayCommand(o =>
            {
                this.ShownCategories = this.Incomes;
                this.IncomesRadioButtonIsChecked = true;
                this.CategoryEditViewModel.CurrentType = ChangeType.Income;
            });
            this.ExpencesCommand.Execute(this);
        }

        private void CategoryAdded(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CategoryEditViewModel.EditableCategory):
                    if (this.CategoryEditViewModel.EditableCategory == null)
                    {
                        this.CategoriesLoad();
                    }
                    if (this.SelectedCategory != this.CategoryEditViewModel.EditableCategory && this.CategoryEditViewModel.EditableCategory != null)
                    {
                        this.ShownCategories.Add(this.CategoryEditViewModel.EditableCategory);
                    }
                    break;
                default:
                    break;
            }
        }

        public void CategoriesLoad()
        {
            if (this.Expences == null)
            {
                this.Expences = new ObservableCollection<CategoryViewModel>();
            }
            else
            {
                this.Expences.Clear();
            }

            if (this.Incomes == null)
            {
                this.Incomes = new ObservableCollection<CategoryViewModel>();
            }
            else
            {
                this.Incomes.Clear();
            }
            foreach (var category in Service.Categories)
            {
                if (category.Type == ChangeType.Income)
                {
                    this.Incomes.Add(new CategoryViewModel(category));
                }
                else if (category.Type == ChangeType.Expenses)
                {
                    this.Expences.Add(new CategoryViewModel(category));
                }
            }
            OnPropertyChange(nameof(this.ShownCategories));
        }
    }
}
