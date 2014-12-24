using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using FrontEnd.Models;
using RestSharp;

namespace FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        public static string Messeg = "";

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ViewResult Authorize()
        {
            if (!RequestsController.BekendSession)
            {
                Messeg = "Включите для начала нужный бэкенд";
            }
            else
            {
                Messeg = "";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Users guestR)
        {
            try
            {
                if (RequestsController.BekendSession)
                {
                    var rc = new RestClient("http://localhost:7004/api/session?login=" + guestR.LogiN + "&password=" + guestR.Password);
                    var rq = new RestRequest(Method.POST);

                    var response_all = rc.Execute(rq);

                    if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        System.Web.HttpCookie Kuka = new System.Web.HttpCookie("SessionKuka");

                        Kuka.Values.Add("session_password", response_all.Content.Trim(new char[] {'/', ' ', '\"'}));
                        Kuka.Values.Add("login", guestR.LogiN);
                        Kuka.Expires = DateTime.Now.AddDays(1);
                        Kuka.Secure = true;
                        Kuka.Shareable = true;
                        HomeController.httpContext.Response.SetCookie(Kuka);
                        HomeController.SessionLog(guestR.LogiN, "Вход");
                        Messeg = "";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Messeg = response_all.StatusCode.ToString();
                    }
                }
                else
                {
                    Messeg = "Включите для начала нужный бэкенд";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return View();
        }
    }
}
