<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="TPC_Equipo18A.FormularioProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Agregar Producto"></asp:Label></h3>
    <hr />

        <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <%-- Completar inputs --%>
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="La descripción es obligatoria" ControlToValidate="txtDescripcion" CssClass="text-danger" Display="Dynamic" runat="server" />
                    </div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />

                    <asp:HyperLink NavigateUrl="~/GestionProducto.aspx" runat="server"
                        Text="Cancelar" CssClass="btn btn-secondary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
