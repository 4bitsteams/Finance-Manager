using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;

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

        public static string? AccountToExel(Account account, string fileName)
        {
            int rowCount = account.InfluenceMoneyChanges.Count;
            if (rowCount > 0)
            {
                XSSFWorkbook workBook = new XSSFWorkbook();
                XSSFSheet sheet = (XSSFSheet)workBook.CreateSheet("Лист 1");
                IRow header = sheet.CreateRow(0);
                ICell categoryNameCell =  header.CreateCell(0);
                ICell dateCell = header.CreateCell(1);
                ICell valueCell = header.CreateCell(2);
                ICell descriptionCell = header.CreateCell(3);
                categoryNameCell.SetCellValue("Category name");
                dateCell.SetCellValue("Money change date");
                valueCell.SetCellValue("Transaction amount");
                descriptionCell.SetCellValue("Description");
                int i = 1;
                foreach(MoneyChange transaction in account.InfluenceMoneyChanges)
                {
                    IRow currentRow = sheet.CreateRow(i);
                    ICell currentCategoryNameCell = currentRow.CreateCell(0);
                    currentCategoryNameCell.SetCellValue(transaction.Category.Name);
                    ICell currentDateCell = currentRow.CreateCell(1);
                    currentDateCell.SetCellValue(transaction.Date.ToString("f"));
                    ICell currentValueCell = currentRow.CreateCell(2);
                    if (transaction.Type == ChangeType.Income)
                    {
                        currentValueCell.SetCellValue(transaction.Impact);
                    }
                    else
                    {
                        currentValueCell.SetCellValue(transaction.Impact - 2*transaction.Impact);
                    }
                    ICell currentDescriptionCell = currentRow.CreateCell(3);
                    if(transaction.Description == string.Empty)
                    {
                        currentDescriptionCell.SetCellValue("No Description");
                    }
                    else
                    {
                        currentDescriptionCell.SetCellValue(transaction.Description);
                    }

                    i++;
                }
                IRow finaleRow = sheet.CreateRow(i);
                ICell finalResultText = finaleRow.CreateCell(1);
                finalResultText.SetCellValue("Total result of transactions");
                ICell totalAmount = finaleRow.CreateCell(2);
                totalAmount.SetCellType(CellType.Formula);
                totalAmount.SetCellFormula($"SUM(C{2}:C{i})");

                sheet.AutoSizeColumn(0);
                sheet.AutoSizeColumn(1);
                sheet.AutoSizeColumn(2);
                sheet.AutoSizeColumn(3);

                if (!File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                try
                {
                    using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        workBook.Write(fs);
                    }
                }
                catch(System.IO.IOException ex)
                {
                    return ex.Message;
                }
            }

            return null;
        }
    }
}
