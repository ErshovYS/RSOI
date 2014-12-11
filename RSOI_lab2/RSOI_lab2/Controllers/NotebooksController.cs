using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using RSOI_lab2.Models;

namespace RSOI_lab2.Controllers
{
    public class NotebooksController : Controller
    {
        public static List<Notebook> SpisokN = new List<Notebook>();
        string tbl = "Tovari";

        // GET: Notebooks
        public ActionResult Index()
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

                Notebook buff;
                SpisokN.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    buff = new Notebook();
                    buff.ID = int.Parse(row["id"].ToString());
                    buff.Name = row["name"].ToString();
                    SpisokN.Add(buff);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return View();
        }

        // GET: Notebooks/Details/5
        public ActionResult Details(int id)
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
            return View();
        }

        // GET: Notebooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notebooks/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notebooks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notebooks/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notebooks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notebooks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
