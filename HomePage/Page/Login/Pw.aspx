<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Pw.aspx.cs" Inherits="HomePage.Page.Login.Pw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../../assets/css/login/find.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div class="content">
        <div class="find">
            <span class="title">비밀번호 찾기</span>
            <table class="findTable">
                <tr><td colspan="2"><div class="line"></div></td></tr>
                <tr>
                    <td class="st">아이디</td>
                    <td><asp:TextBox ID="txtId" runat="server" CssClass="input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="st">이메일</td>
                    <td><asp:TextBox ID="txtEmail" runat="server" CssClass="input"></asp:TextBox></td>
                </tr>
                <tr><td colspan="2"><div class="line"></div></td></tr>                    
            </table>
            <asp:Label ID="alert" runat="server" CssClass="alert"></asp:Label>
            <asp:Button Id="findBtn" Text="비밀번호 찾기" runat="server" CssClass="findBtn" OnClick="findBtn_Click"/>
        </div>
    </div>
</asp:Content>
