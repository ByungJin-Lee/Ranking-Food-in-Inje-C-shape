<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Food_admin.aspx.cs" Inherits="HomePage.Page.Post.Food_admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../../assets/css/Post/Food.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div class="content">
        <asp:Button ID="refreshBtn" Text="refresh" runat="server" OnClick="refreshBtn_Click" />
        <div id="commentTable" class="commentTable" runat="server">

        </div>
    </div>
</asp:Content>
