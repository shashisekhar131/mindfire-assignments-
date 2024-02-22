<%@ Control Language="C#" AutoEventWireup="true"  CodeBehind="DocumentUserControl.ascx.cs" Inherits="DemoUserManagement.DocumentUserControl" %>

<style type="text/css">
    /* Add your styles for the user control here */
    .row {
        margin-bottom: 10px;
    }

    .form-control {
        width: 100%;
    }

    .btn-success {
        /* Add your button styles here */
    }

    /* Add styles for the GridView here */
    .grid-style {
        width: 100%;
        border-collapse: collapse;
        margin-top: 10px;
    }

    .grid-style th, .grid-style td {
        border: 1px solid #ddd;
        padding: 8px;
        text-align: left;
    }

    .grid-style th {
        background-color: #f2f2f2;
    }

    /* Add additional styles for hyperlinks or other elements if needed */
    .hyperlink-style {
        /* Your hyperlink styles here */
    }
</style>


        <div>
            
            <div class="row">
                <div class="col-md-4">
                    
                     <label for="fileInput">select file:</label>
                    <input type="file" id="fileInput" class="form-control" />
                </div>

                <div class="col-md-4">

                     <label for="fileType">select file type:</label>
                 <select id="fileType" class="form-control" >
                     <option value="1">Aadhar</option>
                     <option value="2">PAN</option>
                     <option value="3">Others</option>
                 </select>
                </div>

                <div class="col-md-4">
                    <button id="BtnUpload" class="btn btn-success custom-margin-top" > upload file </button>
                </div>
            </div>

            
<asp:UpdatePanel ID="UpdatePanelDocuments" runat="server">

    <ContentTemplate> 
     <asp:GridView ID="DocumentGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentID" CssClass="grid-style" EmptyDataText="no records to display"
                      OnPageIndexChanging="DocumentGridView_PageIndexChanging" 
AllowSorting="True" OnSorting="DocumentGridView_Sorting" AllowCustomPaging="true" AllowPaging="True" PageSize="3">
      
      <Columns>
        <asp:TemplateField HeaderText="DocumentID" SortExpression="DocumentID">
            <ItemTemplate>
                <asp:Label ID="LabelDocumentID" runat="server" Text='<%# Eval("DocumentID") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="DocumentType">
            <ItemTemplate>
                <asp:Label ID="LabelDocumentID" runat="server" Text='<%# Eval("DocumentType") %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>

        <asp:TemplateField HeaderText="ObjectType">
            <ItemTemplate>
                <asp:Label ID="LabelObjectType" runat="server" Text='<%# Eval("ObjectType") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="DocumentOriginalName" SortExpression="DocumentOriginalName">
            <ItemTemplate>
                 <asp:HyperLink ID="lnkDownloadFile" runat="server" Text='<%# Bind("DocumentOriginalName") %>' NavigateUrl='<%# "FileDownloadHandler.ashx?fileName=" + Eval("DocumentGuidName") %>' Target="_blank"  />

            </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField HeaderText="ObjectID" >
            <ItemTemplate>
                <asp:Label ID="LabelObjectID" runat="server" Text='<%# Eval("ObjectID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="TimeStamp">
            <ItemTemplate>
                <asp:Label ID="LabelTimeStamp" runat="server" Text='<%# Eval("TimeStamp") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>
        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" 
LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />  
            </asp:GridView>
         </ContentTemplate> 
</asp:UpdatePanel>

  
        </div>
   


<script type="text/javascript">

    $(document).ready(function () {
     
       
        $('#BtnUpload').on('click', function (event) {
            // Get the selected file
            event.preventDefault();
            var fileInput = $('#fileInput')[0];
            var file = fileInput.files[0];

            var urlParams = new URLSearchParams(window.location.search);
            var id = urlParams.get('id');

            var fileTypeValue = parseInt($('#fileType').val());

            if (file) {
                // Create FormData object and append the file
                var formData = new FormData();
                formData.append('file', file);
                formData.append('UserID', id);
                formData.append('FileType', fileTypeValue);
                
                $.ajax({
                    url: '/UploadHandler.ashx', 
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        // Handle the success response
                        console.log("uploaded");
                    },
                    error: function (xhr, status, error) {
                        console.log(" not uploaded");
                    }
                });
            } else {
                console.log("select a filie");
            }

        });
    });

</script>