
function Register() {
    document.getElementById('NewCustomer').style.display = 'block';
    document.getElementById('ReturningButton').style.display = 'none'; 
    document.getElementById('RegisterButton').style.display = 'none'; 
}


function AddCustomer()
{
    let customer = {};
    customer.firstName = document.querySelector('#firstName').value;
    customer.lastName = document.querySelector('#lastName').value;
    customer.email = document.querySelector('#email').value;

    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function(){
        if(this.readyState == 4 && this.status > 199 && this.status < 300)
        {
            alert("New Customer added!");
            document.querySelector('#firstName').value = '';
            document.querySelector('#lastName').value = '';
            document.querySelector('#email').value = '';
        }
    };
    xhr.open("POST", 'https://localhost:5001/MainMenu/add', false);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(customer));
    GetCustomerInfo()

}


function ReturningCustomer() {
    document.getElementById('ReturningCustomer').style.display = 'block';
    document.getElementById('ReturningButton').style.display = 'none'; 
    document.getElementById('RegisterButton').style.display = 'none'; 
}

function GetCustomerInfo(){
    localStorage.clear();
    let email = document.querySelector('#userEmail').value;
    let url = 'https://localhost:5001/MainMenu/get/' + email;
    fetch(url)
    .then(response => response.json())
    .then(result => CustomerLocal(result));
}

function CustomerLocal(customer){
    localStorage.setItem("customerId", parseInt(customer.id));
    localStorage.setItem("customerEmail", customer.email);
    if(customer.id == 1){
        window.location.href = "ManagerIndex.html";
    }
    else {
        window.location.href = "CustomerIndex.html";
    }
}

function LocationId(id){
    localStorage.setItem("locationId", parseInt(id));
}

function GetLocationInventory(){
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

            let rnCell = row.insertCell(0);
            rnCell.innerHTML = result[i].name;

            let aCell = row.insertCell(1);
            aCell.innerHTML = result[i].description;

            let tCell = row.insertCell(2);
            let stock;
            for(let j = 0; j < result[i].inventory.length; ++j){
                if(result[i].inventory[j].locationId == id){
                    stock = result[i].inventory[j].stock;
                    
                }
            }
            tCell.innerHTML = stock;

            let pCell = row.insertCell(3);
            pCell.innerHTML = result[i].price;

            // let hCell = row.insertCell(4);
            // hCell.innerHTML = '<input type="text" id="product"'+i+'" placeholder = "Enter Amount"/>'//<input type="submit"  id="add'+i+'" value="AddtoBasket" />';
            //hCell.innerHTML = '<input type="button"  id="add'+i+'" value="AddtoBasket" class="btn btn-info">';
            //hCell.innerHTML =  '<input type="checkbox" id="product"'+i+'" class="form-control" >';

            let oCell = row.insertCell(4);
            oCell.innerHTML = '<input type="button"  id="add'+i+'" value="AddtoBasket" class="btn btn-info">';
            document.getElementById('add'+i).onclick = () => AddtoBasket(result[i].id, result[i].price);//document.getElementById('inventory').rows[i].cells[4].value);
            
            // document.getElementById('add'+i).onclick = () => AddtoBasket(result.id, document.getElementById('product'+i).checked);

        }
        
    });
}

function AddtoBasket(productid, i){
    GetOrderId();

    let item = {};
    item.orderId = parseInt(localStorage.getItem('orderId'));
    item.productId = productid;
    item.amount = 1;
    item.totalPrice = i;

    let xhr = new XMLHttpRequest();
    if(this.readyState == 4 && this.status > 199 && this.status < 300) {
        alert('Added to basket')
    }
    xhr.open("POST", 'https://localhost:5001/Location/add/basketitem', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(item));

    let order = {};
    order.id = parseInt(localStorage.getItem('orderId'));
    order.customerId = parseInt(localStorage.getItem('customerId')); 
    order.locationId = parseInt(localStorage.getItem('locationId'));  
    order.totalPrice = parseFloat(localStorage.getItem('orderTotalPrice')) +i;
    

    xhr.open("PUT", 'https://localhost:5001/Location/edit/totalprice', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(order));


    let stock = {};
    stock.locationId = parseInt(localStorage.getItem('locationId'));
    stock.productId = productid;
    stock.stock = 1;

    xhr.open("PUT", 'https://localhost:5001/Basket/put/stock', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(stock));
    GetLocationInventory();
}


function CreateBasket(){
    let basket = {};
    basket.customerId = parseInt(localStorage.getItem('customerId'));
    basket.locationId = parseInt(localStorage.getItem('locationId'));
    basket.totalPrice = 0.00;
    basket.complete = false;

    let xhr = new XMLHttpRequest();
    if(this.readyState == 4 && this.status > 199 && this.status < 300) {
        alert('New Basket Added!')
    }
    xhr.open("POST", 'https://localhost:5001/Location/add/basket', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(basket));
    GetOrderId();
    
}


function GetOrderId(){
    let locationid = localStorage.getItem('locationId');
    let customerid = localStorage.getItem('customerId');
    let url = 'https://localhost:5001/Location/get/order/' + locationid + '/' + customerid;

    fetch(url)
    .then(result => result.json())
    .then(result => 
        {if(result.status == 404){
            CreateBasket();
        } else{
            result => localStorage.setItem('orderTotalPrice',parseFloat(result.totalPrice))& localStorage.setItem('orderId', parseInt(result.id));
        }
        
    });
    alert(localStorage.getItem('orderTotalPrice'));
    
}


function ViewBasketItem(){
    GetOrderId();
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

            let rnCell = row.insertCell(0);
            rnCell.innerHTML = result.orderItems[i].productId;

            let aCell = row.insertCell(1);
            aCell.innerHTML = result.orderItems[i].amount;

            let pCell = row.insertCell(2);
            pCell.innerHTML = result.orderItems[i].totalPrice;
        }
        
    });

    
}


function PlaceOrder(){

    let order = {};
    order.id = parseInt(localStorage.getItem('orderId'));
    order.complete = true;


    let xhr = new XMLHttpRequest();
    if(this.readyState == 4 && this.status > 199 && this.status < 300) {
        alert('OrderPlaced!')
    }
    xhr.open("PUT", 'https://localhost:5001/Basket/put/basketorder/', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(order));

}


function CustomerOrders(){
    let url = 'https://localhost:5001/MainMenu/get/' + localStorage.getItem('customerEmail');
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#customerorderlist tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#customerorderlist tbody');
        for(let i = 0; i < result.orders.length; ++i)
        {
            if(result.orders[i].complete == true){
            
                let row = table.insertRow(table.rows.length);

                let rnCell = row.insertCell(0);
                rnCell.innerHTML = result.orders[i].id;

                let aCell = row.insertCell(1);
                aCell.innerHTML = result.orders[i].totalPrice;

            }
        }
        
    });
}

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

            let rnCell = row.insertCell(0);
            rnCell.innerHTML = result[i].id;

            let aCell = row.insertCell(1);
            aCell.innerHTML = result[i].totalPrice;

            
        }
        
    });
}

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
            rnCell.innerHTML = result[i].id;

            let aCell = row.insertCell(1);
            aCell.innerHTML = result[i].totalPrice;

            
        }
        
    });
}