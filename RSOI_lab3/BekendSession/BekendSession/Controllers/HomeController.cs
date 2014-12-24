using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace BekendSession.Controllers
{
    public class HomeController : Controller
    {
        public static List<string> PasswordS = new List<string>();
        
        public ActionResult Index()
        {
            var rc = new RestClient("http://localhost:7000/api/requests?password=SessionBoss");
            var rq = new RestRequest(Method.POST);

            var response_all = rc.Execute(rq);

            return null;
        }
    }
}
