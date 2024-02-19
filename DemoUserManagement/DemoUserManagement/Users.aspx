<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DemoUserManagement.Users" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div>

    
<asp:UpdatePanel ID="UpdatePanelUsers" runat="server">

    <ContentTemplate> 

   <asp:GridView ID="userDetailsGridView" runat="server" 
       AutoGenerateColumns="false" AllowCustomPaging="True" 
       PageSize="1" OnPageIndexChanging="UserDetailsGridView_PageIndexChanging" 
       AllowSorting="True" OnSorting="userDetailsGridView_Sorting" AllowPaging="True"  CssClass="gridview-style" EmptyDataText="no records  to display" >
    <Columns>
        <asp:BoundField DataField="UserID" HeaderText="User ID" SortExpression="UserID"/>
        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />   
        <asp:BoundField DataField="Password" HeaderText="Password" />
        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
        <asp:BoundField DataField="AlternatePhoneNumber" HeaderText="Alternate Phone Number" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="AlternateEmail" HeaderText="Alternate Email" />
        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" />
        <asp:BoundField DataField="Favouritecolor" HeaderText="Favorite Color" />
        <asp:BoundField DataField="PreferedLanguage" HeaderText="Preferred Language" />
    </Columns>
    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" 
        LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />  
</asp:GridView>


  <asp:GridView ID="addressGridView" runat="server" AutoGenerateColumns="False">
    <Columns>
         <asp:BoundField DataField="UserID" HeaderText="User id"/>       
        <asp:BoundField DataField="Address" HeaderText="Address"/>
        <asp:BoundField DataField="Type" HeaderText="Type"/>
        <asp:BoundField DataField="StateID" HeaderText="State id" />
        <asp:BoundField DataField="CountryID" HeaderText="Country id" />
    </Columns>
</asp:GridView>

           
            <h3> edit a user </h3>

            enter Id of the user:
       <div class="row"> 
            
            <div class="col-md-4">
                    <asp:TextBox ID="UserIdInput" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                    <asp:Button ID="EditBtn" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="EditBtn_Click"/>
            </div>
       </div>



              
         </ContentTemplate>  

</asp:UpdatePanel>  

        </div>
    </asp:Content>