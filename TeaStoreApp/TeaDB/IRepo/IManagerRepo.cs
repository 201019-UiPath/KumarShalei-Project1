using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IRepo
{
    /// <summary>
    /// Business Logic for Manager tasks
    /// </summary>
    public interface IManagerRepo
    {
        LocationModel GetLocationInventory(int locationId);
        void ReplenishStock(InventoryModel inventory, int amount);
        List<OrderModel> GetOrderHistoryLocationByMostExpensive(int locationid);
        List<OrderModel> GetOrderHistoryLocationByLeastExpensive(int locationid);
        List<OrderModel> GetLocationOrderHistory(int id);
        void CreateNewProduct(ProductModel productModel);
        void AddItemToInventory(InventoryModel inventory);
         
    }
}