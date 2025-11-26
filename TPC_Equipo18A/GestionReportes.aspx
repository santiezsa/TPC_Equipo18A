<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/MiMaster.Master"
    AutoEventWireup="true" CodeBehind="GestionReportes.aspx.cs"
    Inherits="TPC_Equipo18A.GestionReportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Reportes</h2>

    <contenttemplate>

        <div class="form-group">
            <label>Tipo de reporte</label>
            <asp:DropDownList ID="ddlTipoReporte" runat="server" CssClass="form-control">
                <asp:ListItem Value="VentasPeriodo">Ventas por período</asp:ListItem>
                <asp:ListItem Value="VentasProducto">Ventas por producto</asp:ListItem>
                <asp:ListItem Value="VentasCliente">Ventas por cliente</asp:ListItem>
                <asp:ListItem Value="ComprasProveedor">Compras por proveedor</asp:ListItem>
                <asp:ListItem Value="StockMinimo">Stock bajo mínimo</asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Filtros genéricos -->
        <div class="row">
            <div class="col-md-3">
                <label>Fecha desde</label>
                <asp:TextBox ID="txtFechaDesde" runat="server"
                    CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <label>Fecha hasta</label>
                <asp:TextBox ID="txtFechaHasta" runat="server"
                    CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <label>Mínimo stock</label>
                <asp:TextBox ID="txtStockMinimo" runat="server"
                    CssClass="form-control" Text="5" />
            </div>
        </div>

        <br />

        <asp:Button ID="btnGenerar" runat="server" Text="Ver reporte"
            CssClass="btn btn-primary"
            OnClick="btnGenerar_Click" />

        <asp:Button ID="btnExportar" runat="server" Text="Exportar a Excel"
            CssClass="btn btn-success"
            OnClick="btnExportar_Click" />
        <br />
        <br />

        <asp:GridView ID="gvReporte" runat="server"
            CssClass="table table-hover align-middle text-center"
            GridLines="None"
            AutoGenerateColumns="true">
        </asp:GridView>

    </contenttemplate>

</asp:Content>
