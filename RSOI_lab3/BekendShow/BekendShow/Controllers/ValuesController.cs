using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BekendShow.Models;
using System.Data.SqlClient;
using System.Data;

namespace BekendShow.Controllers
{
    public class ShowController : ApiController
    {
        // GET api/show
        public IHttpActionResult Get()
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                List<NotebookShort> SpisokN = new List<NotebookShort>();
                string tbl = "Tovari";

                string qry = @"select id, name from " + tbl;
                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                // Создаем команду запроса для текущего подключения	
                da.SelectCommand = new SqlCommand(qry, connection);
                // Создаем и наполняем набор данных
                DataSet ds = new DataSet();
                da.Fill(ds, tbl);
                // Получаем ссылку на таблицу
                DataTable dt = ds.Tables[tbl];

                NotebookShort buff;
                SpisokN.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    buff = new NotebookShort();
                    buff.ID = int.Parse(row["id"].ToString());
                    buff.Name = row["name"].ToString();
                    SpisokN.Add(buff);
                }
                connection.Close();
                return Json(SpisokN);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return NotFound();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
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
}
