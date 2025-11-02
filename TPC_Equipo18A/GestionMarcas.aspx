<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionMarcas.aspx.cs" Inherits="TPC_Equipo18A.GestionMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h3>Marcas</h3>
        <%-- Boton para ir al form (vacio) --%>
        <asp:HyperLink NavigateUrl="~/FormularioMarca.aspx" runat="server"
            Text="Agregar Marca" CssClass="btn btn-primary" />
    </div>
    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvMarcas" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="false"
                    DataKeyNames="Id"
                    OnRowCommand="gvMarcas_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="Id" />
                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />

                        <%-- Columna de Acciones --%>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <%-- Editar - Le pasa el ID al form --%>
                                <asp:HyperLink runat="server"
                                    NavigateUrl='<%# "~/FormularioMarca.aspx?id=" + Eval("Id") %>'
                                    CssClass="btn btn-sm btn-outline-primary mr-2">
                                <i class="bi bi-pencil"></i> Editar
                            </asp:HyperLink>

                                <%-- Eliminar - No necesito ir al form --%>
                                <asp:Button runat="server"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("Id") %>'
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    OnClientClick="return confirm('¿Está seguro que desea eliminar esta marca?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>