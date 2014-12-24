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
    public class SessionController : ApiController
    {
        // POST api/values
        public IHttpActionResult Post(string login, string password)
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
                        HomeController.PasswordS.Add(login + "Abrakadabra" + password);
                        return Json(login + "Abrakadabra" + password);
                    }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return NotFound();
        }

        public IHttpActionResult Post(string pass)
        {
            if (HomeController.PasswordS.IndexOf(pass) >= 0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(string pass)
        {

            if (HomeController.PasswordS.Remove(pass))
            {
                return Json("Ключ успешно удален");
            }
            return BadRequest();
        }
    }
}
