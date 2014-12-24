using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RSOI_lab2.Models;
using System.Data;
using System.Data.SqlClient;

namespace RSOI_lab2.Controllers
{
    public class NotebookListController : ApiController
    {
        public static List<Notebook> SpisokN = new List<Notebook>();
        public static List<NotebookShort> SpisokS = new List<NotebookShort>();
        string tbl = "Tovari";

        // GET: api/NotebookList
        public List<NotebookShort> Get()
        {
            if (AccountController.UserName != "")
            {
                try
                {
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
        public List<NotebookShort> Get(int count, int num)
        {
            if (AccountController.UserName != "")
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

        public List<Notebook> Get(int id)
        {
            if (AccountController.UserName != "")
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
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
                return SpisokN;
            }
            else
                return null;
        }
    }

    public class ProizvController : ApiController
    {
        static List<Proizv> Proizvoditely = new List<Proizv>();
        string tbl = "Proizv";

        // GET api/values
        public List<Proizv> Get()
        {
            if (AccountController.UserName != "")
            {
                try
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
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
                return Proizvoditely;
            }
            else
                return null;
        }

        public List<NotebookShort> Get(int id)
        {
            if (AccountController.UserName != "")
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
                    return SpisokS;
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

        static string tblU = "Users";
        static string tblC = "Codes";

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
                buff.Code = row["code"].ToString();
                buff.Access_token = row["access_token"].ToString();
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
                    foreach (Codes buff in Codi)
                    {
                        if (buff.Code == code)
                        {
                            return ("Access token = " + buff.Access_token);
                        }
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
        public List<Codes> Codi = new List<Codes>();

        string tblC = "Codes";

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
                buff.Code = row["code"].ToString();
                buff.Access_token = row["access_token"].ToString();
                Codi.Add(buff);
            }
        }

        // GET api/values
        public Users Get(string access_token)
        {
            if (AccountController.Uzver != null)
            {
                try
                {
                    LoadCodes();
                    foreach (Codes buff in Codi)
                    {
                        if (buff.Access_token == access_token)
                        {
                            return AccountController.Uzver;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Ошибочка: " + ex);
                }
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
}
