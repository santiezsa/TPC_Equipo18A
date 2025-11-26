<%-- PAGINA DEFAULT DE BIENVENIDA, DESPUES DEL LOGIN --%>

<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo18A.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card {
            border: none;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.05);
        }

        /* --- Simulación de gráfico de barras --- */
        .chart-container {
            display: flex;
            justify-content: space-around;
            align-items: flex-end;
            height: 150px;
            padding: 10px 0;
        }

        .chart-bar {
            width: 15%;
            background-color: #e0e0e0;
            border-radius: 5px 5px 0 0;
            text-align: center;
            font-size: 0.8rem;
            color: #555;
            padding-top: 5px;
            position: relative;
        }

            .chart-bar.active {
                background-color: #007bff;
            }

        .chart-bar-label {
            position: absolute;
            bottom: -20px;
            width: 100%;
            text-align: center;
            font-size: 0.8rem;
            color: #888;
        }

        /* --- Insignias de Estado (Badges) --- */
        .badge-pagado {
            background-color: #e6f7ec;
            color: #28a745;
        }

        .badge-pendiente {
            background-color: #fff8e6;
            color: #ffc107;
        }

        .badge-atrasado {
            background-color: #fdeeee;
            color: #dc3545;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h4>Resumen General</h4>
    <p class="text-muted">Este es el resumen del negocio.</p>
    <div class="row">

        <div class="col-lg-8 mb-4">
            <div class="card h-100">
                <div class="card-body">
                    <h5 class="card-title">Ventas del Mes</h5>
                    <div class="d-flex align-items-center">
                        <%-- Label monto total --%>
                        <h2 class="mb-0">
                            <asp:Label ID="lblTotalVentasMes" runat="server" Text="$0.00"></asp:Label>
                        </h2>

                        <%-- Label porcentaje --%>
                        <span class="ms-3">
                            <asp:Label ID="lblPorcentajeCambio" runat="server" CssClass="small"> </asp:Label>
                        </span>
                    </div>

                    <div class="chart-container mt-4">
                        <div id="barSemana1" runat="server" class="chart-bar" style="height: 0%;">
                            <span class="chart-bar-label">Semana 1</span>
                            <asp:Label ID="lblMontoSem1" runat="server" Visible="false"></asp:Label>
                        </div>

                        <div id="barSemana2" runat="server" class="chart-bar" style="height: 0%;">
                            <span class="chart-bar-label">Semana 2</span>
                        </div>

                        <div id="barSemana3" runat="server" class="chart-bar" style="height: 0%;">
                            <span class="chart-bar-label">Semana 3</span>
                        </div>

                        <div id="barSemana4" runat="server" class="chart-bar" style="height: 0%;">
                            <span class="chart-bar-label">Semana 4+</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4 mb-4">
            <div class="card h-100">
                <div class="card-body">
                    <h5 class="card-title">Alerta de Stock Bajo</h5>
                    <ul class="list-group list-group-flush">
                        <asp:Repeater ID="rptStockBajo" runat="server">
                            <ItemTemplate>
                                <li class="list-group-item d-flex justify-content-between align-items-center px-0">
                                    <%# Eval("Nombre") %>

                                    <span class='<%# (int)Eval("StockActual") <= 5 ? "text-danger font-weight-bold" : "text-warning font-weight-bold" %>'>
                                        <i class="bi bi-dot"></i>
                                        <%# Eval("StockActual") %> Unidades
                                    </span>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                <% if (rptStockBajo.Items.Count == 0)
                                    { %>
                                <li class="list-group-item text-center text-muted px-0">No hay productos con stock bajo.
                                </li>
                                <% } %>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Ventas Recientes</h5>

                    <div class="table-responsive">
                        <table class="table table-hover align-middle text-center">
                            <thead>
                                <tr>
                                    <th>ID VENTA</th>
                                    <th>CLIENTE</th>
                                    <th>FECHA</th>
                                    <th class="text-center">MONTO</th>
                                </tr>
                            </thead>

                            <tbody>
                                <asp:Repeater ID="repVentasRecientes" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <strong>#INV-<%# Eval("Id") %></strong>
                                            </td>
                                            <td>
                                                <%# Eval("Cliente.NombreCompleto") %>
                                            </td>
                                            <td>
                                                <%# Eval("Fecha", "{0:yyyy-MM-dd}") %>
                                            </td>
                                            <td class="text-center">
                                                <%# String.Format("{0:C2}", Eval("Total")) %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
