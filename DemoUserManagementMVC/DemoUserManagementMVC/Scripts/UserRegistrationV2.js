function loadCountries() {
    $.ajax({
        url: '/UserRegistrationV2/GetCountries', // Replace with your actual server endpoint
        type: 'POST',  // Use POST for WebMethods
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            var countries = data;

            var Countrydropdowns = ['PresentCountry', 'PermanentCountry'];
            var Statedropdowns = ['PresentState', 'PermanentState'];

            for (var i = 0; i < Countrydropdowns.length; i++) {
                var CountrydropdownId = Countrydropdowns[i];

                // Get the dropdown element
                var Countrydropdown = $('#' + CountrydropdownId);

                // Clear existing options
                Countrydropdown.empty();

                // Add options from the received data
                for (var j = 0; j < countries.length; j++) {
                    Countrydropdown.append($('<option>', { value: countries[j].CountryID, text: countries[j].CountryName }));
                }

                LoadStatesForCountry(countries[0].CountryID, Statedropdowns[i]);

            }

        },
        error: function (xhr, status, error) {
            console.log('Error loading countries: ', error);
            console.log(xhr.responseText);
        }

    });
}



function LoadStatesForCountry(CountryID, StatedropdownId) {
    $.ajax({
        url: '/UserRegistrationV2/GetStatesForCountry',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ SelectedCountryID: CountryID }), // Send the countryName parameter
        dataType: 'json',
        success: function (data) {

            var states = data;

            var Statedropdown = $('#' + StatedropdownId);

            Statedropdown.empty();

            $.each(states, function (index, state) {
                Statedropdown.append($('<option>', { value: state.StateID, text: state.StateName }));
            });



        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
}

function CountrySelected(CountrydropdownId, StatedropdownId) {
    var selectedCountry = $('#' + CountrydropdownId).val();
    LoadStatesForCountry(selectedCountry, StatedropdownId);
}

function passwordValidation() {
    var password = $('#Password').val().trim();
    var retypePassword = $('#RetypePassword').val().trim();

    // Check if passwords match
    if (password !== retypePassword) {
        $('#RetypePassword').addClass('is-invalid');

        var errorMessage = $('<span class="error-message text-danger">Passwords do not match.</span>');

        // Insert error message after RetypePassword input element
        $('#RetypePassword').after(errorMessage);
        return false;
    }
    return true;
}

function emailValidation() {
    // Add your email validation regular expression
    var emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
    // Validate each email input
    $('[type="email"]').each(function () {
        var emailInput = $(this);
        var emailValue = emailInput.val().trim();

        if (!emailRegex.test(emailValue)) {
            var errorMessage = $('<span class="error-message text-danger ">Invalid email format.</span>');

            // Insert error message after the email input element
            emailInput.after(errorMessage);
            emailInput.addClass('is-invalid');
        }
    });
    return true;
}

function onlyNumberValidation() {
    var onlyNumbersRegex = /^[0-9]+$/;
    
    // Validate each input with data-allowed="digits"
    $('[data-allowed="digits"]').each(function () {
        var inputElement = $(this);
        var inputValue = inputElement.val().trim();

        if (!onlyNumbersRegex.test(inputValue)) {
            // Add 'is-invalid' class to the input element
            inputElement.addClass('is-invalid');

            // Create and insert error message
            var errorMessage = $('<span class="error-message text-danger">Only numbers are allowed.</span>');
            inputElement.after(errorMessage);

        }
    });

    return true;
}

function collectFormData() {
    // Clear UserInfo and ListofAddresses if needed
    var UserInfo = {};

    var isValid = true;
    // remove all error messages elements added
    $('.error-message').remove();
    // remove  bootstrap class for all input elemnts
    $('.is-invalid').removeClass('is-invalid');

    // password check 
  isValid = passwordValidation();
    isValid = emailValidation();
    isValid =  onlyNumberValidation();
   
    // Collect personal details
    $('[data-input="user-input"]').each(function () {
        var element = $(this);
        var propertyValue;
        var propertyName = element.attr('id') || element.attr('name');

        if (element.val() === undefined || element.val() === null || element.val().trim() === '') {
            isValid = false;
            element.addClass('is-invalid');

            var errorMessage = $('<span class="error-message text-danger">Please fill in this field.</span>');

            // Insert error message after input element
            element.after(errorMessage);
        }
        if (element.is('input') || element.is('select')) {
            propertyValue = element.val();
            UserInfo[propertyName] = propertyValue;
        }


        if (element.is('input[type="radio"]')) {
            if (element.prop('checked')) {
                propertyValue = element.val();
                UserInfo[propertyName] = propertyValue;
            }
        }       

    });
    if (isValid) {
        PostFormData(UserInfo);
    }
}



function PostFormData(UserFormData) {
    let UserId = parseInt($('#UserID').val());
    if (isNaN(UserId)) {
        UserId = 0;
    }

    $.ajax({
        url: '/UserRegistrationV2/Submit_Form',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ UserFormData, UserId }),
        dataType: 'json',
        success: function (data) {

            console.log(data);
            if (UserId == 0) {

                var InsertedUser = data;
                console.log("uploading the file");
                var fileInputPAN = document.getElementById('fileInputPAN');
                var fileInputAadhaar = document.getElementById('fileInputAadhaar');

                if (!fileInputAadhaar.files[0] && !fileInputPAN.files[0]) {
                    window.location.href = "/Users/GetAllUsers";
                } else if (!fileInputAadhaar.files[0] && fileInputPAN.files[0]) {
                    UploadFile(InsertedUser, fileInputPAN);
                } else if (fileInputAadhaar.files[0] && !fileInputPAN.files[0]) {
                    UploadFile(InsertedUser, fileInputAadhaar);
                } else {
                    UploadFile(InsertedUser, fileInputAadhaar);
                    UploadFile(InsertedUser, fileInputPAN);
                }


               


            } else  {
                var Message = data;
                window.location.href = "/Users/GetAllUsers";

            }


        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });

}  


