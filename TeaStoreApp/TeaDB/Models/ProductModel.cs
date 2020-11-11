using System.Collections.Generic;

namespace TeaDB.Models
{
    public class ProductModel
    {
        public int id{get;set;}
        public string name{get;set;}
        public int numberOfTeaBags{get;set;}
        public decimal price{get;set;}
        public string description{get;set;}

        public virtual List<InventoryModel> inventory { get; set; }
        public virtual List<OrderItemModel> orderItems { get; set; }
    }
}