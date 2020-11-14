using System.Collections.Generic;
using System;
using TeaDB;
using TeaDB.Models;
using TeaDB.IRepo;
using System.Linq;

namespace TeaLib
{
    
    public class MainMenuService
    {


        private DBRepo repo;
        public MainMenuService(){
            this.repo = new DBRepo();
        }

        public void NewCustomer(CustomerModel customer)
        {
            repo.NewCustomer(customer);
        }
        public CustomerModel GetCustomerInfo(string email)
        {
            return repo.GetCustomerInfo(email);
        }

        public CustomerModel GetCustomerOrderLeastToMost(string email)
        {
            CustomerModel customer = repo.GetCustomerInfo(email);
            customer.orders.OrderBy(o => o.totalPrice);
            return customer;
        }

        public CustomerModel GetCustomerOrderMostToLeast (string email)
        {
            CustomerModel customer = repo.GetCustomerInfo(email);
            customer.orders.OrderByDescending(o => o.totalPrice);
            return customer;
        }


    }


}