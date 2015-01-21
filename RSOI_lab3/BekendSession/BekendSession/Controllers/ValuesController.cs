using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BekendSession.Models;
using System.Data.SqlClient;
using System.Data;

namespace BekendSession.Controllers
{
    public struct uzer
    {
        public string login;
        public string token;
        public bool admin;
    }

    public class SessionController : ApiController
    {
        // POST api/values
        public IHttpActionResult Put(string login, string password)
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Users";

                string qry = @"select * from " + tbl;
                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                // Создаем команду запроса для текущего подключения	
                da.SelectCommand = new SqlCommand(qry, connection);
                // Создаем и наполняем набор данных
                DataSet ds = new DataSet();
                da.Fill(ds, tbl);
                // Получаем ссылку на таблицу
                DataTable dt = ds.Tables[tbl];
                connection.Close();
                foreach (DataRow row in dt.Rows)
                    if (row["Login"].ToString() == login && row["Password"].ToString() == password)
                    {
                        uzer us = new uzer();
                        Random R = new Random();
                        us.login = login;
                        us.token = R.Next(10000)+password+R.Next(10000);
                        us.admin = bool.Parse(row["admin"].ToString());
                        HomeController.Access_tokens.Add(us);
                        return Json(us.token);
                    }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return NotFound();
        }

        public IHttpActionResult Post(string login, string pass)
        {
            foreach (var uzep in HomeController.Access_tokens)
                if (uzep.login == login && uzep.token == pass)
                {
                return Json("admin = " + uzep.admin);
            }
            return NotFound();

        }

        // DELETE api/sesseion/5
        public IHttpActionResult Delete(string login, string pass)
        {
            foreach (var uzep in HomeController.Access_tokens)
            if (uzep.login == login && uzep.token == pass)
            {
                HomeController.Access_tokens.Remove(uzep);
                return Json("Ключ успешно удален");
            }
            return BadRequest();
        }
    }
}
