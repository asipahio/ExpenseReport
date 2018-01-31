using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TherapeuticsMDSample.Controllers;
using System.Web.Mvc;
using TherapeuticsMDSample.Data.DTO;

namespace TherapeuticsMDSample.Tests.Controllers
{
    [TestClass]
    public class ExpenseReportDataControllerTest
    {
        [TestMethod]
        public void GetExpenseReportsList()
        {
            var controller = new ExpenseReportsDataController();
            var result = controller.GetExpenseReportsList(0, 20, "Name", "Desc", "Report") as JsonResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetExpenseReport()
        {
            var controller = new ExpenseReportsDataController();
            var result = controller.GetExpenseReport(1) as JsonResult;

            Assert.IsNotNull(result);
        }

        public void SaveExpenseReport()
        {
            ExpenseReportDTO dto = new ExpenseReportDTO
            {
                ReportName = "Test Report",
            };
            bool saved = dto.Save();

            Assert.IsTrue(saved);
        }
    }
}
