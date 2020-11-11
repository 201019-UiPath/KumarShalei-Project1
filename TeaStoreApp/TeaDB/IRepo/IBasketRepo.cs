using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IRepo
{
    /// <summary>
    /// Business Logic concerning orders
    /// </summary>
    public interface IBasketRepo
    {
        ProductModel GetProduct(int id);
        void DeleteFromBasket(OrderItemModel order);
        void DecreaseTotalPrice(OrderModel order, decimal amount);
        void PlaceOrder(OrderModel order);
        void DeleteBasket(OrderModel order);
        void DecreaseStock(InventoryModel location, int amount);
        
        
    }
}