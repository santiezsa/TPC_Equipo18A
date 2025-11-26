<%@ Page Title="Productos" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="TPC_Equipo18A.GestionProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Titulo, btn y buscador para filtrar --%>
    <div class="row align-items-center mb-4">
        <div class="col-auto">
            <h3 class="mb-0">Productos</h3>
        </div>
        <div class="col-auto">
            <asp:HyperLink NavigateUrl="~/FormularioProducto.aspx" runat="server"
                CssClass="btn btn-success" ToolTip="Crear nuevo producto">
                <i class="bi bi-plus-lg"></i> Nuevo
            </asp:HyperLink>
        </div>
        <div class="col">
            <div class="input-group">
                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"
                    placeholder="Buscar por nombre, marca o código..." AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>
    </div>
    <p>Vea y administre los productos registrados</p>

    <%-- Panel de confirmacion (hidden) --%>
    <asp:Panel ID="pnlConfirmacion" runat="server" Visible="false" CssClass="alert alert-warning" role="alert">
        <h4 class="alert-heading">¿Confirmar eliminación?</h4>
        <p>
            <asp:Literal ID="lblConfirmarTexto" runat="server"></asp:Literal>
        </p>
        <hr>
        <div class="d-flex justify-content-end">
            <%-- HiddenField para guardar el ID a eliminar --%>
            <asp:HiddenField ID="hfIdParaEliminar" runat="server" />
            <asp:Button ID="btnConfirmarEliminacion" runat="server" Text="Si, eliminar" CssClass="btn btn-danger mr-2" OnClick="btnConfirmarEliminacion_Click" />
            <asp:Button ID="btnCancelarEliminacion" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarEliminacion_Click" />
        </div>
    </asp:Panel>

    <%-- Panel de la GV --%>
    <asp:Panel ID="pnlGV" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvProductos" runat="server" CssClass="table table-hover align-middle text-center" OnRowCommand="gvProductos_RowCommand" DataKeyNames="Id" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No hay productos que mostrar.">
                        <Columns>
                            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
                            <%--                            <asp:BoundField HeaderText="Stock" DataField="StockActual" />--%>


                            <%-- Columna de Acciones --%>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <%-- Editar - Le pasa el ID al form --%>
                                    <asp:HyperLink runat="server"
                                        NavigateUrl='<%# "~/FormularioProducto.aspx?id=" + Eval("Id") %>'
                                        CssClass="btn btn-sm btn-outline-primary mr-2">
                                        <i class="bi bi-pencil"></i> Editar
                                    </asp:HyperLink>

                                    <%-- Eliminar - No necesita ir al form --%>
                                    <asp:Button runat="server"
                                        CommandName="Eliminar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="Eliminar"
                                        CssClass="btn btn-sm btn-outline-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--  --%>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <%-- Muestra el stock actual --%>
                                    <asp:Label Text='<%# Eval("StockActual") %>' runat="server" />

                                    <%-- Btn de acceso al ajuste de stock --%>
                                    <asp:HyperLink runat="server"
                                        NavigateUrl='<%# "~/AjusteStock.aspx?id=" + Eval("Id") %>'
                                        CssClass="btn btn-sm btn-outline-warning ms-2"
                                        ToolTip="Ajustar Stock">
                                        <i class="bi bi-arrow-left-right"></i>
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
