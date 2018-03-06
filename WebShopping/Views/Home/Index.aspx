<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProductModel>" %>
<%@ Import Namespace="WebShopping.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Webショッピングo(^-^)o
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        商品一覧
    </p>

    <p>
    <%
        if(Model.HasPrevPage)
        {    
         %>
         <%: Html.ActionLink("前項", "/", new { page = Model.CurrentPage -1 }) %>
         <% } else {  %>
            前項
         <% } %>


        <%
        if(Model.HasNextPage)
        {    
         %>
         <%: Html.ActionLink("次項", "/", new { page = Model.CurrentPage +1 }) %>
         <br />
         <% } else {  %>
            次項<br />
         <% } %>
  
        <%
        foreach(var item in Model.Products){
        %>

        <table align="left">
            <tr>
                <td align="center"><img src="/Images/<%: item.id %>.jpg" alt=""/>
                </td>
            </tr>

            <tr>
                <td align="center"><%: item.name %>
                </td>
            </tr>

            <tr>
                <td align="center"><%: string.Format("{0:#.###} 円", item.price )%>/>
                </td>
            </tr>
        
        </table>
       <%
        }
         %>
    <br clear="left" />

    </p>
</asp:Content>
