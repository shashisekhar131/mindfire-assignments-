<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="DemoUserManagement.Users" %>

<%@ Register Src="~/CustomUserControl.ascx"  TagName="CustomControl" TagPrefix="custom" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
<asp:GridView ID="userDetailsGridView" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderText="Details">
            <ItemTemplate>
                <table>
                    <tr>
                        <td><b>First Name:</b></td>
                        <td><%# Eval("FirstName") %></td>
                    </tr>
                    <tr>
                        <td><b>Last Name:</b></td>
                        <td><%# Eval("LastName") %></td>
                    </tr>
                    <tr>
                        <td><b>Password:</b></td>
                        <td><%# Eval("Password") %></td>
                    </tr>
                    <tr>
                        <td><b>Phone Number:</b></td>
                        <td><%# Eval("PhoneNumber") %></td>
                    </tr>
                    <tr>
                        <td><b>Alternate Phone Number:</b></td>
                        <td><%# Eval("AlternatePhoneNumber") %></td>
                    </tr>
                    <tr>
                        <td><b>Email:</b></td>
                        <td><%# Eval("Email") %></td>
                    </tr>
                    <tr>
                        <td><b>Alternate Email:</b></td>
                        <td><%# Eval("AlternateEmail") %></td>
                    </tr>
                    <tr>
                        <td><b>Date of Birth:</b></td>
                        <td><%# Eval("DOB") %></td>
                    </tr>
                    <tr>
                        <td><b>Favorite Color:</b></td>
                        <td><%# Eval("Favouritecolor") %></td>
                    </tr>
                    <tr>
                        <td><b>Aadhaar No:</b></td>
                        <td><%# Eval("Aadhaar") %></td>
                    </tr>
                    <tr>
                        <td><b>PAN:</b></td>
                        <td><%# Eval("PAN") %></td>
                    </tr>
                    <tr>
                        <td><b>Preferred Language:</b></td>
                        <td><%# Eval("PreferedLanguage") %></td>
                    </tr>
                    <tr>
                        <td><b>Marital Status:</b></td>
                        <td><%# Eval("MaritalStatus") %></td>
                    </tr>
                    <tr>
                        <td><b>Upto 10th:</b></td>
                        <td><%# Eval("Upto10th") %></td>
                    </tr>
                    <tr>
                        <td><b>Percentage Upto 10th:</b></td>
                        <td><%# Eval("PercentageUpto10th") %></td>
                    </tr>
                    <tr>
                        <td><b>Upto 12th:</b></td>
                        <td><%# Eval("Upto12th") %></td>
                    </tr>
                    <tr>
                        <td><b>Percentage Upto 12th:</b></td>
                        <td><%# Eval("PercentageUpto12th") %></td>
                    </tr>
                    <tr>
                        <td><b>Graduation:</b></td>
                        <td><%# Eval("Graduation") %></td>
                    </tr>
                    <tr>
                        <td><b>Percentage In Graduation:</b></td>
                        <td><%# Eval("PercentageInGraduation") %></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


  <asp:GridView ID="addressGridView" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="Address" HeaderText="Address"/>
        <asp:BoundField DataField="Type" HeaderText="Type"/>
    </Columns>
</asp:GridView>
           
            <asp:Button ID="EditBtn" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="EditBtn_Click"/>

 <h4> Add notes in this page </h4>

          <custom:CustomControl  runat="server" Name="Users" />
      

          
        </div>
    </form>
</body>
</html> 
