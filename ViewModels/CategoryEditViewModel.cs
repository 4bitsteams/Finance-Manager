using FinanceManager.Core;
using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FinanceManager.ViewModels
{
    public class CategoryEditViewModel : ViewModelBase
    {
        private CategoryViewModel? _editableCategory;
        private bool _isEditable;

        public CategoryViewModel EditableCategory
        {
            get
            {
                return this._editableCategory;
            }

            set
            {
                this._editableCategory = value;
                if (EditabilityCheck())
                { }
                OnPropertyChange();
                OnPropertyChange(nameof(this.Color));
                OnPropertyChange(nameof(this.Name));
            }
        }

        public bool IsEditable
        {
            get
            {
                return this._isEditable;
            }
            set
            {
                this._isEditable = value;
                OnPropertyChange();
            }
        }

        public bool IsVisible { get; set; }

        public string Name
        {
            get 
            {
                if (EditabilityCheck())
                {
                    return this._editableCategory.Name;
                }

                return string.Empty;
            }

            set
            {
                if (EditabilityCheck())
                {
                    this._editableCategory.Name = value;
                    OnPropertyChange();
                }
            }
        }

        public Color Color
        {
            get
            {
                if (EditabilityCheck())
                {
                    return this.EditableCategory.Color;
                }

                return Colors.Transparent;
            }

            set
            {
                if (EditabilityCheck())
                {
                    this.EditableCategory.Color = value;
                    OnPropertyChange();
                }
            }
        }

        public RelayCommand AddCategoryCommand { get; set; }
        //public RelayCommand SaveCategoryCommand { get; set; }

        public CategoryEditViewModel()
        {
            this.AddCategoryCommand = new RelayCommand(o =>
            {
                this.EditableCategory = new CategoryViewModel(new Category("New Category", "none", Service.MoneyChanges));
                Service.AddCategory(this.EditableCategory.Category);
            });
            this.Color = Colors.Blue;

        }

        private bool EditabilityCheck()
        {
            if (this._editableCategory != null)
            {
                this.IsEditable = true;
                return true;
            }

            this.IsEditable = false;
            return false;
        }
    }
}
