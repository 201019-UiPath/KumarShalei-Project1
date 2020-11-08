using System.Collections.Generic;
using TeaDB.Entities;
using TeaDB.Models;
using TeaDB.IMappers;
using TeaDB.IRepo;
using System.Linq;
using System;

namespace TeaDB
{
    /// <summary>
    /// Data Access Logic
    /// </summary>
    public class DBRepo : IMainMenuRepo, IManagerRepo, ILocationRepo, IOrderRepo

    {
        public  TeaContext context {get;set;}
        public  IMapper mapper {get;set;}
        public DBRepo(){
            this.context = new TeaContext();
            this.mapper = new DBMapper();
        }




        public void NewCustomerAsync(CustomerModel customer)
        {
            context.Customers.Add(mapper.ParseCustomer(customer));
            context.SaveChanges();
        }

        public CustomerModel GetCustomerInfo(string email)
        {
            try{
                return mapper.ParseCustomer(
                    context.Customers
                    .First(c => c.Customeremail == email)
                );
            }
            catch(InvalidOperationException){
                System.Console.WriteLine("No account with that email was found");
            }
            return null;
        }

        public List<OrderModel> GetOrderHistory(CustomerModel customer)
        {
            try{
                return mapper.ParseOrder(
                    context.Orders
                    .Where(c => c.Customerid == customer.id && c.Payed == true)
                    .ToList()
                );
            }
            catch (System.InvalidOperationException){
                return null;
            }
        }

        public List<OrderModel> GetOrderHistoryByMostExpensive(CustomerModel customer){
        
            try{
                return mapper.ParseOrder(
                    context.Orders
                    .Where(c => c.Customerid == customer.id && c.Payed == true)
                    .OrderByDescending(c => c.Totalprice)
                    .ToList()
                );
            }
            catch (System.InvalidOperationException){
                return null;
            }
        }


        public List<OrderModel> GetOrderHistoryByLeastExpensive(CustomerModel customer){
            try{
                return mapper.ParseOrder(
                    context.Orders
                    .Where(c => c.Customerid == customer.id && c.Payed == true)
                    .OrderBy(c => c.Totalprice)
                    .ToList()
                );
            }
            catch (System.InvalidOperationException){
                return null;
            }
        }

        public List<OrderItemModel> GetOrderItems(int orderid){
            return mapper.ParseOrderItem(
                context.Orderitems
                .Where(o => o.Orderid == orderid)
                .ToList()
            );
        }

       
            

        public void ReplenishStock(int locationid, int productid, int amount)
        {
            
            context.Inventory.First(i => i.Locationid == locationid && i.Productid==productid).Stock += amount;
           
            context.SaveChanges();           
        }





        public LocationModel GetLocation(int id){
            return mapper.ParseLocation(
                context.Locations
                .First(l => l.Locationid == id)
            );
        }

        public List<OrderModel> GetLocationOrderHistory(int x)
        {
            return mapper.ParseOrder(
                context.Orders
                .Where(i => i.Locationid == x)
                .ToList()
            );
        }


        public List<InventoryModel> GetLocationInventory(int x)
        {
            return mapper.ParseInventory(
                context.Inventory
                .Where(i => i.Locationid == x)
                .OrderBy(i => i.Productid)
                .ToList()
            );
        }







        public void NewOrder(OrderModel order)
        {
            
            context.Orders.Add(mapper.ParseOrder(order));
            context.SaveChanges();
        }

        public OrderModel GetCurrentOrder(int customerid, int locationid){
            return mapper.ParseOrder(
                context.Orders
                .First(o => o.Customerid == customerid && o.Locationid == locationid)
            );
        }

        public void DecreaseStock(int locationid, int productid, int stock){
            
            var inventory = context.Inventory.First(i => i.Locationid == locationid && i.Productid == productid);
            inventory.Stock = inventory.Stock -stock;
            context.SaveChanges();
        }

        
        public void AddProductToOrderItem(OrderItemModel order)
        {
            context.Orderitems.Add(mapper.ParseOrderItem(order));
            context.SaveChanges();
        }

        public void DeleteProductFromOrderItem(int orderid, int productid)
        {
            context.Orderitems.Remove(
                context.Orderitems
                .First(o => o.Productid == productid && o.Orderid == orderid)
            );
            
            context.SaveChanges();
        }

        
        public List<OrderItemModel> GetItemsInBasket(int orderid){
            return mapper.ParseOrderItem(
                context.Orderitems
                .Where(i => i.Orderid == orderid)
                .ToList()
            );
        }

        

        public void DeleteOrder(int id)
        {
            context.Orders.Remove(context.Orders.First(o => o.Orderid == id));
            context.SaveChanges();
        }

        
        
        public int GetOrderId(CustomerModel customer, int locationId){
            try{
                var order =  mapper.ParseOrder(
                    context.Orders
                    .Where(o => o.Customerid == customer.id)
                    .First(o => o.Locationid == locationId && o.Payed == false)
                );
                return order.id;
            }
            catch(System.InvalidOperationException){
                return -1;
            }
        }

        



        public void PlaceOrder(OrderModel order){
            context.Orders.First(i => i.Orderid == order.id).Payed = true;

            context.SaveChanges();
        }




       
        

        public void ChangeOrderTotalPrice(int orderid, decimal amount)
        {
            context.Orders.First(i => i.Orderid == orderid).Totalprice += amount;
            context.SaveChanges();
        }



        public ProductModel GetProduct(int productid){
            return mapper.ParseProduct(
                context.Products
                .First(p => p.Productid == productid)
            );
        }

       
        

        public List<OrderModel> GetOrderHistoryLocationByMostExpensive(int locationid){
            return mapper.ParseOrder(
                context.Orders
                .Where(o => o.Locationid == locationid)
                .OrderBy(o => o.Totalprice)
                .ToList()
            );

        }
        public List<OrderModel> GetOrderHistoryLocationByLeastExpensive(int locationid){
            try{
                return mapper.ParseOrder(
                    context.Orders
                    .Where(o => o.Locationid == locationid)
                    .OrderByDescending(o => o.Totalprice)
                    .ToList()
                );
            }
            catch(NullReferenceException){
                return null;
            }
        }
    

    }
}