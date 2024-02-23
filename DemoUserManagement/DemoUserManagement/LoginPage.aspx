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
        <button id="RegisterBtn"  class="btn btn-success">Register</button>
    </div>
</div>

      
    <script type="text/javascript" src="Scripts/LoginPage.js">    </script>

</asp:Content>
