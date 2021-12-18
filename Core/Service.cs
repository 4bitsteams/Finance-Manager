using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManager.Core
{
    public static class Service
    {
        private static ApplicationContext _db;

        static Service()
        {
            _db = new ApplicationContext();
            var categories = _db.Categories.Include(c => c.MoneyChanges).ToList();
            var changes = _db.MoneyChanges.Include(m => m.Category).Include(m => m.Account).ToList();
        }

        public static List<Account> Accounts
        {
            get
            {
                return _db.Accounts.ToList();
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
            change.Category.AddMoneyChange(change);
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
            while (Categories.Count > 0)
            {
                _db.Categories.Remove(Categories[0]);
                _db.SaveChanges();
            }
        }

        public static void ClearAccounts()
        {
            while (Accounts.Count > 0)
            {
                _db.Accounts.Remove(Accounts[0]);
                _db.SaveChanges();
            }
        }

        public static void ClearMoneyChanges()
        {
            while (MoneyChanges.Count > 0)
            {
                _db.MoneyChanges.Remove(MoneyChanges[0]);
                _db.SaveChanges();
            }
        }

    }
}
