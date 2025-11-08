<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionClientes.aspx.cs" Inherits="TPC_Equipo18A.GestionClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h3>Clientes</h3>
        <asp:Button ID="btnNuevoCliente" runat="server" Text="Nuevo Cliente" CssClass="btn btn-primary" />
    </div>
    <p>Administre sus clientes registrados en el sistema.</p>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvClientes" runat="server"
                    CssClass="table table-striped table-hover" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre"/>
                        <asp:BoundField HeaderText="Apellido" DataField="Apellido"/>
                        <asp:BoundField HeaderText="Email" DataField="Email"/>
                        <asp:BoundField HeaderText="Teléfono" DataField="Telefono"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
