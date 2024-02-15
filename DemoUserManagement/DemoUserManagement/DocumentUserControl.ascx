<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentUserControl.ascx.cs" Inherits="DemoUserManagement.DocumentUserControl" %>


 <div >
   
     <asp:Label ID="lblUploadFile" runat="server" AssociatedControlID="fuFileControl">Upload File:</asp:Label>

  <div class="row">
    <div class="col-md-4">
        <asp:FileUpload ID="fuFileControl" runat="server" CssClass="form-control" />
    </div>
    
    <div class="col-md-4">
        <asp:DropDownList ID="ddlFileType" runat="server" CssClass="form-control">
            <asp:ListItem Text="Aadhar" Value="2" />
            <asp:ListItem Text="PAN" Value="3" />
            <asp:ListItem Text="Passport" Value="1" />
             <asp:ListItem Text="Others" Value="4" />
        </asp:DropDownList>
    </div>

      <div class="col-md-4">
         <asp:Button ID="BtnUpload" runat="server" Text="UploadFile" OnClick="BtnUpload_Click" CssClass="btn btn-success"/>
     </div>

</div>


         
     
   <asp:GridView ID="DocumentGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentID" CssClass="grid-style" EmptyDataText="no records  to display">
    <Columns>
        <asp:TemplateField HeaderText="DocumentID">
            <ItemTemplate>
                <asp:Label ID="LabelDocumentID" runat="server" Text='<%# Eval("DocumentID") %>'></asp:Label>
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

        <asp:TemplateField HeaderText="DocumentOriginalName">
            <ItemTemplate>
                 <asp:HyperLink ID="lnkDownloadFile" runat="server" Text='<%# Bind("DocumentOriginalName") %>' NavigateUrl='<%# "FileDownloadHandler.ashx?fileName=" + Eval("DocumentGuidName") %>' Target="_blank" />

            </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField HeaderText="ObjectID">
            <ItemTemplate>
                <asp:Label ID="LabelObjectID" runat="server" Text='<%# Eval("ObjectID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>
   
</asp:GridView>


 </div>