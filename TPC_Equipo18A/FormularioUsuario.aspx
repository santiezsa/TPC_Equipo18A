<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioUsuario.aspx.cs" Inherits="TPC_Equipo18A.FormularioUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfId" runat="server" />
    <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Nuevo Usuario"></asp:Label>
    </h3>
    <hr />

    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-body">

                    <%-- Username --%>
                    <div class="mb-3">
                        <label class="form-label">Nombre de Usuario</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtUsername" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <%-- Password --%>
                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtPassword" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <%-- NUEVO: Email --%>
                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@ejemplo.com"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Requerido para recuperación" ControlToValidate="txtEmail" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        <asp:RegularExpressionValidator ErrorMessage="Formato inválido" ControlToValidate="txtEmail" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <%-- Perfil --%>
                    <div class="mb-3">
                        <label class="form-label">Perfil</label>
                        <%-- value=1 admin y value=2 es vendedor  --%>
                        <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Seleccione un perfil:" Value="" Selected="True" Disabled="True" />
                            <asp:ListItem Text="Administrador" Value="1" />
                            <asp:ListItem Text="Vendedor" Value="2" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Seleccione un perfil" ControlToValidate="ddlPerfil" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <hr />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:HyperLink NavigateUrl="~/GestionUsuarios.aspx" runat="server" Text="Cancelar" CssClass="btn btn-secondary ms-2" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
