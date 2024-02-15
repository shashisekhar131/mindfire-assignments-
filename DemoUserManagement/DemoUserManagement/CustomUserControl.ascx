<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomUserControl.ascx.cs" Inherits="DemoUserManagement.CustomUserControl" %>



<h4> Add notes in this page </h4>

<div class="row">
    
    <div class="col-md-4">
        <asp:TextBox ID="NotesInput" runat="server" CssClass="form-control"> </asp:TextBox>
    </div>
    <div class="col-md-4">
        <asp:Button ID="NotesBtn" runat="server" Text="save notes" OnClick="NotesBtn_Click" CssClass="btn btn-success" />
    </div>
</div>


<asp:GridView ID="notesGridView" runat="server" AutoGenerateColumns="false" CssClass="grid-style" EmptyDataText="no records  to display">
    <Columns>
        <asp:BoundField DataField="NotesID" HeaderText="Notes ID" />
        <asp:BoundField DataField="NoteText" HeaderText="Note Text" />
        <asp:BoundField DataField="ObjectType" HeaderText="Page" />
        <asp:BoundField DataField="ObjectID" HeaderText="user ID" />
        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
        
    </Columns>
</asp:GridView>


