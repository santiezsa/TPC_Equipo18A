<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/RecuperarPass.aspx.cs" Inherits="TPC_Equipo18A.RecuperarPass" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Recuperar contraseña</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
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
        }

        body::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: rgba(0, 0, 0, 0.4);
            z-index: 0;
        }

        .card-container {
            position: relative;
            z-index: 1;
            width: 100%;
            max-width: 450px;
        }

        .card {
            border: none;
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.3);
            background-color: rgba(255, 255, 255, 0.95);
        }

        .card-header {
            background-color: transparent;
            border-bottom: 1px solid rgba(0,0,0,0.1);
            padding: 20px;
            text-align: center;
        }

        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card-container p-3">
            <div class="card">
                <div class="card-header">
                    <h3 class="mb-0"><i class="bi bi-shield-lock-fill"></i> Recuperar acceso</h3>
                    <p class="text-muted mb-0 small">Te enviaremos una nueva contraseña a tu mail.</p>
                </div>
                
                <div class="card-body p-4">
                    
                    <div class="form-group">
                        <label>Email registrado</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="bi bi-envelope"></i></span>
                            </div>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="ejemplo@correo.com" TextMode="Email"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ErrorMessage="Ingresá tu email para continuar." ControlToValidate="txtEmail" CssClass="text-danger small mt-1" Display="Dynamic" runat="server" />
                    </div>

                    <asp:Button ID="btnRecuperar" runat="server" Text="Restablecer contraseña" CssClass="btn btn-primary btn-block font-weight-bold" OnClick="btnRecuperar_Click" />

                    <div class="text-center mt-4">
                        <a href="Login.aspx" class="text-secondary"><i class="bi bi-arrow-left"></i> Volver al Login</a>
                    </div>

                    <asp:Label ID="lblMensaje" runat="server" CssClass="d-block text-center mt-3"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>