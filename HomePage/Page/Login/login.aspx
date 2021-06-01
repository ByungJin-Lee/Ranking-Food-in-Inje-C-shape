<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HomePage.Page.Login.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../../assets/css/login/login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div class="content">
        <div class="loginBox">
            <span class="title">Login</span>
            <table class="loginTable">
                <tr><td colspan="2"><div class="line"></div></td></tr>
                <tr>
                    <td>아이디</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="2"><div class="line"></div></td></tr>
                <tr>
                    <td>비밀번호</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="2"><div class="line"></div></td></tr>
            </table>
            <asp:Button Text="로그인" CssClass="loginBtn" runat="server" OnClick="loginBtn_Click" />
            <asp:Label ID="loginFail" runat="server" CssClass="loginFail" ForeColor="Red"></asp:Label>
            <div class="tool">
                <asp:HyperLink class="toolItem" runat="server" NavigateUrl="~/Page/Login/register.aspx">
                    회원가입
                </asp:HyperLink>
                <asp:HyperLink class="toolItem" runat="server" NavigateUrl="~/Page/Login/Id.aspx">
                    아이디찾기
                </asp:HyperLink>
                <asp:HyperLink class="toolItem" runat="server" NavigateUrl="~/Page/Login/Pw.aspx">
                    비밀번호찾기
                </asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
