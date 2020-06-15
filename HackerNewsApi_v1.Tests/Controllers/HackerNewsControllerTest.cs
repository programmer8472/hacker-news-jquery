using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace HackerNewsApi_v1.Tests.Controllers
{
    [TestClass]
    public class HackerNewsControllerTest
    {
        string firstElement = string.Empty;

        [TestMethod]
        public void HackerNewsControllerOperationsTest()
        {
            Index();
            GetArticleDetail();
        }
        
        public void Index()
        {
            // Exsistance check
            var client = new WebClient();
            var articeIDsResponse = client.DownloadString("https://hacker-news.firebaseio.com/v0/newstories.json");
            Assert.IsNotNull(articeIDsResponse);

            // Element is a numeric check
            firstElement = articeIDsResponse.Trim(new char[] { '[', ']' }).Split(',')[0];
            var isNumeric = int.TryParse(firstElement, out int n);
            Assert.AreEqual(isNumeric, true);

        }
        
        public void GetArticleDetail()
        {
            // Article Exsistance check
            var client = new WebClient();
            var article = client.DownloadString("https://hacker-news.firebaseio.com/v0/item/" + firstElement + ".json");
            JObject jsonObj = JObject.Parse(article);
            Assert.IsNotNull(jsonObj);
        }

    }
}
