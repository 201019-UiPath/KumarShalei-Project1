using System.Collections.Generic;
using System;
using TeaDB;
using TeaDB.Models;
using TeaDB.IRepo;

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

    }
 
    
}