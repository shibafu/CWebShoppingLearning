<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WebShopping.Models.ProductItemModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Webショッピング - <%: Model.Product.name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.Product.name %></h2>

    <p>
        <img src="/images/<%:Model.Product.id %>.jpg" alt="" /><br />
        商品ID:<%: Model.Product.id %><br />
        価格:<%: Model.Product.price %><br />
        詳細情報:<%: Model.ProductDetail.description %><br />

       <% if (User.Identity.IsAuthenticated)
                   {%>
       <tr>
           <td align=center>【買う】</td>
       </tr>
       <% } %>
    </p>

    <br />
    <a href="" onclick="history.back(); return false">戻る</a>

</asp:Content>
