<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomUserControl.ascx.cs" Inherits="ASP.NETpractice_app.CustomUserControl" %>

<asp:TextBox ID="NotesInput" runat="server"> </asp:TextBox>
<asp:Button ID="NotesBtn" runat="server" Text="save notes" OnClick="AddNotes" />



<asp:GridView ID="GridView2" runat="server"  >

    <Columns>

        <asp:BoundField  DataField="notesid" HeaderText="notesid"/>        
        <asp:BoundField  DataField="notes" HeaderText="Notes"/>
        
    </Columns>

</asp:GridView>

