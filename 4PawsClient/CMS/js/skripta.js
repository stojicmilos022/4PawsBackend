﻿// podaci od interesa
var host = "https://localhost:";
var port = "7184/";
var slaveEndpoint = "api/slave/";
var terminEndpoint="api/Termin/"
var pretragaEndpoint = "api/masteri/pretraga";
var loginEndpoint = "api/authentication/login";
var registerEndpoint = "api/authentication/register";
var formAction = "Create";
var editingId;
var jwt_token;

// prikaz forme za prijavu
function showLogin() {
	document.getElementById("data").style.display = "none";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginForm").style.display = "block";
	document.getElementById("registerForm").style.display = "none";
	document.getElementById("logout").style.display = "none";
}

function validateRegisterForm(username, email, password, confirmPassword) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (email.length === 0) {
		alert("Email field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	} else if (confirmPassword.length === 0) {
		alert("Confirm password field can not be empty.");
		return false;
	} else if (password !== confirmPassword) {
		alert("Password value and confirm password value should match.");
		return false;
	}
	return true;
}

function registerUser() {
	var username = document.getElementById("usernameRegister").value;
	var email = document.getElementById("emailRegister").value;
	var password = document.getElementById("passwordRegister").value;
	var confirmPassword = document.getElementById("confirmPasswordRegister").value;

	if (validateRegisterForm(username, email, password, confirmPassword)) {
		var url = host + port + registerEndpoint;
		var sendData = { "Username": username, "Email": email, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful registration");
					alert("Successful registration");
					showLogin();
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

window.onload = function() {
	loadMasteri();
};

// prikaz forme za registraciju
function showRegistration() {
	document.getElementById("data").style.display = "none";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginForm").style.display = "none";
	document.getElementById("registerForm").style.display = "block";
	document.getElementById("logout").style.display = "none";
}

function validateLoginForm(username, password) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	}
	return true;
}

function loginUser() {
	var username = document.getElementById("usernameLogin").value;
	var password = document.getElementById("passwordLogin").value;

	if (validateLoginForm(username, password)) {
		var url = host + port + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful login");
					alert("Successful login");
					response.json().then(function (data) {
						console.log(data);
						document.getElementById("info").innerHTML = "Currently logged in user: <i>" + data.username + "<i/>.";
						document.getElementById("logout").style.display = "block";
						document.getElementById("btnLogin").style.display = "none";

						// koristimo Window sessionStorage Property za cuvanje key/value parova u browser-u
						// sessionStorage cuva podatke za samo jednu sesiju
						// podaci će se obrisati kad se tab browser-a zatvori
						// (postoji i localStorage koji čuva podatke bez datuma njihovog "isteka")
						// dobavljanje tokena: token = sessionStorage.getItem(data.token);
						//sessionStorage.setItem("token", data.token);
						jwt_token = data.token;
						loadTerminCreate();
						// loadSlave();
						// LoadPretraga();
					});
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Username/password is incorrect!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

function loadSlave(){
	// ucitavanje mesta
	var requestUrl = host + port + mestoEndpoint;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	console.log(headers);
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setSlave);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
}

function LoadPretraga(){
	document.getElementById("searchMasterForm").style.display = "block";
}
// prikaz autora
function loadTerminCreate() {
	document.getElementById("data").style.display = "block";
	document.getElementById("loginForm").style.display = "none";
	

	// ucitavanje autora
	var requestUrl = host + port + terminEndpoint;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	console.log(headers);
	if(Object.keys(headers).length === 0)
	{
		fetch(requestUrl)
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setTerminUnAuthorised);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
	}
	else
	{
		fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setTerminAuthorised);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
	}

};



function showError() {
	var container = document.getElementById("data");
	container.innerHTML = "";

	var div = document.createElement("div");
	var h1 = document.createElement("h1");
	var errorText = document.createTextNode("Greska prilikom preuzimanja Autora!");

	h1.appendChild(errorText);
	div.appendChild(h1);
	container.append(div);
}