function UploadFile(InsertedUser, fileInput) {
    // Get the file input element

   
    var file = fileInput.files[0];   
  
    var formData = new FormData();
    formData.append("file", file);
    // uploading from the form directly so no file type selection there so 4- others, object type is usersDetails
    formData.append("DocumentType", 4);
    formData.append("ObjectType", 1);
    formData.append("ObjectID", InsertedUser["UserID"]);

    $.ajax({
        url: '/UserRegistrationV2/UploadDocument',
        type: 'POST',
        processData: false,  // Important! Don't process the files
        contentType: false,  // Important! Set content type to false
        data: formData,
        dataType: 'json',
        success: function (data) {
            // Handle file upload success if needed
            console.log("file uploaded");
         window.location.href = "/Users/GetAllUsers";

        },
        error: function (xhr, status, error) {
            console.log('Error uploading file: ', error);
            console.log(xhr.responseText);
        }
    });
}
function PopulateValuesFromDBIntoForm(UserId) {
    $.ajax({
        url: '/UserRegistrationV2/GetUserData',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ UserId }),
        dataType: 'json',
        success: function (data) {

            var userData = data;
            console.log(data);
            console.log(userData.PresentState + "big thans");

            for (var property in userData) {
                if (userData.hasOwnProperty(property)) {
                    var value = userData[property];

                    // update all other fields as property name is same as id
                    $('#' + property).val(value);
                    // update dropdowns
                    if (property == "PresentState" || property == "PermanentState") updateStateDropDown(property, value);
                    if (property == "PresentCountry" || property == "PermanentCountry") {
                        updateCountryDropDown(property, value);
                    }
                }
            }
            $('#RoleMessage').html("user is currently " + userData.UserRole);
            if (userData.UserRole == "StandardUser") {
                $('#UserRole').hide();
            } else {
                $('#UserRole').show();
            }

            // set the re-password field  as it doesn't comes from db
            $('#RetypePassword').val(userData.Password);

            // set the data as it comes in format with time
            var dateString = userData.DateOfBirth;
            var dateObject = new Date(dateString);
            // Format the date as YYYY-MM-DD (required format for HTML date input)
            var formattedDate = dateObject.toISOString().split('T')[0];
            $('#DateOfBirth').val(formattedDate).val();


        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
}

function updateStateDropDown(statedropdown, stateID) {
      $('#' + statedropdown + ' option[value="' + stateID.toString() + '"]').prop('selected', true);
          
}

function updateCountryDropDown(countrydropdown, countryID) {

    if (countrydropdown == "PresentCountry") LoadStatesForCountry(countryID, "PresentState");

    if (countrydropdown == "PermanentCountry") LoadStatesForCountry(countryID, "PermanentState");

    $('#' + countrydropdown + ' option[value="' + countryID.toString() + '"]').prop('selected', true);

       
}
function CheckIfEmailExists(email) {
    console.log(id);
    var id = new URLSearchParams(window.location.search).get('id');
    if (id == null) id = 0;

    // Send AJAX request to the backend
    $.ajax({
        type: "POST",
        url: "/UserRegistrationV2/CheckIfEmailExists", // Replace with your actual backend endpoint
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ Email: email, UserID: id }),
        dataType: 'json',
        success: function (response) {

            if (response) {
                $('#EmailErrorTip').html("email already exists").css('color', 'red');
            } else {
                $('#EmailErrorTip').html("can proceed with this email").css('color', 'green');

                $('#MainError').html("");
            }
        },
        error: function () {
        }
    });
}

$(document).ready(function () {

    // for the first time
    loadCountries();


    // user selected country
    $('#PresentCountry').on('change', function () {
        CountrySelected('PresentCountry', 'PresentState');
    });
    $('#PermanentCountry').on('change', function () {
        CountrySelected('PermanentCountry', 'PermanentState');
    });


    if ($('#UserID').val() != null) {

        PopulateValuesFromDBIntoForm($('#UserID').val());
        $('#BtnSubmit').html("Save");
        $('#BtnReset').hide();
    } else {
        $('#BtnSubmit').html("Submit");
        $('#BtnReset').show();
    }



    $('#BtnSubmit').on('click', function (event) {
        event.preventDefault(); // Prevent the default form submission

        if ($("#EmailErrorTip").html() != "email already exists") {
            // Collect data when the form is submitted

            collectFormData();
        } else {
            $('#MainError').html("without proper email you can't proceed").css('color', 'red');
        }


    });

    $("#Email").on('input', function () {
        var email = $(this).val();

        CheckIfEmailExists(email);
    });

});

