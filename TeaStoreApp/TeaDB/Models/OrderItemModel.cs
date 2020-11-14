namespace TeaDB.Models
{
    public class OrderItemModel
    {
        public int orderItemId{get;set;}
        public int orderId{get;set;}
        public int productId{get;set;}
        public int amount{get;set;}
        public decimal totalPrice{get;set;}

    }
}