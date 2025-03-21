﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SeñaWeb.Vista.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Iminwe - Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
    <style>
        :root {
            --blue: #003A59;
            --magenta: #D42374;
            --orange: #FF5800;
        }
        
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        .login-container {
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        
        .card {
            border: none;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 12px 24px rgba(0, 0, 0, 0.15);
            max-width: 1000px;
        }
        
        .login-form-side {
            padding: 3rem;
        }
        
        .welcome-side {
            background: linear-gradient(135deg, var(--blue), var(--magenta));
            color: white;
            padding: 3rem;
            display: flex;
            flex-direction: column;
            justify-content: center;
        }
        
        .logo-container {
            text-align: center;
            margin-bottom: 2rem;
        }
        
        .logo {
            max-height: 100px;
            margin-bottom: 1rem;
        }
        
        .logo-text {
            font-size: 2.2rem;
            font-weight: 700;
            letter-spacing: 1px;
            color: var(--blue);
        }
        
        .form-control {
            padding: 0.75rem 1.25rem;
            border-radius: 8px;
            border: 1px solid #e0e0e0;
            margin-bottom: 1.5rem;
            background-color: #f8f9fa;
        }
        
        .form-control:focus {
            border-color: var(--blue);
            box-shadow: 0 0 0 0.2rem rgba(0, 58, 89, 0.25);
        }
        
        .form-label {
            font-weight: 500;
            margin-bottom: 0.5rem;
            color: #495057;
        }
        
        .btn-login {
            background: linear-gradient(90deg, var(--blue), var(--magenta));
            border: none;
            color: white;
            padding: 0.75rem;
            border-radius: 8px;
            font-weight: 600;
            letter-spacing: 0.5px;
            width: 100%;
            margin-top: 1rem;
            transition: all 0.3s ease;
        }
        
        .btn-login:hover {
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
            background: linear-gradient(90deg, var(--magenta), var(--blue));
        }
        
        .welcome-title {
            font-size: 2.5rem;
            font-weight: 700;
            margin-bottom: 1.5rem;
            text-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        
        .welcome-text {
            font-size: 1.1rem;
            font-weight: 400;
            line-height: 1.6;
            opacity: 0.9;
        }
        
        .register-link {
            margin-top: 2rem;
            text-align: center;
            font-size: 0.9rem;
        }
        
        .register-link a {
            color: var(--magenta);
            font-weight: 600;
            text-decoration: none;
            transition: color 0.3s ease;
        }
        
        .register-link a:hover {
            color: var(--blue);
            text-decoration: underline;
        }
        
        .input-with-icon {
            position: relative;
        }
        
        .input-icon {
            position: absolute;
            top: 50%;
            left: 15px;
            transform: translateY(-50%);
            color: #6c757d;
        }
        
        .input-with-icon .form-control {
            padding-left: 45px;
        }
        
        @media (max-width: 768px) {
            .login-form-side, .welcome-side {
                padding: 2rem;
            }
            
            .welcome-title {
                font-size: 2rem;
            }
            
            .card {
                margin: 1rem;
            }
        }
    </style>
</head>
<body>
    <div class="login-container">
        <div class="card">
            <div class="row g-0">

                <div class="col-lg-6 login-form-side">
                    <div class="logo-container">
                        <img src="../recursos/assets/img/LogoSeñas.jpg" alt="Logo Iminwe" class="logo" />
                        <div class="logo-text">Iminwe</div>
                        <p class="text-muted mb-4">Plataforma de Lenguaje de Señas</p>
                    </div>
                    
                    <form runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        
                        <h4 class="mb-4">Iniciar Sesión</h4>
                        
                        <div class="mb-3 input-with-icon">
                            <i class="bi bi-envelope input-icon"></i>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Correo electrónico" required="true"></asp:TextBox>
                        </div>
                        
                        <div class="mb-4 input-with-icon">
                            <i class="bi bi-lock input-icon"></i>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña" required="true"></asp:TextBox>
                        </div>
                        
                        <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-login" OnClick="btnLogin_Click" />
                        
                        <div class="register-link">
                            ¿No tienes cuenta? <a href="Registrarse.aspx">¡Regístrate aquí!</a>
                        </div>
                    </form>
                </div>
                

                <div class="col-lg-6 welcome-side">
                    <h2 class="welcome-title">Bienvenido a Iminwe</h2>
                    <p class="welcome-text">
                        "El poder de tu voz no está en el sonido, sino en tus acciones, tus gestos y la fuerza de tu corazón. 
                        Cada movimiento que haces, cada sonrisa que compartes, puede inspirar al mundo. 
                        Nunca subestimes tu capacidad de cambiar vidas, porque en tu silencio hay un mensaje que grita esperanza, 
                        resiliencia y amor."
                    </p>
                    <div class="mt-4">
                        <i class="bi bi-hand-index-thumb fs-1"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>