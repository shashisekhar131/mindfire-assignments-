<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NETpractice_app._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>


        
<div>  

    
    <h3> user details form </h3>
  <table class="auto-style1">  
        <tr>  
            <td>      <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>      </td>  
            <td>     <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                      
                <asp:RequiredFieldValidator ID="name" runat="server" ControlToValidate="UserName" ErrorMessage="Please enter a user name" ForeColor="Red"></asp:RequiredFieldValidator>  
            </td>  
        </tr>  

        <tr>  
            <td>     <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label></td>  
            <td>    <asp:TextBox ID="Password" runat="server" TextMode="Password" EnableViewState="true"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Password" ErrorMessage="Please enter password" ForeColor="Red"></asp:RequiredFieldValidator>  

            </td>  
        </tr>
       
      <tr>
           <td>    <asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label></td>  
           <td>  <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" EnableViewState="true"></asp:TextBox>
             
                <asp:CompareValidator ID="PasswordCompareValidator" runat="server"
                          ControlToCompare="Password"
                          ControlToValidate="ConfirmPassword"
                          ErrorMessage="Passwords do not match."
                          Display="Dynamic"
                          Operator="Equal"
                          ForeColor="Red"
                      ></asp:CompareValidator>

           </td>  
      </tr> 
      
        

       <tr>
          <td>    <asp:Label ID="Label8" runat="server" Text="Age"></asp:Label></td>  
           <td>  
              <asp:TextBox ID="uesrInput" runat="server"> </asp:TextBox> 
               
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="uesrInput"   
            ErrorMessage="Enter value in specified range" ForeColor="Red" MaximumValue="100" MinimumValue="0"   
            SetFocusOnError="True" Type="Integer"></asp:RangeValidator> 

           </td>  
      </tr>  


        

        <tr> 
        
            <td>      <asp:Label ID="Label4" runat="server" Text="Gender"></asp:Label></td>  
            <td>     <asp:RadioButton ID="RadioButton1" runat="server" GroupName="gender" Text="Male" />  
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="gender" Text="Female" />
            </td>  

        </tr>
      
        <tr> 
        
            <td>     <asp:Label ID="Label5" runat="server" Text="Select Course"></asp:Label>s</td>  
            <td>  
            <asp:CheckBox ID="CheckBox1" runat="server" Text="J2SEE" />  
            <asp:CheckBox ID="CheckBox2" runat="server" Text="J2EE" />  
            <asp:CheckBox ID="CheckBox3" runat="server" Text="Spring Framework" />  
            </td>  
        </tr>  

        <tr>  
              <td>     <asp:Label ID="Label6" runat="server" Text="Email ID"></asp:Label>     </td>  
              <td>      <asp:TextBox ID="EmailID" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailID"
            ErrorMessage="Please enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
            </asp:RegularExpressionValidator> 

              </td>  
       </tr> 

      <tr>
          <td>   <asp:Label ID="Label7" runat="server" Text="Select city"></asp:Label>  </td>
          <td>
               <asp:DropDownList ID="DropDownList1" runat="server" >  
                <asp:ListItem Value="hyderbad">hyderbad</asp:ListItem>  
                <asp:ListItem Value="delhi">New Delhi </asp:ListItem>  
                <asp:ListItem Value="mumbai"> mumbai</asp:ListItem>  
              </asp:DropDownList>  
          </td>
      </tr>

        <tr>  
            <td>     </td>         
            <td>  <asp:Button ID="Button1" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="Button1_Click"/>   </td>  
        </tr>  

    </table>  

     
    </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"/>
    <asp:Label ID="SelectedInputs" runat="server" ></asp:Label>  

    
        
    <h3> data List</h3>

     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="ASP.NETpractice_app.Product"
            SelectMethod="GetProducts">
        </asp:ObjectDataSource>

        <asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource1">
            <ItemTemplate>
                <p>Product ID: <%# Eval("ProductID") %>, Product Name: <%# Eval("ProductName") %>, Price: <%# Eval("Price") %></p>
            </ItemTemplate>
        </asp:DataList>
    </main>

</asp:Content>
