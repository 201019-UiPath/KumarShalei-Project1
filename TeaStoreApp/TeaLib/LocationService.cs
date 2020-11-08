using System.Collections.Generic;
using System;
using TeaDB;
using TeaDB.Models;
using TeaDB.IRepo;

namespace TeaLib
{
    public class LocationService
    {
        private DBRepo repo;

        public LocationService(){
            this.repo = new DBRepo();
        }

        public LocationModel GetLocation(int id){
            return repo.GetLocation(id);
        }

        public List<InventoryModel> GetLocationInventory(int x){
            return repo.GetLocationInventory(x);
        }

    }
}