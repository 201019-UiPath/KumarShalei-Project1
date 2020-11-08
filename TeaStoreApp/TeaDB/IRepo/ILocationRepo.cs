using System.Collections.Generic;
using TeaDB.Models;

namespace TeaDB.IRepo
{
    /// <summary>
    /// Business Logic Concerning Loccations
    /// </summary>
    public interface ILocationRepo
    {
        LocationModel GetLocation(int id);
        List<InventoryModel> GetLocationInventory(int id);
    }
}