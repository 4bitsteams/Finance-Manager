using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinanceManager.Core
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<MoneyChange> MoneyChanges { get; set; }

        public string DbPath { get; private set; }

        public ApplicationContext() : base()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}financialManagerDB.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

    }
}
