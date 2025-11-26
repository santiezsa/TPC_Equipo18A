<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioProveedor.aspx.cs" Inherits="TPC_Equipo18A.FormularioProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hfId" runat="server" />

    <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Nuevo Proveedor"></asp:Label>
    </h3>
    <hr />

    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-body">

                    <%-- Razon social y CUIT --%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Razón Social</label>
                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La razón social es obligatoria." ControlToValidate="txtRazonSocial" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="form-group col-md-6">
                            <label>CUIT</label>
                            <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El CUIT es obligatorio." ControlToValidate="txtCUIT" CssClass="text-danger small" Display="Dynamic" runat="server" />
                            <asp:RegularExpressionValidator
                                ErrorMessage="Debe tener 11 números sin guiones."
                                ControlToValidate="txtCUIT"
                                ValidationExpression="^\d{11}$"
                                CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <%-- Telefono --%>
                    <div class="form-row">
                        <div class="form-group col-md-12">
                            <label>Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>

                    <%-- Email --%>
                    <div class="form-group">
                        <label>Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100"></asp:TextBox>
                    </div>

                    <hr />

                    <%-- Buttons --%>
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                        <asp:HyperLink NavigateUrl="~/GestionProveedores.aspx" runat="server" Text="Cancelar" CssClass="btn btn-secondary ms-2" />
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
