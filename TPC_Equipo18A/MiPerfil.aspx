<%@ Page Title="Mi perfil" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="TPC_Equipo18A.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card shadow-sm">
                <div class="card-header bg-secondary text-white">
                    <h5 class="mb-0"><i class="bi bi-person-gear"></i>Mi Perfil</h5>
                </div>
                <div class="card-body">

                    <%-- Datos solo lectura --%>
                    <div class="form-group row">
                        <label class="col-sm-4 col-form-label font-weight-bold">Usuario:</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtUser" runat="server" CssClass="form-control-plaintext font-weight-bold" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-4 col-form-label font-weight-bold">Perfil:</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtPerfil" runat="server" CssClass="form-control-plaintext" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <hr />

                    <%-- Datos editables --%>
                    <div class="form-group">
                        <label>Email (para recupero)</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtEmail" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <div class="form-group mt-4">
                        <label>Contraseña Actual <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtPassActual" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingresá tu clave actual para guardar cambios"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Debes ingresar tu clave actual" ControlToValidate="txtPassActual" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <div class="form-group">
                        <label>Nueva Contraseña (Opcional)</label>
                        <asp:TextBox ID="txtPassNueva" runat="server" CssClass="form-control" TextMode="Password" placeholder="Dejar vacío para mantener la actual"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Repetir Nueva Contraseña</label>
                        <asp:TextBox ID="txtPassRepetir" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden" ControlToValidate="txtPassRepetir" ControlToCompare="txtPassNueva" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <hr />

                    <div class="text-right">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                        <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" Text="Cancelar" CssClass="btn btn-link text-secondary" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
