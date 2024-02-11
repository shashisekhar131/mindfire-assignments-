<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DemoUserManagement._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<main>
 <div class="container">

    <h2 class="text-center"> USER FORM </h2>

  <div class="personal-details">

        <h3> personal details </h3>

        <div class="row">
            <div class="col-sm-6">
                <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" Text="First Name:"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter here..." Text="ram" name="first-name"></asp:TextBox>    
            </div>

            <div class="col-sm-6">
                <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" Text="Last Name:"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Enter here..." Text="kumar" name="last-name"></asp:TextBox>              
            </div>
        </div>

       <div class="row">

        <div class="col-sm-6">
            <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" Text="Password:"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Text="88888888" placeholder="Enter password" name="password"></asp:TextBox>
        </div>

        <div class="col-sm-6">
            <asp:Label ID="lblRetypePassword" runat="server" AssociatedControlID="txtRetypePassword" Text="Re-type Password:"></asp:Label>
            <asp:TextBox ID="txtRetypePassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password" Text="88888888" name="re-type-password"></asp:TextBox>          
        </div>

     </div>


    <div class="row">
        <div class="col-sm-6">
            <asp:Label ID="lblPhoneNumber" runat="server" AssociatedControlID="txtPhoneNumber" Text="Phone Number:"></asp:Label>
            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Enter here..." Text="1234567890" name="ph"></asp:TextBox>          
        </div>

        <div class="col-sm-6">
            <asp:Label ID="lblAlternatePhoneNumber" runat="server" AssociatedControlID="txtAlternatePhoneNumber" Text="Alternate Phone Number:"></asp:Label>
            <asp:TextBox ID="txtAlternatePhoneNumber" runat="server" CssClass="form-control" placeholder="Enter here..." Text="1234567890" name="alternate-phone-number"></asp:TextBox>            
        </div>
    </div>

      <div class="row">
        <div class="col-sm-6">
            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter here..." Text="abc@gmail.com" name="email"></asp:TextBox>
        </div>

        <div class="col-sm-6">
            <asp:Label ID="lblAlternateEmail" runat="server" AssociatedControlID="txtAlternateEmail" Text="Alternate Email:"></asp:Label>
            <asp:TextBox ID="txtAlternateEmail" runat="server" CssClass="form-control" placeholder="Enter here..." Text="abc@gmail.com" name="alt-email"></asp:TextBox>
        </div>
     </div>


    <div class="row">
        <div class="col-sm-6">
            <asp:Label ID="lblDOB" runat="server" AssociatedControlID="txtDOB" Text="Date of Birth:"></asp:Label>
           <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" placeholder="Enter here..." name="dob" Text="2024-02-10"></asp:TextBox>
        </div>

        <div class="col-sm-6">
            <asp:Label ID="lblFavouriteColor" runat="server" AssociatedControlID="txtFavouriteColor" Text="Favorite Color:"></asp:Label>
            <asp:TextBox ID="txtFavouriteColor" runat="server" CssClass="form-control" placeholder="Enter here..." Text="red" name="fav-color" ></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <asp:Label ID="lblAadhaar" runat="server" AssociatedControlID="txtAadhaar" Text="Aadhaar No:"></asp:Label>
            <asp:TextBox ID="txtAadhaar" runat="server" CssClass="form-control" placeholder="Enter here..." Text="123 456 789" name="Aadhaar"></asp:TextBox>     
        </div>

        <div class="col-sm-6">
            <asp:Label ID="lblPAN" runat="server" AssociatedControlID="txtPAN" Text="PAN:"></asp:Label>
            <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" placeholder="Enter here..." Text="123A123B" name="PAN"></asp:TextBox>
        </div>
    </div>


      
       <div class="row">
        <div class="col-sm-6">
    <asp:Label ID="lblMaritalStatus" runat="server" AssociatedControlID="maritalStatus">Marital Status:</asp:Label>
    <asp:RadioButtonList ID="maritalStatus" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Text="Married" Value="married" />
        <asp:ListItem Text="Unmarried" Value="unmarried" />
    </asp:RadioButtonList>
   
</div>
        <div class="col-sm-6">
            <asp:Label ID="lblLanguage" runat="server" Text="Preferred Language:"></asp:Label>
            <asp:DropDownList ID="language" runat="server" CssClass="form-control">
                <asp:ListItem Text="English" Value="English" />
                <asp:ListItem Text="Hindi" Value="Hindi" />
                <asp:ListItem Text="Others" Value="Others" />
                
            </asp:DropDownList>
        </div>
    </div>




      <div class="row">
   
    <div class="col-sm-6">
       
        <div class="form-group">
            <asp:Label ID="lblPresentAddress" runat="server" AssociatedControlID="txtPresentAddress"> present address:</asp:Label>
            <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" placeholder="Enter present address" Text="xyz"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-6">
       
        <div class="form-group">
            <asp:Label ID="lblPermanentAddress" runat="server" AssociatedControlID="txtPermanentAddress">permanent Address:</asp:Label>
            <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" placeholder="Enter permanent address" Text="abc"></asp:TextBox>
        </div>
    </div>
</div>








  </div>
      
  <div class="education-details">
    <h3> Educational Details </h3>

    <div class="row">
        <div class="col-sm-6">
            <asp:Label ID="lblPrimaryEducation" runat="server" AssociatedControlID="txtPrimaryEducation" Text="Primary Education (up to 10th):"></asp:Label>
            <asp:TextBox ID="txtPrimaryEducation" runat="server" CssClass="form-control" placeholder="Enter here..." Text="xyz" name="primary education(upto 10th)"></asp:TextBox>
        </div>
        <div class="col-sm-6">
            <asp:Label ID="lblPercentageIn10th" runat="server" AssociatedControlID="txtPercentageIn10th" Text="Percentage in 10th:"></asp:Label>
            <asp:TextBox ID="txtPercentageIn10th" runat="server" CssClass="form-control" placeholder="Enter here..." Text="99" name="percentage in 10th" data-allowed="marks"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <asp:Label ID="lblIntermediateEducation" runat="server" AssociatedControlID="txtIntermediateEducation" Text="Intermediate Education:"></asp:Label>
            <asp:TextBox ID="txtIntermediateEducation" runat="server" CssClass="form-control" placeholder="Enter here..." Text="xyz" name="intermediate education"></asp:TextBox>
        </div>
        <div class="col-sm-6">
            <asp:Label ID="lblIntermediatePercentage" runat="server" AssociatedControlID="txtIntermediatePercentage" Text="Intermediate Percentage:"></asp:Label>
            <asp:TextBox ID="txtIntermediatePercentage" runat="server" CssClass="form-control" placeholder="Enter here..." Text="99" name="intermediate-percentage" data-allowed="marks"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <asp:Label ID="lblBTech" runat="server" AssociatedControlID="txtBTech" Text="BTech:"></asp:Label>
            <asp:TextBox ID="txtBTech" runat="server" CssClass="form-control" placeholder="Enter here..." Text="xyz" name="btech"></asp:TextBox>
        </div>
         <div class="col-sm-6">
            <asp:Label ID="lblBTechPercentage" runat="server" AssociatedControlID="txtBTechPercentage" Text="BTech Percentage:"></asp:Label>
            <asp:TextBox ID="txtBTechPercentage" runat="server" CssClass="form-control" placeholder="Enter here..." Text="99" name="btech-percentage" data-allowed="marks"></asp:TextBox>
         </div>
    </div>

</div>


    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="BtnReset_Click" />


</div>

    </main>

</asp:Content>
