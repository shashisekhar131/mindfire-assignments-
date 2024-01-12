var userDetails = [];
var popUpElement = document.getElementById("pop-up");
   

   //form validation
    function validateForm(e){
     // to prevent refresh 
     e.preventDefault();
    
     // get all input elements by attribute name
     var inputElements = document.getElementsByTagName("input");
    
     var j;
     for( j=0;j<inputElements.length;j++) {
       
        // to check if any field is empty except comments(it is textarea) if empty then alert them
        if(inputElements[j].value == 0 ){
            
            showPopUp("Enter the value of " + inputElements[j].name);
            break;
          }

        //password should be atleast 8 characters
       
        if(inputElements[j].type == "password" && inputElements[j].value.length <8){
            showPopUp("Password must be atleast 8 characters");
            break;
        }

        //validate phone number 
        if(inputElements[j].type == "tel" && !((/^[89]\d{9}$/).test(inputElements[j].value)) ){
              showPopUp("phone number should be 10 digit number starting with 8 or 9");
              break;
        }
       // validate email 
       if(inputElements[j].name == "email" && !(/^[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]{2,}$/).test(inputElements[j].value)){
           showPopUp("Enter valid email address");
           break;
       }
        // validate the check boxes

        if(inputElements[j].type == "checkbox" && inputElements[j].name == "terms and conditions" && !inputElements[j].checked){
           
          showPopUp("see the terms and conditions");
          break;     

        }
        
        
       }
   

        // validate radio buttons
        // we can't put them inside loop becuase we have to check for one complete group at the same time. 
        var genderSelected = document.querySelector('input[name="gender"]:checked');
        if (!genderSelected){
          showPopUp("Please select a gender");
          
        } 
      
        var maritalSelected = document.querySelector('input[name="marital-status"]:checked');
        if(!maritalSelected) showPopUp("Please select marital-status");
        
         // j goes to end means everything is correct as no pop is shown 
         // after form is validated 
      
       if(j == inputElements.length && genderSelected && maritalSelected){
             getDetails(inputElements);
       }
       
   }

   // show pop-up
   function showPopUp(message){

    //create close button and add functionality
    var closeButton = document.createElement('button');
    closeButton.innerHTML="OK";
    closeButton.addEventListener('click', function () {
        popUpElement.style.display = "none";
    });
    // attach message and close button
    popUpElement.innerHTML =`<div>${message}</div>`;
    popUpElement.appendChild(closeButton);

    //display the pop up as block
    popUpElement.style.display="block";

   }



   // push all input values into userDetails array
  function getDetails(inputElements){
    // get the all details of user
    for(let i=0;i<inputElements.length;i++){
        if(inputElements[i].type != "radio")  userDetails.push({[inputElements[i].name]:inputElements[i].value});
       }

        // get the answer for the radio option gender
       var genderOptions = document.getElementsByName("gender");

       for(let i=0;i<genderOptions.length;i++){
        if(genderOptions[i].checked) userDetails.push({gender:genderOptions[i].value});
       }
       // get the answer for radio option marital status

        var maritalStatus = document.getElementsByName("marital-status");
        for(let i=0;i<maritalStatus.length;i++){
          if(maritalStatus[i].checked)userDetails.push({"marital-status":maritalStatus[i].value});
        }

        // form validated and got details now show the details 
        showDetails();
  }


  // show the details 
  function showDetails(){
    var detailsString=`<h2>details you submitted</h2>`;

    // iterate over array of objects
    for (var i = 0; i < userDetails.length; i++) {
    var userObject = userDetails[i];

    // Iterate over the properties of each object
    for (var property in userObject) {
        if (userObject.hasOwnProperty(property)) {
           
            detailsString +=""+property + " : " + userObject[property]+"<br>";
        }
      }
  
   }

    showPopUp(detailsString);

    localStorage.setItem("storedDetailsString",JSON.stringify(detailsString));
  }


// for every refresh take data from localStorage if any
if(localStorage.getItem("storedDetailsString")){
  detailsString = JSON.parse(localStorage.getItem("storedDetailsString"));
  showPopUp(detailsString);
}

document.body.addEventListener('click',function(e){

// when pressed outside  pop-up box remove it
 if(popUpElement.style.display == "block"){
  popUpElement.style.display="none";
 }

});

 //show slider input 
 var slider = document.getElementById("age"); 
   slider.oninput=function(){ document.getElementById("show-range").innerHTML = slider.value; } 
 
