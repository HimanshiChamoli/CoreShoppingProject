using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShoppingAdminPortal.Models
{
    public class Feedback
    {
        public string Name { get; set; }
        public long PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Comment { get; set; }
    }
}
