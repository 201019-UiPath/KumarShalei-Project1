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

        private readonly CustomerModel testCustomerModel = new CustomerModel(){
            id = 1,
            firstName = "Customer",
            lastName = "Test",
            email = "test@yahoo.com"
        };

        private readonly Customers testCustomers = new Customers(){
            Customerid = 1,
            Customerfirstname = "Customer",
            Customerlastname = "Test",
            Customeremail = "test@yahoo.com"
        };

        private readonly ProductModel productModel = new ProductModel(){
            name = "test",
            numberOfTeaBags = 1,
            description = "testing",
            price = Convert.ToDecimal(1.00)
        };


        private readonly OrderModel testOrderModel = new OrderModel(){
            customerId = 1,
            locationId = 1,
            totalPrice = Convert.ToDecimal(4.99),
            complete = false
        };

        private readonly Orders testOrders = new Orders(){
            Orderid =1,
            Customerid = 1,
            Locationid = 1,
            Totalprice = Convert.ToDecimal(4.99),
            Payed = false
        };

        private readonly Orders testOrderHistory1 = new Orders(){
            Orderid = 2,
            Customerid = 1,
            Locationid = 1,
            Totalprice = Convert.ToDecimal(4.99),
            Payed = true
        };

        private readonly Orders testOrderHistory2 = new Orders(){
            Orderid = 3,
            Customerid = 1,
            Locationid = 2,
            Totalprice = Convert.ToDecimal(9.99),
            Payed = true
        };

        private readonly OrderModel testOrderHistoryModel2 = new OrderModel(){
            id = 3,
            customerId = 1,
            locationId = 2,
            totalPrice = Convert.ToDecimal(9.99),
            complete = true
        };

        private readonly OrderItemModel testOrderItemModel = new OrderItemModel(){
            orderId = 1,
            productId = 1,
            amount = 1,
            totalPrice = Convert.ToDecimal(5.99)
        };

        private readonly Orderitems testOrderItems = new Orderitems(){
            Orderitemsid = 1,
            Orderid = 1,
            Productid = 1,
            Amount = 1,
            Totalprice = Convert.ToDecimal(5.99)
        };

        private readonly Products testProducts = new Products(){
            Productid = 1,
            Productname = "Test Tea",
            Numberofteabags = 10,
            Price = Convert.ToDecimal(5.99),
            Description = "this is a test"
        };

        private readonly Locations testLocations = new Locations(){
            Locationid = 1,
            City = "tests",
            Stateacronym = "TT"
        };

        private readonly Locations testLocations2 = new Locations(){
            Locationid = 2,
            City = "tests2",
            Stateacronym = "UU"
        };

        private readonly Inventory testInventory = new Inventory(){
            Locationid = 1,
            Productid = 1,
            Stock = 2
        };

        private void Seed(TeaContext testcontext)
        {
            testcontext.Products.AddRange(testProducts);
            testcontext.Customers.AddRange(testCustomers);
            testcontext.Locations.AddRange(testLocations);
            testcontext.Locations.AddRange(testLocations2);
            testcontext.Orders.AddRange(testOrders);
            testcontext.Orders.AddRange(testOrderHistory1);
            testcontext.Orders.AddRange(testOrderHistory1);
            testcontext.Orderitems.AddRange(testOrderItems);
            testcontext.Inventory.AddRange(testInventory);
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
            repo.NewCustomer (testCustomerModel);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Customers.Single(h => h.Customeremail == testCustomerModel.email));
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
            var result = repo.GetCustomerInfo("test@yahoo.com");

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.email, "test@yahoo.com");
        }


        [Fact]
        public void GetCustomerOrdersShouldGetCustomerOrders()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetCustomerOrdersShouldGetCustomerOrders").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetCustomerOrders(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public void GetCustomerOrdersFromLeastToMostShouldGetCustomerOrders()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetCustomerOrdersFromLeastToMostShouldGetCustomerOrders").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetCustomerOrderLeastToMost(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public void GetCustomerOrdersFromMostToLeastShouldGetCustomerOrders()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetCustomerOrdersFromMostToLeastShouldGetCustomerOrders").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetCustomerOrderMostToLeast(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(result.Count == 1);
        }


        [Fact]
        public void GetLocationOrderHistoryShouldGetLocationOrder()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetLocationOrderHistoryShouldGetLocationOrder").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetLocationOrderHistory(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(result.Count == 1);
        }


        [Fact]
        public void GetLocationOrderHistoryLeastToMostShouldGetLocationOrder()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetLocationOrderHistoryLeastToMostShouldGetLocationOrder").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetOrderHistoryLocationByMostExpensive(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(result.Count == 1);
        }


        [Fact]
        public void GetLocationOrderHistoryMostToLeastShouldGetLocationOrder()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetLocationOrderHistoryMostToLeastShouldGetLocationOrder").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetOrderHistoryLocationByMostExpensive(1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(result.Count == 1);
        }

        

        [Fact]
        public void CreateNewProductShouldCreateNewProduct()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("CreateNewProductShouldCreateNewProduct").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            
            //Act
            repo.CreateNewProduct(productModel);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Products.First(p => p.Productname == productModel.name));
        }


        [Fact]
        public void AddItemToInventoryShouldAddItemToInventory()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("AddItemToInventoryShouldAddItemToInventory").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            InventoryModel inventory = new InventoryModel(){
                locationId =1,
                productId = 1,
                stock =1
            };
            //Act
            repo.AddItemToInventory(inventory);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Inventory.First(i => i.Locationid == 1));
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
            Assert.Equal(result.id, testLocations.Locationid);
        }


        [Fact]
        public void GetAllProductsShouldGetAllProducts()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("GetAllProductsShouldGetAllProducts").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            var result = repo.GetAllProducts();

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(result.Count == 1);
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
            Assert.Equal(result.id, testProducts.Productid);
        }


         [Fact]
        public void CreateNewBasketShouldCreateNewBasket()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("CreateNewBasketShouldCreateNewBasket").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            
            OrderModel order = new OrderModel(){
                locationId =1,
                customerId=1,
                totalPrice=Convert.ToDecimal(0.00),
                complete =false
            };
            //Act
            repo.CreateNewBasket(order);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Orders.First(h => h.Locationid == 1 && h.Customerid ==1));
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
            Assert.Equal(result.id, testOrders.Orderid);
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
            var result = repo.GetCurrentOrder(testCustomerModel.id,1);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.Equal(result.customerId, testCustomerModel.id);
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
            repo.AddToBasket(testOrderItemModel);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.NotNull(assertContext.Orderitems.First(h => h.Orderid == testOrderItemModel.orderId));
        }
        
        [Fact]
        public void IncreaseTotalPriceShouldIncreaseTotalPrice()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("IncreaseTotalPriceShouldIncreaseTotalPrice").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            //Act
            repo.IncreaseTotalPrice(1, Convert.ToDecimal(1.00));

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(assertContext.Orders.First(h => h.Orderid == 1).Totalprice == Convert.ToDecimal(5.99));
        }

        [Fact]
        public void DecreaseStockShouldDecreaseStock()
        {
            
            var options = new DbContextOptionsBuilder<TeaContext>().UseInMemoryDatabase("DecreaseStockShouldDecreaseStock").Options;
            using var testContext = new TeaContext(options);
            repo = new DBRepo(){
                context = testContext,
                mapper = mapper
            };
            Seed(testContext);
            InventoryModel invenotry = new InventoryModel(){
                locationId =1,
                productId =1,
                stock=1
            };
            //Act
            repo.DecreaseStock(invenotry);

            //Assert
            using var assertContext = new TeaContext(options);
            Assert.True(assertContext.Inventory.First(h => h.Locationid == 1 && h.Productid ==1).Stock ==1);
        }


    }
}
