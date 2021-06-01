<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Kind.aspx.cs" Inherits="HomePage.Page.Post.Kind" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../../assets/css/Post/kind.css" rel="stylesheet" />
    <title>종류</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div ID="adminControl" class="adminControl" Visible="false" runat="server">
        <asp:HyperLink ID="updata" CssClass="updata adminControlBtn" NavigateUrl="~/Page/Post/Kind_admin.aspx?c=" runat="server"></asp:HyperLink>
    </div>
    <div class="content">
        <div class="price">
            <div class="priceImg">

            </div>
            <table class="item">
                <tr>
                    <td rowspan="3">
                        <asp:Image Id="oneImage" CssClass="itemImage" runat="server"></asp:Image>
                    </td>
                    <td>
                        <asp:Label Id="oneTitle" CssClass="itemTitle" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Id="oneStar" CssClass="itemStar" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label Id="oneComment" CssClass="itemStar" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="controlBox">
                        <asp:HyperLink ID="goFood" CssClass="goDetail" runat="server" NavigateUrl="~/Page/Post/Food.aspx?food=">review ></asp:HyperLink>    
                    </td>
                </tr>                                                
            </table>             
        </div>          
        <asp:Table CssClass="foodItems" ID="foodItems" runat="server">

        </asp:Table>        
    </div>    
</asp:Content>
