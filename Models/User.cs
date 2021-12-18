using System.Collections.Generic;

namespace FinanceManager.Models
{
    public class User
    {
        private string _id;
        private string _name;
        private string _password;
        private string _avatarIconSource;
        private List<Account> _accounts;

        public string Id
        {
            get { return this._id; }
        }
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public string Password
        {
            get { return this._password; }
        }
        public List<Account> Accounts
        {
            get { return this._accounts; }
        }

        public User() { }
    }
}
