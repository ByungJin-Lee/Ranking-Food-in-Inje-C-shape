<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="code.aspx.cs" Inherits="HomePage.Page.Login.code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../../assets/css/login/find.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div class="content">
        <div class="find">
            <span class="title">이메일 인증</span>
            <span class="explain">서비스를 이용하실려면 인증을 해주세요.</span>
            <table class="findTable">
                <tr><td colspan="2"><div class="line"></div></td></tr>
                <tr>
                    <td class="st">인증 코드</td>
                    <td><asp:TextBox ID="txtCode" runat="server" CssClass="input"></asp:TextBox></td>
                </tr>                    
                <tr><td colspan="2"><div class="line"></div></td></tr>                    
            </table>
            <asp:Label ID="alert" runat="server" CssClass="alert"></asp:Label>
            <asp:Button Id="findBtn" Text="인증 하기" runat="server" CssClass="findBtn" OnClick="findBtn_Click"/>
        </div>
    </div>
    
</asp:Content>
