using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TherapeuticsMDSample.Controllers;
using System.Web.Mvc;
using TherapeuticsMDSample.Data.DTO;

namespace TherapeuticsMDSample.Tests.Controllers
{
    [TestClass]
    public class ExpenseItemDataControllerTest
    {
        [TestMethod]
        public void GetExpenseItemTest()
        {
            var controller = new ExpenseItemDataController();
            var result = controller.GetExpenseItem(1, 1) as JsonResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SaveExpenseItem()
        {
            ExpenseItemDTO dto = new ExpenseItemDTO
            {
                ExpenseAmount = 10.30,
                ExpenseCategoryId = 1,
                ExpenseDate = DateTime.Now,
                ExpenseDescription = "Test Description",
                ExpenseReportId = 1
            };
            bool saved = dto.Save();

            var controller = new ExpenseItemDataController();
            var result = controller.SaveExpenseItem(dto) as JsonResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteExpenseItem()
        {
            var controller = new ExpenseItemDataController();
            var result = controller.DeleteExpenseItem(1) as JsonResult;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AssignReceiptToItem()
        {
            var controller = new ExpenseItemDataController();
            var result = controller.AssignReceiptToItem(1, 5) as JsonResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RemoveReceipt()
        {
            var controller = new ExpenseItemDataController();
            var result = controller.RemoveReceipt(1) as JsonResult;

            Assert.IsNotNull(result);
        }
    }
}
