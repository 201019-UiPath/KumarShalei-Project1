using System;
using System.Collections.Generic;

namespace TeaDB.Entities
{
    public partial class Locations
    {
        public Locations()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
        }

        public int Locationid { get; set; }
        public string City { get; set; }
        public string Stateacronym { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
