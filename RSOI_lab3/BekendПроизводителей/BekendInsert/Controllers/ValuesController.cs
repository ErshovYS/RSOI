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
    public class CreatorsController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                List<Proizv> SpisokN = new List<Proizv>();
                string tbl = "Proizv";

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

                Proizv buff;
                SpisokN.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    buff = new Proizv();
                    buff.ProID = int.Parse(row["id"].ToString());
                    buff.Name = row["name"].ToString();
                    buff.Country = row["country"].ToString();
                    buff.Year = int.Parse(row["year"].ToString());
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

        public IHttpActionResult Get(string param, string value)
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Proizv";
                List<Proizv> Infa = new List<Proizv>();

                string qry = @"select * from " + tbl + " where " + param + "='" + value +"'";
                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                // Создаем команду запроса для текущего подключения	
                da.SelectCommand = new SqlCommand(qry, connection);
                // Создаем и наполняем набор данных
                DataSet ds = new DataSet();
                da.Fill(ds, tbl);
                // Получаем ссылку на таблицу
                DataTable dt = ds.Tables[tbl];

                Proizv buff;
                Infa.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    buff = new Proizv();
                    buff.ProID = int.Parse(row["id"].ToString());
                    buff.Name = row["name"].ToString();
                    buff.Country = row["country"].ToString();
                    buff.Year = int.Parse(row["year"].ToString());
                    Infa.Add(buff);
                }

                connection.Close();
                if (Infa.Count > 0)
                    return Json(Infa);
                else
                    return NotFound();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return NotFound();
        }

        // Добавление производителя
        public IHttpActionResult Put(string str)
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Proizv";

                string qry = @"select * from " + tbl;
                string ins = @"insert into " + tbl + " values (" + str + ")";

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
                return Json("Производитель успешно добавлен");
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
                string tbl = "Proizv";

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
                return Json("Успешно удален " + id + " производитель");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return NotFound();
        }
    }
}
