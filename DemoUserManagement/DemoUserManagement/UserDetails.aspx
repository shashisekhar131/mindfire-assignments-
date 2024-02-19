<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="DemoUserManagement.UserDetails" %>

<%@ Register Src="~/CustomUserControl.ascx" TagName="Note" TagPrefix="uc" %>

<%@ Register Src="~/DocumentUserControl.ascx" TagName="DocumentUpload" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
<main>
 <div class="container">

    <h2 class="text-center"> USER FORM </h2>

  <div class="personal-details">

        <h3> personal details </h3>

      <div class="row">
        <div class="col-sm-6">
            <label for="FirstName">First Name:</label>
            <input type="text" id="FirstName" class="form-control" placeholder="Enter here..." value="ram" name="first-name" data-custom="user-input" >
        </div>

        <div class="col-sm-6">
            <label for="LastName">Last Name:</label>
            <input type="text" id="LastName" class="form-control" placeholder="Enter here..." value="kumar" name="last-name" data-custom="user-input">
        </div>
    </div>

      <div class="row">
        <div class="col-sm-6">
            <label for="Password">Password:</label>
            <input type="password" id="Password" class="form-control" value="88888888" placeholder="Enter password" name="password" data-custom="user-input">
        </div>

        <div class="col-sm-6">
            <label for="RetypePassword">Re-type Password:</label>
            <input type="password" id="RetypePassword" class="form-control" value="88888888" placeholder="Enter password" name="re-type-password" data-custom="user-input">
        </div>
    </div>


    <div class="row">
        <div class="col-sm-6">
            <label for="PhoneNumber">Phone Number:</label>
            <input type="text" id="PhoneNumber" class="form-control" value="1234567890" placeholder="Enter here..." name="ph" data-custom="user-input">
        </div>

        <div class="col-sm-6">
            <label for="AlternatePhoneNumber">Alternate Phone Number:</label>
            <input type="text" id="AlternatePhoneNumber" class="form-control" value="1234567890" placeholder="Enter here..." name="alternate-phone-number" data-custom="user-input">
        </div>
   </div>

      <div class="row">
        <div class="col-sm-6">
            <label for="Email">Email:</label>
            <input type="text" id="Email" class="form-control" placeholder="Enter here..." value="abc@gmail.com" name="email" data-custom="user-input">
            <span id="EmailErrorTip"></span>
        </div>

        <div class="col-sm-6">
            <label for="AlternateEmail">Alternate Email:</label>
            <input type="text" id="AlternateEmail" class="form-control" placeholder="Enter here..." value="abc@gmail.com" name="alt-email" data-custom="user-input">
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <label for="DOB">Date of Birth:</label>
            <input type="date" id="DOB" class="form-control" placeholder="Enter here..." value="2024-02-10" name="dob" data-custom="user-input">
        </div>

        <div class="col-sm-6">
            <label for="FavouriteColor">Favorite Color:</label>
            <input type="text" id="FavouriteColor" class="form-control" placeholder="Enter here..." value="red" name="fav-color" data-custom="user-input">
        </div>
    </div>

      <div class="row">
    <div class="col-sm-6">
        <asp:Label runat="server" ID="lblPAN" AssociatedControlID="fuPAN">PAN Picture:</asp:Label>
        <asp:FileUpload runat="server" ID="fuPAN" CssClass="form-control"/>
    </div>

    <div class="col-sm-6">
        <asp:Label runat="server" ID="lblAadhar" AssociatedControlID="fuAadhar">Aadhar Picture:</asp:Label>
        <asp:FileUpload runat="server" ID="fuAadhar" CssClass="form-control"/>
    </div>
