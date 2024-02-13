<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomUserControl.ascx.cs" Inherits="DemoUserManagement.CustomUserControl" %>



<h4> Add notes in this page </h4>
<asp:TextBox ID="NotesInput" runat="server"> </asp:TextBox>
<asp:Button ID="NotesBtn" runat="server" Text="save notes" OnClick="NotesBtn_Click" />


<asp:GridView ID="notesGridView" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="NotesID" HeaderText="Notes ID" />
        <asp:BoundField DataField="NoteText" HeaderText="Note Text" />
        <asp:BoundField DataField="ObjectType" HeaderText="Page" />
        <asp:BoundField DataField="UserID" HeaderText="user ID" />
        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
        
    </Columns>
</asp:GridView>


