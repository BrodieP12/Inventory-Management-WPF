using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Vendor
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public int AccountOwner {  get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int LeadTimeDays { get; set; }
    }
}
