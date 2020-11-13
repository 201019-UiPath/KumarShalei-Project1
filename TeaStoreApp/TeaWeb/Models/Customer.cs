using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeaDB.Models;

namespace TeaWeb.Models
{
    public class Customer
    {
        public int id { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string email { get; set; }

        public virtual List<OrderModel> orders { get; set; }
    }
}
