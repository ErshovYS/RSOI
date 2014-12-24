using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RSOI_lab2.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace RSOI_lab2.Controllers
{
    public class NotebookListController : ApiController
    {
        public static List<Notebook> SpisokN = new List<Notebook>();
        public static List<NotebookShort> SpisokS = new List<NotebookShort>();
        string tbl = "Tovari";
        LevClass L = new LevClass();

        // GET: api/NotebookList
        public List<NotebookShort> Get()
        {         
            if (L.ProverkaCookies())
            {
                try
                {
                    var Otvet = Json(L);
                    string qry = @"select id, name from " + tbl;
                    // Создаем адаптер данных
                    SqlDataAdapter da = new SqlDataAdapter();
                    // Создаем команду запроса для текущего подключения	
                    da.SelectCommand = new SqlCommand(qry, HomeController.connection);
                    // Создаем и наполняем набор данных
                    DataSet ds = new DataSet();
                    da.Fill(ds, tbl);
                    // Получаем ссылку на таблицу
                    DataTable dt = ds.Tables[tbl];

                    NotebookShort buff;
                    SpisokS.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        buff = new NotebookShort();
                        buff.ID = int.Parse(row["id"].ToString());
                        buff.Name = row["name"].ToString();
                        SpisokS.Add(buff);
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
                return SpisokS;
            }
            else
                return null;
        }

        // GET: api
        public IHttpActionResult Get(int count, int num)
        {
            if (L.ProverkaCookies())
            {
                try
                {
                    string qry = @"select id, name from " + tbl + " where id >= " + count * (num - 1) + " AND id <= " + count * num;
                    // Создаем адаптер данных
                    SqlDataAdapter da = new SqlDataAdapter();
                    // Создаем команду запроса для текущего подключения	
                    da.SelectCommand = new SqlCommand(qry, HomeController.connection);
                    // Создаем и наполняем набор данных
                    DataSet ds = new DataSet();
                    da.Fill(ds, tbl);
                    // Получаем ссылку на таблицу
                    DataTable dt = ds.Tables[tbl];

                    NotebookShort buff;
                    SpisokS.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        buff = new NotebookShort();
                        buff.ID = int.Parse(row["id"].ToString());
                        buff.Name = row["name"].ToString();
                        SpisokS.Add(buff);
                    }
                    if (SpisokS.Count <= 0)
                    {
                        return BadRequest();
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
                return Json(SpisokS);
            }
            else
                return null;
        }

        public IHttpActionResult Get(int id)
        {
            if (L.ProverkaCookies())
            {
                try
                {
                    string qry = @"select * from " + tbl + " where id = " + id;
                    // Создаем адаптер данных
                    SqlDataAdapter da = new SqlDataAdapter();
                    // Создаем команду запроса для текущего подключения	
                    da.SelectCommand = new SqlCommand(qry, HomeController.connection);
                    // Создаем и наполняем набор данных
                    DataSet ds = new DataSet();
                    da.Fill(ds, tbl);
                    // Получаем ссылку на таблицу
                    DataTable dt = ds.Tables[tbl];

                    Notebook buff;
                    SpisokN.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        buff = new Notebook();
                        buff.ID = int.Parse(row["id"].ToString());
                        buff.Name = row["name"].ToString();
                        buff.Proc = row["proc"].ToString();
                        buff.Oper = int.Parse(row["operative"].ToString());
                        buff.Memory = row["memory"].ToString();
                        buff.VideoCard = row["video"].ToString();
                        buff.Desksize = int.Parse(row["desksize"].ToString());
                        buff.ProizvID = int.Parse(row["proizvID"].ToString());
                        buff.Price = int.Parse(row["price"].ToString());

                        SpisokN.Add(buff);
                    }
                    if (SpisokN.Count <= 0)
                    {
                        return NotFound();
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
                return Json(SpisokN);
            }
            else
                return null;
        }
    }

    public class ProizvController : ApiController
    {
        static List<Proizv> Proizvoditely = new List<Proizv>();
        string tbl = "Proizv";
        LevClass L = new LevClass();

        void LoadProizv()
        {
            string qry = @"select * from " + tbl;
            // Создаем адаптер данных
            SqlDataAdapter da = new SqlDataAdapter();
            // Создаем команду запроса для текущего подключения	
            da.SelectCommand = new SqlCommand(qry, HomeController.connection);
            // Создаем и наполняем набор данных
            DataSet ds = new DataSet();
            da.Fill(ds, tbl);
            // Получаем ссылку на таблицу
            DataTable dt = ds.Tables[tbl];

            Proizv buff;
            Proizvoditely.Clear();
            foreach (DataRow row in dt.Rows)
            {
                buff = new Proizv();
                buff.ProID = int.Parse(row["id"].ToString());
                buff.Name = row["name"].ToString();
                buff.Country = row["country"].ToString();
                buff.Year = int.Parse(row["year"].ToString());
                Proizvoditely.Add(buff);
            }
        }
        
        // GET api/values
        public List<Proizv> Get()
        {
            if (L.ProverkaCookies())
            {
                try
                {
                    LoadProizv();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
                return Proizvoditely;
            }
            else
                return null;
        }

        public PolnProizv Get(int id)
        {
            if (L.ProverkaCookies())
            {
                try
                {
                    List<NotebookShort> SpisokS = new List<NotebookShort>();
                    string qry = @"select * from Tovari where proizvID = " + id;
                    // Создаем адаптер данных
                    SqlDataAdapter da = new SqlDataAdapter();
                    // Создаем команду запроса для текущего подключения	
                    da.SelectCommand = new SqlCommand(qry, HomeController.connection);
                    // Создаем и наполняем набор данных
                    DataSet ds = new DataSet();
                    da.Fill(ds, tbl);
                    // Получаем ссылку на таблицу
                    DataTable dt = ds.Tables[tbl];

                    NotebookShort buff;
                    foreach (DataRow row in dt.Rows)
                    {
                        buff = new NotebookShort();
                        buff.ID = int.Parse(row["id"].ToString());
                        buff.Name = row["name"].ToString();
                        SpisokS.Add(buff);
                    }

                    LoadProizv();

                    int j = 0;
                    while (Proizvoditely[j].ProID != id)
                    {
                        j++;
                        if (j >= Proizvoditely.Count)
                        {
                            PolnProizv Otvet = new PolnProizv();
                            Otvet.Infa = new Proizv();
                            Otvet.Infa.Name = "Нет производителя с таким номером";
                            Otvet.Spisok = null;
                            return Otvet;
                        }
                    }

                    PolnProizv Res = new PolnProizv();
                    Res.Infa = Proizvoditely[j];
                    Res.Spisok = SpisokS;
                    return Res;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
            }
            return null;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class AuthorizeController : ApiController
    {
        public static List<Users> Useri = new List<Users>();
        public static List<Codes> Codi = new List<Codes>();
        LevClass L = new LevClass();
        static string tblU = "Users";
        static string tblC = "tokens";

        public static void LoadUsers()
        {
            string qry = @"select * from " + tblU;
            // Создаем адаптер данных
            SqlDataAdapter da = new SqlDataAdapter();
            // Создаем команду запроса для текущего подключения	
            da.SelectCommand = new SqlCommand(qry, HomeController.connection);
            // Создаем и наполняем набор данных
            DataSet ds = new DataSet();
            da.Fill(ds, tblU);
            // Получаем ссылку на таблицу
            DataTable dt = ds.Tables[tblU];

            Users buff;
            foreach (DataRow row in dt.Rows)
            {
                buff = new Users();
                buff.LogiN = row["login"].ToString();
                buff.Password = row["password"].ToString();
                buff.Email = row["email"].ToString();
                buff.Phone = row["phone"].ToString();
                buff.client_id = row["client_id"].ToString();
                buff.client_key = row["client_key"].ToString();
                buff.redirect_url = row["redirect_url"].ToString();
                Useri.Add(buff);
            }
        }

        public static void LoadCodes()
        {
            string qry2 = @"select * from " + tblC;
            // Создаем адаптер данных
            SqlDataAdapter da2 = new SqlDataAdapter();
            // Создаем команду запроса для текущего подключения	
            da2.SelectCommand = new SqlCommand(qry2, HomeController.connection);
            // Создаем и наполняем набор данных
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, tblC);
            // Получаем ссылку на таблицу
            DataTable dt2 = ds2.Tables[tblC];

            Codes buff;
            foreach (DataRow row in dt2.Rows)
            {
                buff = new Codes();
                buff.Access_token = row["access_token"].ToString();
                buff.Refresh_token = row["refresh_token"].ToString();
                buff.Expires_in = row["expires_in"].ToString();
                Codi.Add(buff);
            }
        }

        // POST api/values
        public string Post(string clientID, string clientKEY, string code)
        {
            try
            {
                bool Right = false;
                LoadUsers();
                foreach (Users buff in Useri)
                {
                    if (buff.client_id == clientID && buff.client_key == clientKEY)
                    {
                        Right = true;
                        break;
                    }
                }

                if (Right)
                {
                    LoadCodes();
                    Random R = new Random();
                    var buff = Codi[R.Next(Codi.Count)];
                    if (AccountController.RandCodes.IndexOf(code) >= 0)
                    {
                        L.ZapisCookies(buff.Access_token);
                        return ("Access token = " + buff.Access_token);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return "Error";
        }
    }

    public class MeController : ApiController
    {
        LevClass L = new LevClass();

        // GET api/values
        public Users Get()
          {
            try
            {
                var HeadeR = Request.Headers.ToString();
                if (L.Proverka(HeadeR))
                {
                    return L.CookiesUserName();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return null;
        }
    }

    public class StatusController : ApiController
    {
        // GET api/values
        public string Get()
        {
            if (AccountController.Uzver != null)
            {
                return "Авторизован";
            }
            else
            {
                return "Не авторизован";
            }
        }
    }

    public class LevClass : Controller
    {
        public List<Users> Useri = new List<Users>();
        public List<Codes> Codi = new List<Codes>();

        public string tblU = "Users";
        public string tblC = "tokens";

        void LoadCodes()
        {
            string qry2 = @"select * from " + tblC;
            // Создаем адаптер данных
            SqlDataAdapter da2 = new SqlDataAdapter();
            // Создаем команду запроса для текущего подключения	
            da2.SelectCommand = new SqlCommand(qry2, HomeController.connection);
            // Создаем и наполняем набор данных
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, tblC);
            // Получаем ссылку на таблицу
            DataTable dt2 = ds2.Tables[tblC];

            Codes buff;
            foreach (DataRow row in dt2.Rows)
            {
                buff = new Codes();
                buff.Access_token = row["access_token"].ToString();
                buff.Refresh_token = row["refresh_token"].ToString();
                buff.Expires_in = row["expires_in"].ToString();
                Codi.Add(buff);
            }
        }

        public void LoadUsers()
        {
            string qry = @"select * from " + tblU;
            // Создаем адаптер данных
            SqlDataAdapter da = new SqlDataAdapter();
            // Создаем команду запроса для текущего подключения	
            da.SelectCommand = new SqlCommand(qry, HomeController.connection);
            // Создаем и наполняем набор данных
            DataSet ds = new DataSet();
            da.Fill(ds, tblU);
            // Получаем ссылку на таблицу
            DataTable dt = ds.Tables[tblU];

            Users buff;
            foreach (DataRow row in dt.Rows)
            {
                buff = new Users();
                buff.LogiN = row["login"].ToString();
                buff.Password = row["password"].ToString();
                buff.Email = row["email"].ToString();
                buff.Phone = row["phone"].ToString();
                buff.client_id = row["client_id"].ToString();
                buff.client_key = row["client_key"].ToString();
                buff.redirect_url = row["redirect_url"].ToString();
                Useri.Add(buff);
            }
        }

        public void ZapisCookies(string access_token)
        {
            try
            {
                HttpCookie Kuka = new System.Web.HttpCookie("lab2");

                Kuka.Values.Add("access_token", access_token);
                Kuka.Values.Add("login", AccountController.Uzver.LogiN);
                Kuka.Expires = DateTime.Now.AddDays(1);
                Kuka.Secure = true;
                Kuka.Shareable = true;
                HomeController.NasheVse.Response.SetCookie(Kuka);
//                HomeController.NasheVse.Response.Cookies.Add(Kuka);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
        }

        public bool ProverkaCookies()
        {
            LoadCodes();
            HttpCookie Kuka = HomeController.NasheVse.Request.Cookies["lab2"];
            foreach (var buff in Codi)
                if (Kuka.Values["access_token"] == buff.Access_token)
                {
                    return true;
                }
            return false;
        }

        public bool Proverka(string HeadeR)
        {
            LoadCodes();
            var Kuka = HeadeR.Substring(HeadeR.IndexOf("lab2: ") + "lab2: ".Length).Trim('\r', '\n');
            foreach (var buff in Codi)
                if (Kuka == buff.Access_token)
                {
                    return true;
                }
            return false;
        }

        public Users CookiesUserName()
        {
            LoadUsers();
            HttpCookie Kuka;
            Kuka = HomeController.NasheVse.Request.Cookies["lab2"];
            foreach (var buff in Useri)
                if (Kuka.Values["login"] == buff.LogiN)
                {
                    return buff;
                }
            return null;
        }
    }
}
