using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BekendInsert.Models;
using System.Data.SqlClient;
using System.Data;
using RestSharp;
namespace BekendInsert.Controllers
{
    public class InsertController : ApiController
    {
        // POST api/values
        public IHttpActionResult Post()
        {
            try
            {
                var rc = new RestClient("http://localhost:7000/api/ninf");
                var rq = new RestRequest();

                var response_all = rc.Execute(rq);
                Notebook buff = SimpleJson.DeserializeObject<Notebook>(response_all.Content);

                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Tovari";

                string qry = @"select * from " + tbl;
                string ins = @"insert into " + tbl + " values ('" + buff.ID + "', '" +
                    buff.Name + "', '" + buff.Proc + "', '" + buff.Oper + "', '" +
                    buff.Memory + "', '" + buff.VideoCard + "', '" + buff.Desksize + "', '" +
                    buff.ProizvID + "', '" + buff.Price + "')";

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
                return Json("Ноутбук успешно добавлен");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return BadRequest();
        }
    }
}
