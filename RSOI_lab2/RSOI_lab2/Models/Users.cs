using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSOI_lab2.Models
{
    public class Users
    {
        public string LogiN { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string client_id { get; set; }
        public string client_key { get; set; }
        public string redirect_url { get; set; }
    }
}