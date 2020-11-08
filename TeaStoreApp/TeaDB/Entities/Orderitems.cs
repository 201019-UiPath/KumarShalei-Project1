using System;
using System.Collections.Generic;

namespace TeaDB.Entities
{
    public partial class Orderitems
    {
        public int Orderitemsid { get; set; }
        public int? Orderid { get; set; }
        public int? Productid { get; set; }
        public int? Amount { get; set; }
        public decimal? Totalprice { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
