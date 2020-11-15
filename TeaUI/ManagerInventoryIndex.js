function IncreaseStock(id, amount){
    let stock = {};
    stock.locationId = parseInt(localStorage.getItem('locationId'));
    stock.productId = id;
    stock.stock = (-1)*amount;

    xhr.open("PUT", 'https://localhost:5001/Basket/put/stock', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(stock));
    GetLocationInventory();
}

function LocationId(id){
    localStorage.setItem("locationId", parseInt(id));
}

function Inventory(){
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

            // let aCell = row.insertCell(1);
            // aCell.innerHTML = result[i].description;

            let tCell = row.insertCell(1);
            let stock;
            for(let j = 0; j < result[i].inventory.length; ++j){
                if(result[i].inventory[j].locationId == id){
                    stock = result[i].inventory[j].stock;
                    
                }
            }
            tCell.innerHTML = stock;

            let pCell = row.insertCell(2);
            pCell.innerHTML = result[i].price;

           
        }
        
    });

}

function AddItem(){
    let inventory = {};
    
    inventory.locationId = parseInt(localStorage.getItem('locationId'));
    inventory.productId = parseInt(document.querySelector('#newproductid').value);
    inventory.stock = parseInt(document.querySelector('#newamount').value);
    

    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function(){
        if(this.readyState == 4 && this.status > 199 && this.status < 300)
        {
            alert("New Product added!");
            document.querySelector('#newproductid').value = '';
            document.querySelector('#newamount').value = '';
            Inventory()
        }
    };
    xhr.open("POST", 'https://localhost:5001/Manager/add/inventory', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(inventory));
}

function ReplenishStock(){
    let inventory = {};
    
    inventory.locationId = parseInt(localStorage.getItem('locationId'));
    inventory.productId = parseInt(document.querySelector('#productid').value);
    inventory.stock = parseInt(document.querySelector('#amount').value);
    

    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function(){
        if(this.readyState == 4 && this.status > 199 && this.status < 300)
        {
            alert("Stock Replenished!");
            document.querySelector('#productid').value = '';
            document.querySelector('#amount').value = '';
            Inventory()
        }
    };
    xhr.open("PUT", 'https://localhost:5001/Manager/put/location', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(inventory));
}