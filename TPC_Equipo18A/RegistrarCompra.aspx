<%@ Page Title="Registrar compra" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="RegistrarCompra.aspx.cs" Inherits="TPC_Equipo18A.RegistrarCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Registrar Compra</h3>
    <p>Complete los datos para registrar un ingreso de stock.</p>

    <div class="row">
        <div class="col-md-4">
            <%-- Seleccion proveedor --%>
            <div class="card mb-3">
                <div class="card-header bg-primary text-white">
                    Datos del Proveedor
               
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label">Proveedor</label>
                        <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Seleccione un proveedor" ControlToValidate="ddlProveedor" InitialValue="" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Compra" runat="server" />
                    </div>
                </div>
            </div>

            <%-- Agregar productos --%>
            <div class="card">
                <div class="card-header bg-secondary text-white">
                    Agregar Producto
               
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label">Producto</label>
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Seleccione producto" ControlToValidate="ddlProducto" InitialValue="" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Agregar" runat="server" />
                    </div>

                    <div class="row">
                        <div class="col-6 mb-3">
                            <label class="form-label">Cantidad</label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtCantidad" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Agregar" runat="server" />
                            <asp:RangeValidator ErrorMessage="Mínimo 1" ControlToValidate="txtCantidad" MinimumValue="1" MaximumValue="10000" Type="Integer" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Agregar" runat="server" />
                            <asp:RangeValidator
                                ErrorMessage="Mínimo 1"
                                ControlToValidate="txtCantidad"
                                MinimumValue="1" MaximumValue="10000" Type="Integer"
                                ValidationGroup="Agregar"
                                CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="col-6 mb-3">
                            <label class="form-label">Costo Unit. ($)</label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtPrecio" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Agregar" runat="server" />
                            <asp:RangeValidator
                                ErrorMessage="Debe ser positivo"
                                ControlToValidate="txtPrecio"
                                MinimumValue="0,01" MaximumValue="9999999" Type="Double"
                                ValidationGroup="Agregar"
                                CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar al Listado" CssClass="btn btn-outline-success w-100" OnClick="btnAgregar_Click" ValidationGroup="Agregar" />
                </div>
            </div>
        </div>

        <%-- gv y total --%>
        <div class="col-md-8">
            <div class="card h-100">
                <div class="card-header">
                    Detalle de la Compra
               
                </div>
                <div class="card-body d-flex flex-column">

                    <div class="table-responsive flex-grow-1">
                        <asp:GridView ID="gvDetalle" runat="server"
                            CssClass="table table-hover"
                            AutoGenerateColumns="false"
                            OnRowCommand="gvDetalle_RowCommand"
                            EmptyDataText="No hay productos agregados.">
                            <Columns>
                                <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
                                <asp:BoundField HeaderText="Cant." DataField="Cantidad" />
                                <asp:BoundField HeaderText="Precio Unit." DataField="PrecioUnitario" DataFormatString="{0:C}" />
                                <asp:BoundField HeaderText="Subtotal" DataField="SubTotal" DataFormatString="{0:C}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button runat="server" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' Text="🗑" CssClass="btn btn-sm btn-danger" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="mt-3 text-right">
                        <h4>Total:
                            <asp:Label ID="lblTotal" runat="server" Text="$0.00"></asp:Label></h4>
                    </div>

                    <hr />

                    <div class="d-flex justify-content-end">
                        <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" Text="Cancelar" CssClass="btn btn-secondary me-2" />
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra" CssClass="btn btn-success btn-lg" OnClick="btnConfirmar_Click" ValidationGroup="Compra" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