// metoda za postavljanje autora u tabelu
function setTerminAuthorised(data) {
	var container = document.getElementById("data");
	container.innerHTML = "";

	console.log(data);

	// ispis naslova
	var div = document.createElement("div");
	var h1 = document.createElement("h1");
	var headingText = document.createTextNode("Termini");
	h1.appendChild(headingText);
	div.appendChild(h1);

	// ispis tabele
	var table = document.createElement("table");
	table.classList.add("table","table-bordered","table-striped","table-hover");
	var header = createHeader();
	table.append(header);

	var tableBody = document.createElement("tbody");

	for (var i = 0; i < data.length; i++)
	{
		// prikazujemo novi red u tabeli
		var row = document.createElement("tr");
		// prikaz podataka

		row.appendChild(createTableCell(data[i].text));



		// prikaz dugmadi za izmenu i brisanje
		var stringId = data[i].id.toString();

		var buttonDelete = document.createElement("button");
		buttonDelete.name = stringId;
		buttonDelete.classList.add("btn", "btn-danger","align-middle","d-flex", "justify-content-center");
		buttonDelete.addEventListener("click", deleteMaster);
		var buttonDeleteText = document.createTextNode("Delete");
		buttonDelete.appendChild(buttonDeleteText);
		var buttonDeleteCell = document.createElement("td");
		buttonDeleteCell.appendChild(buttonDelete);
		row.appendChild(buttonDeleteCell);

		// var buttonEdit = document.createElement("button");
		// buttonEdit.name = stringId;
		// buttonEdit.classList.add("btn","btn-warning","align-middle","d-flex", "justify-content-center")
		// buttonEdit.addEventListener("click", editProdavac);
		// var buttonEditText = document.createTextNode("Edit");
		// buttonEdit.appendChild(buttonEditText);
		// var buttonEditCell = document.createElement("td");
		// buttonEditCell.appendChild(buttonEdit);
		// row.appendChild(buttonEditCell);

		
		tableBody.appendChild(row);		
	}

	

	table.appendChild(tableBody);
	div.appendChild(table);

	// prikaz forme
	document.getElementById("formDiv").style.display = "block";

	// ispis novog sadrzaja
	container.appendChild(div);
};
function setTerminUnAuthorised(data) {
	var container = document.getElementById("data");
	container.innerHTML = "";

	console.log(data);

	// ispis naslova
	var div = document.createElement("div");
	var h1 = document.createElement("h1");
	var headingText = document.createTextNode("Masteri");
	h1.appendChild(headingText);
	div.appendChild(h1);

	// ispis tabele
	var table = document.createElement("table");
	table.classList.add("table","table-bordered","table-striped","table-hover");
	var header = createHeaderUnAuth();
	table.append(header);

	var tableBody = document.createElement("tbody");

	for (var i = 0; i < data.length; i++)
	{
		// prikazujemo novi red u tabeli
		var row = document.createElement("tr");
		// prikaz podataka
		row.appendChild(createTableCell(data[i].name));
		row.appendChild(createTableCell(data[i].mestoName));
		row.appendChild(createTableCell(data[i].year));
		row.appendChild(createTableCell(data[i].price));

		// prikaz dugmadi za izmenu i brisanje


		tableBody.appendChild(row);		
	}

	

	table.appendChild(tableBody);
	div.appendChild(table);



	// ispis novog sadrzaja
	container.appendChild(div);
};

function setSlave(data)
{
	var selectElement=document.getElementById("mesto");
	for (var i=0;i<data.length;i++)
	{
		console.log(data[i])
		var option = document.createElement('option');
		option.value = data[i].id;
		option.text = data[i].name;
		selectElement.appendChild(option);
	}
}

function createHeader() {
	var thead = document.createElement("thead");
	var row = document.createElement("tr");
	

	row.appendChild(createTableCell("Datum i vreme za termin"));
	row.appendChild(createTableCell("Akcija"));


	thead.appendChild(row);
	return thead;
}

function createHeaderUnAuth() {
	var thead = document.createElement("thead");
	var row = document.createElement("tr");
	
	row.appendChild(createTableCell("Id"));
	row.appendChild(createTableCell("Name"));
	row.appendChild(createTableCell("Last Name"));
	row.appendChild(createTableCell("Price"));

	thead.appendChild(row);
	return thead;
}

