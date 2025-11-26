<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioCliente.aspx.cs" Inherits="TPC_Equipo18A.FormularioCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfId" runat="server" />
    <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Agregar Cliente"></asp:Label>

    </h3>
    <hr />

    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-body">
                    <%-- Nombre, apellido y documento --%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombre" CssClass="text-danger small" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6">
                            <label>Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio." ControlToValidate="txtApellido" CssClass="text-danger small" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6">
                            <label>Documento</label>
                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El documento es obligatorio." ControlToValidate="txtDocumento" CssClass="text-danger small" Display="Dynamic" runat="server" MaxLength="8"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ErrorMessage="DNI inválido (solo números, 7 u 8 dígitos)."
                                ControlToValidate="txtDocumento"
                                ValidationExpression="^\d{7,8}$"
                                CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <%-- Email y Telefono --%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="txtEmail" CssClass="text-danger small" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6">
                            <label>Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El telefono es obligatorio." ControlToValidate="txtTelefono" CssClass="text-danger small" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ErrorMessage="Solo se permiten números (sin guiones)."
                                ControlToValidate="txtTelefono"
                                ValidationExpression="^\d+$"
                                CssClass="text-danger small"
                                Display="Dynamic"
                                runat="server" />
                        </div>
                    </div>

                    <%-- Direccion --%>
                    <div class="form-row">
                        <div class="form-group col-md-12">
                            <label>Dirección</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La dirección es obligatoria." ControlToValidate="txtDireccion" CssClass="text-danger small" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <%-- Buttons --%>
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                        <asp:HyperLink NavigateUrl="~/GestionClientes.aspx" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



