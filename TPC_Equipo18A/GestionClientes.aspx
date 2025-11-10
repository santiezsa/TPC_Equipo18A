<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionClientes.aspx.cs" Inherits="TPC_Equipo18A.GestionClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h3>Clientes</h3>
        <asp:Button ID="btnNuevoCliente" runat="server" Text="Nuevo Cliente" CssClass="btn btn-primary" />
    </div>
    <p>Administre sus clientes registrados en el sistema.</p>

    <%-- Panel de confirmacion (hidden) --%>
    <asp:Panel ID="pnlConfirmacion" runat="server" Visible="false" CssClass="alert alert-warning" role="alert">
        <h4 class="alert-heading">¿Confirmar eliminación?</h4>
        <p>
            <asp:Literal ID="lblConfirmarTexto" runat="server"></asp:Literal>
        </p>
        <hr>
        <div class="d-flex justify-content-end">
            <%-- HiddenField para guardar el ID a eliminar --%>
            <asp:HiddenField ID="hfIdParaEliminar" runat="server" />
            <asp:Button ID="btnConfirmarEliminacion" runat="server" Text="Si, eliminar" CssClass="btn btn-danger mr-2" OnClick="btnConfirmarEliminacion_Click" />
            <asp:Button ID="btnCancelarEliminacion" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarEliminacion_Click" />
        </div>
    </asp:Panel>

    <%-- Panel de la GV --%>
    <asp:Panel ID="pnlGV" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvClientes" runat="server"
                        CssClass="table table-hover align-middle"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        OnRowCommand="gvClientes_RowCommand"
                        EmptyDataText="No hay clientes registrados.">
                        <Columns>
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                            <asp:BoundField HeaderText="Email" DataField="Email" />
                            <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                            <asp:BoundField HeaderText="Dirección" DataField="Direccion" />

                            <%-- Columna de Acciones --%>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <%-- Editar - Le pasa el ID al form --%>
                                    <asp:HyperLink runat="server"
                                        NavigateUrl='<%# "~/FormularioClientes.aspx?id=" + Eval("Id") %>'
                                        CssClass="btn btn-light">
                                        <i class="bi bi-pencil"></i> Editar
                                    </asp:HyperLink>

                                    <%-- Eliminar - No necesita ir al form --%>
                                    <asp:Button runat="server"
                                        CommandName="Eliminar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="Eliminar"
                                        CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
