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

        public static List<Account> Categories
        {
            get
            {
                List<Account> accounts = new List<Account>();
                foreach (var account in _db.Accounts)
                {
                    accounts.Add(account);
                }
                return accounts;
            }
        }

        public static List<Account> MoneyChanges
        {
            get
            {
                List<Account> accounts = new List<Account>();
                foreach (var account in _db.Accounts)
                {
                    accounts.Add(account);
                }
                return accounts;
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

        public static void AddCategory(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
        }

        public static void RemoveCategory(Account account)
        {
            _db.Accounts.Remove(account);
            _db.SaveChanges();
        }

        public static void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
