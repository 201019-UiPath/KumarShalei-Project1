// Stores location id
function LocationId(id){
    localStorage.setItem("locationId", parseInt(id));
}

// Gets location inventory
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

            let idCell = row.insertCell(0);
            idCell.innerHTML = result[i].id;


            let nameCell = row.insertCell(1);
            nameCell.innerHTML = result[i].name;

            let sCell = row.insertCell(2);
            let stock;
            for(let j = 0; j < result[i].inventory.length; ++j){
                if(result[i].inventory[j].locationId == id){
                    stock = result[i].inventory[j].stock;
                    
                }
            }
            sCell.innerHTML = stock;

            let pCell = row.insertCell(3);
            pCell.innerHTML = result[i].price;

           
        }
        
    });

}

// adds item to inventory
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


// Replenishes stock
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