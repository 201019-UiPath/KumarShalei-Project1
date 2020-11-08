using System;
using System.Collections.Generic;

namespace TeaDB.Entities
{
    public partial class Products
    {
        public Products()
        {
            Inventory = new HashSet<Inventory>();
            Orderitems = new HashSet<Orderitems>();
        }

        public int Productid { get; set; }
        public string Productname { get; set; }
        public int? Numberofteabags { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orderitems> Orderitems { get; set; }
    }
}
