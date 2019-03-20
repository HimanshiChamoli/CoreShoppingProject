using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceUserPanel.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public double PhoneNo { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public bool ShippingAddress { get; set; }
        public string Username { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
