<%@ Page Title="Ajuste de Stock" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="AjusteStock.aspx.cs" Inherits="TPC_Equipo18A.AjusteStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card shadow">
                <div class="card-header bg-warning text-dark">
                    <h4 class="mb-0"><i class="bi bi-exclamation-triangle"></i> Ajuste de Stock Manual</h4>
                </div>
                <div class="card-body">

                    <p class="text-muted small">Utilice este formulario para registrar bajas por rotura, pérdida, robo o ajustes de inventario.</p>

                    <%-- Seleccion de producto --%>
                    <div class="mb-3">
                        <label class="form-label">Producto</label>
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Seleccione un producto" ControlToValidate="ddlProducto" InitialValue="" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <div class="row">
                        <%-- Cantidad --%>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Cantidad</label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ej: 5"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtCantidad" CssClass="text-danger small" Display="Dynamic" runat="server" />
                            <asp:RangeValidator ErrorMessage="Debe ser mayor a 0" ControlToValidate="txtCantidad" MinimumValue="1" MaximumValue="9999" Type="Integer" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>

                        <%-- Tipo de movimiento --%>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Tipo de Ajuste</label>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Egreso (Resta Stock)" Value="REST" Selected="True" />
                                <asp:ListItem Text="Ingreso (Suma Stock)" Value="SUM" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <%-- Motivo (lo hago obligatorio) --%>
                    <div class="mb-3">
                        <label class="form-label">Motivo / Comentario</label>
                        <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Ej: Caja dañada por lluvia en depósito..."></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="El motivo es obligatorio." ControlToValidate="txtMotivo" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <hr />

                    <div class="d-flex justify-content-between">
                        <asp:HyperLink NavigateUrl="~/GestionProductos.aspx" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Confirmar Ajuste" CssClass="btn btn-warning" OnClick="btnGuardar_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
