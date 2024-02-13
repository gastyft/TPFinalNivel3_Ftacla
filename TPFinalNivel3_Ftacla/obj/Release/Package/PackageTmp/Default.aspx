<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPFinalNivel3_Ftacla._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

      <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="Proyecto">Proyecto de Carrito con C#ASP .NET </h1>
            <p class="lead">Proyecto para curso Nivel 3 MaxiPrograma </p>
        </section>
                          <% if ( Negocio.Seguridad.esAdmin(Session["usuario"])) { %>
                     <a class="btn btn-info" style="margin-bottom:1%" href="FormularioArticulo.aspx">Agregar nuevo producto</a>
<% } %>
        <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater runat="server" id="Repetidor">

           <ItemTemplate>
                <div class="col">
                <div class="card">
                    <img src="<%#Eval("Imagen") %>" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h3 class="card-title"><%#Eval("Nombre") %></h3>
                        <p class="card-text"><%#Eval("Descripcion") %></p>
                        <h4 class="card-text"> <bold>$<%#((decimal)Eval("Precio")).ToString("F2")%> </bold></h4>
                        <a class="btn btn-info" href="VerDetalle.aspx?id=<%#Eval("Id") %>">Ver Detalle</a>
                                        <% if ( Negocio.Seguridad.esAdmin(Session["usuario"])) { %>
                        <a class="btn btn-info" href="FormularioArticulo.aspx?id=<%#Eval("Id") %>">Editar</a>
                         <% } %>
  <!-- BOTON EJEMPLO CON ASP   <asp:button text="Ejemplo" cssclass="btn btn-primary" runat="server" id="btnEjemplo" CommandArgument='<%#Eval("Id") %>' CommandName="PokemonId" OnClick="btnEjemplo_Click"/> -->
                    </div>
                </div>
            </div>
            </ItemTemplate>
        </asp:Repeater>
      </div>
    </main>

</asp:Content>
