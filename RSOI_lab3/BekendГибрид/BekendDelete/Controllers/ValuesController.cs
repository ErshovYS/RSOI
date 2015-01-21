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
    public class InfCreatorController : ApiController
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
        public IHttpActionResult Post(string param, string value)
        {
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Proizv";
                List<PolnProizv> Infa = new List<PolnProizv>();

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

                PolnProizv Res;
                Proizv buff;
                Infa.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    Res = new PolnProizv();
                    buff = new Proizv();
                    buff.ProID = int.Parse(row["id"].ToString());
                    buff.Name = row["name"].ToString();
                    buff.Country = row["country"].ToString();
                    buff.Year = int.Parse(row["year"].ToString());
                    Res.Infa = buff;
                    Res.Spisok = FindNotebooks(buff.ProID);
                    Infa.Add(Res);
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

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        List<NotebookShort> FindNotebooks(int id)
        {
            List<NotebookShort> SpisokS = new List<NotebookShort>(); 
                
            try
            {
                string ConStr = @"server = JUDGE;integrated security = true;database = Notebooks";
                SqlConnection connection = new SqlConnection(ConStr);
                connection.Open();
                string tbl = "Proizv";

                string qry = @"select * from Tovari where proizvID = " + id;
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
                foreach (DataRow row in dt.Rows)
                {
                    buff = new NotebookShort();
                    buff.ID = int.Parse(row["id"].ToString());
                    buff.Name = row["name"].ToString();
                    SpisokS.Add(buff);
                }

                connection.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return SpisokS;
        }
    }
}
