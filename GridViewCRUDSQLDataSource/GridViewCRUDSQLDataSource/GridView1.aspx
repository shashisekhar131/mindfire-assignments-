<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridView1.aspx.cs" Inherits="GridViewCRUDSQLDataSource.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

           <center>

              <table style="width:100%;">  
        <tr>  
            <td class="style1">  
                <strong>Edit Update Delete Operation in Gridview</strong></td>  
            <td>  
                 </td>  
            <td>  
                 </td>  
        </tr>  
        <tr>  
            <td>  
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
                    BorderStyle="None" BorderWidth="1px"   
                    DataKeyNames="id"   
                     onrowcancelingedit="GridView1_RowCancelingEdit"   
                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"   
                    onrowupdating="GridView1_RowUpdating" CellSpacing="2" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" AllowSorting="true" OnSorting="GridView1_Sorting">
                    <Columns>  

                        
       <asp:CommandField ShowEditButton="True" />
       <asp:CommandField ShowDeleteButton="True" /> 

           
                        <asp:BoundField DataField="id" HeaderText="student ID" SortExpression="id" />
        
                        <asp:TemplateField HeaderText="Name" SortExpression="name" >                              
                           
                            <EditItemTemplate>  
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>  
                            </EditItemTemplate>  
                            <ItemTemplate >  
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("name") %>'></asp:Label>  
                            </ItemTemplate>  

                        </asp:TemplateField>
        

                        <asp:TemplateField HeaderText="City">  
                            <EditItemTemplate>  
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("city") %>'></asp:TextBox>  
                            </EditItemTemplate>  
                            <ItemTemplate>  
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("city") %>'></asp:Label>  
                            </ItemTemplate>  
                        </asp:TemplateField>  

                    </Columns>  
                   
                </asp:GridView>  
            </td>  
            <td>  
                 </td>  
            <td>  
                 </td>  
        </tr>  
        <tr>  
            <td>  
                 </td>  
            <td>  
                 </td>  
            <td>  
                 </td>  
        </tr>  
    </table>  
               <asp:TextBox ID="name" runat="server"></asp:TextBox>
               <asp:TextBox ID="city" runat="server"></asp:TextBox>
               <asp:Button ID="InsertBtn" runat="server" Text="InsertRow" OnClick="InsertRow_Click" />
           </center>
           
        </div>
    </form>
</body>
</html>
