using System.Collections.Generic;
using System;
using TeaDB.Models;
using TeaLib;
using System.Linq;
using Serilog;



namespace TeaUI.Menus
{
    /// <summary>
    /// Customer can remove items are place order
    /// </summary>
    public class BasketMenu
    {
        private  MainMenuService customerService;

        private OrderService orderService;
        private LocationService locationService;
        public CustomerModel customer;
        public LocationModel location;
        public int orderid;

        private List<OrderItemModel> products;

        
        public BasketMenu(CustomerModel customer, int locationid, int orderid){
            this.locationService  = new LocationService();
            this.orderService = new OrderService();
            this.customerService = new MainMenuService();

            this.customer = customer;
            this.location = locationService.GetLocation(locationid);
            this.orderid = orderid;
            this.products = orderService.GetItemsInBasket(orderid);
        }

        public void Start(){

            
            
            
            string input;
            do{

                
                Console.WriteLine("Your Basket");
                System.Console.WriteLine("[ID] [Product Name] [Price] [Amount]");
                foreach(var p in products){
                    System.Console.WriteLine($"{p.productId} {orderService.GetProduct(p.productId).name} {orderService.GetProduct(p.productId).price} {p.amount}");
                }

                Options();
                input = System.Console.ReadLine();
                
                switch(input){
                    case "0":
                        DeleteItem();
                        break;
                    case "1":
                        OrderModel order = orderService.GetCurrentOrder(customer.id, location.id);
                        orderService.PlaceOrder(order);
                        System.Console.WriteLine("Your Order has been placed!");
                        Log.Information($"Order Has been placed at {location.id}");
                        break;
                    case "2":
                        System.Console.WriteLine("Go Back");
                        break;
                    default:
                        System.Console.WriteLine("Please enter valid input");
                        break;
                }
            }while(input!="2");
        }
        public void Options(){
            System.Console.WriteLine("[0] Delete Item");
            System.Console.WriteLine("[1] Place Order");
            System.Console.WriteLine("[2] Go Back");
        }

        public void DeleteItem(){
            
            System.Console.WriteLine("Which Product ID would you like to remove from your Basket?");
            int id = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine();
            OrderItemModel item = products.First(o => o.productId == id);
            products.Remove(item);
            orderService.DeleteProductFromOrderItem(orderid, id);
            decimal dec = (item.totalPrice) * (-1);
            orderService.DecreaseStock(location.id, item.productId, (item.amount*-1));
            if(!products.Any()){
                System.Console.WriteLine("Basket is Empty");
                orderService.DeleteOrder(orderid);
            } else {
                orderService.ChangeOrderTotalPrice(orderid, dec);
            }
            
        }
    }
}