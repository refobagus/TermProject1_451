<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Milestone1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div id="ApplicationHomePage" style="text-align:center">

    <div id="StateSelector" runat="server">
        <%-- State Selection DropDown -- data source to be set --%>
        <p>State: <asp:DropDownList ID="ddlState" runat="server" /></p>
    </div>

    <div id="citySelection" runat="server" style="text-align:center">
        <table>
            <tr>
                <td>City</td>
                <td>
                    City's should appear here
                </td>
            </tr>
        </table>
    </div>

    <div id="Businesses">
        <asp:GridView runat="server" ID="gvBusinesses">
            <Columns>
                <asp:BoundField HeaderText="BuisnessName" />
                <asp:BoundField HeaderText="State" />
                <asp:BoundField HeaderText="City" />
            </Columns>
        </asp:GridView>
        
    </div>


</div>   

</asp:Content>
