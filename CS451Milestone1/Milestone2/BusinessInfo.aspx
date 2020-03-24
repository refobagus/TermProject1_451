<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusinessInfo.aspx.cs" Inherits="Milestone1.BusinessInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .BusinessSelectedAlert {
            position: absolute;
            left: 0;
            margin-left: 0px;
            margin-right: 0px;
            width: 50%;
            background-color: #d1d1d1;
            font-size: 20px;
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
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
