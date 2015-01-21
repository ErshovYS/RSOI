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
        public static List<NotebookShort> Spisok = new List<NotebookShort>();
        public static List<Proizv> SpisokP = new List<Proizv>();
        public static List<PolnProizv> SpisokC = new List<PolnProizv>();
    }

    public class NInfController : ApiController
    {
        public static Notebook buff = new Notebook();

        // GET api/values
        public IHttpActionResult Get()
        {
            return Json(buff);
        }
    }

    public class СInfController : ApiController
    {
        public static Proizv buff = new Proizv();

        // GET api/values
        public IHttpActionResult Get()
        {
            return Json(buff);
        }
    }

    public class PInfController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            return Json(RequestsController.SpisokP);
        }
    }
}
