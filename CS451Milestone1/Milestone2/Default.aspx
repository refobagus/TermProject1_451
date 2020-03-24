<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Milestone1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .GridRow{
            text-align:center;
            border:none;
        }
   
    </style>

<div id="ApplicationHomePage" style="text-align:center">

    

    <div id="StateSelector" runat="server">
        <%-- State Selection DropDown -- data source to be set --%>
        <p>State: <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" /></p>
        <h3>City: </h3> <asp:DropDownList ID="ddlCity" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true" />
    </div>

   

    <div id="Businesses" style="text-align:center">
        <asp:GridView runat="server" ID="gvBusinesses" AutoGenerateColumns="false">
            <RowStyle CssClass="GridRow" />
            
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="BusinessID"
                     DataNavigateUrlFormatString="~\BusinessInfo.aspx?businessID={0}" 
                     Target="_blank" 
                     HeaderText="Details"
                     Text="View Details"
                     ControlStyle-CssClass="btn btn-primary"/>
                <asp:BoundField HeaderText="BusinessID" DataField="BusinessID" Visible="false"/>
                <asp:BoundField HeaderText="Name" DataField="name" />
                <asp:BoundField HeaderText="State" DataField="State" />
                <asp:BoundField HeaderText="City" DataField="City" />
            </Columns>
        </asp:GridView>
        
    </div>
    <asp:Label runat="server" ID="lblError" ForeColor="Red" />

</div>   

</asp:Content>
