<%@ Page Title="Producto" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="TPC_Equipo18A.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hfId" runat="server" />

    <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Agregar Producto"></asp:Label>
    </h3>
    <hr />

    <div class="row">
        <div class="col-md-8">

            <div class="card mb-4">
                <div class="card-body">

                    <%-- Codigo y nombre --%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Código</label>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtCodigo" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="form-group col-md-6">
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtNombre" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <%-- Marca y categoria --%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Marca</label>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="ddlMarca" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="form-group col-md-6">
                            <label>Categoría</label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="ddlCategoria" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <%-- Descripcion --%>
                    <div class="form-group">
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtDescripcion" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <%-- Stocks y ganancia --%>
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <label>Stock Actual</label>
                            <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtStockActual" CssClass="text-danger small" Display="Dynamic" runat="server" />
                            <asp:RegularExpressionValidator ErrorMessage="Solo enteros" ControlToValidate="txtStockActual" ValidationExpression="^\d+$" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>

                        <div class="form-group col-md-4">
                            <label>Stock Mínimo</label>
                            <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtStockMinimo" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>

                        <div class="form-group col-md-4">
                            <label>Ganancia (%)</label>
                            <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtPorcentajeGanancia" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <hr />

                    <%-- btns Guardar/Cancelar --%>
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                        <asp:HyperLink NavigateUrl="~/GestionProductos.aspx" runat="server" Text="Cancelar" CssClass="btn btn-secondary ml-2" />
                    </div>
                </div>
            </div>
            <%-- Seccion para agregar nuevos proveedores --%>

            <asp:Panel ID="pnlProveedores" runat="server" Visible="false">
                <div class="card border-info">
                    <div class="card-header bg-info text-white">
                        <strong>Proveedores del Producto</strong>
                    </div>
                    <div class="card-body">

                        <%-- ddl para vincular nuevo proveedor --%>
                        <div class="form-row align-items-end mb-3">
                            <div class="form-group col-md-8">
                                <label>Seleccionar Proveedor:</label>
                                <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group col-md-4">
                                <asp:Button ID="btnAgregarProveedor" runat="server" Text="Vincular" CssClass="btn btn-outline-primary btn-block" OnClick="btnAgregarProveedor_Click" CausesValidation="false" />
                                <%-- CausesValidation="false" es IMPORTANTE para que no valide los campos de arriba al agregar un proveedor --%>
                            </div>
                        </div>

                        <hr />

                        <%-- gv proveedores vinculados --%>
                        <div class="table-responsive">
                            <asp:GridView ID="gvProveedoresProducto" runat="server"
                                CssClass="table table-sm table-bordered table-hover"
                                AutoGenerateColumns="false"
                                DataKeyNames="Id"
                                OnRowCommand="gvProveedoresProducto_RowCommand"
                                EmptyDataText="Este producto no tiene proveedores asignados.">
                                <Columns>
                                    <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial" />
                                    <asp:BoundField HeaderText="CUIT" DataField="CUIT" />
                                    <asp:BoundField HeaderText="Email" DataField="Email" />

                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%-- btn que desvincula proveedor --%>
                                            <asp:Button runat="server"
                                                CommandName="Eliminar"
                                                CommandArgument='<%# Eval("Id") %>'
                                                Text="Desvincular"
                                                CssClass="btn btn-sm btn-danger"
                                                CausesValidation="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </asp:Panel>

            <%-- mensaje cuando creo producto nuevo --%>
            <asp:Label ID="lblMensajeProveedores" runat="server" CssClass="text-muted font-italic mt-3 d-block" Text="Guarde el producto primero para poder asignar proveedores." Visible="false"></asp:Label>

        </div>
    </div>
</asp:Content>
