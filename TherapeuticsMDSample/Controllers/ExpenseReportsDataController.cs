using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TherapeuticsMDSample.Data.DTO;
using TherapeuticsMDSample.Helper;

namespace TherapeuticsMDSample.Controllers
{
    public class ExpenseReportsDataController : Controller
    {
        public JsonResult GetExpenseReportsList(int startIndex, int endIndex, string sortBy, string sortOrder, string keyword)
        {
            var dto = new ExpenseReportsDTO();

            if (dto.Load(startIndex, endIndex, sortBy, sortOrder, keyword))
            {
                return new JsonDotNetResultHelper { Data = new { viewModel = dto, success = true } };
            }

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }

        public JsonResult GetExpenseReport(int ExpenseReportId)
        {
            var dto = new ExpenseReportDTO();

            if (dto.Load(ExpenseReportId))
            {
                return new JsonDotNetResultHelper { Data = new { viewModel = dto, success = true } };
            }

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }

        public JsonResult SaveExpenseReport(ExpenseReportDTO viewModel)
        {
            if (!ModelState.IsValid) return new JsonDotNetResultHelper { Data = new { success = false } };
            if (viewModel.Save()) return new JsonDotNetResultHelper { Data = new { success = true } };

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }

    }
}