using System;
using Xunit;
using TeaDB;
using TeaDB.Entities;
using TeaDB.IMappers;
using TeaDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TeaTest
{
    public class TeaTests
    {
        private readonly IMapper mapper = new DBMapper();
        private DBRepo repo;

        private readonly CustomerModel testCustomer = new CustomerModel(){
            id = 1,
            firstName = "Customer",
            lastName = "Test",
            email = "test@yahoo.com"
        };

        private readonly Customers testCustomer2 = new Customers(){
            Customerid = 1,
            Customerfirstname = "Customer",
            Customerlastname = "Test",
            Customeremail = "test@yahoo.com"
        };

        private readonly OrderModel testOrder = new OrderModel(){
            customerId = 2,
            locationId = 1,
            totalPrice = Convert.ToDecimal(4.99),
            complete = false
        };

        private readonly Orders testOrder2 = new Orders(){
            Customerid = 1,
            Locationid = 1,
            Totalprice = Convert.ToDecimal(4.99),
            Payed = false
        };

        private readonly OrderItemModel testOrderItem = new OrderItemModel(){
            orderId = 1,
            productId = 1,
            amount = 1,
            totalPrice = Convert.ToDecimal(5.99)
        };

        private readonly Orderitems testOrderItem2 = new Orderitems(){
            Orderitemsid = 1,
            Orderid = 1,
            Productid = 1,
            Amount = 1,
            Totalprice = Convert.ToDecimal(5.99)
        };

        private readonly Products testProduct = new Products(){
            Productid = 1,
            Productname = "Test Tea",
            Numberofteabags = 10,
            Price = Convert.ToDecimal(5.99),
            Description = "this is a test"
        };

        private readonly Locations testLocation = new Locations(){
            Locationid = 1,
            City = "tests",
            Stateacronym = "TT"
        };

        private void Seed(TeaContext testcontext)
        {
            testcontext.Products.AddRange(testProduct);
            testcontext.Customers.AddRange(testCustomer2);
            testcontext.Locations.AddRange(testLocation);
            testcontext.Orders.AddRange(testOrder2);
            testcontext.Orderitems.AddRange(testOrderItem2);
            testcontext.SaveChanges();
        }

        [Fact]
        public void NewCustomerShouldAddCustomer()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("NewCustomerShouldAddCustomer").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            
            //Act
            repo.NewCustomer (testCustomer);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Customers.Single(h => h.Customeremail == testCustomer.email));
        }






        [Fact]
        public void NewOrderShouldCreateNewOrder()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("NewOrderShouldCreateNewOrder").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            
            //Act
            repo.CreateNewBasket(testOrder);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Orders.Single(h => h.Customerid == testOrder.customerId && h.Locationid == testOrder.locationId ));
        }

        [Fact]
        public void NewOrderItemShouldCreateNewOrderItem()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("NewOrderItemShouldCreateNewOrderItem").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            
            //Act
            repo.AddToBasket(testOrderItem);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Orderitems.First(h => h.Orderid == testOrderItem.orderId));
        }


        [Fact]
        public void GetProductShouldGetProduct()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetProductShouldGetProduct").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetProduct(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.id, testProduct.Productid);
        }



        [Fact]
        public void GetCustomerShouldGetCustomer()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetCustomerShouldGetCustomer").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetCustomerInfo(testCustomer2.Customeremail);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.email, testCustomer2.Customeremail);
        }
       
        [Fact]

        public void GetLocationShouldGetLocation()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetLocationShouldGetLocation").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetLocationInventory(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.id, testLocation.Locationid);
        }

        [Fact]
        public void GetCurrentOrderShouldGetCurrentOrder()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetCurrentOrderShouldGetCurrentOrder").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetCurrentOrder(1,1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.id, testOrder2.Orderid);
        }



        [Fact]
        public void GetOrderIdShouldGetOrderId()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetOrderIdShouldGetOrderId").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetCurrentOrder(testCustomer.id,1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.customerId, testCustomer.id);
        }


        [Fact]
        public void GetItemsInBasketShouldGetItemsInBasket()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetItemsInBasketShouldGetItemsInBasket").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetCurrentOrder(1,1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.orderItems.Count, 1);
        }



        [Fact]
        public void DeleteOrderShouldDeleteOrder()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("DeleteItemShouldDeleteItem").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            repo.DeleteBasket(testOrder);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Throws<InvalidOperationException>(() => assertContext.Orders.First(h => h.Orderid == 1));
        }
        
    }
}
