using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.IO;
using System.Web;
using Moq;
using TherapeuticsMDSample.Controllers;
using System.Web.Mvc;
using TherapeuticsMDSample.Models.Interfaces;
using TherapeuticsMDSample.Tests.Models;

namespace TherapeuticsMDSample.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        private FileStream _stream;

        [TestMethod]
        public void UploadReceipts()
        {

            FileStream fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["TestReceipt"]), FileMode.Open);
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();

            uploadedFile.Setup(f => f.ContentLength).Returns(10);
            uploadedFile.Setup(f => f.FileName).Returns("test.png");
            uploadedFile.Setup(f => f.InputStream).Returns(fileStream);

            var controller = new ReceiptDataController(new TestPathProvider(), new TestConfigurationProvider());
            var result = controller.UploadReceipts(uploadedFile.Object, 1) as JsonResult;

            Assert.IsNotNull(result);

        }
    }
}
