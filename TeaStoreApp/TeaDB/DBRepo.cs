using System.Collections.Generic;
using TeaDB.Entities;
using TeaDB.Models;
using TeaDB.IMappers;
using TeaDB.IRepo;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace TeaDB
{
    /// <summary>
    /// Data Access Logic
    /// </summary>
    public class DBRepo : IMainMenuRepo, IManagerRepo, ILocationRepo, IBasketRepo

    {
        public TeaContext context { get; set; }
        public IMapper mapper { get; set; }
        public DBRepo() {
            this.context = new TeaContext();
            this.mapper = new DBMapper();
        }

        public void NewCustomer(CustomerModel customer)
        {
            context.Customers.Add(mapper.ParseCustomer(customer));
            context.SaveChanges();
        }

        public CustomerModel GetCustomerInfo(string email)
        {
            return mapper.ParseCustomer(
                context.Customers
                .Include("Orders")
                .First(c => c.Customeremail == email )
            );
        }

        public List<OrderModel> GetCustomerOrderLeastToMost(int id)
        {
            return mapper.ParseOrder(
                context.Orders
                .Where(o => o.Customerid == id && o.Payed == true)
                .OrderBy(o=> o.Totalprice)
                .Include("Orderitems")
                .ToList()
            );
        }


        public List<OrderModel> GetCustomerOrderMostToLeast(int id)
        {
            return mapper.ParseOrder(
                context.Orders
                .Where(o => o.Customerid == id && o.Payed == true)
                .OrderByDescending(o => o.Totalprice)
                .Include("Orderitems")
                .ToList()
            );
        }



        public void ReplenishStock(InventoryModel inventory)
        {
            context.Inventory.First(i => i.Locationid == inventory.locationId && i.Productid == inventory.productId).Stock += inventory.stock;
            context.SaveChanges();
        }

        public List<OrderModel> GetOrderHistoryLocationByMostExpensive(int locationid)
        {
            return mapper.ParseOrder(
                   context.Orders
                   .Where(o => o.Locationid == locationid && o.Payed == true)
                   .Include("Orderitems")
                   .OrderBy(o => o.Totalprice)
                   .ToList()
            );
        }

        public List<OrderModel> GetOrderHistoryLocationByLeastExpensive(int locationid)
        {
            return mapper.ParseOrder(
                context.Orders
                .Where(o => o.Locationid == locationid && o.Payed == true)
                .Include("Orderitems")
                .OrderByDescending(c => c.Totalprice)
                .ToList()
            );
        }

        public List<OrderModel> GetLocationOrderHistory(int id)
        {
            return mapper.ParseOrder(
                context.Orders
                .Where(o => o.Locationid == id && o.Payed == true)
                .Include("Orderitems")
                .ToList()
            );
        }

        public void CreateNewProduct(ProductModel product)
        {
            context.Products.Add(mapper.ParseProduct(product));
            context.SaveChanges();
        }

        public void AddItemToInventory(InventoryModel inventory)
        {
            context.Inventory.Add(mapper.ParseInventory(inventory));
            context.SaveChanges();
        }

        public LocationModel GetLocationInventory(int id)
        {
            return mapper.ParseLocation(
                context.Locations
                .Include("Inventory")
                .Include("Orders")
                .First(l => l.Locationid == id)
            );
        }



        public List<ProductModel> GetAllProducts()
        {
            return mapper.ParseProduct(
                context.Products
                .Include("Inventory")
                //.Where(p => p.Locationid == id)
                .ToList());
        }
        public ProductModel GetProduct(int id)
        {
            return mapper.ParseProduct(
                context.Products
                .First(p => p.Productid == id)
            );
        }

        public void CreateNewBasket(OrderModel order)
        {
            context.Orders.Add(mapper.ParseOrder(order));
            context.SaveChanges();
        }

        public OrderModel GetCurrentOrder(int customerId, int locationId)
        {
            try
            {
                return mapper.ParseOrder(
                    context.Orders
                    .Include("Orderitems")
                    .First(o => o.Customerid == customerId && o.Locationid == locationId && o.Payed == false)
                );
            } catch (Exception)
            {
                OrderModel order = new OrderModel()
                {
                    locationId = locationId,
                    customerId = customerId
                };
                CreateNewBasket(order);
                return mapper.ParseOrder(
                    context.Orders
                    .Include("Orderitems")
                    .First(o => o.Customerid == customerId && o.Locationid == locationId && o.Payed == false)
                );
            }
        }

        public void AddToBasket(OrderItemModel order)
        {
            context.Orderitems.Add(mapper.ParseOrderItem(order));
            context.SaveChanges();
        }

        public void IncreaseTotalPrice(int orderid, decimal amount)
        {
            context.Orders.First(o => o.Orderid == orderid).Totalprice += amount;
            context.SaveChanges();
        }

        public void DeleteFromBasket(OrderItemModel order)
        {
            context.Orderitems.Remove(mapper.ParseOrderItem(order));
            context.SaveChanges();
        }

        public void DecreaseTotalPrice(OrderModel order, decimal amount)
        {
            context.Orders.First(o => o.Equals(order)).Totalprice -= amount;
            context.SaveChanges();
        }

        public void PlaceOrder(OrderModel order)
        {
            context.Orders.First(i => i.Orderid == order.id).Payed = true;
            context.SaveChanges();
        }

        public void DeleteBasket(OrderModel order)
        {
            context.Orders.Remove(mapper.ParseOrder(order));
            context.SaveChanges();
        }

        public void DecreaseStock(InventoryModel inventory)
        {
            context.Inventory.First(i => i.Locationid == inventory.locationId && i.Productid == inventory.productId).Stock -= inventory.stock;
            context.SaveChanges();
        }


        
    }
}