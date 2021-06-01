<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HomePage.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <title>Log - User</title>  
    <link href="../../assets/css/Home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>       
    <div class="content">   
        <input type="radio" id="one" name="h1" />
        <input type="radio" id="two"name="h1" />
        <input type="radio" id="three"name="h1" />
        <input type="radio" id="four"name="h1" />
        <div class="labels">
            <label for="one">1</label>
            <label for="two">2</label>
            <label for="three">3</label>
            <label for="four">4</label>
        </div>
        <div class="dishs">
            <div class="dish">
                <div class="dishImg"></div>
                <div class="dishContent">
                    <div class="dishInnerItem">
                        <div class="itemTitle">
                            인정관
                        </div>
                        <div class="itemMenu">
                            <div class="menuItem">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/Post/Kind.aspx?c=인정관">리뷰 보기</asp:HyperLink>
                            </div>
                            <div class="menuItem">
                                <a href="https://www.inje.ac.kr/kor/Template/Bsub_page.asp?Ltype=5&Ltype2=3&Ltype3=3&Tname=S_Food&Ldir=board/S_Food&Lpage=s_food_view&d1n=5&d2n=4&d3n=4&d4n=0">식단 보기</a>
                            </div>
                        </div>
                    </div>
                </div>                                  
            </div>
            <div class="dish">
                <div class="dishImg"></div>
                <div class="dishContent">
                    <div class="dishInnerItem">
                        <div class="itemTitle">
                            인덕재
                        </div>
                        <div class="itemMenu">
                            <div class="menuItem">
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Page/Post/Kind.aspx?c=인덕재">리뷰 보기</asp:HyperLink>
                            </div>
                            <div class="menuItem">
                                <a href="https://dorm.inje.ac.kr/M02/M02_S04_SS02.php?mn_code=M02_S04_SS02">식단 보기</a>
                            </div>
                        </div>
                    </div>
                </div>                                  
            </div>
            <div class="dish">
                <div class="dishImg"></div>
                <div class="dishContent">
                    <div class="dishInnerItem">
                        <div class="itemTitle">
                            늘빛관
                        </div>
                        <div class="itemMenu">
                            <div class="menuItem">
                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Page/Post/Kind.aspx?c=늘빛관">늘빛관</asp:HyperLink>                
                            </div>
                            <div class="menuItem">
                                <a href="https://www.inje.ac.kr/kor/Template/Bsub_page.asp?Ltype=5&Ltype2=3&Ltype3=3&Tname=S_Food&Ldir=board/S_Food&Lpage=s_food_view&d1n=5&d2n=4&d3n=4&d4n=0">식단 보기</a>
                            </div>
                        </div>
                    </div>
                </div>                                  
            </div>
            <div class="dish">
            <div class="dishImg"></div>
            <div class="dishContent">
                <div class="dishInnerItem">
                    <div class="itemTitle">
                        그외
                    </div>
                    <div class="itemMenu">
                        <div class="menuItem">
                            리뷰 보기                            
                        </div>
                        <div class="menuItem">
                            리뷰 보기
                        </div>
                    </div>
                </div>
            </div>                                  
        </div>
        </div>

    </div>
    
            
</asp:Content>
