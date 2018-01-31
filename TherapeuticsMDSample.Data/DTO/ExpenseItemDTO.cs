using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Data.DataModels;
using TherapeuticsMDSample.Data.Repositories;

namespace TherapeuticsMDSample.Data.DTO
{
    public class ExpenseItemDTO
    {
        #region Public Properties
        public int ExpenseItemId { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }

        [StringLength(50)]
        public ExpenseCategoryDTO ExpenseCategory { get; set; }

        public int ExpenseCategoryId { get; set; }

        [StringLength(250)]
        public string ExpenseDescription { get; set; }

        public double ExpenseAmount { get; set; }

        public int ExpenseReportId { get; set; }

        public ReceiptDTO Receipt { get; set; }

        public List<ExpenseCategoryDTO> ExpenseCategories { get; set; }
        #endregion

        #region Load Data
        public bool Load(int expenseReportId, int? expenseItemId)
        {
            ExpenseReportRepository _repository = new ExpenseReportRepository();

            var item = _repository.GetExpenseItem(expenseReportId, expenseItemId);

            if (item != null)
            {
                return this.FillFrom(item);
            }
            return false;

        }

        public bool DeleteExpenseItem(int expenseItemId)
        {
            ExpenseReportRepository _repository = new ExpenseReportRepository();

            return _repository.DeleteExpenseItem(expenseItemId);
        }

        public bool FillFrom(ExpenseItemDataModel t)
        {
            this.ExpenseItemId = t.Id;
            this.ExpenseDate = t.ExpenseDate;
            this.ExpenseDescription = t.ExpenseDescription;
            this.ExpenseAmount = t.ExpenseAmount;
            this.ExpenseReportId = t.ExpenseReportId;

            ExpenseCategoryDTO cat = new ExpenseCategoryDTO();
            this.ExpenseCategories = cat.GetExpenseCategoryList();
            this.ExpenseCategoryId = t.ExpenseCategoryId;

            if (t.ExpenseCategory != null)
            {
                ExpenseCategoryDTO c = new ExpenseCategoryDTO();
                c.FillFrom(t.ExpenseCategory);
                this.ExpenseCategory = c;
            }

            if (t.Receipt != null)
            {
                ReceiptDTO r = new ReceiptDTO();
                r.FillFrom(t.Receipt);
                this.Receipt = r;
            }

            return true;
        }

        public bool AssignReceiptToItem(int expenseItemId, int receiptId)
        {
            ExpenseReportRepository _repo = new ExpenseReportRepository();
            return _repo.AssignReceiptToItem(expenseItemId, receiptId);
        }

        public bool RemoveReceipt(int expenseItemId)
        {
            ExpenseReportRepository _repository = new ExpenseReportRepository();

            return _repository.RemoveReceipt(expenseItemId);
        }

        public bool Save()
        {
            ExpenseReportRepository _repository = new ExpenseReportRepository();

            return _repository.SaveExpenseItem(this);
        }


        #endregion
    }
}
