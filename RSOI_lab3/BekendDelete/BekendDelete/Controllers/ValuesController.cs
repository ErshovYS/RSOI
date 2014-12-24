using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BekendDelete.Models;
using System.Data.SqlClient;
using System.Data;

namespace BekendDelete.Controllers
{
    public class DeleteController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IHttpActionResult Post(int id)
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Tovari";

                // Создаем строку запроса на выборку
                string qry = @"select * from " + tbl;

                // Создаем строку запроса на удаление
                string del = @"delete from " + tbl +
                " where id = " + id;

                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(qry, connection);
                // Создаем и наполняем набор данных
                DataSet ds = new DataSet();
                da.Fill(ds, tbl);
                // Сначала надо уточнить, сколько записей попадет в таблицу!!!
                DataTable dt = ds.Tables[tbl];
                // Создаем команду на удаление
                SqlCommand cmd = new SqlCommand(del, connection);
                // Создаем фильтр для удаления
                string filt = "(id = " + id + ")";
               
                // Удаляем
                if (dt.Select(filt).Length > 0)
                    foreach (DataRow row in dt.Select(filt))
                    {
                        row.Delete();
                    }
                else
                    return NotFound();

                da.DeleteCommand = cmd;
                da.Update(ds, tbl);

                connection.Close();
                return Json("Успешно удален " + id + " ноутбук");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return NotFound();
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
}
