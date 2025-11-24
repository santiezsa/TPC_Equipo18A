<%@ Page Title="Gestión de ventas" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionVentas.aspx.cs" Inherits="TPC_Equipo18A.GestionVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Historial de ventas</h3>
    <hr />

    <%-- Panel de confirmacion --%>
    <asp:Panel ID="pnlConfirmacion" runat="server" Visible="false" CssClass="alert alert-danger" role="alert">
        <h4 class="alert-heading">¿Anular Venta?</h4>
        <p>
            <asp:Literal ID="lblConfirmarTexto" runat="server"></asp:Literal></p>
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
                    <asp:GridView ID="gvVentas" runat="server"
                        CssClass="table table-hover align-middle"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        OnRowCommand="gvVentas_RowCommand"
                        OnRowDataBound="gvVentas_RowDataBound"
                        EmptyDataText="No hay ventas registradas.">
                        <Columns>
                            <asp:BoundField HeaderText="Nro. Factura" DataField="NumeroFactura" />
                            <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                            <%-- Cliente --%>
                            <asp:TemplateField HeaderText="Cliente">
                                <ItemTemplate>
                                    <%# Eval("Cliente.Nombre") + " " + Eval("Cliente.Apellido") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vendedor">
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
