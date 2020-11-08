using TeaDB.Models;
using TeaLib;
using System;
using Serilog;

namespace TeaUI.Menus
{
    /// <summary>
    /// Allow new and returning customers to login with an email
    /// </summary>

    public class MainMenu
    {
        private int userInput;
        private string email;

        private CustomerModel customer;
        private  MainMenuService customerService;
        
        public MainMenu(){
            this.customerService = new MainMenuService();
            email = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
        }


        public void Start(){

            while(customer==null){
                startMethod();
            }

            if(customer.email == "Manager123@gmail.com"){
                
                ManagerMenu managerMenu = new ManagerMenu();
                
                managerMenu.Start();
            } else {
                
                System.Console.WriteLine($"Welcome {customer.firstName}!");
                do{
                    Options();
                    userInput = Convert.ToInt32(System.Console.ReadLine());
                    switch(userInput){
                        case 0:
                            LocationMenu locationMenu1 = new LocationMenu(1,customer);
                            locationMenu1.Start();
                            break;
                        case 1:
                            LocationMenu locationMenu2 = new LocationMenu(2,customer);
                            locationMenu2.Start();
                            break;
                        case 2:
                            LocationMenu locationMenu3 = new LocationMenu(3,customer);
                            locationMenu3.Start();
                            break;
                        case 3:
                            System.Console.WriteLine("GoodBye! Hope to see you again!");
                            break;
                        default:
                            System.Console.WriteLine("Please enter valid input");
                            break;
                    }
                } while(userInput!=3);
            }
        }


        public CustomerModel NewCustomer(){
            System.Console.WriteLine("Enter First Name: ");
            string firstName = System.Console.ReadLine();

            System.Console.WriteLine("Enter Last Name: ");
            string lastName = System.Console.ReadLine();
            
            do{
                try{
                    System.Console.WriteLine("Enter email: ");
                    email = System.Console.ReadLine();
                    break;
                } catch (InvalidOperationException){
                    System.Console.WriteLine("Please enter a valid email");
                    continue;
                }
            }while(true);
            

            CustomerModel customer = new CustomerModel(){
                firstName = firstName,
                lastName = lastName,
                email = email
            };

            customerService.AddCustomer(customer);
            customer = customerService.GetCustomerInfo(email);
            return customer;
        }



        public CustomerModel OldCustomer(){
            
            do{
                try{
                    System.Console.WriteLine("Enter email: ");
                    email = System.Console.ReadLine();
                    break;
                } catch (InvalidOperationException){
                    System.Console.WriteLine("Please enter a valid email");
                    continue;
                }
            }while(true);
            
            
            CustomerModel customer = customerService.GetCustomerInfo(email);
            return customer;
            
        }


        public void startMethod(){
            System.Console.WriteLine("Welcome to BesTea \n Your local Tea Store ");
            string oldCustomer = @"[YyNn]{1}";
            while(true){
                try{
                    System.Console.WriteLine("Are you a returning Customer? [Y/N]");
                    
                    oldCustomer = System.Console.ReadLine().ToLower();
                    break;
                    
                } catch (InvalidOperationException){
                    System.Console.WriteLine("Please enter a valid input");
                }
            }
           
            if(oldCustomer == "n"){
                customer = NewCustomer();
                Log.Information($"New Customer {customer.email} has been Added");
                           
            } else {                        
                customer = OldCustomer();    
            }
                
                
        }



        
        



        public void Options(){
            System.Console.WriteLine("[0] Would You like to go to our Albany, NY Location?");
            System.Console.WriteLine("[1] Would You like to go to our Buffalo, NY Location?");
            System.Console.WriteLine("[2] Would You like to go to our Syracuse, NY Location?");
            System.Console.WriteLine("[3] Would You like to leave?");

        }
        
        
    }
}