using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSOI_lab2.Models
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
}