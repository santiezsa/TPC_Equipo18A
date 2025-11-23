<%@ Page Title="Producto" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="TPC_Equipo18A.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hfId" runat="server" />

    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pb-2 mb-3 border-bottom">
        <h1 class="h2">
            <asp:Label ID="lblTitulo" runat="server" Text="Agregar producto"></asp:Label></h1>
        <div class="btn-toolbar mb-2 mb-md-0">
            <asp:HyperLink NavigateUrl="~/GestionProductos.aspx" runat="server" Text="Volver al listado" CssClass="btn btn-sm btn-outline-secondary" />
        </div>
    </div>

    <div class="row">

        <div class="col-lg-8">
            <%-- Datos Principales --%>
            <div class="card shadow-sm mb-4">
                <div class="card-header bg-white">
                    <h5 class="mb-0">Información General</h5>
                </div>
                <div class="card-body">
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <label class="font-weight-bold">Código</label>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" placeholder="Ej: P-001"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtCodigo" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="form-group col-md-8">
                            <label class="font-weight-bold">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del producto"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtNombre" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label class="font-weight-bold">Marca</label>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="ddlMarca" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="form-group col-md-6">
                            <label class="font-weight-bold">Categoría</label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="ddlCategoria" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtDescripcion" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>
                </div>
            </div>

            <%-- Configuiracion --%>
            <div class="card shadow-sm mb-4">
                <div class="card-header bg-white">
                    <h5 class="mb-0">Configuración</h5>
                </div>
                <div class="card-body">
                    <div class="form-row">

                        <%-- Stock minimo --%>
                        <div class="form-group col-md-6">
                            <label>Stock Mínimo (Alerta)</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                <div class="input-group-append"><span class="input-group-text">un.</span></div>
                            </div>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtStockMinimo" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>

                        <%-- Ganancia --%>
                        <div class="form-group col-md-6">
                            <label>Margen Ganancia</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                                <div class="input-group-append"><span class="input-group-text">%</span></div>
                            </div>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtPorcentajeGanancia" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="card-footer bg-light text-right">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success px-4" OnClick="btnGuardar_Click" />
                    <asp:HyperLink NavigateUrl="~/GestionProductos.aspx" runat="server" Text="Cancelar" CssClass="btn btn-link text-danger" />
                </div>
            </div>
        </div>

        <%-- Columna proveedores --%>
        <div class="col-lg-4">

            <asp:Panel ID="pnlProveedores" runat="server" Visible="false">
                <div class="card shadow-sm">
                    <div class="card-header bg-info text-white">
                        <h6 class="mb-0"><i class="bi bi-truck mr-2"></i>Proveedores</h6>
                    </div>
                    <div class="card-body p-3">
                        <label class="small text-muted">Vincular nuevo proveedor:</label>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                            <div class="input-group-append">
                                <asp:Button ID="btnAgregarProveedor" runat="server" Text="+" CssClass="btn btn-sm btn-info" OnClick="btnAgregarProveedor_Click" CausesValidation="false" />
                            </div>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvProveedoresProducto" runat="server"
                                CssClass="table table-sm table-borderless table-striped small mb-0"
                                AutoGenerateColumns="false"
                                DataKeyNames="Id"
                                OnRowCommand="gvProveedoresProducto_RowCommand"
                                GridLines="None"
                                EmptyDataText="Sin proveedores asignados.">
                                <Columns>
                                    <asp:BoundField HeaderText="Empresa" DataField="RazonSocial" />
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="text-danger" ToolTip="Desvincular" CausesValidation="false">
                                                <i class="bi bi-x-circle-fill"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Label ID="lblMensajeProveedores" runat="server" Visible="false">
                <div class="alert alert-secondary mt-0" role="alert">
                    <h6 class="alert-heading"><i class="bi bi-info-circle"></i> Atención</h6>
                    <p class="small mb-0">Guarde el producto primero para poder habilitar la asignación de proveedores.</p>
                </div>
            </asp:Label>

        </div>
    </div>
</asp:Content>
