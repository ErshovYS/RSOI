using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BekendInsert.Models
{
    public class Notebook
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Proc { get; set; }
        public int Oper { get; set; }
        public string Memory { get; set; }
        public string VideoCard { get; set; }
        public int Desksize { get; set; }
        public int ProizvID { get; set; }
        public int Price { get; set; }
    }
    public class NotebookShort
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}