</div>



       <div class="row">
        <div class="col-sm-6">
            <label for="maritalStatus">Marital Status:</label>
            <input type="radio"  name="maritalStatus" value="married" data-custom="user-input"> Married
            <input type="radio"  name="maritalStatus" value="unmarried" data-custom="user-input"> Unmarried
        </div>
    
        <div class="col-sm-6">
            <label for="language">Preferred Language:</label>
            <select id="language" class="form-control" data-custom="user-input">
                <option value="English">English</option>
                <option value="Hindi">Hindi</option>
                <option value="Others">Others</option>
            </select>
        </div>
    </div>


      <div class="row">
    <div class="col-sm-6">
        <label for="PresentCountry">Present Country:</label>
        <select id="PresentCountry" class="form-control"  data-custom="user-input">
        </select>
    </div>

    <div class="col-sm-6">
        <label for="PermanentCountry">Permanent Country:</label>
        <select id="PermanentCountry" class="form-control"  data-custom="user-input">
        </select>
    </div>
</div>

<div class="row">
    <div class="col-sm-6">
        <label for="PresentState">Present State:</label>
        <select id="PresentState" class="form-control" placeholder="Select state" data-custom="user-input">
        </select>
    </div>

    <div class="col-sm-6">
        <label for="PermanentState">Permanent State:</label>
        <select id="PermanentState" class="form-control" placeholder="Select state" data-custom="user-input">
        </select>
    </div>
</div>



      <div class="row">   
    <div class="col-sm-6">
        <div class="form-group">
            <label for="PresentAddress">Present Address (street):</label>
            <input type="text" id="PresentAddress" class="form-control" placeholder="Enter present address" value="xyz" data-custom="user-input">
        </div>
    </div>

    <div class="col-sm-6">
        <div class="form-group">
            <label for="PermanentAddress">Permanent Address (street):</label>
            <input type="text" id="PermanentAddress" class="form-control" placeholder="Enter permanent address" value="abc" data-custom="user-input">
        </div>
    </div>
</div>



     

  </div>
      
  <div class="education-details">
    <h3> Educational Details </h3>

        <div class="row">
        <div class="col-sm-6">
            <label for="PrimaryEducation">Primary Education (up to 10th):</label>
            <input type="text" id="PrimaryEducation" class="form-control" placeholder="Enter here..." value="xyz" name="primary education(upto 10th)" data-custom="user-input">
        </div>
        <div class="col-sm-6">
            <label for="PercentageIn10th">Percentage in 10th:</label>
            <input type="text" id="PercentageIn10th" class="form-control" placeholder="Enter here..." value="99" name="percentage in 10th" data-allowed="marks" data-custom="user-input">
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <label for="IntermediateEducation">Intermediate Education:</label>
            <input type="text" id="IntermediateEducation" class="form-control" placeholder="Enter here..." value="xyz" name="intermediate education" data-custom="user-input">
        </div>
        <div class="col-sm-6">
            <label for="IntermediatePercentage">Intermediate Percentage:</label>
            <input type="text" id="IntermediatePercentage" class="form-control" placeholder="Enter here..." value="99" name="intermediate-percentage" data-allowed="marks" data-custom="user-input">
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <label for="BTech">BTech:</label>
            <input type="text" id="BTech" class="form-control" placeholder="Enter here..." value="xyz" name="btech" data-custom="user-input">
        </div>
        <div class="col-sm-6">
            <label for="BTechPercentage">BTech Percentage:</label>
            <input type="text" id="BTechPercentage" class="form-control" placeholder="Enter here..." value="99" name="btech-percentage" data-allowed="marks" data-custom="user-input">
        </div>
    </div>



      
    <div class="row">       
        <div class="col-sm-6">            
        <label for="UserRole">select your role</label>
        <select id="UserRole" class="form-control" data-custom="user-input">
            <option value="StandardUser">standard user</option>
            <option value="Admin">Admin</option>
    
        </select>
        <span id="RoleMessage"></span>
        </div>
    </div>

</div>

              
    <uc:Note runat="server" ObjectType="1" ID ="NotesInUsersPage" />
    <uc:DocumentUpload runat="server" ObjectType="1" ID="UploadInUsersPage" />

    <span id="MainError"></span>
     <button type="button" id="BtnSubmit" class="btn btn-success" >Submit</button>
    <button type="button" id="BtnReset" class="btn btn-danger" onclick="BtnReset_Click()">Reset</button>


