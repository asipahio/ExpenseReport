using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TherapeuticsMDSample.Data.DataModels;
using TherapeuticsMDSample.Data.DTO;

namespace TherapeuticsMDSample.Data.Repositories
{
    public class ReceiptRepository
    {
        public string UploadFile(HttpPostedFileBase file, string UploadPath)
        {
            if (file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid() + extension;
                var path = Path.Combine(UploadPath, fileName);
                if (!Directory.Exists(UploadPath)) { Directory.CreateDirectory(UploadPath); }
                file.SaveAs(path);
                return path;
            }
            return null;
        }

        public FileUploadDTO AddReceipt(HttpPostedFileBase file, string UploadPath, int ExpenseReportId)
        {
            using (DataContext dc = new DataContext())
            {
                ExpenseReportDataModel expenseReport = dc.ExpenseReports.Where(t => t.Id == ExpenseReportId).FirstOrDefault();
                if(expenseReport == null)
                {
                    return new FileUploadDTO(false, "Expense Report doesn't exist");
                }
                ReceiptDataModel dm = new ReceiptDataModel
                {
                    ExpenseReportId = expenseReport.Id
                };

                string uploadPathPerExpenseReport = Path.Combine(UploadPath, ExpenseReportId.ToString());
                uploadPathPerExpenseReport = this.UploadFile(file, uploadPathPerExpenseReport);
                if (string.IsNullOrEmpty(uploadPathPerExpenseReport))
                {
                    return new FileUploadDTO(false, "There was a problem uploading your file. Please try again.");
                }

                dm.Path = uploadPathPerExpenseReport.Replace(UploadPath, string.Empty);
                if (dm.Path.StartsWith("\\")) { dm.Path = dm.Path.Substring(1); }
                dm.Path = dm.Path.Replace("\\", "/");

                dc.Receipts.Add(dm);
                dc.SaveChanges();

                return new FileUploadDTO(true, dm);
            }
        }

        public bool DeleteReceipt(int receiptId, string uploadPath)
        {
            using (DataContext dc = new DataContext())
            {
                var receipt = dc.Receipts.Include("ExpenseItems").Where(t => t.Id == receiptId).FirstOrDefault();
                if(receipt == null) { return false; }
                dc.Receipts.Remove(receipt);

                foreach(var ExpenseItem in receipt.ExpenseItems)
                {
                    ExpenseItem.ReceiptId = null;
                }

                if(File.Exists(Path.Combine(uploadPath, receipt.Path)))
                {
                    File.Delete(Path.Combine(uploadPath, receipt.Path));
                }

                dc.SaveChanges();
            }

            return true;
        }
    }
}
