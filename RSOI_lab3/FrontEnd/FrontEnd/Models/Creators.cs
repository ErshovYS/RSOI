using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class Proizv
    {
        public int ProID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        
    }

    public class PolnProizv
    {
        public Proizv Infa { get; set; }
        public List<NotebookShort> Spisok { get; set; }
    }

    public class Params
    {
        public string Param { get; set; }
        public string Value { get; set; }
    }
}