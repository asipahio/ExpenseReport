using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TherapeuticsMDSample.Data.DataModels;
using TherapeuticsMDSample.Data.Repositories;

namespace TherapeuticsMDSample.Data.DTO
{
    public class ReceiptDTO
    {
        #region Private Properties
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        #endregion
        #region Public Properties
        public int Id { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        public bool IsImage { get; set; }
        #endregion

        #region Load Data
        public void FillFrom(ReceiptDataModel receipt)
        {
            this.Id = receipt.Id;
            this.Path = receipt.Path;
            this.IsImage = ImageExtensions.Contains(System.IO.Path.GetExtension(receipt.Path).ToUpper());

        }

        public bool UploadReceipt(HttpPostedFileBase file, string UploadPath, int ExpenseReportId, out string error)
        {
            ReceiptRepository _repo = new ReceiptRepository();
            FileUploadDTO upload = _repo.AddReceipt(file, UploadPath, ExpenseReportId);

            if (upload.data != null)
            {
                this.FillFrom(upload.data);
            }

            error = upload.error;

            return upload.success;
        }

        public bool DeleteReceipt(int receiptId, string uploadPath)
        {
            ReceiptRepository _repo = new ReceiptRepository();
            return _repo.DeleteReceipt(receiptId, uploadPath);
        }
        #endregion
    }
}
