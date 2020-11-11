using System.Collections.Generic;
using System;
using TeaDB;
using TeaDB.Models;
using TeaDB.IRepo;

namespace TeaLib
{
    //public delegate void InventoryDel();

    public class ManagerService
    {

        private DBRepo repo;
        public ManagerService(){
            this.repo = new DBRepo();
        }

        public LocationModel GetLocationInventory(int locationId)
        {
            return repo.GetLocationInventory(locationId);
        }
        public void ReplenishStock(InventoryModel inventory, int amount)
        {
            repo.ReplenishStock(inventory, amount);
        }
        public List<OrderModel> GetOrderHistoryLocationByMostExpensive(int locationid)
        {
            return repo.GetOrderHistoryLocationByMostExpensive(locationid);
        }
        public List<OrderModel> GetOrderHistoryLocationByLeastExpensive(int locationid)
        {
            return repo.GetOrderHistoryLocationByLeastExpensive(locationid);
        }
        public List<OrderModel> GetLocationOrderHistory(int id)
        {
            return repo.GetLocationOrderHistory(id);
        }
        public void CreateNewProduct(ProductModel productModel)
        {
            repo.CreateNewProduct(productModel);
        }
        public void AddItemToInventory(InventoryModel inventory)
        {
            repo.AddItemToInventory(inventory);
        }
    }
    

}