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
            <input type="text" id="FirstName" class="form-control" placeholder="Enter here..." value="ram" name="first-name" data-input="user-input" >
        </div>

        <div class="col-sm-6">
            <label for="LastName">Last Name:</label>
            <input type="text" id="LastName" class="form-control" placeholder="Enter here..." value="kumar" name="last-name" data-input="user-input">
        </div>
    </div>

      <div class="row">
        <div class="col-sm-6">
            <label for="Password">Password:</label>
            <input type="password" id="Password" class="form-control" value="88888888" placeholder="Enter password" name="password" data-input="user-input">
        </div>

        <div class="col-sm-6">
            <label for="RetypePassword">Re-type Password:</label>
            <input type="password" id="RetypePassword" class="form-control" value="88888888" placeholder="Enter password" name="re-type-password" data-input="user-input">
        </div>
    </div>


    <div class="row">
        <div class="col-sm-6">
            <label for="PhoneNumber">Phone Number:</label>
            <input type="text" id="PhoneNumber" class="form-control" value="1234567890" placeholder="Enter here..." name="ph" data-input="user-input">
        </div>

        <div class="col-sm-6">
            <label for="AlternatePhoneNumber">Alternate Phone Number:</label>
            <input type="text" id="AlternatePhoneNumber" class="form-control" value="1234567890" placeholder="Enter here..." name="alternate-phone-number" data-input="user-input">
        </div>
   </div>

      <div class="row">
        <div class="col-sm-6">
            <label for="Email">Email:</label>
            <input type="email" id="Email" class="form-control" placeholder="Enter here..."  name="email" data-input="user-input" required >
            <span id="EmailErrorTip"></span>
        </div>

        <div class="col-sm-6">
            <label for="AlternateEmail">Alternate Email:</label>
            <input type="text" id="AlternateEmail" class="form-control" placeholder="Enter here..."  name="alt-email" data-input="user-input">
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <label for="DOB">Date of Birth:</label>
            <input type="date" id="DOB" class="form-control" placeholder="Enter here..." value="2024-02-10" name="dob" data-input="user-input">
        </div>

        <div class="col-sm-6">
            <label for="FavouriteColor">Favorite Color:</label>
            <input type="text" id="FavouriteColor" class="form-control" placeholder="Enter here..." value="red" name="fav-color" data-input="user-input">
        </div>
    </div>

      <div class="row">
    <div class="col-sm-6">
        <label for="fileInputPAN">PAN file:</label>
        <input type="file" id="fileInputPAN" class="form-control" />
    </div>

    <div class="col-sm-6">
        <label for="fileInputAadhaar">Aadhaar file:</label>
        <input type="file" id="fileInputAadhaar" class="form-control" />
    </div>
</div>



       <div class="row">
        <div class="col-sm-6">
            <label for="maritalStatus">Marital Status:</label>
            <input type="radio"  name="maritalStatus" value="married" data-input="user-input"> Married
            <input type="radio"  name="maritalStatus" value="unmarried" data-input="user-input"> Unmarried
        </div>
    
        <div class="col-sm-6">
            <label for="language">Preferred Language:</label>
            <select id="language" class="form-control" data-input="user-input">
                <option value="English">English</option>
                <option value="Hindi">Hindi</option>
                <option value="Others">Others</option>
            </select>
        </div>
    </div>


      <div class="row">
    <div class="col-sm-6">
        <label for="PresentCountry">Present Country:</label>
        <select id="PresentCountry" class="form-control"  data-input="user-input">
        </select>
    </div>

    <div class="col-sm-6">
        <label for="PermanentCountry">Permanent Country:</label>
        <select id="PermanentCountry" class="form-control"  data-input="user-input">
        </select>
    </div>
</div>

<div class="row">
    <div class="col-sm-6">
        <label for="PresentState">Present State:</label>
        <select id="PresentState" class="form-control" placeholder="Select state" data-input="user-input">
        </select>
    </div>

    <div class="col-sm-6">
        <label for="PermanentState">Permanent State:</label>
        <select id="PermanentState" class="form-control" placeholder="Select state" data-input="user-input">
        </select>
    </div>
</div>



      <div class="row">   
    <div class="col-sm-6">
        <div class="form-group">
            <label for="PresentAddress">Present Address (street):</label>
            <input type="text" id="PresentAddress" class="form-control" placeholder="Enter present address" value="xyz" data-input="user-input">
        </div>
    </div>

    <div class="col-sm-6">
        <div class="form-group">
            <label for="PermanentAddress">Permanent Address (street):</label>
            <input type="text" id="PermanentAddress" class="form-control" placeholder="Enter permanent address" value="abc" data-input="user-input">
        </div>
    </div>
</div>



     

  </div>
      
  <div class="education-details">
    <h3> Educational Details </h3>

        <div class="row">
        <div class="col-sm-6">
            <label for="PrimaryEducation">Primary Education (up to 10th):</label>
            <input type="text" id="PrimaryEducation" class="form-control" placeholder="Enter here..." value="xyz" name="primary education(upto 10th)" data-input="user-input">
        </div>
        <div class="col-sm-6">
            <label for="PercentageIn10th">Percentage in 10th:</label>
            <input type="text" id="PercentageIn10th" class="form-control" placeholder="Enter here..." value="99" name="percentage in 10th" data-allowed="marks" data-input="user-input">
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <label for="IntermediateEducation">Intermediate Education:</label>
            <input type="text" id="IntermediateEducation" class="form-control" placeholder="Enter here..." value="xyz" name="intermediate education" data-input="user-input">
        </div>
        <div class="col-sm-6">
            <label for="IntermediatePercentage">Intermediate Percentage:</label>
            <input type="text" id="IntermediatePercentage" class="form-control" placeholder="Enter here..." value="99" name="intermediate-percentage" data-allowed="marks" data-input="user-input">
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <label for="BTech">BTech:</label>
            <input type="text" id="BTech" class="form-control" placeholder="Enter here..." value="xyz" name="btech" data-input="user-input">
        </div>
        <div class="col-sm-6">
            <label for="BTechPercentage">BTech Percentage:</label>
            <input type="text" id="BTechPercentage" class="form-control" placeholder="Enter here..." value="99" name="btech-percentage" data-allowed="marks" data-input="user-input">
        </div>
    </div>



      
    <div class="row">       
        <div class="col-sm-6">            
        <label for="UserRole">select your role</label>
        <select id="UserRole" class="form-control" data-input="user-input">
            <option value="Admin">Admin</option>
            <option value="StandardUser">StandardUser</option>            
    
        </select>
        <span id="RoleMessage"></span>
        </div>
    </div>

</div>

     <input type="hidden" value="1" id="NoteInObjectType" />        
     <input type="hidden" value="1" id="DocumentInObjectType" />        
         
         
    <uc:Note runat="server" ObjectType="1" ID ="NotesInUsersPage" />
    <uc:DocumentUpload runat="server" ObjectType="1" ID="UploadInUsersPage" />

    <span id="MainError"></span>
     <button type="button" id="BtnSubmit" class="btn btn-success input-margin-top" >Submit</button>
    <button type="button" id="BtnReset" class="btn btn-danger input-margin-top" onclick="BtnReset_Click()">Reset</button>


</div>

    </main>

    
    <script  type="text/javascript" src="Scripts/UserDetails.js"> </script>

</asp:Content>
