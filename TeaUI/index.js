function Register() {
    document.getElementById('NewCustomer').style.display = 'block';
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
    xhr.open("POST", 'https://localhost:5001/MainMenu/add', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(customer));

}


function ReturningCustomer() {
    document.getElementById('ReturningCustomer').style.display = 'block';
}

function GetCustomerInfo(){
    let email = document.querySelector('#userEmail').value;
    let url = 'https://localhost:5001/MainMenu/get/' + email;
    fetch(url)
    .then(response => response.json());
}