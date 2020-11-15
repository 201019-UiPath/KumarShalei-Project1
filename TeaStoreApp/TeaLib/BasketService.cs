using System.Collections.Generic;
using System;
using TeaDB;
using TeaDB.Models;
using TeaDB.IRepo;

namespace TeaLib
{
    public class BasketService
    {
        
        private DBRepo repo;

        public BasketService(){
            this.repo = new DBRepo();
        }

        public ProductModel GetProduct(int id)
        {
            return repo.GetProduct(id);
        }
        public void DeleteFromBasket(OrderItemModel order)
        {
            repo.DeleteFromBasket(order);
        }
        public void DeleteBasket(OrderModel order)
        {
            repo.DeleteBasket(order);
        }
        public void DecreaseTotalPrice(OrderModel order, decimal amount)
        {
            repo.DecreaseTotalPrice(order, amount);
        }
        public void PlaceOrder(OrderModel order)
        {
            repo.PlaceOrder(order);
        }
        public void DecreaseStock(InventoryModel inventory)
        {
            repo.DecreaseStock(inventory);
        }
    }
}