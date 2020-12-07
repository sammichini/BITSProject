class suppliersPage {

  constructor() {
    this.state = {
      supplierId: "",
      supplier: null
    };

    // instance variables that the app needs but are not part of the "state" of the application
    this.server = "http://localhost:5000/api"
    this.url = this.server + "/suppliers";

    // instance variables related to ui elements simplifies code in other places
    this.$form = document.querySelector('#suppliersForm');
    this.$suppliersId = document.querySelector('#suppliersId');
    this.$suppliersName = document.querySelector('#name');
    this.$suppliersPhone = document.querySelector('#phone');
    this.$suppliersEmail = document.querySelector('#email');
    this.$suppliersWebsite = document.querySelector('#website');
    this.$suppliersContactFirstName = document.querySelector('#contactFirstName');
    this.$suppliersContactLastName = document.querySelector('#contactLastName');
    this.$suppliersContactPhone = document.querySelector('#contactPhone');
    this.$suppliersContactEmail = document.querySelector('#contactEmail');
    this.$suppliersNote = document.querySelector('#note');
    this.$findButton = document.querySelector('#findBtn');
    this.$addButton = document.querySelector('#addBtn');
    this.$deleteButton = document.querySelector('#deleteBtn');
    this.$editButton = document.querySelector('#editBtn');
    this.$saveButton = document.querySelector('#saveBtn');
    this.$cancelButton = document.querySelector('#cancelBtn');

    // call these methods to set up the page
    this.bindAllMethods();
    this.makeFieldsReadOnly(true);
    this.makeFieldsRequired(false);
    this.enableButtons("pageLoad");

  }

  // any method that is used as part of an event handler must bind this to the class
  // not all of these methods need to be bound but it was easier to do them all as I wrote them
  bindAllMethods() {
    this.onFindsuppliers = this.onFindsuppliers.bind(this);
    this.onEditsuppliers = this.onEditsuppliers.bind(this);
    this.onCancel = this.onCancel.bind(this);
    this.onDeletesuppliers = this.onDeletesuppliers.bind(this);
    this.onSavesuppliers = this.onSavesuppliers.bind(this);
    this.onAddsuppliers = this.onAddsuppliers.bind(this);

    this.makeFieldsReadOnly = this.makeFieldsReadOnly.bind(this);
    this.makeFieldsRequired = this.makeFieldsRequired.bind(this);
    this.fillsuppliersFields = this.fillsuppliersFields.bind(this);
    this.clearsuppliersFields = this.clearsuppliersFields.bind(this);
    this.disableButtons = this.disableButtons.bind(this);
    this.disableButton = this.disableButton.bind(this);
    this.enableButtons = this.enableButtons.bind(this);
  }

  
  // makes an api call to /api/suppliers/# where # is the primary key of the suppliers
  // finds a suppliers based on suppliers id.  in a future version it would be better to search by name
  onFindsuppliers(event) {
    event.preventDefault();
    if (this.$suppliersId.value != "") {
      this.state.suppliersId = this.$suppliersId.value;
      fetch(`${this.url}/${this.state.suppliersId}`)
      .then(response => response.json())
      .then(data => { 
        if (data.status == 404) {
          alert('That supplier does not exist in our database'); 
        }
        else {
          this.state.suppliers = data;
          this.fillsuppliersFields();
          this.enableButtons("found");
        }
      })
      .catch(error => {
        alert('There was a problem getting suppliers info!'); 
      });
    }
    else {
      this.clearsuppliersFields();
    }
  }

  // makes a delete request to /api/suppliers/# where # is the primary key of the suppliers
  // deletes the suppliers displayed on the screen from the database
  onDeletesuppliers(event) {
    event.preventDefault();
    if (this.state.suppliersId != "") {
      fetch(`${this.url}/${this.state.suppliersId}`, {method: 'DELETE'})
      .then(response => response.json())
      .then(data => { 
        // returns the record that we deleted so the ids should be the same 
        if (this.state.suppliersId == data.suppliersId)
        {
          this.state.suppliersId = "";
          this.state.suppliers = null;
          this.$suppliersId.value = "";
          this.clearsuppliersFields();
          this.enableButtons("pageLoad");
          alert("suppliers was deleted.")
        }
        else{
          alert('There was a problem deleting suppliers info!'); 
        }
      })
      .catch(error => {
        alert('There was a problem deleting suppliers info!'); 
      });
    }
    else {
      // this should never happen if the right buttons are enabled
    }
  }

  // makes either a post or a put request to /api/supplierss
  // either adds a new suppliers or updates an existing suppliers in the database
  // based on the suppliers information in the form
  onSavesuppliers(event) {
    event.preventDefault();
    // adding
    if (this.state.suppliersId == "") {
      fetch(`${this.url}`, {
        method: 'POST', 
        body: JSON.stringify({
          suppliersId: 0, 
          name: this.$suppliersName.value,
          phone: this.$suppliersPhone.value,
          email: this.$suppliersEmail.value,
          website: this.$suppliersWebsite.value,
          contactFirstName: this.$suppliersContactFirstName.value,
          contactLastName: this.$suppliersContactLastName.value,
          contactPhone: this.$suppliersContactPhone.value,
          contactEmail: this.$suppliersContactEmail.value,
          note: this.$suppliersNote.value
        }),
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(response => response.json())
      .then(data => { 
        // returns the record that we added so the ids should be there 
        if (data.suppliersId)
        {
          this.state.suppliersId = data.suppliersId;
          this.state.suppliers = data;
          this.$suppliersId.value = this.state.suppliersId;
          this.fillsuppliersFields();
          this.$suppliersId.readOnly = false;
          this.enableButtons("found");
          alert("suppliers was added.")
        }
        else{
          alert('There was a problem adding suppliers info!'); 
        }
      })
      .catch(error => {
        alert('There was a problem adding suppliers info!'); 
      });
    }
    // updating
    else {
      // the format of the body has to match the original object exactly 
      // so make a copy of it and copy the values from the form
      let suppliers = Object.assign(this.state.suppliers);
      suppliers.name = this.$suppliersName.value;
      suppliers.phone = this.$suppliersPhone.value;
      suppliers.email = this.$suppliersEmail.value;
      suppliers.website = this.$suppliers.Website.value;
      suppliers.contactFirstName = this.$suppliersContactFirstName.value;
      suppliers.contactLastName = this.$suppliersContactLastName.value;
      suppliers.contactPhone = this.$suppliersContactPhone.value;
      suppliers.contactEmail = this.$suppliersContactEmail.value;
      suppliers.note = this.$suppliersNote.value;
      fetch(`${this.url}/${this.state.suppliersId}`, {
        method: 'PUT', 
        body: JSON.stringify(suppliers),
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(response => {
        // doesn't return a body just a status code of 204 
        if (response.status == 204)
        {
          this.state.suppliers = Object.assign(suppliers);
          this.fillsuppliersFields();
          this.$suppliersId.readOnly = false;
          this.enableButtons("found");
          alert("suppliers was updated.")
        }
        else{
          alert('There was a problem updating suppliers info!'); 
        }
      })
      .catch(error => {
        alert('There was a problem adding suppliers info!'); 
      });
    }
  }

  // makes the fields editable
  onEditsuppliers(event) {
    event.preventDefault();
    // can't edit the suppliers id
    this.$suppliersId.readOnly = true;
    this.makeFieldsReadOnly(false);
    this.makeFieldsRequired(true);
    this.enableButtons("editing");
  }

  // clears the form for input of a new suppliers
  onAddsuppliers(event) {
    event.preventDefault();
    // can't edit the suppliers id
    this.state.suppliersId = ""
    this.state.suppliers = null;
    this.$suppliersId.value = "";
    this.$suppliersId.readOnly = true;
    this.clearsuppliersFields();
    this.makeFieldsReadOnly(false);
    this.makeFieldsRequired(true);
    //this.enableButtons("editing");
  }

  // cancels the editing for either a new suppliers or an existing suppliers
  onCancel(event) {
    event.preventDefault();
    if (this.state.suppliersId == "") {
      this.clearsuppliersFields();
      this.makeFieldsReadOnly();
      this.makeFieldsRequired(false);
      this.$suppliersId.readOnly = false;
      //this.enableButtons("pageLoad");
    }
    else {
      this.fillsuppliersFields();
      this.$suppliersId.readOnly = false;
     // this.enableButtons("found");
    }
  }

  // fills the form with data based on the suppliers
  fillsuppliersFields() {
    // fill the fields
    this.$suppliersName.value = this.state.suppliers.name;
    this.$suppliersPhone.value = this.state.suppliers.phone;
    this.$suppliersEmail.value = this.state.suppliers.email;
    this.$suppliersWebsite.value = this.state.suppliers.website;
    this.$suppliersContactFirstName.value = this.state.suppliers.contactFirstName;
    this.$suppliersContactLastName.value = this.state.suppliers.contactLastName;
    this.$suppliersContactPhone.value = this.state.suppliers.contactPhone;
    this.$suppliersContactEmail.value = this.state.suppliers.contactEmail;
    this.$suppliersNote.value = this.state.suppliers.note;
    this.makeFieldsReadOnly();
  }

  // clears the ui
  clearsuppliersFields() {
    this.$suppliersName.value = "";
    this.$suppliersPhone.value = "";
    this.$suppliersEmail.value = "";
    this.$suppliersWebsite.value = "";
    this.$suppliersContactFirstName.value = "";
    this.$suppliersContactLastName.value = "";
    this.$suppliersContactPhone.value = "";
    this.$suppliersContactEmail.value = "";
    this.$suppliersNote.value = "";
  }

  // enables or disables ui elements
  makeFieldsReadOnly(readOnly=true) {
    this.$suppliersName.readOnly = readOnly;
    this.$suppliersPhone.readOnly = readOnly;
    this.$suppliersEmail.readOnly = readOnly;
    this.$suppliersWebsite.readOnly = readOnly;
    this.$suppliersContactFirstName.readOnly = readOnly;
    this.$suppliersContactLastName.readOnly = readOnly;
    this.$suppliersContactPhone.readOnly = readOnly;
    this.$suppliersContactEmail.readOnly = readOnly;
    this.$suppliersNote.readOnly = readOnly;
  }

  // makes ui elements required when editing
  makeFieldsRequired(required=true) {
    this.$suppliersName.required = required;
    this.$suppliersPhone.required = required;
    this.$suppliersEmail.required = required;
    this.$suppliersWebsite.required = required;
    this.$suppliersContactFirstName.required = required;
    this.$suppliersContactLastName.required = required;
    this.$suppliersContactPhone.required = required;
    this.$suppliersContactEmail.required = required;
    this.$suppliersNote.required = required;
  }

  // disables an array of buttons
  disableButtons(buttons) {
    buttons.forEach(b => b.onclick = this.disableButton); 
    buttons.forEach(b => b.classList.add("disabled"));
  }

  // disables one button
  disableButton(event) {
    event.preventDefault();
  }

  // enables ui elements based on the editing state of the page
 enableButtons(state) {
    switch (state){
      case "pageLoad":
        this.disableButtons([this.$deleteButton, this.$editButton, this.$saveButton, this.$cancelButton]);
        this.$findButton.onclick = this.onFindsuppliers;
        this.$findButton.classList.remove("disabled");
        this.$addButton.onclick = this.onAddsuppliers;
        this.$addButton.classList.remove("disabled");
        break;
      case "editing": case "adding":
        this.disableButtons([this.$deleteButton, this.$editButton, this.$addButton]);
        this.$saveButton.onclick = this.onSavesuppliers;
        this.$cancelButton.onclick = this.onCancel;
        [this.$saveButton, this.$cancelButton].forEach(b => b.classList.remove("disabled"));
        break;
      case "found":
        this.disableButtons([this.$saveButton, this.$cancelButton]);
        this.$findButton.onclick = this.onFindsuppliers;
        this.$editButton.onclick = this.onEditsuppliers;
        this.$deleteButton.onclick = this.onDeletesuppliers;
        this.$addButton.onclick = this.onAddsuppliers;
        [this.$findButton, this.$editButton, this.$deleteButton, this.$addButton].forEach(b => b.classList.remove("disabled"));
        break;
      default:
    }
  }
}

// instantiate the js app when the html page has finished loading
window.addEventListener("load", () => new suppliersPage());
