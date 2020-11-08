using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IRepo
{
    /// <summary>
    /// Business Logic for Manager tasks
    /// </summary>
    public interface IManagerRepo
    {
         void ReplenishStock(int locationid, int productid, int amount);
        List<OrderModel> GetOrderHistoryLocationByMostExpensive(int locationid);
        List<OrderModel> GetOrderHistoryLocationByLeastExpensive(int locationid);
        List<OrderModel> GetLocationOrderHistory(int id);
         
    }
}