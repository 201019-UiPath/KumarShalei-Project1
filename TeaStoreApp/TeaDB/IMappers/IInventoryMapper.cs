using TeaDB.Entities;
using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IMappers
{
    /// <summary>
    /// Mapping between Inventory Model and Entities
    /// </summary>
    public interface IInventoryMapper
    {
        Inventory ParseInventory(InventoryModel inventory);
        ICollection<Inventory> ParseInventory(List<InventoryModel> inventory);
        InventoryModel ParseInventory(Inventory inventory);
        List<InventoryModel> ParseInventory(ICollection<Inventory> inventory);
    }
}