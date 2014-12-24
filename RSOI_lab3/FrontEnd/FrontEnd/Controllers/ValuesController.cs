using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FrontEnd.Models;
using RestSharp;

namespace FrontEnd.Controllers
{
    public class RequestsController : ApiController
    {
        public static bool BekendShow = false;
        public static bool BekendInsert = false;
        public static bool BekendDelete = false;
        public static bool BekendSession = false;
        public static List<NotebookShort> Spisok = new List<NotebookShort>();

        // POST api/values
        public void Post(string password)
        {
            if (password == "BaoBab")
            {
                BekendShow = true;
            }
            if (password == "Lukashenko")
            {
                BekendDelete = true;
            }
            if (password == "ParamPamPam")
            {
                BekendInsert = true;
            }
            if (password == "SessionBoss")
            {
                BekendSession = true;
            }
        }
    }

    public class NInfController : ApiController
    {
        public static Notebook buff = new Notebook();

        // GET api/values
        public IHttpActionResult Get()
        {
            return Json(buff);
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
