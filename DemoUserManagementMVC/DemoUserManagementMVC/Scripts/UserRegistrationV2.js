﻿function loadCountries() {
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

function collectFormData() {
    // Clear UserInfo and ListofAddresses if needed
    var UserInfo = {};


    // Collect personal details
    $('[data-input="user-input"]').each(function () {
        var element = $(this);
        var propertyValue;
        var propertyName = element.attr('id') || element.attr('name');

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

        if (element.is('input[type="file"]')) {
            UserInfo[propertyName] = element;
        }

    });
    console.log(UserInfo);
    PostFormData(UserInfo);
}



function PostFormData(UserFormData) {
    let UserId = parseInt($('#UserID').val());
    alert(UserId);
    if (UserId == null) UserId = 0;

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
                UploadFile(InsertedUser);

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


function UploadFile(InsertedUser) {
    // Get the file input element

    var fileInput = document.getElementById('fileInputPAN');

    alert("Please select a file");
    var file = fileInput.files[0];

    if (!file) {
        alert("Please select a file");
        return;
    }
   
  
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
    alert(UserId);
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

        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
}

function updateStateDropDown(statedropdown, stateID) {


    $.ajax({
        url: '/UserRegistrationV2/GetStateName',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ StateID: parseInt(stateID) }),
        dataType: 'json',
        success: function (data) {
            console.log(stateID);
            $('#' + statedropdown + ' option[value="' + stateID.toString() + '"]').prop('selected', true);
            console.log($('#' + statedropdown).val());
        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
}

function updateCountryDropDown(countrydropdown, countryID) {

    if (countrydropdown == "PresentCountry") LoadStatesForCountry(countryID, "PresentState");

    if (countrydropdown == "PermanentCountry") LoadStatesForCountry(countryID, "PermanentState");



    $.ajax({
        url: '/UserRegistrationV2/GetCountryName',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ CountryID: parseInt(countryID) }),
        dataType: 'json',
        success: function (data) {

            $('#' + countrydropdown + ' option[value="' + countryID.toString() + '"]').prop('selected', true);

        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
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
