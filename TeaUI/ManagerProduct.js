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

            let rnCell = row.insertCell(0);
            rnCell.innerHTML = result[i].id;

            let aCell = row.insertCell(1);
            aCell.innerHTML = result[i].name;

            let tCell = row.insertCell(2);
            tCell.innerHTML = result[i].description;

            let pCell = row.insertCell(3);
            pCell.innerHTML = result[i].price;

            let hCell = row.insertCell(4);
            hCell.innerHTML = result[i].numberOfTeaBags;
        }
        
    });
}

function NewProduct(){
    let product = {};
    product.names = document.querySelector('#name').value;
    product.description = document.querySelector('#description').value;
    product.numberOfTeaBags = document.querySelector('#numberofteabags').value;
    product.price = document.querySelector('#price').value;

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