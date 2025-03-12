<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="SeñaWeb.Vista.Registrarse" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Iminwe - Registro</title>
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
        
        .registro-container {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 2rem 0;
        }
        
        .card {
            border: none;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 12px 24px rgba(0, 0, 0, 0.15);
            max-width: 1000px;
        }
        
        .registro-form-side {
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
        
        .btn-registro {
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
        
        .btn-registro:hover {
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
        
        .login-link {
            margin-top: 2rem;
            text-align: center;
            font-size: 0.9rem;
        }
        
        .login-link a {
            color: var(--magenta);
            font-weight: 600;
            text-decoration: none;
            transition: color 0.3s ease;
        }
        
        .login-link a:hover {
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
            .registro-form-side, .welcome-side {
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
    <div class="registro-container">
        <div class="card">
            <div class="row g-0">
                <!-- Lado del formulario -->
                <div class="col-lg-6 registro-form-side">
                    <div class="logo-container">
                        <img src="../recursos/assets/img/LogoSeñas.jpg" alt="Logo Iminwe" class="logo" />
                        <div class="logo-text">Iminwe</div>
                        <p class="text-muted mb-4">Plataforma de Lenguaje de Señas</p>
                    </div>
                    
                    <form runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        
                        <h4 class="mb-4">Crear una cuenta</h4>
                        
                        <div class="mb-3 input-with-icon">
                            <i class="bi bi-person input-icon"></i>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" required="true"></asp:TextBox>
                        </div>
                        
                        <div class="mb-3 input-with-icon">
                            <i class="bi bi-person input-icon"></i>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido" required="true"></asp:TextBox>
                        </div>
                        
                        <div class="mb-3 input-with-icon">
                            <i class="bi bi-envelope input-icon"></i>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Correo electrónico" required="true" TextMode="Email"></asp:TextBox>
                        </div>
                        
                        <div class="mb-3 input-with-icon">
                            <i class="bi bi-lock input-icon"></i>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña" required="true"></asp:TextBox>
                        </div>
                        
                        <div class="mb-4 input-with-icon">
                            <i class="bi bi-lock-fill input-icon"></i>
                            <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirmar contraseña" required="true"></asp:TextBox>
                        </div>
                        
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn btn-registro" OnClick="btnRegistrar_Click" />
                        
                        <div class="login-link">
                            ¿Ya tienes cuenta? <a href="Login.aspx">Iniciar sesión</a>
                        </div>
                    </form>
                </div>
                
                <!-- Lado de bienvenida -->
                <div class="col-lg-6 welcome-side">
                    <h2 class="welcome-title">Únete a Iminwe</h2>
                    <p class="welcome-text">
                        Bienvenido a nuestra comunidad dedicada al aprendizaje del lenguaje de señas. 
                        Al registrarte, tendrás acceso a:
                    </p>
                    <ul class="welcome-text mt-3">
                        <li>Módulos interactivos de aprendizaje</li>
                        <li>Videos de demostración de señas</li>
                        <li>Seguimiento de tu progreso</li>
                        <li>Comunidad de aprendizaje inclusiva</li>
                    </ul>
                    <p class="welcome-text mt-3">
                        Tu viaje hacia la comunicación inclusiva comienza aquí.
                    </p>
                    <div class="mt-4">
                        <i class="bi bi-hand-index-thumb fs-1 me-2"></i>
                        <i class="bi bi-hand-thumbs-up fs-1"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>