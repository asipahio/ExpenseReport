using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Data.DataModels;

namespace TherapeuticsMDSample.Data
{
    public class DataContext : DbContext
    {
        public DbSet<ExpenseReportDataModel> ExpenseReports { get; set; }

        public DbSet<ExpenseItemDataModel> ExpenseItems { get; set; }

        public DbSet<ReceiptDataModel> Receipts { get; set; }

        public DbSet<ExpenseCategoryDataModel> ExpenseCategories { get; set; }
    }
}
