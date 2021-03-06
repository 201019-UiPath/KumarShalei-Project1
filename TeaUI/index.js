// hides buttons when registering a new custoner
function Register() {
    document.getElementById('NewCustomer').style.display = 'block';
    document.getElementById('ReturningButton').style.display = 'none'; 
    document.getElementById('RegisterButton').style.display = 'none'; 
}

// validates email follows email pattern
function ValidateEmail(mail) 
{
 if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(mail))
  {
    return (true)
  }
    alert("You have entered an invalid email address!")
    return (false)
}

// creates a new customer
function AddCustomer()
{
    if(ValidateEmail(document.querySelector('#newemail').value)){
    let customer = {};
    customer.firstName = document.querySelector('#firstName').value;
    customer.lastName = document.querySelector('#lastName').value;
    customer.email = document.querySelector('#newemail').value;

    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function(){
        if(this.readyState == 4 && this.status > 199 && this.status < 300)
        {
            document.querySelector('#firstName').value = '';
            document.querySelector('#lastName').value = '';
            document.querySelector('#newemail').value = '';
        }
    };
    xhr.open("POST", 'https://localhost:5001/MainMenu/add', false);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(customer));
    
    let url = 'https://localhost:5001/MainMenu/get/' + customer.email;
    fetch(url)
    .then(response => response.json())
    .then(result => CustomerLocal(result));

}
}

// hides buttons when a returning customer signs in
function ReturningCustomer() {
    document.getElementById('ReturningCustomer').style.display = 'block';
    document.getElementById('ReturningButton').style.display = 'none'; 
    document.getElementById('RegisterButton').style.display = 'none'; 
}

// Gets customer info of returning customer
function GetCustomerInfo(){
    if(ValidateEmail(document.querySelector('#userEmail').value)){
    localStorage.clear();
    let email = document.querySelector('#userEmail').value;
    let url = 'https://localhost:5001/MainMenu/get/' + email;
    fetch(url)
    .then(result=> result.json())
    .then(result=>
        {if(result.id == 0){
            alert("No Customer with the email");
        } else{
            
            CustomerLocal(result);
        }
        });
}}

// stores customer email, id and checks if customer is manager, then sends to appropriate page
function CustomerLocal(customer){
    localStorage.clear();
    localStorage.setItem("customerId", parseInt(customer.id));
    localStorage.setItem("customerEmail", customer.email);
    if(customer.id == 1){
        window.location.href = "ManagerProduct.html";
    }
    else {
        window.location.href = "CustomerIndex.html";
    }
}

// stores location id
function LocationId(id){
    localStorage.setItem("locationId", parseInt(id));
}

// retrieves location inventory
function GetLocationInventory(){
    GetOrderId();
    id = parseInt(localStorage.getItem("locationId"));
    let url = 'https://localhost:5001/Location/get/location/' + id;
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#inventory tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#inventory tbody');
        for(let i = 0; i < result.length; ++i)
        {
            
            let row = table.insertRow(table.rows.length);

            let nameCell = row.insertCell(0);
            nameCell.innerHTML = result[i].name;

            let desCell = row.insertCell(1);
            desCell.innerHTML = result[i].description;

            let stockCell = row.insertCell(2);
            let stock;
            for(let j = 0; j < result[i].inventory.length; ++j){
                if(result[i].inventory[j].locationId == id){
                    stock = result[i].inventory[j].stock;
                    
                }
            }
            stockCell.innerHTML = stock;

            let priceCell = row.insertCell(3);
            priceCell.innerHTML = result[i].price;

            //let bCell = row.insertCell(4);
            //bCell.innerHTML = '<input type="text" id="product"'+i+'" placeholder = "Enter Amount"/>'//<input type="submit"  id="add'+i+'" value="AddtoBasket" />';
            //bCell.innerHTML = '<input type="button"  id="add'+i+'" value="AddtoBasket" class="btn btn-info">';
            //bCell.innerHTML =  '<input type="checkbox" id="product"'+i+'" class="form-control" >';

            let basketCell = row.insertCell(4);
            basketCell.innerHTML = '<input type="button"  id="add'+i+'" value="AddtoBasket" class="btn btn-info">';
            document.getElementById('add'+i).onclick = () => AddtoBasket(result[i].id, result[i].price);//document.getElementById('inventory').rows[i].cells[4].value);
            
        }
        
    });
}

