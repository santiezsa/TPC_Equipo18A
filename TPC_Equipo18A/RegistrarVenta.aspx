<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="RegistrarVenta.aspx.cs" Inherits="TPC_Equipo18A.RegistrarVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Venta</h3>
    <p>Completá los datos para generar una nueva venta.</p>

    <div class="row">
        <div class="col-md-8">
            <div class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title">Agregar Producto</h5>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Producto</label>
                            <%-- Dropdown, ver posibilidad de buscar por nombre --%>
                            <asp:DropDownList ID="ddlProductoVenta" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Cantidad</label>
                            <asp:TextBox ID="txtCantidadVenta" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 d-flex align-items-end">
                            <asp:Button ID="btnAgregarProductoVenta" runat="server" Text="Agregar" OnClick="btnAgregarProductoVenta_Click" CssClass="btn btn-secondary btn-block" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    Detalle de la venta
                </div>
                <div class="card-body" style="padding: 0;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDetalleVenta" runat="server" 
                            CssClass="table table-hover align-middle text-center" 
                            AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true" 
                            GridLines="None"
                            EmptyDataText="Agregue productos al carrito.">
                            <Columns>
                                <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                                <asp:BoundField HeaderText="Precio Unit." DataField="PrecioUnitario" DataFormatString="{0:C}" />
                                <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title">Cliente</h5>
                    <div class="form-group">
                        <label>Seleccionar Cliente</label>
                        <asp:DropDownList ID="ddlClienteVenta" runat="server" CssClass="form-control"></asp:DropDownList>
                        <%-- Verificar de agregar algun dato mas para corroborar que esta ok el cliente --%>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body text-right">
                    <h4 class="mb-3">Total: <asp:Label ID="lblTotalVenta" runat="server" Text="$0.00" CssClass="font-weight-bold"></asp:Label></h4>
                    <asp:Button ID="btnConfirmarVenta" runat="server" Text="Confirmar Venta" OnClick="btnConfirmarVenta_Click" CssClass="btn btn-success btn-lg btn-block" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
