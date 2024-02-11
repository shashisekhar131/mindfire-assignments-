<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomUserControl.ascx.cs" Inherits="DemoUserManagement.CustomUserControl" %>


<asp:TextBox ID="NotesInput" runat="server"> </asp:TextBox>
<asp:Button ID="NotesBtn" runat="server" Text="save notes" OnClick="NotesBtn_Click" />



<asp:GridView ID="NotesGridView" runat="server" AutoGenerateColumns="false">

    <Columns>

        <asp:BoundField  DataField="Notesid" HeaderText="notesid"/>        
        <asp:BoundField  DataField="NoteText" HeaderText="Notes"/>
        <asp:BoundField  DataField="CreatedDate" HeaderText="Time stamp"/>
        
    </Columns>

</asp:GridView>