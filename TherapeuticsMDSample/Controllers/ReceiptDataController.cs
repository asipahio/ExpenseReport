using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TherapeuticsMDSample.Data.DTO;
using TherapeuticsMDSample.Data.Repositories;
using TherapeuticsMDSample.Helper;
using TherapeuticsMDSample.Models;
using TherapeuticsMDSample.Models.Interfaces;

namespace TherapeuticsMDSample.Controllers
{
    public class ReceiptDataController : Controller
    {
        IPathProvider pathProvider;
        IConfigurationProvider configurationProvider;

        public ReceiptDataController(IPathProvider pathProvider, IConfigurationProvider confProvider)
        {
            this.pathProvider = pathProvider;
            this.configurationProvider = confProvider;
        }
        // GET: ReceiptData
        public JsonResult UploadReceipts(HttpPostedFileBase file, int ExpenseReportId)
        {
            string error = null;
            ReceiptDTO dto = new ReceiptDTO();

            string receiptsPath = this.pathProvider.MapPath(this.configurationProvider.ReceiptsLocation());
            bool success = dto.UploadReceipt(file, receiptsPath, ExpenseReportId, out error);
            if (success)
            {
                return new JsonDotNetResultHelper { Data = new { viewModel = dto, success = success, error = error } };
            }

            return new JsonDotNetResultHelper { Data = new { success = false, error = error } };
        }

        public JsonResult DeleteReceipt(int ReceiptId)
        {
            ReceiptDTO dto = new ReceiptDTO();
            string receiptsPath = this.pathProvider.MapPath(this.configurationProvider.ReceiptsLocation());

            return new JsonDotNetResultHelper { Data = new { success = dto.DeleteReceipt(ReceiptId, receiptsPath) } };
        }
    }
}