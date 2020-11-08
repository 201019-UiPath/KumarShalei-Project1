using System;
using System.Collections.Generic;

namespace TeaDB.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int? Locationid { get; set; }
        public int? Productid { get; set; }
        public int? Stock { get; set; }

        public virtual Locations Location { get; set; }
        public virtual Products Product { get; set; }
    }
}
