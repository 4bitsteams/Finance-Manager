using FinanceManager.Core;
using FinanceManager.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace FinanceManager.ViewModels
{
    public class CategoryEditViewModel : ViewModelBase
    {
        private CategoryViewModel? _editableCategory;
        private ChangeType _currentChangeType;
        private bool _isAvailible;
        private bool _isEditable;

        public ObservableCollection<ChangeType> ChangeTypes { get; set; }

        public CategoryViewModel? EditableCategory
        {
            get
            {
                return this._editableCategory;
            }

            set
            {
                if (this._editableCategory != value)
                {
                    this._editableCategory = value;
                    if (this._editableCategory != null)
                    {
                        this.IsAvailible = true;
                    }
                    else
                    {
                        this.IsAvailible = false;
                    }
                    OnPropertyChange();
                    OnPropertyChange(nameof(this.Color));
                    OnPropertyChange(nameof(this.Name));
                    OnPropertyChange(nameof(this.ChangeType));
                }
            }
        }

        public bool IsAvailible
        {
            get
            {
                return this._isAvailible;
            }

            set
            {
                this._isAvailible = value;
                if (value == false)
                {
                    this.IsEditable = false;
                }
                OnPropertyChange();
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

        public ChangeType CurrentType
        {
            get
            {
                return this._currentChangeType;
            }

            set
            {
                this._currentChangeType = value;
                OnPropertyChange(nameof(this.ChangeType));
            }
        }

        public string Name
        {
            get
            {
                if (this._editableCategory != null)
                {
                    return this._editableCategory.Name;
                }

                return string.Empty;
            }

            set
            {
                if (this._editableCategory != null)
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
                if (this._editableCategory != null)
                {
                    return this.EditableCategory.Color;
                }

                return Colors.Transparent;
            }

            set
            {
                if (this._editableCategory != null)
                {
                    this.EditableCategory.Color = value;
                    OnPropertyChange();
                    OnPropertyChange(nameof(this.ColorBrush));
                }
            }
        }

        public SolidColorBrush ColorBrush
        {
            get
            {
                return new SolidColorBrush(this.Color);
            }
        }

        public ChangeType ChangeType
        {
            get
            {
                if (this._editableCategory != null)
                {
                    return this.EditableCategory.Type;
                }

                return this.CurrentType;
            }

            set
            {
                if (this._editableCategory != null)
                {
                    if (this.EditableCategory.Type != value)
                    {
                        this.EditableCategory.Type = value;
                        this.EditableCategory = null;
                        OnPropertyChange();
                    }
                }
            }
        }

        public RelayCommand AddCategoryCommand { get; set; }

        public RelayCommand EditCategoryCommand { get; set; }

        public RelayCommand RemoveCategoryCommand { get; set; }

        public CategoryEditViewModel()
        {
            this.AddCategoryCommand = new RelayCommand(o =>
            {
                this.EditableCategory = new CategoryViewModel(new Category("New Category", "none", this.CurrentType));
                Service.AddCategory(this.EditableCategory.Category);
                this.IsEditable = true;
            });
            this.RemoveCategoryCommand = new RelayCommand(o =>
            {
                Service.RemoveCategory(this.EditableCategory.Category);
                this.EditableCategory = null;
            });
            this.EditCategoryCommand = new RelayCommand(o =>
            {
                this.IsEditable = !this.IsEditable;
            });
            this.ChangeTypes = new ObservableCollection<ChangeType>();
            this.ChangeTypes.Add(ChangeType.Expenses);
            this.ChangeTypes.Add(ChangeType.Income);
        }
    }
}
