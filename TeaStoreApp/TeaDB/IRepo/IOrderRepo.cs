using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IRepo
{
    /// <summary>
    /// Business Logic concerning orders
    /// </summary>
    public interface IOrderRepo
    {
        void NewOrder(OrderModel order);
        void DeleteOrder(int orderid);
        void AddProductToOrderItem(OrderItemModel order);
        void DeleteProductFromOrderItem(int orderid, int productid);
        List<OrderItemModel> GetItemsInBasket(int orderid);
        ProductModel GetProduct(int productid);
        void PlaceOrder(OrderModel order);
        int GetOrderId(CustomerModel customerid, int locationId);
        OrderModel GetCurrentOrder(int customerid, int locationid);
        List<OrderItemModel> GetOrderItems(int orderid);
        void ChangeOrderTotalPrice(int orderid, decimal amount);
        void DecreaseStock(int locationid, int productid, int stock);
        List<OrderModel> GetOrderHistory(CustomerModel customer);
        List<OrderModel> GetOrderHistoryByMostExpensive(CustomerModel customer);
        List<OrderModel> GetOrderHistoryByLeastExpensive(CustomerModel customer);
        
    }
}