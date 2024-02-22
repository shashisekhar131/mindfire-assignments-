<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="DemoUserManagement.LoginPage" %>

<asp:Content ID="LoginPage" ContentPlaceHolderID="MainContent" runat="server">

      <div class="row">    
        <div class="col-md-4">
            <label for="UserEmail">Email:</label>
            <input type="text" id="UserEmail" class="form-control">
        </div> 
    </div>
                                              
    <div class="row">    
        <div class="col-md-4">
            <label for="UserPassword">Password:</label>
            <input type="password" id="UserPassword" class="form-control">
        </div>
    </div>

    
    <div class="row">    
        <div class="col-md-4">
            <button id="SubmitBtn"  class="btn btn-success custom-margin-top">Login</button>
        </div>
    </div>

      <div id="tip" class="custom-margin-top"></div>

    <div class="row custom-margin-top">    
    <div class="col-md-4">
        New User?
        <button id="NewUserBtn"  class="btn btn-success">Register</button>
    </div>
</div>

      
    <script type="text/javascript">

     

        function AuthenticateUser(UserEmail,UserPassword) {
            $.ajax({
                url: 'LoginPage.aspx/CheckIfUserExists',                
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({UserEmail,UserPassword }), // Send the countryName parameter
                dataType: 'json',
                success: function (data) {
                    console.log(data.d);
                    var User = data.d;
                    if (User["IsUserExists"]) {
                        console.log("your a valid user");
                        document.getElementById("tip").innerHTML = "your a valid user";
                        window.location.href = "/UserDetails.aspx?id=" + User["UserID"];
                    } else {
                        console.log("please first sign up");
                        document.getElementById("tip").innerHTML = "please create an account";
                    }


                },
                error: function (xhr, status, error) {
                    console.log('Error loading states: ', error);
                    console.log(xhr.responseText);
                }
            });
        }
       

        $(document).ready(function () {

            $('#SubmitBtn').on('click', function (event) {
                event.preventDefault(); // Prevent the default form submission
                console.log("entered" + $('#UserEmail').val()+ $('#UserPassword').val());
                AuthenticateUser($('#UserEmail').val(), $('#UserPassword').val());

            });

            $('#NewUserBtn').on('click', function (event) {
                event.preventDefault();
                window.location.href = "/UserDetails.aspx"; 
            })

        });

    </script>

</asp:Content>
