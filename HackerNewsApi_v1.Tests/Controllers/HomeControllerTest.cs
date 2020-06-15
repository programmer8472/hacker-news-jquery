using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackerNewsApi_v1;
using HackerNewsApi_v1.Controllers;

namespace HackerNewsApi_v1.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Hacker News Top Stories", result.ViewBag.Title);
        }
    }
}
