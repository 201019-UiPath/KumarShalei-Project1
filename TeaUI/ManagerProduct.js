// Shows all products
function GetAllProducts(){
    let url = 'https://localhost:5001/Manager/get/products';
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#product tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#product tbody');
        for(let i = 0; i < result.length; ++i)
        {
            
            let row = table.insertRow(table.rows.length);

            let idCell = row.insertCell(0);
            idCell.innerHTML = result[i].id;

            let nameCell = row.insertCell(1);
            nameCell.innerHTML = result[i].name;

            let desCell = row.insertCell(2);
            desCell.innerHTML = result[i].description;

            let pCell = row.insertCell(3);
            pCell.innerHTML = result[i].price;

            let nCell = row.insertCell(4);
            nCell.innerHTML = result[i].numberOfTeaBags;
        }
        
    });
}

// Creates a new product
function NewProduct(){
    let product = {};
    product.name = document.querySelector('#name').value;
    product.description = document.querySelector('#description').value;
    product.numberOfTeaBags = parseInt(document.querySelector('#numberofteabags').value);
    product.price = parseFloat(document.querySelector('#price').value);

    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function(){
        if(this.readyState == 4 && this.status > 199 && this.status < 300)
        {
            alert("New Product added!");
            document.querySelector('#name').value = '';
            document.querySelector('#description').value = '';
            document.querySelector('#numberofteabags').value = '';
            document.querySelector('#price').value = '';
            GetAllProducts();
        }
    };
    xhr.open("POST", 'https://localhost:5001/Manager/add/product', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(product));
}