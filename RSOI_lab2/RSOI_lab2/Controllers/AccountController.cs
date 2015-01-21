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
    public class AccountController : Controller
    {
        public static string Message = "", Message1 = "";
        public static int Client_id = 0;
        public static Users Uzver;
        public static HttpCookie Kuka;
        public static List<string> RandCodes = new List<string>();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users guestR)
        {
            try
            {
                string tbl = "Users";

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
                
                if (Prov(dt, guestR.LogiN))
                {
                    Random R = new Random();
                    guestR.client_id = (1000 + R.Next(9000)).ToString();
                    guestR.client_key = "abc" + R.Next(100000).ToString();
                    string ins = @"insert into " + tbl + " values ('" + guestR.LogiN + "', '" + guestR.Password + "', '" + guestR.Email + "', '" + guestR.Phone + "', '" + guestR.client_id + "', '" + guestR.client_key + "', '" + guestR.redirect_url + "')";

                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);

                    // Создаем команду вставки для текущего подключения
                    SqlCommand cmd = new SqlCommand(ins, HomeController.connection);

                    da.InsertCommand = cmd;
                    da.Update(ds, tbl);
                    Message = "Регистрация прошла успешно";
//                    FormsAuthentication.SetAuthCookie(guestR.Name, true);

                    return RedirectToAction("Index", "Notebooks");
                }
                else
                {
                    Message = "Пользователь с таким логином уже зарегистрирован, попробуйте придумать другой логин!";
                    return View();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return View("Thanks", guestR);
        }

        bool Prov(DataTable Dat, string Name)
        {
            foreach (DataRow row in Dat.Rows)
            {
                if (Name == row["Login"].ToString())
                    return false;
            }
            return true;
        }

        bool Prov(DataTable Dat, Users Uz)
        {
            foreach (DataRow row in Dat.Rows)
            {
                if (Uz.LogiN == row["Login"].ToString() && Uz.Password == row["Password"].ToString())
                {
                    Uz.Email = row["Email"].ToString();
                    Uz.Phone = row["Phone"].ToString();
                    Uz.client_id = row["client_id"].ToString();
                    Uz.client_key = row["client_key"].ToString();
                    Uz.redirect_url = row["redirect_url"].ToString();
                    return true;
                }
            }
            return false;
        }

        [HttpGet]
        public ViewResult Authorize(int client_id)
        {
            AuthorizeController.LoadUsers();
            List<Users> Useri = AuthorizeController.Useri;
            bool Right = false;
            foreach (var Userok in Useri)
            {
                if (Userok.client_id == client_id.ToString())
                {
                    Right = true;
                    Client_id = client_id;
                    break;
                }
            }
            if (Right)
                return View();
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult Authorize(Users guestR)
        {
            try
            {
                string tbl = "Users";
                
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
                if (Prov(dt, guestR))
                {
//                    FormsAuthentication.SetAuthCookie(guestR.Name, true);
                    Message1 = "";
                    Uzver = guestR;
                    AuthorizeController.LoadCodes();

                    Random R = new Random();
                    string Code = R.Next(10000).ToString() + R.Next(10000).ToString();
                    RandCodes.Add(Code);
                    return Redirect(guestR.redirect_url + "?code=" + Code);
                }
                else
                {
                    Message1 = "О нет! Вашего имени нет в базе данных! Скорее зарегистрируйтесь!!!";
                    return View();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибочка: " + ex);
            }
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
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

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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