// adds an item to basket, reduces stock and updates total price of the order
function AddtoBasket(productid, i){

    let item = {};
    item.orderId = parseInt(localStorage.getItem('orderId'));
    item.productId = productid;
    item.amount = 1;
    item.totalPrice = i;

    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function(){
        if(this.readyState == 4 && this.status > 199 && this.status < 300) {
            //alert('Added to Basket');
        }}
    ;
    xhr.open("POST", 'https://localhost:5001/Location/add/basketitem', false);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(item));

    let order = {};
    order.id = parseInt(localStorage.getItem('orderId'));
    order.customerId = parseInt(localStorage.getItem('customerId')); 
    order.locationId = parseInt(localStorage.getItem('locationId'));  
    order.totalPrice = i;

    
    xhr.open("PUT", 'https://localhost:5001/Location/edit/totalprice', false);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(order));


    let stock = {};
    stock.locationId = parseInt(localStorage.getItem('locationId'));
    stock.productId = productid;
    stock.stock = 1;


    xhr.open("PUT", 'https://localhost:5001/Basket/put/stock', false);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(stock));
    GetLocationInventory();
}


// function CreateBasket(){
//     let basket = {};
//     basket.customerId = parseInt(localStorage.getItem('customerId'));
//     basket.locationId = parseInt(localStorage.getItem('locationId'));
//     basket.totalPrice = 0.00;
//     basket.complete = false;

//     let xhr = new XMLHttpRequest();
//     if(this.readyState == 4 && this.status > 199 && this.status < 300) {
//         alert('New Basket Added!')
//     }
//     xhr.open("POST", 'https://localhost:5001/Location/add/basket', false);
//     xhr.setRequestHeader('Content-Type', 'application/json');
//     xhr.send(JSON.stringify(basket));
//     //GetOrderId();
    
// }


// retreives order id (repo method will create a basket if none is found)
function GetOrderId(){
    let locationid = localStorage.getItem('locationId');
    let customerid = localStorage.getItem('customerId');
    let url = 'https://localhost:5001/Location/get/order/' + locationid + '/' + customerid;

    // fetch(url)
    // .then(result => {
    //     if (result.status == 404) {
    //         CreateBasket();
    //         GetOrderId();
    //     } else {
    //         result => result.json();
    //         localStorage.setItem('orderTotalPrice',parseFloat(result.totalPrice));
    //         localStorage.setItem('orderId', parseInt(result.id));
            
    //     }
    //   });
    
    //   .then(result => result.json())
    // .then(result => 
    //     {if(parseInt(result.status) == 404){
    //         CreateBasket();
    //         GetOrderId();
    //     } else{
    //         localStorage.setItem('orderTotalPrice',parseFloat(result.totalPrice));
    //         localStorage.setItem('orderId', parseInt(result.id));
    //     }
    // });

    // if(fetch(url).status == 404){
    //     CreateBasket();
    //     GetOrderId();
    // } 
    fetch(url)
    .then(result => result.json())
    .then(result => localStorage.setItem('orderTotalPrice',parseFloat(result.totalPrice))& localStorage.setItem('orderId', parseInt(result.id)));
   
}

// View items in basket
function ViewBasketItem(){
    let locationid = localStorage.getItem('locationId');
    let customerid = localStorage.getItem('customerId');
    let url = 'https://localhost:5001/Location/get/order/' + locationid + '/' + customerid;
    fetch(url)
    .then(result => result.json())
    .then(result => {
        document.querySelectorAll('#basket tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#basket tbody');
        for(let i = 0; i < result.orderItems.length; ++i)
        {
            
            let row = table.insertRow(table.rows.length);

            let nCell = row.insertCell(0);
            nCell.innerHTML =  fetch('https://localhost:5001/Location/get/product/' + result.orderItems[i].productId)
                .then(r => r.json())
                .then(r => nCell.innerHTML =r.name);

            let pCell = row.insertCell(1);
            pCell.innerHTML = result.orderItems[i].totalPrice;
        }
        
    });

    
}


