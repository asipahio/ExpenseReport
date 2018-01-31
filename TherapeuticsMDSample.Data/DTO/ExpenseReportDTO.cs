using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Data.DataModels;
using TherapeuticsMDSample.Data.Repositories;

namespace TherapeuticsMDSample.Data.DTO
{
    public class ExpenseReportDTO
    {
        #region Public Properties
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public List<ExpenseItemDTO> ExpenseItems { get; set; }
        public List<ReceiptDTO> Receipts { get; set; }
        #endregion

        #region Load Data
        public bool Load(int ExpenseReportId)
        {
            ExpenseReportRepository _repository = new ExpenseReportRepository();

            var report = _repository.GetExpenseReportById(ExpenseReportId);

            if(report != null)
            {
                return this.FillFrom(report);
            }
            return false;
        }

        public bool Save()
        {
            ExpenseReportRepository _repository = new ExpenseReportRepository();

            return _repository.SaveExpenseReport(this);

        }

        public bool FillFrom(ExpenseReportDataModel report)
        {
            this.ReportId = report.Id;
            this.ReportName = report.Name;

            this.ExpenseItems = new List<ExpenseItemDTO>();
            report.ExpenseItems.Where(t => !t.RowDeleted).ToList().ForEach(t =>
            {
                ExpenseItemDTO item = new ExpenseItemDTO();
                item.FillFrom(t);
                this.ExpenseItems.Add(item);
            });

            this.Receipts = new List<ReceiptDTO>();
            report.Receipts.ForEach(t =>
            {
                ReceiptDTO receipt = new ReceiptDTO();
                receipt.FillFrom(t);
                this.Receipts.Add(receipt);
            });

            return true;
        }
        #endregion

    }
}
