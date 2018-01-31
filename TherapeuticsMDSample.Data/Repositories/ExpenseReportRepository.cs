using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Data.DataModels;
using TherapeuticsMDSample.Data.DTO;

namespace TherapeuticsMDSample.Data.Repositories
{
    public class ExpenseReportRepository
    {
        public ExpenseReportDataModel GetExpenseReportById(int Id)
        {
            ExpenseReportDataModel dm = new ExpenseReportDataModel();
            using (DataContext dc = new DataContext())
            {
                dm = dc.ExpenseReports.Include("ExpenseItems.ExpenseCategory").Include("Receipts").Where(t => t.Id == Id).FirstOrDefault();
            }
            return dm;
        }

        public List<ExpenseReportDataModel> GetExpenseReports(int startIndex, int endIndex, string sortBy, string sortOrder, string keyword, out int TotalReports)
        {
            List<ExpenseReportDataModel> dm = new List<ExpenseReportDataModel>();
            using (DataContext dc = new DataContext())
            {
                Func<ExpenseReportDataModel, Object> orderByFunc = GetSortBy(sortBy);

                var Reports = dc.ExpenseReports.Include("ExpenseItems").Include("Receipts").OrderByDescending(t => t.Id);
                TotalReports = Reports.Count();
                if (!string.IsNullOrEmpty(keyword))
                {
                    Reports = Reports.Where(t => t.Name.Contains(keyword)).OrderByDescending(t => t.Id);
                }
                if (sortOrder.Equals("Asc"))
                {
                    dm = Reports.OrderBy(orderByFunc).Skip(startIndex).Take(endIndex - startIndex).ToList();
                }
                else
                {
                    dm = Reports.OrderByDescending(orderByFunc).Skip(startIndex).Take(endIndex - startIndex).ToList();
                }
            }
            return dm;
        }

        internal List<ExpenseCategoryDataModel> GetExpenseCategoryList()
        {
            List<ExpenseCategoryDataModel> dm = new List<ExpenseCategoryDataModel>();
            using (DataContext dc = new DataContext())
            {
                dm = dc.ExpenseCategories.ToList();
            }

            return dm;
        }

        public ExpenseReportDataModel GetExpenseReportToSave(int Id)
        {
            ExpenseReportDataModel dm = new ExpenseReportDataModel();
            using (DataContext dc = new DataContext())
            {
                dm = dc.ExpenseReports.Where(t => t.Id == Id).FirstOrDefault();
                if (dm == null)
                {
                    dc.ExpenseReports.Add(dm);
                }
            }
            
            return dm;
        }

        public bool DeleteExpenseItem(int expenseItemId)
        {
            using (DataContext dc = new DataContext())
            {
                ExpenseItemDataModel item = dc.ExpenseItems.Where(t => t.Id == expenseItemId).FirstOrDefault();
                if(item != null)
                {
                    item.RowDeleted = true;
                    dc.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public bool SaveExpenseReport(ExpenseReportDTO report)
        {
            ExpenseReportDataModel dm = new ExpenseReportDataModel();
            using (DataContext dc = new DataContext())
            {
                dm = dc.ExpenseReports.Where(t => t.Id == report.ReportId).FirstOrDefault();
                if(dm == null)
                {
                    dm = new ExpenseReportDataModel();
                    dc.ExpenseReports.Add(dm);
                }
                dm.Name = report.ReportName;
                dc.SaveChanges();
            }
            return true;
        }

        public bool RemoveReceipt(int expenseItemId)
        {
            using (DataContext dc = new DataContext())
            {
                ExpenseItemDataModel expenseItem = dc.ExpenseItems.Where(t => t.Id == expenseItemId).FirstOrDefault();
                if (expenseItem == null) { return false; }
                expenseItem.ReceiptId = null;
                dc.SaveChanges();
                return true;
            }
        }

        public bool AssignReceiptToItem(int expenseItemId, int receiptId)
        {
            using (DataContext dc = new DataContext())
            {
                ExpenseItemDataModel expenseItem = dc.ExpenseItems.Where(t => t.Id == expenseItemId).FirstOrDefault();
                if(expenseItem == null) { return false; }
                expenseItem.ReceiptId = receiptId;
                dc.SaveChanges();
                return true;
            }
        }

        public ExpenseItemDataModel GetExpenseItem(int expenseReportId, int? expenseItemId)
        {
            if (!expenseItemId.HasValue)
            {
                return new ExpenseItemDataModel { ExpenseReportId = expenseReportId };
            }
            ExpenseItemDataModel dm = new ExpenseItemDataModel();
            using (DataContext dc = new DataContext())
            {
                dm = dc.ExpenseItems.Include("Receipt").Include("ExpenseCategory").Where(t => t.ExpenseReportId == expenseReportId && t.Id == expenseItemId.Value).FirstOrDefault();
            }
            return dm;
        }

        public bool SaveExpenseItem(ExpenseItemDTO item)
        {
            ExpenseItemDataModel dm = new ExpenseItemDataModel();
            using (DataContext dc = new DataContext())
            {
                dm = dc.ExpenseItems.Where(t => t.Id == item.ExpenseItemId).FirstOrDefault();
                if (dm == null)
                {
                    dm = new ExpenseItemDataModel
                    {
                        ExpenseReportId = item.ExpenseReportId,
                        RowDeleted = false
                    };
                    dc.ExpenseItems.Add(dm);
                }
                dm.ExpenseAmount = item.ExpenseAmount;
                dm.ExpenseCategoryId = item.ExpenseCategoryId;
                dm.ExpenseDate = item.ExpenseDate;
                dm.ExpenseDescription = item.ExpenseDescription;
                dc.SaveChanges();
            }
            return true;
        }

        #region Private Methods
        private Func<ExpenseReportDataModel, object> GetSortBy(string sortBy)
        {
            Func<ExpenseReportDataModel, Object> orderByFunc = item => item.Id;
            switch (sortBy)
            {
                case "NoOfItems":
                    orderByFunc = item => item.ExpenseItems.Count;
                    break;
                case "Name":
                    orderByFunc = item => item.Name;
                    break;
                case "Total":
                    orderByFunc = item => item.ExpenseItems.Sum(t => t.ExpenseAmount);
                    break;
            }
            return orderByFunc;
        }
        #endregion
    }
}
