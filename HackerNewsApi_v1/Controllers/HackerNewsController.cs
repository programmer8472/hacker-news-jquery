using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json.Linq;
using HackerNewsApi_v1.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HackerNewsApi_v1.Controllers
{
    public class HackerNewsController : Controller
    {
        // GET: HackerNews
        public string Index()
        {
            try
            {
                var client = new WebClient();
                var articeIDsResponse = client.DownloadString("https://hacker-news.firebaseio.com/v0/newstories.json");
                return articeIDsResponse.Trim(new char[] { '[', ']' });
            }
            catch
            {
                return "Server error";
            }

        }

        public string GetArticleDetail(string id)
        {
            try
            {
                var client = new WebClient();
                var article = client.DownloadString("https://hacker-news.firebaseio.com/v0/item/" + id + ".json");
                if (article != null && !article.Contains("null"))
                {
                    JObject jsonObj = JObject.Parse(article);
                    return jsonObj.ToString(Newtonsoft.Json.Formatting.None);
                }
                return string.Empty;
            }
            catch
            {
                return "Server error";
            }
        }

    }
}