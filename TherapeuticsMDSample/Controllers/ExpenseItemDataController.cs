using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TherapeuticsMDSample.Data.DTO;
using TherapeuticsMDSample.Helper;

namespace TherapeuticsMDSample.Controllers
{
    public class ExpenseItemDataController : Controller
    {
        public JsonResult GetExpenseItem(int ExpenseReportId, int? ExpenseItemId)
        {
            var dto = new ExpenseItemDTO();

            if (dto.Load(ExpenseReportId, ExpenseItemId))
            {
                return new JsonDotNetResultHelper { Data = new { viewModel = dto, success = true } };
            }

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }

        public JsonResult SaveExpenseItem(ExpenseItemDTO viewModel)
        {
            if (!ModelState.IsValid) return new JsonDotNetResultHelper { Data = new { success = false } };
            if (viewModel.Save()) return new JsonDotNetResultHelper { Data = new { success = true } };

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }

        public JsonResult DeleteExpenseItem(int ExpenseItemId)
        {
            ExpenseItemDTO dto = new ExpenseItemDTO();

            if (dto.DeleteExpenseItem(ExpenseItemId))
            {
                return new JsonDotNetResultHelper { Data = new { viewModel = dto, success = true } };
            }

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }

        public JsonResult AssignReceiptToItem(int ExpenseItemId, int ReceiptId)
        {
            ExpenseItemDTO dto = new ExpenseItemDTO();

            if (dto.AssignReceiptToItem(ExpenseItemId, ReceiptId))
            {
                return new JsonDotNetResultHelper { Data = new { viewModel = dto, success = true } };
            }

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }

        public JsonResult RemoveReceipt(int ExpenseItemId)
        {
            ExpenseItemDTO dto = new ExpenseItemDTO();

            if (dto.RemoveReceipt(ExpenseItemId))
            {
                return new JsonDotNetResultHelper { Data = new { viewModel = dto, success = true } };
            }

            return new JsonDotNetResultHelper { Data = new { success = false } };
        }
    }
}