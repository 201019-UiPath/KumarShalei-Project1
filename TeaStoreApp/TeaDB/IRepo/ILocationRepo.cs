using System.Collections.Generic;
using TeaDB.Models;

namespace TeaDB.IRepo
{
    /// <summary>
    /// Business Logic Concerning Loccations
    /// </summary>
    public interface ILocationRepo
    {
        LocationModel GetLocationInventory(int id);
        
        ProductModel GetProduct(int id);


        void CreateNewBasket(OrderModel order);
        OrderModel GetCurrentOrder(int customerId, int locationId);
        void AddToBasket(OrderItemModel order);
        void IncreaseTotalPrice(int orderid, decimal amount);


    }
}