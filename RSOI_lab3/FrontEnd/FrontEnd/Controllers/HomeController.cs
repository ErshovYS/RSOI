using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using FrontEnd.Models;
using System.Data.SqlClient;
using System.Data;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {

        public static string MessegShow = "";
        public static string MessegDelete = "";
        public static string MessegInsert = "";
        public static HttpContextBase httpContext;

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            if (httpContext == null)
                httpContext = HttpContext;
            else
            {
                for (int i = 0; i < httpContext.Response.Cookies.Count; i++)
                    Response.SetCookie(httpContext.Response.Cookies[i]);
            }
            return View();
        }

        public ActionResult Notebooks()
        {
            ViewBag.Title = "Ноутбуки";

            RestClient rc2;
            if (httpContext.Request.Cookies.Count > 3)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=" + httpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.Content == "true")
            {
                if (RequestsController.BekendShow)
                {
                    var rc = new RestClient("http://localhost:7001/api/show");
                    var rq = new RestRequest();

                    var response_all = rc.Execute(rq);

                    string answer;
                    if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        answer = response_all.Content;
                        RequestsController.Spisok = SimpleJson.DeserializeObject<List<NotebookShort>>(answer);
                        SessionLog(httpContext.Request.Cookies["SessionKuka"].Values["login"], "Просмотр списка ноутбуков");
                        MessegShow = "";
                    }
                    else
                        MessegShow = response_all.StatusCode.ToString();
                }
                else
                {
                    RequestsController.Spisok = null;
                    MessegShow = "Включите для начала нужный бэкенд";
                }
            }
            else
            {
                RequestsController.Spisok = null;
                MessegShow = "Вы не авторизованы";
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteForm()
        {
            ViewBag.Title = "Удаление";

            RestClient rc2;
            if (httpContext.Request.Cookies.Count > 3)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=" + httpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=");
            } 
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.Content == "true")
            {
                if (RequestsController.BekendDelete)
                {
                    MessegDelete = "";
                }
                else
                {
                    MessegDelete = "Включите для начала нужный бэкенд";
                }
            }
            else
            {
                MessegDelete = "Вы не авторизованы";
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteForm(Notebook ForDelete)
        {
            ViewBag.Title = "Удаление";

            RestClient rc2;
            if (httpContext.Request.Cookies.Count > 3)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=" + httpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.Content == "true")
            {
                if (RequestsController.BekendDelete)
                {
                    var rc = new RestClient("http://localhost:7002/api/delete?id=" + ForDelete.ID);
                    var rq = new RestRequest(Method.POST);

                    var response_all = rc.Execute(rq);

                    if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SessionLog(httpContext.Request.Cookies["SessionKuka"].Values["login"], "Удаление ноутбука №" + ForDelete.ID);
                        MessegDelete = response_all.Content;
                    }
                    else
                        MessegDelete = response_all.StatusCode.ToString();
                }
                else
                {
                    MessegDelete = "Включите для начала нужный бэкенд";
                }
            }
            else
            {
                MessegDelete = "Вы не авторизованы";
            }
            return View();
        }

        [HttpGet]
        public ActionResult InsertForm()
        {
            ViewBag.Title = "Добавление";

            RestClient rc2;
            if (httpContext.Request.Cookies.Count > 3)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=" + httpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.Content == "true")
            {
                if (RequestsController.BekendInsert)
                {
                    MessegInsert = "";
                }
                else
                {
                    MessegInsert = "Включите для начала нужный бэкенд";
                }
            }
            else
            {
                MessegInsert = "Вы не авторизованы";
            }
            return View();
        }

        [HttpPost]
        public ActionResult InsertForm(Notebook ForInsert)
        {
            ViewBag.Title = "Добавление";

            RestClient rc2;
            if (httpContext.Request.Cookies.Count > 3)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=" + httpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.Content == "true")
            {
                if (RequestsController.BekendInsert)
                {
                    NInfController.buff = ForInsert;
                    var rc = new RestClient("http://localhost:7003/api/insert");
                    var rq = new RestRequest(Method.POST);

                    var response_all = rc.Execute(rq);

                    if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SessionLog(httpContext.Request.Cookies["SessionKuka"].Values["login"], "Добавление ноутбука №" + ForInsert.ID);
                        MessegInsert = response_all.Content;
                    }
                    else
                        MessegInsert = response_all.StatusCode.ToString();
                }
                else
                {
                    MessegInsert = "Включите для начала нужный бэкенд";
                }
            }
            else
            {
                MessegInsert = "Вы не авторизованы";
            }
            return View();
        }

        public static void SessionLog(string login, string action)
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "SessionLog";

                string qry = @"select * from " + tbl;
                string ins = @"insert into " + tbl + " values ('" + login + "', '" +
                    action + "', '" + DateTime.Now + "')";

                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                // Создаем команду запроса для текущего подключения	
                da.SelectCommand = new SqlCommand(qry, connection);
                // Создаем и наполняем набор данных
                DataSet ds = new DataSet();
                da.Fill(ds, tbl);
                // Получаем ссылку на таблицу
                DataTable dt = ds.Tables[tbl];

                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);

                // Создаем команду вставки для текущего подключения
                SqlCommand cmd = new SqlCommand(ins, connection);

                da.InsertCommand = cmd;
                da.Update(ds, tbl);
                connection.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
        }
    }
}
