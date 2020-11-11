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


        public LocationModel GetLocationInventory(int id)
        {
            return repo.GetLocationInventory(id);
        }

        public ProductModel GetProduct(int id)
        {
            return repo.GetProduct(id);
        }


        public void CreateNewBasket(OrderModel order)
        {
            repo.CreateNewBasket(order);
        }

        public OrderModel GetCurrentOrder(int customerId, int locationId)
        {
            return repo.GetCurrentOrder(customerId, locationId);
        }
        public void AddToBasket(OrderItemModel order)
        {
            repo.AddToBasket(order);
        }
        public void IncreaseTotalPrice(OrderModel order, decimal amount)
        {
            repo.IncreaseTotalPrice(order, amount);
        }


    }
}