
function validateForm(e){
    // to prevent refresh 
    e.preventDefault();
    
    var inputElements = $("input");
    var j;
    for( j=0;j<inputElements.length;j++) {
  
       var currentInput = inputElements.eq(j);
       
            if (currentInput.attr('type') === 'radio'|| currentInput.attr('type') === 'checkbox') {
               continue;  
            }
           
            // Check if the current input is empty (assuming 0 means empty)
            if (currentInput.val().trim() === '') {
                showErrorTip('This field is required',currentInput);
            } else {
                removeErrorTip(currentInput);
            }            
            
            
      }

    
    // validate terms and conditions
    var temrsCheck = $('input[name="terms and conditions"]');
    var errorTip = temrsCheck.next('.error-tip');

    if (!temrsCheck.prop('checked'))errorTip.html("This is required");
    else errorTip.html(""); 

    // valiate gender and marital status
    var radioButtons = $('input[name="gender"]');
    var radioErrorTip = radioButtons.closest('.form-check').next('.error-tip');

    if (!radioButtons.is(':checked')) radioErrorTip.html("Select one option");
    else radioErrorTip.html("");


    var maritalStatusRadioButtons = $('input[name="marital-status"]');
    var maritalStatusErrorTip = maritalStatusRadioButtons.closest('.form-check').next('.error-tip');

    if (!maritalStatusRadioButtons.is(':checked'))maritalStatusErrorTip.html("Select one option");
    else maritalStatusErrorTip.html("");

    // if everything is filled check for allowedRules

     allowedRules(inputElements);
    

       // check all error-tips if there any
        var isAnyError = false;
        if ($('.error-tip:empty').length === $('.error-tip').length) {
            isAnyError = false;
        }else{
            // there is an error so don't submit
           isAnyError =true;
           
        } 
  
       // j goes to end && gender,marital,terms and conditions selected && no errors  - means all values are entered
      
      if(!isAnyError){
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
      for (let i = 0; i < inputElements.length; i++) {
        let currentInput = inputElements.eq(i);
        
        if (currentInput.prop('type') !== 'radio') {
            userDetails.push({ [currentInput.prop('name')]: currentInput.val() });
        }
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
  
  function allowedRules(inputElements){
  
      for (let i = 0; i < inputElements.length; i++) {
        let currentInput = inputElements.eq(i);
       
            if(currentInput.is('[data-allowed="text"]')){

            if (!/^[a-zA-Z]+$/.test(currentInput.val())) showErrorTip("only text allowed",currentInput);
            else removeErrorTip(currentInput);
                
            }
        
            if(currentInput.is('[type="password"]')){
        
            if(currentInput.val().length < 8) showErrorTip("minimum 8 characters",currentInput);
            else  removeErrorTip(currentInput);
        
            }
        
            if(currentInput.is('[type="tel"]')){
        
            if(currentInput.val().length != 10) showErrorTip("min 10 characters",currentInput);
            else  removeErrorTip(currentInput);
        
            }
        
            if(currentInput.is('[name="email"]')){
           
            if(!(/^[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]{2,3}$/).test(currentInput.val())) showErrorTip("enter valid email",currentInput);
            else  removeErrorTip(currentInput);
        
            }
        
            if(currentInput.is('[data-allowed="marks"]')){
        
            if(!(/^([0-9]{1,2})$/).test(currentInput.val())) showErrorTip("enter valid percentage",currentInput);
            else  removeErrorTip(currentInput);
        
            }
        
    }
 
}
  
  
  function showErrorTip(message,currentInput){
      currentInput.next().html(message);
      currentInput.css({
          borderColor: 'red',
          boxShadow: '0 0 8px rgba(255, 0, 0, 0.5)'
      });
      
  }
  function removeErrorTip(currentInput){
      currentInput.next().html("");
      currentInput.css({
          borderColor: 'rgba(120,120,120,0.5)',
          boxShadow: '0 0 0 white'
      });
  }
  
  