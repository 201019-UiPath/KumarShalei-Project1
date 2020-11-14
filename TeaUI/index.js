
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
    let email = document.querySelector('#userEmail').value;
    let url = 'https://localhost:5001/MainMenu/get/' + email;
    fetch(url)
    .then(response => response.json())
    .then(result => CustomerLocal(result));
}

function CustomerLocal(customer){
    localStorage.setItem("customerId", parseInt(customer.id));
    if(customer.id == 1){
        window.location.href = "ManagerIndex.html";
    }
    else {
        window.location.href = "LocationIndex.html";
    }
}

function GetLocationInventory(id){
    localStorage.setItem("LocationId", parseInt(id));
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

            let hCell = row.insertCell(4);
            hCell.innerHTML = '<input type="text" id="product"'+i+'" placeholder = "Enter Amount"/><input type="submit"  id="add'+i+'" value="AddtoBasket" />';
            document.getElementById('add'+i).onclick = () => AddtoBasket(result[i].id, document.getElementById('product'+parseInt( i)).value);
            //hCell.innerHTML = '<input type="button"  id="add'+i+'" value="AddtoBasket" class="btn btn-info">';
            //hCell.innerHTML =  '<input type="checkbox" id="product"'+i+'" class="form-control" >';

            // let oCell = row.insertCell(5);
            // oCell.innerHTML = '<input type="button"  id="add'+i+'" value="AddtoBasket" class="btn btn-info">';
            // document.getElementById('add'+i).onclick = () => AddtoBasket(result.id, document.getElementById('product'+i).checked);

        }
        
    });
}

function AddtoBasket(id, i){
    alert(id + " amount = "+ i );
    // if notfound == featch (get/order/{locationid}/{customerid})
    // create new baskest add/basket
    // add item to order add/basketitem
    // change price put/totalprice
    // if(confirm('Are you sure you want to delete house with ID='+houseId+'?'))
    // {
    //     fetch('https://localhost:5001/location/get/product/'+houseId, {method: 'DELETE'})
    //     .then(response =>
    //     {
    //         alert('Deleted house with ID='+houseId+'.');
    //         GetAllHouses();
    //     });
    // }
}

function ViewBasket(){

}

function PlaceOrder(){

}

function ViewBasketItems(){

}