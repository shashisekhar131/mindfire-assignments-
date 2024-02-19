<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogoutPage.aspx.cs" Inherits="DemoUserManagement.LogoutPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h3>Do you want to logout?</h3>
    <asp:Button ID="Logout_Button" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="LogoutButton_Click"/>
</asp:Content>
