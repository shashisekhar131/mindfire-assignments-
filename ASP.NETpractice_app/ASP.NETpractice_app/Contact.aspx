<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ASP.NETpractice_app.Contact" %>

<%@ Register Src="~/CustomUserControl.ascx" TagName="CustomControl" TagPrefix="custom" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    
  <h4> Add notes in this page </h4>
 <custom:CustomControl  runat="server" Name="contact" />
           
    

</asp:Content>
