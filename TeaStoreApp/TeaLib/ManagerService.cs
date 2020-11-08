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

        public void ReplenishStock(int locationid, int productid,int amount){
            repo.ReplenishStock(locationid, productid, amount);
        }

        public List<OrderModel> GetOrderHistoryLocationByMostExpensive(int locationid){
            return repo.GetOrderHistoryLocationByMostExpensive(locationid);

        }
        public List<OrderModel> GetOrderHistoryLocationByLeastExpensive(int locationid){
            return repo.GetOrderHistoryLocationByLeastExpensive(locationid);
        }

        public List<OrderModel> GetLocationOrderHistory(int x){
            return repo.GetLocationOrderHistory(x);
        }
    }

}