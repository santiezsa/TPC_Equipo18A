<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="TPC_Equipo18A.GestionProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <h3>Productos</h3>
        <p>Acá podés administrar el catálogo de productos.</p>

        <%-- TODO: Agregar formato al gridview --%>
        <asp:GridView ID="gvProductos" runat="server" CssClass="table table-hover" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
                <asp:BoundField HeaderText="Stock" DataField="StockActual" />
            </Columns>
        </asp:GridView>
</asp:Content>
