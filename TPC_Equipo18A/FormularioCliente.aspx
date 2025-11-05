<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioCliente.aspx.cs" Inherits="TPC_Equipo18A.FormularioCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Agregar Cliente"></asp:Label></h3>
    <hr />

        <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <%-- Completar inputs --%>
                    </div>
                    <asp:HyperLink NavigateUrl="~/GestionClientes.aspx" runat="server"
                        Text="Cancelar" CssClass="btn btn-secondary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
