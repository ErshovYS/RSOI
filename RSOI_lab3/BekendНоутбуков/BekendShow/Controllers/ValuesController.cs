using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BekendShow.Models;
using System.Data.SqlClient;
using System.Data;
using RestSharp;

namespace BekendShow.Controllers
{
    public class NotebooksController : ApiController
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

        public IHttpActionResult Get(string ind)
        {
            try
            {
                var rc = new RestClient("http://localhost:7000/api/pinf");
                var rq = new RestRequest();

                var response_all = rc.Execute(rq);
                List<Proizv> Creatt = SimpleJson.DeserializeObject<List<Proizv>>(response_all.Content);

                List<Notebook> SpisokS = new List<Notebook>();
            
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Proizv";

                string qry = @"select * from Tovari where";
                for (int i = 0; i < Creatt.Count; i++)
                {
                    if (i == 0)
                        qry += " proizvID = " + Creatt[i].ProID;
                    else
                    {
                        qry += " OR proizvID = " + Creatt[i].ProID;
                    }
                }
                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                // Создаем команду запроса для текущего подключения	
                da.SelectCommand = new SqlCommand(qry, connection);
                // Создаем и наполняем набор данных
                DataSet ds = new DataSet();
                da.Fill(ds, tbl);
                // Получаем ссылку на таблицу
                DataTable dt = ds.Tables[tbl];

                Notebook buff;
                foreach (DataRow row in dt.Rows)
                {
                    buff = new Notebook();
                    buff.ID = int.Parse(row["id"].ToString());
                    buff.Name = row["name"].ToString();
                    buff.ProizvID = int.Parse(row["proizvID"].ToString());
                    SpisokS.Add(buff);
                }
                connection.Close();
                if (SpisokS.Count > 0)
                    return Json(SpisokS);
                else
                    return NotFound();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return NotFound();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // Добавление ноутбука
        public IHttpActionResult Put()
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

        // Удаление ноутбука
        public IHttpActionResult Delete(int id)
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
    }
}
