// Gets location Order history
function GetLocationOrders(id){
    localStorage.setItem("locationId", parseInt(id));
    let url = 'https://localhost:5001/Manager/get/locationorder/' + id;
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#orderlist tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#orderlist tbody');
        for(let i = 0; i < result.length; ++i)
        {
            
            let row = table.insertRow(table.rows.length);

            let idCell = row.insertCell(0);
            idCell.innerHTML = result[i].id;

            let cusCell = row.insertCell(1);
            cusCell.innerHTML = result[i].customerId;

            let pCell = row.insertCell(2);
            pCell.innerHTML = result[i].totalPrice;

            let nameCell = row.insertCell(3);
            for (let j = 0; j < result[i].orderItems.length; ++j){
                fetch('https://localhost:5001/Location/get/product/' + result[i].orderItems[j].productId)
                .then(result => result.json())
                .then(result => nameCell.innerHTML += result.name + ", ");
            }
        }
        
    });

}

// Gets location Order history from least to most expensive
function LeastToMost(){
    let url = 'https://localhost:5001/Manager/get/locationorder/most/' + localStorage.getItem("locationId");
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#orderlist tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#orderlist tbody');
        for(let i = 0; i < result.length; ++i)
        {
            
            let row = table.insertRow(table.rows.length);

            let idCell = row.insertCell(0);
            idCell.innerHTML = result[i].id;

            let cusCell = row.insertCell(1);
            cusCell.innerHTML = result[i].customerId;

            let pCell = row.insertCell(2);
            pCell.innerHTML = result[i].totalPrice;

            let nameCell = row.insertCell(3);
            for (let j = 0; j < result[i].orderItems.length; ++j){
                fetch('https://localhost:5001/Location/get/product/' + result[i].orderItems[j].productId)
                .then(result => result.json())
                .then(result => nameCell.innerHTML += result.name + ", ");
            }
        }
        
    });

    
}

// Gets location Order history from most to least expensive
function MostToLeast(){
    let url = 'https://localhost:5001/Manager/get/locationorder/least/' + localStorage.getItem("locationId");
    fetch(url)
    .then(response => response.json())
    .then(result => {
        document.querySelectorAll('#orderlist tbody tr').forEach(element => element.remove());
        let table = document.querySelector('#orderlist tbody');
        for(let i = 0; i < result.length; ++i)
        {
            
            let row = table.insertRow(table.rows.length);

            let idCell = row.insertCell(0);
            idCell.innerHTML = result[i].id;

            let cusCell = row.insertCell(1);
            cusCell.innerHTML = result[i].customerId;

            let pCell = row.insertCell(2);
            pCell.innerHTML = result[i].totalPrice;

            let nameCell = row.insertCell(3);
            for (let j = 0; j < result[i].orderItems.length; ++j){
                fetch('https://localhost:5001/Location/get/product/' + result[i].orderItems[j].productId)
                .then(result => result.json())
                .then(result => nameCell.innerHTML += result.name + ", ");
            }
            
        }
        
    });
}

