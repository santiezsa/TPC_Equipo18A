<%@ Page Title="Compras" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionCompras.aspx.cs" Inherits="TPC_Equipo18A.GestionCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-hover > tbody > tr > td.gv-pager,
        .table-hover > tbody > tr:hover > td.gv-pager {
            background-color: transparent !important;
            border: none !important;
        }

        /* Quitar hover dentro de la mini-tabla del pager */
        .gv-pager table,
        .gv-pager tr,
        .gv-pager td,
        .gv-pager tr:hover,
        .gv-pager td:hover {
            background-color: transparent !important;
            border: none !important;
        }

        .gv-pager {
            text-align: center;
            padding: 10px 0 !important;
        }

            .gv-pager a,
            .gv-pager span {
                display: inline-block;
                min-width: 22px;
                margin: 0 4px;
                text-align: center;
                text-decoration: none;
                background: none;
                border: none;
                padding: 0;
                color: #0d6efd;
                font-weight: 500;
            }

            .gv-pager span {
                font-weight: 700;
                text-decoration: underline;
            }

            .gv-pager a:hover {
                text-decoration: underline;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Listado Compras</h3>
    <hr />

    <%-- Panel de confirmacion --%>
    <asp:Panel ID="pnlConfirmacion" runat="server" Visible="false" CssClass="alert alert-danger" role="alert">
        <h4 class="alert-heading">¿Anular Compra?</h4>
        <p>
            <asp:Literal ID="lblConfirmarTexto" runat="server"></asp:Literal>
        </p>
        <p class="small">Se devolverá el stock de los productos involucrados.</p>
        <hr>
        <div class="d-flex justify-content-end">
            <asp:HiddenField ID="hfIdParaAnular" runat="server" />
            <asp:Button ID="btnConfirmarAnulacion" runat="server" Text="Sí, Anular" CssClass="btn btn-danger mr-2" OnClick="btnConfirmarAnulacion_Click" />
            <asp:Button ID="btnCancelarAnulacion" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarAnulacion_Click" />
        </div>
    </asp:Panel>

    <%-- GV --%>
    <asp:Panel ID="pnlGV" runat="server">
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:Button ID="btnExportarExcel"
                        runat="server"
                        Text="Exportar a Excel"
                        CssClass="btn btn-success mb-3"
                        OnClick="btnExportarExcel_Click" />
                    <asp:GridView ID="gvCompras" runat="server"
                        CssClass="table table-hover align-middle text-center"
                        GridLines="None"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        OnRowCommand="gvCompras_RowCommand"
                        OnRowDataBound="gvCompras_RowDataBound"
                        AllowPaging="true"
                        PageSize="15"
                        OnPageIndexChanging="gvCompras_PageIndexChanging"
                        PagerStyle-CssClass="gv-pager"
                        EmptyDataText="No hay compras registradas.">
                        <Columns>
                            <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                            <%-- Proveedor --%>
                            <asp:TemplateField HeaderText="Proveedor">
                                <ItemTemplate>
                                    <%# Eval("Proveedor.Nombre") + " " + Eval("Proveedor.RazonSocial") %>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Usuario">
                                <ItemTemplate>
                                    <%# Eval("Usuario.Username") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-Font-Bold="true" />

                            <%-- Estado ACTIVA O ANULADA --%>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <span class='<%# (bool)Eval("Activo") ? "badge badge-success" : "badge badge-secondary" %>'>
                                        <%# (bool)Eval("Activo") ? "Completada" : "Anulada" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <%-- btn Anular (se oculta desde el back si ya se anulo) --%>
                                    <asp:Button ID="btnAnular" runat="server"
                                        CommandName="Anular"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="Anular"
                                        CssClass="btn btn-sm btn-outline-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
