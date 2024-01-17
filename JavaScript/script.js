
function validateForm(e){
  // to prevent refresh 
  e.preventDefault();
  
  var inputElements = $("input");
  var j;
  for( j=0;j<inputElements.length;j++) {

     var currentInput = inputElements.eq(j);
     // to check if any field is empty except comments(it is textarea) if empty then alert them
     if(currentInput.val() == 0 ){
         showPopUp("Enter the value of " + currentInput.attr('name'));
         break;
       }       
    }
    
     
     var agreedTermsConditions =  $('input[name="terms and conditions"]').prop('checked');
     if (!agreedTermsConditions) showPopUp("see terms and conditions");
    
     // jQuery statement results an array of checked radio options if checked then array.length >0
 
      var genderSelected = $('input[name="gender"]:checked').length>0?true:false;
     if (!genderSelected) showPopUp("Please select a gender");
    
   
     var maritalSelected = $('input[name="marital-status"]:checked').length>0?true:false;
     if(!maritalSelected) showPopUp("Please select marital-status");
     

     // check all error-tips if there any
      var isAnyError = false;
      if ($('.error-tip:empty').length === $('.error-tip').length) {
          isAnyError = false;
      }else{
         isAnyError =true;
         showPopUp("there are some errors");
      } 

     // j goes to end && gender,marital,terms and conditions selected && no errors  - means all values are entered
    
    if(j == inputElements.length && genderSelected && maritalSelected && !isAnyError){
          getDetails(inputElements);
    }
    
    
}



var popUpElement = $("#pop-up");
// show pop-up
function showPopUp(message){

  // Create close button and add functionality
  var closeButton = $('<button>').html('OK').on('click', function () {
     popUpElement.hide();
 });

 // Attach message and close button
 popUpElement.html(`<div>${message}</div>`).append(closeButton);

 // Display the pop-up 
 popUpElement.show();

}

// push all input values into userDetails array
var userDetails = [];

 function getDetails(inputElements){
 
 // get the answer for inputs of user
 for(let i=0;i<inputElements.length;i++){
     if(inputElements[i].type != "radio")  userDetails.push({[inputElements[i].name]:inputElements[i].value});
    }

     // get the answer for the radio option gender and marital staus      
    userDetails.push({ gender: $('input[name="gender"]:checked').val() });
    userDetails.push({ "marital-status": $('input[name="marital-status"]:checked').val() });

     // get the answer from hobbies means if hobbies checked is others then show prompt else directly push value
     $('input[name="hobbies"]:checked').each(function() {
         userDetails.push({ "hobbies": this.value === "others" ? prompt("Enter the hobbies") : this.value });
     });

     // form validated and got details now show the details 
     showDetails();
}


// show the details 
function showDetails(){

 // using string builder instead of looping and concatenating the strings
 const detailsStringBuilder = ['<h2>Details you submitted</h2>'];

 userDetails.forEach(userObject => {
     for (const [property, value] of Object.entries(userObject)) {
         detailsStringBuilder.push(`${property} : ${value}<br>`);
     }
 });

 const detailsString = detailsStringBuilder.join('');
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
if (popUpElement.is(":visible")) {
 popUpElement.hide();
}
});




// tip error for text inputs 
// for every input element add event 'input'  and check if it is text with only alphabets or not

var textInputs = $('input');

textInputs.on('input', function() {
 var currentInput = $(this);
if(currentInput.is('[data-allowed="text"]')){

 //(currentInput.val() !== "") condtion is  when there is no input don't show errors

 if (currentInput.val() !== "" && !/^[a-zA-Z]+$/.test(currentInput.val())) {
     showErrorTip("only text allowed",currentInput);
 } else removeErrorTip(currentInput);
    
}

if(currentInput.is('[type="password"]')){

if(currentInput.val() !== "" && currentInput.val().length < 8) showErrorTip("it should be more than 8 characters",currentInput);
else  removeErrorTip(currentInput);

}

if(currentInput.is('[type="tel"]')){

 if(currentInput.val() !== "" && currentInput.val().length != 10) showErrorTip("it should be 10 characters",currentInput);
 else  removeErrorTip(currentInput);

}

if(currentInput.is('[name="email"]')){

 if(currentInput.val() !== "" && !(/^[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]{2,3}$/).test(currentInput.val())) showErrorTip("enter valid email",currentInput);
 else  removeErrorTip(currentInput);

}

if(currentInput.is('[data-allowed="marks"]')){
 if(currentInput.val() !== "" && !(/^([0-9]{1,2})$/).test(currentInput.val())) showErrorTip("enter valid percentage",currentInput);
 else  removeErrorTip(currentInput);

}

 
});



function showErrorTip(message,currentInput){
 currentInput.next().html(message);
 currentInput.css('borderColor', 'red');
}
function removeErrorTip(currentInput){
 currentInput.next().html("");
 currentInput.css('borderColor', 'rgba(120,120,120,0.5)');

}

