﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Models;

namespace FinanceManager.Core
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public string DbPath { get; private set; }

        public ApplicationContext() : base()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}blogging.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                    .ToTable("Accounts").HasKey(p => p.Id);
        }

    }
}