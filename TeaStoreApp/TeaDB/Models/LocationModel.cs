using System.Collections.Generic;

namespace TeaDB.Models
{
    public class LocationModel
    {
        public int id{get;set;}
        public string city{get;set;}
        public string state{get;set;}

        public virtual List<InventoryModel> inventory { get; set; }
        public virtual List<OrderModel> orders { get; set; }
    }
}