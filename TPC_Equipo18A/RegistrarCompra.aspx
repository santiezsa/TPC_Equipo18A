<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="RegistrarCompra.aspx.cs" Inherits="TPC_Equipo18A.RegistrarCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Registrar Compra</h3>
    <p>Complete los datos para registrar un ingreso de stock.</p>

    <div class="row">
        <div class="col-md-8">
            <div class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title">Agregar Producto</h5>
                    <div class="form-row">
                        <div class="form-group col-md-5">
                            <label>Producto</label>
                            <asp:DropDownList ID="ddlProductoCompra" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group col-md-2">
                            <label>Cantidad</label>
                            <asp:TextBox ID="txtCantidadCompra" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Precio Costo (Unit.)</label>
                            <asp:TextBox ID="txtPrecioCosto" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-2 d-flex align-items-end">
                            <asp:Button ID="btnAgregarProductoCompra" runat="server" Text="Agregar" CssClass="btn btn-secondary btn-block" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    Detalle de Compra
                </div>
                <div class="card-body" style="padding: 0;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDetalleCompra" runat="server" 
                            CssClass="table table-hover align-middle mb-0" 
                            AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true" 
                            EmptyDataText="Agregue productos.">
                            <Columns>
                                <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                                <asp:BoundField HeaderText="Precio Costo" DataField="PrecioCosto" DataFormatString="{0:C}" />
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
                    <h5 class="card-title">Proveedor</h5>
                    <div class="form-group">
                        <label>Seleccionar Proveedor</label>
                        <%-- Verificar de agregar algun dato mas para corroborar que esta ok el proveedor --%>
                        <asp:DropDownList ID="ddlProveedorCompra" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body text-right">
                    <h4 class="mb-3">Total: <asp:Label ID="lblTotalCompra" runat="server" Text="$0.00" CssClass="font-weight-bold"></asp:Label></h4>
                    <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-success btn-lg btn-block" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
