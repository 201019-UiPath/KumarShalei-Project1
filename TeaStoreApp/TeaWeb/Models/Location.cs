using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaDB.Models;

namespace TeaWeb.Models
{
    public class Location
    {
        public int id { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public CustomerModel customer { get; set; }

        public virtual List<InventoryModel> inventory { get; set; }
        public virtual List<OrderModel> orders { get; set; }
    }
}