</div>

    </main>

    
    <script type="text/javascript">


        function loadCountries() {
            $.ajax({
                url: 'UserDetails.aspx/GetCountries', // Replace with your actual server endpoint
                type: 'POST',  // Use POST for WebMethods
                contentType: 'application/json; charset=utf-8',
                success: function (data) {                 

                    var countries = data.d;

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
                            Countrydropdown.append($('<option>', { value: countries[j], text: countries[j] }));
                        }
                        // Add default option
                        Countrydropdown.append($('<option>', { value: countries[0], text: countries[0] }));

                        LoadStatesForCountry(countries[0], Statedropdowns[i]);

                    }

                },
                error: function (xhr, status, error) {
                    console.log('Error loading countries: ', error);
                    console.log(xhr.responseText);
                }

            });
        }



        function LoadStatesForCountry(CountryName,StatedropdownId) {
            $.ajax({
                url: 'UserDetails.aspx/GetStatesForCountry',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ CountryName }), // Send the countryName parameter
                dataType: 'json',
                success: function (data) {

                        var states = data.d;
                        var Statedropdown = $('#' + StatedropdownId);

                        Statedropdown.empty();
                        
                        $.each(states, function (index, state) {
                            Statedropdown.append($('<option>', { value: state, text: state }));
                        });
                    Statedropdown.append($('<option>', { value: states[0], text: states[0] }));

                    

                },
                error: function (xhr, status, error) {
                    console.log('Error loading states: ', error);
                    console.log(xhr.responseText);
                }
            });
        }

        function CountrySelected(CountrydropdownId,StatedropdownId) {
            var selectedCountry = $('#' + CountrydropdownId).val();
            LoadStatesForCountry(selectedCountry,StatedropdownId);
        }

        function collectFormData() {
            // Clear UserInfo and ListofAddresses if needed
            var UserInfo = {};
            

            // Collect personal details
            $('[data-custom="user-input"]').each(function () {
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
            let urlParams = new URLSearchParams(window.location.search);
            let UserId = urlParams.get('id');           
            if (UserId == null) UserId = 0;

            $.ajax({
                url: 'UserDetails.aspx/Submit_Form',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ UserFormData, UserId }),
                dataType: 'json',
                success: function (data) {

                    if (UserId == 0) {
                        var InsertedUser = data.d;

                        if (InsertedUser.RoleId == 1) window.location.href = "/Users.aspx";
                        else window.location.href = "/Users.aspx?id=" + InsertedUser.UserID;

                    } else {
                        var Message = data.d;                       
                        window.location.href = "/Users.aspx";

                    }
                    

                },
                error: function (xhr, status, error) {
                    console.log('Error loading states: ', error);
                    console.log(xhr.responseText);
                }
            });

        }

        function PopulateValuesFromDBIntoForm(UserId) {

            $.ajax({
                url: 'UserDetails.aspx/GetUserData',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({UserId}),
                dataType: 'json',
                success: function (data) {

                     var userData = data.d;
                    for (var property in userData) {
                        if (userData.hasOwnProperty(property)) {
                            var value = userData[property];
                            // property names match the id attributes of the form fields so we can use loop other wise manually set values by getting each element
                            $('#' + property).val(value);
                        }
                    }
                    var userRoleDropDown = $('#RoleMessage').html("your currently" + userData.UserRole);


                },
                error: function (xhr, status, error) {
                    console.log('Error loading states: ', error);
                    console.log(xhr.responseText);
                }
            });
        }

        function CheckIfEmailExists(email) {

            // Send AJAX request to the backend
            $.ajax({
                type: "POST",
                url: "UserDetails.aspx/CheckIfEmailExists", // Replace with your actual backend endpoint
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ Email: email }),
                dataType: 'json',
                success: function (response) {
                    
                    if (response.d) {
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
                CountrySelected('PresentCountry','PresentState');
            });
            $('#PermanentCountry').on('change', function () {
                CountrySelected('PermanentCountry','PermanentState');
            });


            if (new URLSearchParams(window.location.search).get('id') != null) {
                PopulateValuesFromDBIntoForm(new URLSearchParams(window.location.search).get('id'));
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

            $("#Email").on('input',function () {
                var email = $(this).val();
              
                CheckIfEmailExists(email);
            });       



        });

        

        

    </script>

</asp:Content>
