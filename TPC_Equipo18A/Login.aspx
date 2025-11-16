<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_Equipo18A.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Mi negocio</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
    <style>
        body {
            background-color: #f8f9fa;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
        }

        .login-card {
            max-width: 400px;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card login-card shadow-sm">
            <div class="card-body p-4">
                <h3 class="text-center mb-4">
                    <i class="bi bi-shop"></i>Mi Negocio
                </h3>

                <div class="form-group">
                    <label>Usuario</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="sonny123"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Usuario requerido" ControlToValidate="txtUsername" CssClass="text-danger small" Display="Dynamic" runat="server" />
                </div>

                <div class="form-group">
                    <label>Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="******"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Contraseña requerida" ControlToValidate="txtPassword" CssClass="text-danger small" Display="Dynamic" runat="server" />
                </div>

                <div class="form-group mt-4">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-block" OnClick="btnIngresar_Click" />
                </div>

                <%-- Panel para mostrar errores de login --%>
                <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger mt-3" Visible="false">
                    <asp:Literal ID="lblError" runat="server" />
                </asp:Panel>

            </div>
        </div>
    </form>
</body>
</html>
