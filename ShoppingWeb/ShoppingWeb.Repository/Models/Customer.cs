using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWeb.Repository.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string? Password { get; set; }
        public string? ContactName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? AccountId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
