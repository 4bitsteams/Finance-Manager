using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Core
{
    public static class Service
    {
        private static ApplicationContext _db;

        static Service()
        {
            _db = new ApplicationContext();
        }

        public static List<Account> Accounts
        {
            get 
            { 
                List<Account> accounts = new List<Account>();
                foreach(var account in _db.Accounts)
                {
                    accounts.Add(account);
                }
                return accounts;
            }
        }

        public static List<Category> Categories
        {
            get
            {
                List<Category> categories = new List<Category>();
                foreach (var category in _db.Categories)
                {
                    categories.Add(category);
                }
                return categories;
            }
        }

        public static List<MoneyChange> MoneyChanges
        {
            get
            {
                List<MoneyChange> changes = new List<MoneyChange>();
                foreach (var change in _db.MoneyChanges)
                {
                    changes.Add(change);
                }
                return changes;
            }
        }

        public static void AddAccount(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
        }

        public static void RemoveAccount(Account account)
        {
            _db.Accounts.Remove(account);
            _db.SaveChanges();
        }

        public static void AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public static void RemoveCategory(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }

        public static void AddMoneyChange(MoneyChange change)
        {
            _db.MoneyChanges.Add(change);
            _db.SaveChanges();
        }

        public static void RemoveMoneyChange(MoneyChange change)
        {
            _db.MoneyChanges.Remove(change);
            _db.SaveChanges();
        }

        public static void SaveChanges()
        {
            _db.SaveChanges();
        }

        public static void ClearCategories()
        {
            while(Categories.Count > 0)
            {
                _db.Categories.Remove(Categories[0]);
                _db.SaveChanges();
            }
        }

    }
}
