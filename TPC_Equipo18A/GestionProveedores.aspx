<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="GestionProveedores.aspx.cs" Inherits="TPC_Equipo18A.GestionProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h3>Proveedores</h3>
        <asp:Button ID="btnNuevoProveedor" runat="server" Text="Nuevo Proveedor" CssClass="btn btn-primary" />
    </div>
    <p>Administre los proveedores de sus productos.</p>
    
    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvProveedores" runat="server" 
                    CssClass="table table-hover align-middle" 
                    AutoGenerateColumns="false" 
                    EmptyDataText="No hay proveedores registrados.">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre"/>
                        <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial"/>
                        <asp:BoundField HeaderText="CUIT" DataField="CUIT"/>
                        <asp:BoundField HeaderText="Email" DataField="Email"/>
                        <asp:BoundField HeaderText="Teléfono" DataField="Telefono"/>
                        <asp:BoundField HeaderText="Dirección" DataField="DireccionCompleta"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
