<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Kind_admin.aspx.cs" Inherits="HomePage.Page.Post.Kind_updata" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../../assets/css/Post/kind.css" rel="stylesheet" />
    <title>종류 - 관리자</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div ID="adminControl" class="adminControl" runat="server">
        <asp:HyperLink ID="updata" CssClass="updata adminControlBtn" NavigateUrl="~/Page/Post/Kind.aspx?c=" runat="server" />
    </div>
    <div class="content">       
        <asp:Button ID="refreshBtn" Text="refresh" runat="server" OnClick="refreshBtn_Click" />
        <div class="foodList">            
            <asp:Table CssClass="foodItems" ID="foodItems" runat="server">

            </asp:Table>
            <div class="plusBox">
                <div class="box front">
                    <span class="plusBtn">
                        +
                    </span>
                </div>
                <div class="box back">
                    <div class="image">                        
                        <img src=" "/>
                    </div>
                    <div class="name">
                        <asp:TextBox ID="foodName" CssClass="input" runat="server" />
                        <asp:FileUpload ID="imageUpload" runat="server" AllowMultiple="False" />
                    </div>
                    <div class="star">
                        <asp:Button ID="addFood" CssClass="addBtn" OnClick="addFood_Click" Text="추가" runat="server" />
                    </div>
                </div>                
            </div>            
        </div>
    </div>
</asp:Content>
