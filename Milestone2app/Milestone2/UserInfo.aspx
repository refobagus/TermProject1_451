<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="Milestone2.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divApplicationLogin" style="text-align: center; font-size: 20px; position: relative; margin-top: 100px;" runat="server" visible="true">
        <p id="usernameSelect" runat="server">
            UserName:
            <asp:TextBox runat="server" ID="tbuser" placeholder="Username"></asp:TextBox>
        </p>
        <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" Text="Submit" />
        <p id="userDDLSelect" runat="server" visible="false">
            Select yourself from the list:
            <asp:DropDownList ID="ddlUser" runat="server" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </p>
    </div>
    <div id="loggedIn" runat="server" visible="false" style="font-size:20px">
        <div id="UserFormDiv" style="margin:10px ">


            <asp:FormView ID="fvUserForm" runat="server" BackColor="White" BorderColor="#CCCCCC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="100%">
                <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White"/>
                <ItemTemplate>
                    <p>Name: <%#Eval("name") %></p>
                    <p>User ID: <%#Eval("userID") %></p>
                    <table style="width:100%">
                        <tr>
                            <td colspan="2">Average Stars: <%#Eval("stars") %>
                            </td>
                            <td colspan="2">Fans: <%#Eval("fans") %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">Yelping Since: <%#Eval("yelping_since") %>
                            </td>
                        </tr>
                        <tr>
                            <td>Votes: <%#Eval("numLikes")%>
                            </td>
                            <td>Funny: <%#Eval("funny") %>
                            </td>
                            <td>Cool:<%#Eval("cool") %></td>
                            <td>Useful: <%#Eval("fans") %>
                            </td>
                        </tr>
                        <tr>
                            <td>Tip Count: <%#Eval("tipCount") %>
                            </td>
                        </tr>
                        <tr>
                            <td>Total Tip Likes: <%#Eval("numLikes") %>
                            </td>
                        </tr>
                        <tr>
                            <td>Location:</td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divLatitude">
                                    &nbsp; Latitude: <%#Eval("latitude") %> <a id="btnUpdateLatitude">Update</a> <br />
                                </div>
                                <div id="divLatitudeUpdate" style="display:none">
                                    <asp:TextBox ID="tbLatitude" runat="server">

                                    </asp:TextBox>
                                    <asp:Button ID="btnSubmitLat" Text="Submit" runat="server" OnClick="btnSubmitLat_Click" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divLongitude" runat="server">
                                    &nbsp; Longitude: <%#Eval("longitude") %> <asp:Button runat="server" id="btnUpdateLong" Text="Update" OnClick="btnUpdateLong_Click" />
                                </div>
                                <div id="divLongUpdate" runat="server" visible="false">
                                    <asp:TextBox ID="tbLong" runat="server">

                                    </asp:TextBox>
                                    <asp:Button ID="btnSubmitLong" runat="server" Text="Submit" OnClick="btnSubmitLong_Click" />

                                </div>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            </asp:FormView>
        </div>
        <div id="FreindsTable" style="margin-top: 10px;">
            <asp:GridView ID="gvFriends" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                EmptyDataText="No Friends Found"
                 Width="100%">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="TotalLikes" HeaderText="Total Likes" />
                    <asp:BoundField DataField="AvgStars" HeaderText="Average Stars" />
                    <asp:BoundField DataField="yelping_since" HeaderText="Yelping Since" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>

        <div id="friendsTips" style="margin-top:10px; ">
            <asp:GridView ID="gvTipsFriends" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Horizontal" EmptyDataText="Still No Friends">
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="Name" />
                    <asp:BoundField DataField="business" HeaderText="Business" />
                    <asp:BoundField DataField="city" HeaderText="City" />
                    <asp:BoundField DataField="text" HeaderText="Text" />
                    <asp:BoundField DataField="date" HeaderText="Date" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>
    </div>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />

    <script type="text/javascript">

        $(function () {
            $('#btnUpdateLatitude').click = function () {
                $('#divLatitude').hide(500);
                $('#divLatitudeUpdate').show(500);
            };
            $('#btnUpdateLong').click = function () {
                $('#divLongitude').hide(500);
                $('#divLongUpdate').show(500);
            };
        });
    </script>

</asp:Content>
