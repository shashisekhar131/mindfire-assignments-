<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomUserControl.ascx.cs" Inherits="DemoUserManagement.CustomUserControl" %>



<h4>Add notes in this page</h4>


<div class="row">
    <div class="col-md-4">
        <input type="text" id="NotesInput" class="form-control" />
    </div>
    <div class="col-md-4">
        <button id="NotesBtn" class="btn btn-success">Save Notes</button>
    </div>
</div>

        <div class="row">
            <div class="col-md-12">
                
<asp:UpdatePanel ID="UpdatePanelNotes" runat="server">

    <ContentTemplate> 
                <asp:GridView ID="NotesGridView" runat="server" AutoGenerateColumns="false" CssClass="grid-style" EmptyDataText="no records to display"
                    OnPageIndexChanging="NotesGridView_PageIndexChanging"
                    AllowSorting="True" OnSorting="NotesGridView_Sorting" AllowCustomPaging="true" AllowPaging="True" PageSize="3">
                    <Columns>
                        <asp:BoundField DataField="NotesID" HeaderText="Notes ID" SortExpression="NotesID" />
                        <asp:BoundField DataField="NoteText" HeaderText="Note Text" SortExpression="NoteText" />
                        <asp:BoundField DataField="ObjectType" HeaderText="Page" />
                        <asp:BoundField DataField="ObjectID" HeaderText="user ID" SortExpression="ObjectID" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                    </Columns>

                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                        LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />

                </asp:GridView>
    
    </ContentTemplate> 
  </asp:UpdatePanel>

            </div>
        </div>
   



<script type="text/javascript">

    $(document).ready(function () {


        $('#NotesBtn').on('click', function (event) {
            event.preventDefault();
            let urlParams = new URLSearchParams(window.location.search);
            let UserID = urlParams.get('id');
            let ObjectType = 1;
            let NoteText = $('#NotesInput').val();
            $.ajax({
                url: 'UserDetails.aspx/InsertNotes',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ NoteText, UserID, ObjectType }),
                dataType: 'json',
                success: function (data) {
                    console.log(data.d);
                    

                },
                error: function (xhr, status, error) {
                    console.log('Error loading states: ', error);
                    console.log(xhr.responseText);
                }
            });

        });
    });

    





</script>
