using System;
using System.Collections.Generic;
using TeaDB.Entities;
using TeaDB.Models;
using TeaDB.IMappers;

namespace TeaDB
{
    /// <summary>
    /// Mapping Entities to Models and vice versa
    /// </summary>
    public class DBMapper : IMapper
    {
        public Customers ParseCustomer(CustomerModel customer)
        {
            return new Customers(){
                Customerfirstname = customer.firstName,
                Customerlastname = customer.lastName,
                Customeremail = customer.email,
                Orders = ParseOrder(customer.orders)
            };
        }

        public ICollection<Customers> ParseCustomer(List<CustomerModel> customer)
        {
            ICollection<Customers> customers = new List<Customers>();
            foreach (var c in customer){
                customers.Add(ParseCustomer(c));
            }
            return customers;
        }

        public CustomerModel ParseCustomer(Customers customer)
        {
            return new CustomerModel(){
                id = customer.Customerid,
                firstName = customer.Customerfirstname,
                lastName = customer.Customerlastname,
                email = customer.Customeremail,
                orders = ParseOrder(customer.Orders)
            };
        }

        public List<CustomerModel> ParseCustomer(ICollection<Customers> customer)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            foreach (var c in customer){
                customers.Add(ParseCustomer(c));
            }
            return customers;
        }

        public Inventory ParseInventory(InventoryModel inventory)
        {
            return new Inventory(){
                Locationid = inventory.locationId,
                Productid = inventory.productId,
                Stock = inventory.stock
            };
        }

        public ICollection<Inventory> ParseInventory(List<InventoryModel> inventory)
        {
            ICollection<Inventory> inventories = new List<Inventory>();
            foreach (var i in inventory){

                inventories.Add(ParseInventory(i));
            }
            return inventories;
        }

        public InventoryModel ParseInventory(Inventory inventorys)
        {
            return new InventoryModel(){
                id = inventorys.Id,
                locationId = Convert.ToInt32(inventorys.Locationid),
                productId = Convert.ToInt32(inventorys.Productid),
                stock = Convert.ToInt32(inventorys.Stock)
            };
        }

        public List<InventoryModel> ParseInventory(ICollection<Inventory> inventorys)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            foreach (var i in inventorys){
                inventory.Add(ParseInventory(i));
            }
            return inventory;
        }

        public Locations ParseLocation(LocationModel location)
        {
            return new Locations(){
                City = location.city,
                Stateacronym = location.state,
                Inventory = ParseInventory(location.inventory),
                Orders = ParseOrder(location.orders)
            };
        }

        public ICollection<Locations> ParseLocation(List<LocationModel> location)
        {
            ICollection<Locations> locations = new List<Locations>();
            foreach (var l in location){

                locations.Add(ParseLocation(l));
            }
            return locations;
        }

        public LocationModel ParseLocation(Locations locations)
        {
            return new LocationModel(){
                id = locations.Locationid,
                city = locations.City,
                state = locations.Stateacronym,
                inventory = ParseInventory(locations.Inventory),
                orders = ParseOrder(locations.Orders)
            };
        }

        public List<LocationModel> ParseLocation(ICollection<Locations> locations)
        {
            List<LocationModel> location = new List<LocationModel>();
            foreach (var l in locations){
                location.Add(ParseLocation(l));
            }
            return location;
        }








        public Orders ParseOrder(OrderModel order)
        {
            return new Orders() {
                Customerid = order.customerId,
                Locationid = order.locationId,
                Totalprice = order.totalPrice,
                Payed = order.complete,
                Orderitems = ParseOrderItem(order.orderItems)
            };
        }

        

        public ICollection<Orders> ParseOrder(List<OrderModel> order)
        {
            if (order != null)
            {
                ICollection<Orders> orders = new List<Orders>();
                foreach (var o in order)
                {

                    orders.Add(ParseOrder(o));
                }
                return orders;
            }
            return null;
        }

        public OrderModel ParseOrder(Orders orders)
        {
            return new OrderModel() {
                id = orders.Orderid,
                customerId = Convert.ToInt32(orders.Customerid),
                locationId = Convert.ToInt32(orders.Locationid),
                totalPrice = Convert.ToDecimal(orders.Totalprice),
                complete = Convert.ToBoolean(orders.Payed),
                orderItems = ParseOrderItem(orders.Orderitems)
            };
        }

        public List<OrderModel> ParseOrder(ICollection<Orders> orders)
        {
            List<OrderModel> order = new List<OrderModel>();
            foreach (var o in orders){
                order.Add(ParseOrder(o));
            }
            return order;
        }










        public Orderitems ParseOrderItem(OrderItemModel orderItem)
        {
            return new Orderitems(){
                Orderid = orderItem.orderId,
                Productid = orderItem.productId,
                Amount = orderItem.amount,
                Totalprice = orderItem.totalPrice
            };
        }

        public ICollection<Orderitems> ParseOrderItem(List<OrderItemModel> orderItem)
        {
            ICollection<Orderitems> orderItems = new List<Orderitems>();
            foreach (var o in orderItem){

                orderItems.Add(ParseOrderItem(o));
            }
            return orderItems;
        }

        public OrderItemModel ParseOrderItem(Orderitems ordertimes)
        {
            return new OrderItemModel(){
                orderItemId = ordertimes.Orderitemsid,
                orderId = Convert.ToInt32(ordertimes.Orderid),
                productId = Convert.ToInt32(ordertimes.Productid),
                amount = Convert.ToInt32(ordertimes.Amount),
                totalPrice = Convert.ToDecimal(ordertimes.Totalprice)
            };
        }

        public List<OrderItemModel> ParseOrderItem(ICollection<Orderitems> orderlist)
        {
            List<OrderItemModel> orderlists = new List<OrderItemModel>();
            foreach (var o in orderlist){
                orderlists.Add(ParseOrderItem(o));
            }
            return orderlists;
        }

        public Products ParseProduct(ProductModel product)
        {
            return new Products(){

                Productname = product.name,
                Numberofteabags = product.numberOfTeaBags,
                Price = product.price,
                Description = product.description
            };
        }

        public ICollection<Products> ParseProduct(List<ProductModel> product)
        {
            ICollection<Products> products = new List<Products>();
            foreach (var p in product){

                products.Add(ParseProduct(p));
            }
            return products;
        }

        public ProductModel ParseProduct(Products products)
        {
            return new ProductModel(){
                id = products.Productid,
                name = products.Productname,
                numberOfTeaBags = Convert.ToInt32(products.Numberofteabags),
                price = Convert.ToDecimal(products.Price),
                description = products.Description,
                //inventory = ParseInventory(products.Inventory),
                //orderItems = ParseOrderItem(products.Orderitems)
            };
        }

        public List<ProductModel> ParseProduct(ICollection<Products> products)
        {
            List<ProductModel> product = new List<ProductModel>();
            foreach (var p in products){
                product.Add(ParseProduct(p));
            }
            return product;
        }
    }
}
