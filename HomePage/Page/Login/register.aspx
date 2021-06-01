<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="HomePage.assets.css.login.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <title>뭐물래? - 회원가입</title>
    <link href="../../assets/css/login/register.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody01" runat="server">
    <div class="back"></div>
    <div class="content">
        <div class="register">
            <span class="title">Register</span>
            <table class="registerTable">
                <tr><td colspan="3"><div class="line"></div></td></tr>
                <tr>
                    <td>이름</td>
                    <td colspan="2"><asp:TextBox ID="txtName" placeholder="8자 이하" runat="server" CssClass="input"></asp:TextBox></td>
                </tr>
                <tr><td colspan="3"><div class="line"></div></td></tr>
                <tr>
                    <td>아이디</td>
                    <td>
                        <asp:TextBox ID="txtId" placeholder="15자 이하, 한글 미포함" runat="server" CssClass="input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button Id="idCheckBtn" Text="사용 여부" runat="server" CssClass="idBtn" OnClick="idCheckBtn_Click" />
                    </td>
                </tr>
                <tr><td colspan="3"><div class="line"></div></td></tr>
                <tr>
                    <td>비밀번호</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtPassword" placeholder="15자 이하" runat="server" TextMode="Password" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="3"><div class="line"></div></td></tr>
                <tr>
                    <td>비밀번호 확인</td>
                    <td>
                        <asp:TextBox ID="txtPwCheck" runat="server" TextMode="Password" CssClass="input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="pwSame" Text="" runat="server" CssClass="pwBtn" />
                    </td>
                </tr>
                <tr><td colspan="3"><div class="line"></div></td></tr>
                <tr>
                    <td>성별</td>
                    <td class="genderBox">
                        <asp:RadioButton ID="male" Text=" 남성" runat="server" CssClass="gender" GroupName="gender" />
                        <asp:RadioButton ID="female" Text=" 여성" runat="server" CssClass="gender" GroupName="gender" />
                    </td>
                    <td>
                        <asp:Label ID="Label1" Text="" runat="server" CssClass="pwBtn" />
                    </td>
                </tr>
                <tr><td colspan="3"><div class="line"></div></td></tr>
                <tr>
                    <td>이메일</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input inputEmail" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="emailCheck" Text="수신 동의" runat="server" CssClass="emailCheck" />
                    </td>
                </tr>
                <tr><td colspan="3"><div class="line"></div></td></tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="alert" runat="server" CssClass="auto-style1" ForeColor="Red" />
                    </td>
                    <td colspan="1">
                        <asp:Button ID="registerBtn" Text="가입" runat="server" class="registerBtn" OnClick="registerBtn_Click"/>
                    </td>                        
                </tr>
            </table>                
        </div>
    </div>
</asp:Content>
