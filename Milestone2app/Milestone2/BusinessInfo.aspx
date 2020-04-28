<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusinessInfo.aspx.cs" Inherits="Milestone2.BusinessInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .BusinessSelectedAlert {
            position: relative;
            margin-top: 15px;
            margin-left: 0px;
            margin-right: 0px;
            width: 50%;
            background-color: #e8e8e8;
            font-size: 20px;
            border-radius: 15px;
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

                <p>Categories: <%#Eval("categories") %></p>
                Number of Businesses in Same City: <%#Eval("SameCity") %>
                Number of Business in Same State: <%#Eval("SameState") %>
                <br />
                <p>Number to Checkins: <%#Eval("numCheckins")%></p>
                <p>Number of Tips: <%#Eval("numTips") %></p>


                <div style="text-align: center">
                    <asp:Button CssClass="btn btn-primary" ID="donebtn" runat="server" OnClick="donebtn_Click" Text="Done" />
                </div>
            </ItemTemplate>
        </asp:FormView>

    </div>
    <div>
        <h3>Tips</h3>
        <br />
        <div id="divLoggedIn" runat="server" visible="false">
            <a id="btnNewTip" class="btn btn-primary" runat="server">Add a Tip</a>
            <asp:Button CssClass="btn btn-primary" ID="btnCheckin" runat="server" OnClick="BtnCheckin_Click" Text="Check In" />
            <div id="divNewTip" style="display: none" runat="server">
               
                <asp:TextBox ID="tbTip" TextMode="MultiLine" style="min-width:80%; font-size:16px; text-align:left;" runat="server">

                </asp:TextBox>
               <br />
                <asp:Button ID="btnSubmitNewTip" runat="server" Text="Submit" OnClick="btnSubmitNewTip_Click" />
                <asp:Button ID="CancelNewTip" runat="server" Text="Cancel" OnClick="CancelNewTip_Click" />
            </div>
        </div>

        <br />
   <%--     <asp:GridView ID="gvTips" runat="server" AutoGenerateColumns="true">
            <Columns>
                <asp:BoundField DataField="userName" HeaderText="User" SortExpression="userName" />
            </Columns>
        </asp:GridView>--%>

        <asp:GridView ID="gvTips" runat="server" AutoGenerateColumns="false" style="width:100%; font-size:16px;">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table style="width:100%">
                            <tr style="width:100%">
                                <td style="width:10%">
                                    <%#Eval("userName")%>
                                </td>
                                <td style="width:10%">
                                    <%#Eval("likes") %>
                                </td>
                                <td style="width:50%">
                                    <%#Eval("tip") %>
                                </td>
                                <td style="width: 20%;">
                                    <%#Convert.ToDateTime(Eval("timeMade")).ToShortDateString()%> <%# Convert.ToDateTime(Eval("timeMade")).ToShortTimeString() %>
                                </td>
                                <td style="width:10%"> 
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("userID") + "," + Eval("timeMade")%>' CommandName="Tip" CssClass="btn btn-secondary" OnClick="LikeBtn_Click">Like</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>


    <script type="text/javascript">
        $(function () {
            $('#<%=btnNewTip.ClientID%>').click(function () {
                $('#<%=divNewTip.ClientID%>').show(500);
                $('#<%=btnNewTip.ClientID%>').hide();
            });

        });

    </script>
</asp:Content>
