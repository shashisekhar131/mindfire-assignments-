<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="DemoUserManagement.Users" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

   <asp:GridView ID="userDetailsGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="UserDetailsGridView_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="UserID" HeaderText="User ID"  />
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />   
        <asp:BoundField DataField="Password" HeaderText="Password" />
        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
        <asp:BoundField DataField="AlternatePhoneNumber" HeaderText="Alternate Phone Number" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="AlternateEmail" HeaderText="Alternate Email" />
        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" />
        <asp:BoundField DataField="Favouritecolor" HeaderText="Favorite Color" />
        <asp:BoundField DataField="Aadhaar" HeaderText="Aadhaar No" />
        <asp:BoundField DataField="PAN" HeaderText="PAN" />
        <asp:BoundField DataField="PreferedLanguage" HeaderText="Preferred Language" />
    </Columns>
</asp:GridView>


  <asp:GridView ID="addressGridView" runat="server" AutoGenerateColumns="False">
    <Columns>
         <asp:BoundField DataField="UserID" HeaderText="User id"/>       
        <asp:BoundField DataField="Address" HeaderText="Address"/>
        <asp:BoundField DataField="Type" HeaderText="Type"/>
    </Columns>
</asp:GridView>

           
            <h3> edit a user </h3>
            enter Id of the user:<asp:TextBox ID="UserIdInput" runat="server"></asp:TextBox>
            <asp:Button ID="EditBtn" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="EditBtn_Click"/>
              

          
        </div>
    </form>
</body>
</html> 
