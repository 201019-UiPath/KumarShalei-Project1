using System.Collections.Generic;

namespace TeaDB.Models
{
    public class CustomerModel
    {
        public int id{get;set;}
        public string firstName{get;set;}
        public string lastName{get;set;}
        public string email{get;set;}

        public virtual List<OrderModel> orders { get; set; }

    }
}