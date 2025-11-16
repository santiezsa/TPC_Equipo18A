<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_Equipo18A.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Mi negocio</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
    <style>
        body {
            background-image: url('img/fondo-login.jpg');
            background-size: cover;
            background-position: center;
            background-attachment: fixed;
            background-color: #1a1a2e;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            position: relative;
            overflow-x: hidden;
        }


            body::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background: rgba(0, 0, 0, 0.3);
                z-index: 0;
            }

        .login-container {
            position: relative;
            z-index: 1;
        }

        .login-card {
            max-width: 400px;
            width: 100%;
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
            background-color: rgba(255, 255, 255, 0.95);
            animation: fadeInUp 0.7s ease-out;
        }

        .card-header {
            background-color: #007bff;
            color: white;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
            text-align: center;
            padding: 20px;
        }

        .card-body h3 {
            color: #343a40;
            font-weight: 600;
        }

        .form-group label {
            font-weight: 500;
            color: #555;
        }

        .form-control {
            border-radius: 5px;
        }

        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            border-radius: 5px;
            transition: background-color 0.3s ease, border-color 0.3s ease, transform 0.2s ease;
        }

            .btn-primary:hover {
                background-color: #0056b3;
                border-color: #0056b3;
                transform: translateY(-2px);
            }

        .alert-danger {
            border-radius: 5px;
            font-size: 0.9rem;
        }

        .login-footer {
            position: absolute;
            bottom: 20px;
            width: 100%;
            text-align: center;
            color: rgba(255, 255, 255, 0.6);
            font-size: 0.9rem;
            z-index: 1;
        }

            .login-footer p {
                margin: 0;
            }

        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="card login-card">
                <div class="card-header">
                    <h3 class="mb-0">
                        <i class="bi bi-box-seam"></i>Mi Negocio
                    </h3>
                    <p class="mb-0 small">Sistema de Gestión</p>
                </div>
                <div class="card-body p-4">

                    <div class="form-group">
                        <label for="txtUsername"><i class="bi bi-person-fill"></i>Usuario</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="sonny123"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Usuario requerido" ControlToValidate="txtUsername" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <div class="form-group">
                        <label for="txtPassword"><i class="bi bi-key-fill"></i>Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="*******"></asp:TextBox>
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
        </div>

        <div class="login-footer">
            <p>Developed by Grupo 18A | UTN FRGP</p>
        </div>
    </form>
</body>
</html>
