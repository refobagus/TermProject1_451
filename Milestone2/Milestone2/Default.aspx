<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Milestone2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .GridRow {
            text-align: center;
            border: none;
        }

        .SidSelectors {
            width: 30%;
            height: 100%;
            left: 0px;
            top: 6%;
            position: absolute;
        }

        .gridViewContainer {
            top: 40px;
            left: 20px;
            position: relative;
        }
    </style>

    <div id="ApplicationHomePage" style="text-align: center">




        <div id="divSelectors" class="SidSelectors" runat="server">
            <p>State</p>
            <asp:ListBox ID="lbStates" Style="width: 30%; height: 10%;" runat="server" OnSelectedIndexChanged="lbStates_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
            <div id="divCity" runat="server" style="height: 20%" visible="false">
                <p>City</p>
                <asp:ListBox ID="lbCities" Style="width: 30%; height: 100%" runat="server" OnSelectedIndexChanged="lbCities_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
            </div>
            <br />
            <br />
            <div id="divPostalCodes" style="height: 30%" runat="server" visible="false">
                <p>Postal Code</p>
                <asp:ListBox ID="lbPostalCode" Style="width: 30%; height: 100%" runat="server" OnSelectedIndexChanged="lbPostalCode_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
            </div>
            <br />
            <br />
            <div id="divCategories" style="height: 30%;" runat="server" visible="false">
                <p>Categories</p>
                <asp:ListBox ID="lbCategories" Style="height: 100%" runat="server" OnSelectedIndexChanged="lbCategories_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
            </div>
        </div>


        <div id="Businesses" style="text-align: center" class="gridViewContainer">
            <asp:GridView runat="server" ID="gvBusinesses" AutoGenerateColumns="false">
                <RowStyle CssClass="GridRow" />

                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="BusinessID"
                        DataNavigateUrlFormatString="~\BusinessInfo.aspx?businessID={0}"
                        Target="_blank"
                        HeaderText="Details"
                        Text="View Details"
                        ControlStyle-CssClass="btn btn-primary" />
                    <asp:BoundField HeaderText="BusinessID" DataField="BusinessID" Visible="false" />
                    <asp:BoundField HeaderText="Name" DataField="name" />
                    <asp:BoundField HeaderText="State" DataField="State" />
                    <asp:BoundField HeaderText="City" DataField="City" />
                </Columns>
            </asp:GridView>

        </div>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />

    </div>

</asp:Content>
