using System;
using System.Collections.Generic;

namespace TeaDB.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            Orderitems = new HashSet<Orderitems>();
        }

        public int Orderid { get; set; }
        public int? Customerid { get; set; }
        public int? Locationid { get; set; }
        public decimal? Totalprice { get; set; }
        public bool? Payed { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Locations Location { get; set; }
        public virtual ICollection<Orderitems> Orderitems { get; set; }
    }
}
