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
        public static string MessegInsert = "";
        public static string MessegDelete = "";
        public static string MessegInsert2 = "";
        public static string MessegDelete2 = "";
        public static string MessegCreators = "";
        public static string MessegCreator = "";

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult Notebooks()
        {
            ViewBag.Title = "Ноутбуки";

            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login="+HttpContext.Request.Cookies["SessionKuka"].Values["login"]+"&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rc = new RestClient("http://localhost:7001/api/Notebooks");
                var rq = new RestRequest();

                var response_all = rc.Execute(rq);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "GET http://localhost:7001/api/Notebooks");


                string answer;
                if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    answer = response_all.Content;
                    RequestsController.Spisok = SimpleJson.DeserializeObject<List<NotebookShort>>(answer);
                    MessegShow = "";
                }
                else
                    MessegShow = response_all.StatusCode.ToString();
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
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                    MessegDelete = "";
                }
                else
                {
                    MessegDelete = "Вы не админ";
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
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                        var rc = new RestClient("http://localhost:7001/api/Notebooks?id=" + ForDelete.ID);
                        var rq = new RestRequest(Method.DELETE);

                        var response_all = rc.Execute(rq);
                        SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "DELETE http://localhost:7001/api/Notebooks");

                        if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            MessegDelete = response_all.Content;
                        }
                        else
                            MessegDelete = response_all.StatusCode.ToString();
                }
                else
                {
                    MessegDelete = "Вы не админ";
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
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login="+HttpContext.Request.Cookies["SessionKuka"].Values["login"]+"&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                    MessegInsert = "";
                }
                else
                {
                    MessegInsert = "Вы не админ";
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
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
               rc2 = new RestClient("http://localhost:7004/api/session?login="+HttpContext.Request.Cookies["SessionKuka"].Values["login"]+"&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
               SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login="+HttpContext.Request.Cookies["SessionKuka"].Values["login"]+"&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                        NInfController.buff = ForInsert;
                        var rc = new RestClient("http://localhost:7001/api/Notebooks");
                        var rq = new RestRequest(Method.PUT);

                        var response_all = rc.Execute(rq);
                        SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "PUT http://localhost:7001/api/Notebooks");

                        if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            MessegInsert = response_all.Content;
                        }
                        else
                            MessegInsert = response_all.StatusCode.ToString();
                }
                else
                {
                    MessegInsert = "Вы не админ";
                }
            }
            else
            {
                MessegInsert = "Вы не авторизованы";
            }
            return View();
        }

        public ActionResult Creators()
        {
            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);
            
            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                    var rc = new RestClient("http://localhost:7003/api/Creators");
                    var rq = new RestRequest();

                    var response_all = rc.Execute(rq);
                    SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "GET http://localhost:7003/api/Creators");
                        

                    string answer;
                    if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        answer = response_all.Content;
                        RequestsController.SpisokP = SimpleJson.DeserializeObject<List<Proizv>>(answer);
                        MessegCreators = "";
                    }
                    else
                        MessegCreators = response_all.StatusCode.ToString();
            }
            else
            {
                RequestsController.SpisokP = null;
                MessegCreators = "Вы не авторизованы";
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreatorForm()
        {
            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                    MessegCreator = "";
            }
            else
            {
                MessegCreator = "Вы не авторизованы";
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreatorForm(Params Par)
        {
            
            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("CreatorInfo", Par);
            }
            else
            {
                MessegCreator = "Вы не авторизованы";
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreatorInfo(Params Par)
        {
            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                    RequestsController.SpisokC = new List<PolnProizv>();
                    var rc = new RestClient("http://localhost:7003/api/Creators?param=" + Par.Param + "&value=" + Par.Value);
                    var rq = new RestRequest(Method.GET);

                    var response_all = rc.Execute(rq);
                    SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "GET http://localhost:7003/api/Creators?param=" + Par.Param + "&value=" + Par.Value);


                    if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string answer = response_all.Content;
                        RequestsController.SpisokP = SimpleJson.DeserializeObject<List<Proizv>>(answer);

                        string Ind = RequestsController.SpisokP[0].ProID.ToString();
                        for (int i = 1; i < RequestsController.SpisokP.Count; i++ )
                        {
                            Ind += "," + RequestsController.SpisokP[i].ProID.ToString();
                        }

                        rc = new RestClient("http://localhost:7001/api/Notebooks?ind=" + Ind);
                        rq = new RestRequest(Method.GET);

                        response_all = rc.Execute(rq);
                        SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "GET http://localhost:7001/api/Notebooks?ind=" + Ind);

                        if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            answer = response_all.Content;
                            List<Notebook> SpisokN = SimpleJson.DeserializeObject<List<Notebook>>(answer);

                            PolnProizv buff = new PolnProizv();
                            for (int i = 0; i < RequestsController.SpisokP.Count; i++)
                            {
                                buff = new PolnProizv();
                                buff.Infa = RequestsController.SpisokP[i];
                                buff.Spisok = new List<NotebookShort>();
                                for (int j = 0; j < SpisokN.Count; j++)
                                {
                                    if (SpisokN[j].ProizvID == buff.Infa.ProID)
                                    {
                                        var NB = new NotebookShort();
                                        NB.ID = SpisokN[j].ID;
                                        NB.Name = SpisokN[j].Name;
                                        buff.Spisok.Add(NB);
                                    }
                                }
                                RequestsController.SpisokC.Add(buff);
                            }
                        }

                        MessegCreator = "";
                    }
                    else
                        MessegCreator = response_all.StatusCode.ToString();
            }
            else
            {
                MessegCreator = "Вы не авторизованы";
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

        [HttpGet]
        public ActionResult DeleteCreator()
        {
            ViewBag.Title = "Удаление";

            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                        MessegDelete2 = "";
                }
                else
                {
                    MessegDelete2 = "Вы не админ";
                }
            }
            else
            {
                MessegDelete2 = "Вы не авторизованы";
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCreator(Proizv ForDelete)
        {
            ViewBag.Title = "Удаление";

            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                        var rc = new RestClient("http://localhost:7003/api/Creators?id=" + ForDelete.ProID);
                        var rq = new RestRequest(Method.DELETE);

                        var response_all = rc.Execute(rq);
                        SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "DELETE http://localhost:7003/api/Creators?id=" + ForDelete.ProID);

                        if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            MessegDelete2 = response_all.Content;
                        }
                        else
                            MessegDelete2 = response_all.StatusCode.ToString();
                }
                else
                {
                    MessegDelete2 = "Вы не админ";
                }
            }
            else
            {
                MessegDelete2 = "Вы не авторизованы";
            }
            return View();
        }

        [HttpGet]
        public ActionResult InsertCreator()
        {
            ViewBag.Title = "Добавление";

            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                        MessegInsert2 = "";
                }
                else
                {
                    MessegInsert2 = "Вы не админ";
                }
            }
            else
            {
                MessegInsert2 = "Вы не авторизованы";
            }
            return View();
        }

        [HttpPost]
        public ActionResult InsertCreator(Proizv ForInsert)
        {
            ViewBag.Title = "Добавление";

            RestClient rc2;
            if (HttpContext.Request.Cookies["SessionKuka"] != null)
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
                SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "POST http://localhost:7004/api/session?login=" + HttpContext.Request.Cookies["SessionKuka"].Values["login"] + "&pass=" + HttpContext.Request.Cookies["SessionKuka"].Values["session_password"]);
            }
            else
            {
                rc2 = new RestClient("http://localhost:7004/api/session?login=&pass=");
                SessionLog("Unnamed", "POST http://localhost:7004/api/session?login=&pass=");
            }
            var rq2 = new RestRequest(Method.POST);
            var response_all2 = rc2.Execute(rq2);

            if (response_all2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response_all2.Content.IndexOf("True") > 0)
                {
                        СInfController.buff = ForInsert;
                        string S = "'" + ForInsert.ProID.ToString() + "'";
                        S += ", '" + ForInsert.Name + "'";
                        S += ", '" + ForInsert.Country + "'";
                        S += ", '" + ForInsert.Year + "'";

                        var rc = new RestClient("http://localhost:7003/api/Creators?str=" + S);
                        var rq = new RestRequest(Method.PUT);

                        var response_all = rc.Execute(rq);
                        SessionLog(HttpContext.Request.Cookies["SessionKuka"].Values["login"], "PUT http://localhost:7003/api/Creators?str=" + S.Replace("'", ""));
                       
                        if (response_all.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            MessegInsert2 = response_all.Content;
                        }
                        else
                            MessegInsert2 = response_all.StatusCode.ToString();
                }
                else
                {
                    MessegInsert2 = "Вы не админ";
                }
            }
            else
            {
                MessegInsert2 = "Вы не авторизованы";
            }
            return View();
        }
    }
}
