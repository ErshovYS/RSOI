using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;

namespace RSOI_lab2.Controllers
{
    public class HomeController : Controller
    {
        public static SqlConnection connection;
        
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            LoaD();

            return View();
        }

        public void LoaD()
        {
            /*string dbLocation = System.IO.Path.GetFullPath("Magazin.mdf");
            connection = new SqlConnection
                            (
                            @"data source=.\SQLEXPRESS;" +
                            "User Instance=true;Integrated Security=SSPI;AttachDBFilename="
                            + @dbLocation
                            );
            */
            string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
            connection = new SqlConnection(ConStr);
            connection.Open();
        }
    }
}
