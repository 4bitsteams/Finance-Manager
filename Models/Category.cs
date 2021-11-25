using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Category
    {
        private string _id;
        private string _name;
        private string _imageSource;

        public string Id
        {
            get { return this._id; }
        }
        public string Name 
        {
            set { this._name = value; }
            get { return this._name; } 
        }
        public string ImageSource
        {
            set { this._imageSource = value; }
            get { return this._imageSource; }
        }

        public Category() { }
    }
}
