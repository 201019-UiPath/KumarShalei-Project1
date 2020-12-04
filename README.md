# Store Application

### Project Overview
This application is designed with functionality that would make virtual shopping much simpler! Customers can sign up for an account, place orders, view their order history, and specific location inventory. It also comes with an additional interface for managing your business. Managers can view and replenish location inventory, add new products, and view the order history of specific locations. 


This application used Entity Framework Core to connect to a PostgreSQL database, ASP.NET Core API to create a RESTful API, and HTML, CSS, BootstrapJS, and Javascript to create the front end.

### Technologies Used
* C#
* PostgreSQL DB
* EF Core
* ASP.NET Core
* HTML
* CSS
* JS
* BootstrapJS
* Xunit
* Serilog

### Functionalities
* The customer should be able to sign up for an account
* The customer should be able to place orders
* The customer should be able to view their order history
* The customer should be able to view location inventory
* The customer should know how much of a product is remaining
* The customer should be able to purchase multiple products
* The manager should be able to view a location’s order history
* The manager should be able to replenish inventory
* The manager should be able to add a new product to a location’s inventory
* Order histories should have the option to be sorted by date (latest to oldest and vice versa) or cost (least expensive to most expensive)

### Getting Started
In order to run project you will need the following environment(s):
* Git Bash
* Visual Studio 2019


Right Click inside the folder you wish to save the project and click 'Git Bash Here'


Run the following git commands in order to download project onto your machine:
* git clone https://github.com/201019-UiPath/KumarShalei-Project1.git

Navigate to the TeaStoreApp folder and click TeaStoreApp.sln
This will open the API project in Visual Studio 2019.


Click the Debug Tab and select 'Start Debugging'. 
This creates the connection between our Database and UI


Back in the File Explorer, navigate to outside our the TeaStoreApp folder, and click on the TeaUI folder. Here click on SignInIndex.html. You will be met with the log in screen for the store application!


#### License
This project uses the following license: <MIT License>.
