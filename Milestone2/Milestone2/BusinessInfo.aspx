<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusinessInfo.aspx.cs" Inherits="Milestone2.BusinessInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <style>
        .BusinessSelectedAlert {
            position: relative;
            margin-top:15px;
            margin-left: 0px;
            margin-right: 0px;
            width: 50%;
            background-color: #e8e8e8;
            font-size: 20px;
            border-radius:15px;
        }
    </style>
    <div id="BusinessSelectedAlert" class="BusinessSelectedAlert" runat="server">
        <asp:FormView ID="businessForm" runat="server">
            <ItemTemplate>
                Business:  <%#Eval("Name") %>
                <br />
                State: <%#Eval("State") %>
                <br />
                City:  <%#Eval("City") %>
                <br />
                Number of Businesses in Same City: <%#Eval("SameCity") %>
                Number of Business in Same State: <%#Eval("SameState") %>

                <div style="text-align: center">
                    <asp:Button CssClass="btn btn-primary" ID="donebtn" runat="server" OnClick="donebtn_Click" Text="Done" />
                </div>
            </ItemTemplate>
        </asp:FormView>
    </div>
    <div>
         <h3>Tips</h3> <br />
        <asp:GridView ID="gvTips" runat="server" AutoGenerateColumns="true">
            <Columns>
                <asp:BoundField DataField="userName" HeaderText="User" SortExpression="userName" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>
