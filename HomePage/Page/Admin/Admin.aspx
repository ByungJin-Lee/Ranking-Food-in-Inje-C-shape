<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="HomePage.Page.Log.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <title>Log</title>
    <link href="../../assets/css/Admin/Admin.css" rel="stylesheet" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div class="content">        
        <table class="status">
            <tr>
                <td class="title" colspan="4">
                    HomePage Status
                </td>
            </tr>
            <tr>
                <th>접속한 관리자</th>
                <td>
                    <asp:Label ID="adminId" runat="server" Text=""></asp:Label>
                </td>
                <th>현재 시간</th>
                <td>
                    <asp:Label ID="timeLabel" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>현재 Session 수</th>
                <td>
                    <asp:Label ID="sessionCount" runat="server" Text=""></asp:Label>
                </td>
                <th>총 가입자 수</th>
                <td>
                    <asp:Label ID="memberCount" runat="server" Text=""></asp:Label>
                </td>
            </tr>        
        </table>
        <asp:GridView CssClass="logArea" ID="userLogGrid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-CssClass="logHeader" ItemStyle-CssClass="logItem" >
<HeaderStyle CssClass="logHeader"></HeaderStyle>

<ItemStyle CssClass="logItem"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="userId" HeaderText="userID" HeaderStyle-CssClass="logHeader" ItemStyle-CssClass="logItem">
<HeaderStyle CssClass="logHeader"></HeaderStyle>

<ItemStyle CssClass="logItem"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="connection" HeaderText="Success" HeaderStyle-CssClass="logHeader" ItemStyle-CssClass="logItem">
<HeaderStyle CssClass="logHeader"></HeaderStyle>

<ItemStyle CssClass="logItem"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="time" HeaderText="AccessTime" HeaderStyle-CssClass="logHeader" ItemStyle-CssClass="logItem">
<HeaderStyle CssClass="logHeader"></HeaderStyle>

<ItemStyle CssClass="logItem"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="postKind" HeaderText="Kind" HeaderStyle-CssClass="logHeader" ItemStyle-CssClass="logItem">
<HeaderStyle CssClass="logHeader"></HeaderStyle>

<ItemStyle CssClass="logItem"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="postItem" HeaderText="Detail" HeaderStyle-CssClass="logHeader" ItemStyle-CssClass="logItem">
<HeaderStyle CssClass="logHeader"></HeaderStyle>

<ItemStyle CssClass="logItem"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="accessIP" HeaderText="Ip" HeaderStyle-CssClass="logHeader" ItemStyle-CssClass="logItem" >
<HeaderStyle CssClass="logHeader"></HeaderStyle>

<ItemStyle CssClass="logItem"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
    </div>
</asp:Content>
