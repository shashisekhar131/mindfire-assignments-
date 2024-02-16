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
            <button id="SubmitBtn" onclick="AuthenticateUser(); return false;" class="btn btn-success custom-margin-top">Login</button>
        </div>
    </div>

    <div id="tip" class="custom-margin-top"></div>

    <div class="row custom-margin-top">    
    <div class="col-md-4">
        New User?
        <button id="NewUserBtn" onclick="GoToRegisterPage(); return false;" class="btn btn-success">Register</button>
    </div>
</div>

      
    <script type="text/javascript">

        function AuthenticateUser() {
            var UserEmail = document.getElementById('UserEmail').value;  
            var UserPassword = document.getElementById('UserPassword').value;


            PageMethods.CheckIfUserExists(UserEmail,UserPassword,onSucess,onError);
            function onSucess(User) {
                

                if (User["IsUserExists"]) {
                    console.log("your a valid user");
                    document.getElementById("tip").innerHTML = "your a valid user";


                    window.location.href = "/Users.aspx";
                } else {
                    console.log("please first sign up");
                    document.getElementById("tip").innerHTML = "please create an account";               
                }
            }

            function onError(IsUserExists) {
                             
            }
        } 

        function GoToRegisterPage() {

            window.location.href = "/UserDetails.aspx";    
        }
    </script>

</asp:Content>
