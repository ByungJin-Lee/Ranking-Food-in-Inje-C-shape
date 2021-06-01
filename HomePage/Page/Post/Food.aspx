<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Food.aspx.cs" Inherits="HomePage.Page.Post.Food" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../../assets/css/Post/Food.css" rel="stylesheet" />
    <title>음식</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div ID="adminControl" class="adminControl" Visible="false" runat="server">
        <asp:HyperLink ID="updata" CssClass="updata adminControlBtn" NavigateUrl="~/Page/Post/Food_admin.aspx?food=" runat="server"></asp:HyperLink>
    </div>
    <div class="content">
        <%-- profile Area --%>
        <table class="profile">
            <tr>
                <td class="proImage" rowspan="3">
                    <asp:Image ID="proImg" runat="server" />
                </td>
                <td class="proTitle" colspan="2">
                    <asp:Label ID="proName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="proStarView" colspan="2">
                    <div class="onStar">
                        <div class="ga" id="proGa" runat="server"></div>
                    </div>                    
                </td>
            </tr>
            <tr>
                <td class="proStar">
                    <asp:Label ID="proStar" runat="server" Text=""></asp:Label>                    
                </td>
                <td class="proCount">
                    총 리뷰 수 : <asp:Label ID="proCount" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <%-- profile End --%>

        <%-- comment input Area --%>
        <table class="commentInput">
            <tr>
                <td>
                    <asp:TextBox ID="inputComment" placeholder="input Comment..." runat="server"></asp:TextBox>
                </td>
                <td>
                    <%-- hidden Radio Btns --%>
                    <input type="radio" ID="star1" GroupName="star" runat="server" checked/>
                    <input type="radio" ID="star2" GroupName="star" runat="server" />
                    <input type="radio" ID="star3" GroupName="star" runat="server" />
                    <input type="radio" ID="star4" GroupName="star" runat="server" />
                    <input type="radio" ID="star5" GroupName="star" runat="server" />
                    <%-- progress --%>
                    <div class="starProgress">
                        <div class="labels">
                            <label for="ContentBody01_star1"></label>
                            <label for="ContentBody01_star2"></label>
                            <label for="ContentBody01_star3"></label>
                            <label for="ContentBody01_star4"></label>
                            <label for="ContentBody01_star5"></label>
                        </div>
                        <div class="progressBack">

                        </div>
                        <div class="progress">

                        </div>
                    </div>                    
                </td>
                <td>
                    <asp:Button ID="submitBtn" runat="server" Text="작성" OnClick="submitBtn_Click" />
                </td>
            </tr>
        </table>
        <%-- comment input End --%>                    
        <div id="commentListBox" class="commentTable" runat="server"></div>  
    </div>    
</asp:Content>
