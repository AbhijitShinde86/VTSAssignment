using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTS_User_Service.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Organization { get; set; }
        public string Address { get; set; }
        public string Emailaddress { get; set; }
        public string Location { get; set; }
        public string Photopath { get; set; }

        public int UpdatedBy { get; set; }
    }
}