function createTableCell(text) {
	var cell = document.createElement("td");
	cell.classList.add("text-center", "align-middle");
	var cellText = document.createTextNode(text);
	cell.appendChild(cellText);
	return cell;
}
function validateMasterForm(masterName, masterPrice, masterYear,mesto) {
	if (masterName.length === 0) {
		alert("Name field can not be empty.");
		return false;
	} else if (masterPrice.length === 0) {
		alert("Price field can not be empty.");
		return false;
	} else if (masterYear.length === 0) {
		alert("Year field can not be empty.");
		return false;
	} else if (mesto.length === 0) {
		alert("mesto field can not be empty.");
		return false;
	} 
	return true;
}
// dodavanje novog autora
function submitMasterForm(){

	var masterName = document.getElementById("masterName").value;
	var masterPrice = document.getElementById("masterPrice").value;
	var masterYear = document.getElementById("masterYear").value;
	var masterSlaveId=document.getElementById("mesto").value;

	var httpAction;
	var sendData;
	var url;
	if (validateMasterForm(masterName, masterPrice, masterYear,mesto))
	{
		if (formAction === "Create") {
			httpAction = "POST";
			url = host + port + masterEndpoint;
			sendData = {
				"Name": masterName,
				"Price": masterPrice,
				"Year":masterYear,
				"SlaveId":masterSlaveId
			};
		}
		else {
			httpAction = "PUT";
			url = host + port + masterEndpoint + editingId.toString();
			sendData = {
				"Id": editingId,
				"Name": masterName,
				"Price": masterPrice,
				"Year":masterYear,
				"SlaveId":masterSlaveId
			};
		}
	
		console.log("Objekat za slanje");
		console.log(sendData);
		var headers = { 'Content-Type': 'application/json' };
		if (jwt_token) {
			headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
		}
		fetch(url, { method: httpAction, headers: headers, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200 || response.status === 201) {
					console.log("Successful action");
					formAction = "Create";
					refreshTable();
				} else {
					console.log("Error occured with code " + response.status);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
		}
			else{
				alert("Form validation failed please fill in all the required fields.")
			}
		return false;
	}


	// u zavisnosti od akcije pripremam objekat



// brisanje autora
function deleteMaster() {
	// izvlacimo {id}
	var deleteID = this.name;
	// saljemo zahtev 
	var url = host + port + masterEndpoint + deleteID.toString();
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	fetch(url, { method: "DELETE", headers: headers})
		.then((response) => {
			if (response.status === 204) {
				console.log("Successful action");
				refreshTable();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
};

// izmena autora
function editMaster(){
	// izvlacimo id
	var editId = this.name;

	// saljemo zahtev da dobavimo tog autora

	var url = host + port + masterEndpoint + editId.toString();
	var headers = { };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	fetch(url, { headers: headers})
		.then((response) => {
			if (response.status === 200) {
				console.log("Successful action");
				response.json().then(data => {
					document.getElementById("masterName").value = data.name;
					document.getElementById("masterPrice").value = data.lastName;
					document.getElementById("masterYear").value = data.birthYear;
					document.getElementById("slave").value = data.masterSlaveId;
					editingId = data.id;
					formAction = "Update";
				});
			} else {
				formAction = "Create";
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
};

// osvezi prikaz tabele
function refreshTable() {
	// cistim formu
	document.getElementById("masterName").value = "";
	document.getElementById("masterPrice").value = "";
	document.getElementById("masterYear").value = "";
	//document.getElementById("prodavacProdavnica").value = "";
	// osvezavam
	document.getElementById("btnMasteri").click();
};

function clearProdavciForm() {
	// cistim formu
	document.getElementById("prodavacName").value = "";
	document.getElementById("prodavacLastName").value = "";
	document.getElementById("prodavacBirthYear").value = "";

};



function logout() {
	jwt_token = undefined;
	document.getElementById("usernameLogin").value = "";
	document.getElementById("passwordLogin").value = "";
	document.getElementById("info").innerHTML = "";
	document.getElementById("data").style.display = "none";
	document.getElementById("formDiv").style.display = "block";
	document.getElementById("loginForm").style.display = "block";
	document.getElementById("registerForm").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("btnLogin").style.display = "initial";
	document.getElementById("btnRegister").style.display = "initial";
	document.getElementById("searchMasterForm").style.display = "none";
	loadMasteri()
}

function submitSearchMasterForm() {

	var masterStart = parseInt(document.getElementById("masterStart").value);
	var masterKraj = parseInt(document.getElementById("masterKraj").value);
	var sendData = {
			"masterStart": masterStart,
			"masterKraj": masterKraj,
		};
	var url = host + port + pretragaEndpoint;
	var headers = { 'Content-Type': 'application/json' };
	
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	console.log("Objekat za slanje");
	console.log(sendData);
	// console.log(url);
	console.log( headers,JSON.stringify(sendData))
	
	
	fetch(url, { method: "POST", headers: headers, body: JSON.stringify(sendData) })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setMasterAuthorised);
				document.getElementById("searchMasterForm").reset();
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
	return false;
}