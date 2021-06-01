<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Warning.aspx.cs" Inherits="HomePage.Page.Post.warning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <title>경고 - 뭐물래?</title>
    <link href="../../assets/css/login/find.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div class="content">
        <div class="warning">
            <span class="title">경고</span>   
            <span class="explain">올바르지 못한 접근</span>
            <asp:HyperLink ID="goHome" CssClass="link" NavigateUrl="~/Home.aspx" runat="server">메인으로 바로가기</asp:HyperLink>
        </div>
    </div>    
</asp:Content>