// places order
function PlaceOrder(){

    let order = {};
    order.id = parseInt(localStorage.getItem('orderId'));
    order.complete = true;


    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function(){
    if(this.readyState == 4 && this.status > 199 && this.status < 300) {
        alert('Order Placed!');
    }};
    xhr.open("PUT", 'https://localhost:5001/Basket/put/basketorder/', false);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(order));
    localStorage.removeItem('orderTotalPrice');
    ViewBasketItem()
}

// Grab customer order history
function CustomerOrders(){
    let url = 'https://localhost:5001/MainMenu/get/orders/' + localStorage.getItem('customerId');
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#customerorderlist tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#customerorderlist tbody');
        for(let i = 0; i < result.length; ++i)
        {
            let row = table.insertRow(table.rows.length);

            let locCell = row.insertCell(0);
            let location;
            if(result[i].locationId == 1) {location = "Albany";}
            if(result[i].locationId == 2) {location = "Syracuse";}
            if(result[i].locationId == 3) {location = "Buffalo";}
            locCell.innerHTML = location;

            let pCell = row.insertCell(1);
            pCell.innerHTML = result[i].totalPrice;

            let productCell = row.insertCell(2);
            for (let j = 0; j < result[i].orderItems.length; ++j){
                fetch('https://localhost:5001/Location/get/product/' + result[i].orderItems[j].productId)
                .then(result => result.json())
                .then(result => productCell.innerHTML += result.name + " - " + result.price + ", ");
            }
        }
        
    });
}

// Grab customer order history from least to most expensive
function CustomerOrderLeastToMost(){
    let url = 'https://localhost:5001/MainMenu/get/order/least/' + localStorage.getItem('customerId');
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#customerorderlist tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#customerorderlist tbody');
        for(let i = 0; i < result.length; ++i)
        {
            let row = table.insertRow(table.rows.length);

            let locCell = row.insertCell(0);
            let location;
            if(result[i].locationId == 1) {location = "Albany";}
            if(result[i].locationId == 2) {location = "Syracuse";}
            if(result[i].locationId == 3) {location = "Buffalo";}
            locCell.innerHTML = location;

            let pCell = row.insertCell(1);
            pCell.innerHTML = result[i].totalPrice;

            let productCell = row.insertCell(2);
            for (let j = 0; j < result[i].orderItems.length; ++j){
                fetch('https://localhost:5001/Location/get/product/' + result[i].orderItems[j].productId)
                .then(result => result.json())
                .then(result => productCell.innerHTML += result.name + " - " + result.price + ", ");
            }
        }
        
    });
}

// Grab customer order history from most to least expensive
function CustomerOrderMostToLeast(){
    let url = 'https://localhost:5001/MainMenu/get/order/most/' + localStorage.getItem('customerId');
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#customerorderlist tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#customerorderlist tbody');
        for(let i = 0; i < result.length; ++i)
        {
            
            let row = table.insertRow(table.rows.length);

            let rnCell = row.insertCell(0);
            let location;
            if(result[i].locationId == 1) {location = "Albany";}
            if(result[i].locationId == 2) {location = "Syracuse";}
            if(result[i].locationId == 3) {location = "Buffalo";}
            rnCell.innerHTML = location;

            let aCell = row.insertCell(1);
            aCell.innerHTML = result[i].totalPrice;

            let hCell = row.insertCell(2);
            for (let j = 0; j < result[i].orderItems.length; ++j){
                fetch('https://localhost:5001/Location/get/product/' + result[i].orderItems[j].productId)
                .then(result => result.json())
                .then(result => hCell.innerHTML += result.name + " - " + result.price + " ");
            }
        }
        
    });
}

