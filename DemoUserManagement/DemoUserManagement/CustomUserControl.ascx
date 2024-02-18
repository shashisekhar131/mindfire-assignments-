<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomUserControl.ascx.cs" Inherits="DemoUserManagement.CustomUserControl" %>



<h4>Add notes in this page</h4>

<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-4">
                <asp:TextBox ID="NotesInput" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Button ID="NotesBtn" runat="server" Text="save notes" OnClick="NotesBtn_Click" CssClass="btn btn-success" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="NotesGridView" runat="server" AutoGenerateColumns="false" CssClass="grid-style" EmptyDataText="no records to display"
                    OnPageIndexChanging="NotesGridView_PageIndexChanging" 
       AllowSorting="True" OnSorting="NotesGridView_Sorting" AllowCustomPaging="true" AllowPaging="True" PageSize="3">
                    <Columns>
                        <asp:BoundField DataField="NotesID" HeaderText="Notes ID" SortExpression="NotesID"/>
                        <asp:BoundField DataField="NoteText" HeaderText="Note Text" SortExpression="NoteText" />
                        <asp:BoundField DataField="ObjectType" HeaderText="Page" />
                        <asp:BoundField DataField="ObjectID" HeaderText="user ID" SortExpression="ObjectID"/>
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                    </Columns>

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" 
        LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />  
                    
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="NotesBtn" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>