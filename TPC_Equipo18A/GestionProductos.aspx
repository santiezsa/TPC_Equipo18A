<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="TPC_Equipo18A.GestionProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h3>Productos</h3>
        <%--<p>Acá podés administrar el catálogo de productos.</p>--%>
        <asp:HyperLink NavigateUrl="~/FormularioProducto.aspx" runat="server" Text="Nuevo Producto" CssClass="btn btn-primary mb-3" />
    </div>
    <p>Vea y administre los productos registrados</p>
    <%-- TODO: Agregar formato al gridview --%>
    <asp:GridView ID="gvProductos" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" EmptyDataText="No hay productos que mostrar.">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
            <asp:BoundField HeaderText="Stock" DataField="StockActual" />

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
