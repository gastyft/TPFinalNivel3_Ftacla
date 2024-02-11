<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerDetalle.aspx.cs" Inherits="TPFinalNivel3_Ftacla.VerDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Image ID="ImagenArti" AlternateText="Imagen del articulo" runat="server" />
   <h5> <bold>Nombre del articulo:</bold> 
    <asp:Label ID="lblNombre" runat="server" Text="lblNombre.nombre" font-size="XX-Large"></asp:Label>
       </h5>
    <h5>
        Descripcion:
        <asp:Label ID="DescripcionArticulo" runat="server" Text="DescripcionArticulo" font-size="XX-Large"></asp:Label>
    </h5>
    <h5>
        Marca:
        <asp:Label ID="DescripcionMarca" runat="server" Text="Label" font-size="XX-Large" ></asp:Label>
    </h5>
    <h5>
        Categoria:
        <asp:Label ID="DescripcionCate" runat="server" Text="Label" font-size="XX-Large"></asp:Label>
    </h5>
    <h5>
        Precio:
        <asp:Label ID="Precio" runat="server" Text="Label" font-size="XX-Large"></asp:Label>
    </h5>
    <h5>
        Codigo:
        <asp:Label ID="Codigo" runat="server" Text="Label" font-size="XX-Large"></asp:Label>
    </h5>

</asp:Content>
