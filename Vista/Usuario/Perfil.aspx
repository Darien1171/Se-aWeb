<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="SeñaWeb.Vista.Usuario.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .profile-card {
            transition: all 0.3s ease;
            border-radius: 15px;
            overflow: hidden;
        }
        
        .profile-header {
            background: linear-gradient(135deg, var(--blue), var(--magenta));
            color: white;
            padding: 30px;
            position: relative;
        }
        
        .profile-avatar {
            width: 120px;
            height: 120px;
            background-color: white;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 20px;
            font-size: 3rem;
            color: var(--magenta);
            box-shadow: 0 10px 20px rgba(0,0,0,0.1);
        }
        
        .profile-name {
            font-size: 1.8rem;
            font-weight: 700;
            margin-bottom: 5px;
            text-align: center;
        }
        
        .profile-role {
            font-size: 1rem;
            opacity: 0.9;
            text-align: center;
        }
        
        .profile-body {
            padding: 30px;
        }
        
        .stats-card {
            transition: all 0.3s ease;
            border-radius: 15px;
            overflow: hidden;
            height: 100%;
        }
        
        .stats-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 15px 30px rgba(0,0,0,0.1);
        }
        
        .stats-header {
            padding: 15px 20px;
            background: #f8f9fa;
            border-bottom: 1px solid #e9ecef;
        }
        
        .stats-body {
            padding: 20px;
        }
        
        .stats-item {
            margin-bottom: 15px;
            padding-bottom: 15px;
            border-bottom: 1px solid #e9ecef;
        }
        
        .stats-item:last-child {
            margin-bottom: 0;
            padding-bottom: 0;
            border-bottom: none;
        }
        
        .stats-label {
            font-size: 0.9rem;
            color: #6c757d;
        }
        
        .stats-value {
            font-size: 1.5rem;
            font-weight: 700;
            color: var(--blue);
        }
        
        .form-section {
            margin-bottom: 30px;
        }
        
        .form-section h5 {
            margin-bottom: 20px;
            font-weight: 600;
        }
        
        .change-avatar-btn {
            position: absolute;
            bottom: -15px;
            left: 50%;
            transform: translateX(-50%);
            background: white;
            color: var(--blue);
            border-radius: 50%;
            width: 40px;
            height: 40px;
            display: flex;
            align-items: center;
            justify-content: center;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
            border: none;
            transition: all 0.3s ease;
        }
        
        .change-avatar-btn:hover {
            background: var(--blue);
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Título de la página -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Mi Perfil</h1>
        </div>
        
        <!-- Contenido principal -->
        <div class="row">
            <!-- Columna izquierda - Información del perfil -->
            <div class="col-lg-4 mb-4">
                <!-- Tarjeta de perfil -->
                <div class="card shadow profile-card mb-4">
                    <div class="profile-header">
                        <div class="profile-avatar">
                            <i class="bi bi-person-fill"></i>
                        </div>
                        <h2 class="profile-name">
                            <asp:Label ID="lblNombreCompleto" runat="server" Text="Nombre del Usuario"></asp:Label>
                        </h2>
                        <p class="profile-role">
                            <asp:Label ID="lblRolUsuario" runat="server" Text="Estudiante"></asp:Label>
                        </p>
                    </div>
                    <div class="profile-body">
                        <!-- Datos de contacto -->
                        <div class="mb-4">
                            <h5 class="mb-3">Información de Contacto</h5>
                            <div class="mb-3">
                                <label class="d-block text-muted mb-1"><i class="bi bi-envelope me-2"></i>Email</label>
                                <div class="fw-bold">
                                    <asp:Label ID="lblEmail" runat="server" Text="email@ejemplo.com"></asp:Label>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Información de la cuenta -->
                        <div>
                            <h5 class="mb-3">Información de la Cuenta</h5>
                            <div class="mb-3">
                                <label class="d-block text-muted mb-1"><i class="bi bi-person-badge me-2"></i>ID de Usuario</label>
                                <div class="fw-bold">
                                    <asp:Label ID="lblIdUsuario" runat="server" Text="1"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <!-- Estadísticas del usuario -->
                <div class="card shadow stats-card">
                    <div class="stats-header">
                        <h5 class="m-0 font-weight-bold">Mis Estadísticas</h5>
                    </div>
                    <div class="stats-body">
                        <div class="stats-item">
                            <div class="stats-label">Señas Aprendidas</div>
                            <div class="stats-value">
                                <asp:Label ID="lblSenasAprendidas" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                        <div class="stats-item">
                            <div class="stats-label">Módulos Completados</div>
                            <div class="stats-value">
                                <asp:Label ID="lblModulosCompletados" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                        <div class="stats-item">
                            <div class="stats-label">Progreso General</div>
                            <div class="stats-value">
                                <asp:Label ID="lblProgresoGeneral" runat="server" Text="0%"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Columna derecha - Formularios -->
            <div class="col-lg-8">
                <!-- Alertas -->
                <div id="alertSuccess" runat="server" class="alert alert-success alert-dismissible fade show mb-4" role="alert" visible="false">
                    <i class="bi bi-check-circle-fill me-2"></i>
                    <span id="successMessage" runat="server"></span>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
                <div id="alertError" runat="server" class="alert alert-danger alert-dismissible fade show mb-4" role="alert" visible="false">
                    <i class="bi bi-exclamation-triangle-fill me-2"></i>
                    <span id="errorMessage" runat="server"></span>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
                
                <!-- Formulario de edición de perfil -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Editar Información Personal</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-section">
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label for="txtNombre" class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese su nombre"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                        ControlToValidate="txtNombre" 
                                        ErrorMessage="El nombre es obligatorio" 
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        ValidationGroup="EditProfile">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtApellido" class="form-label">Apellido</label>
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingrese su apellido"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvApellido" runat="server" 
                                        ControlToValidate="txtApellido" 
                                        ErrorMessage="El apellido es obligatorio" 
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        ValidationGroup="EditProfile">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label for="txtEmail" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingrese su email" TextMode="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                                    ControlToValidate="txtEmail" 
                                    ErrorMessage="El email es obligatorio" 
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationGroup="EditProfile">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                                    ControlToValidate="txtEmail" 
                                    ErrorMessage="Email no válido" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationGroup="EditProfile">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                                <asp:Button ID="btnGuardarInfo" runat="server" CssClass="btn btn-primary" 
                                           Text="Guardar Cambios" OnClick="btnGuardarInfo_Click" ValidationGroup="EditProfile" />
                            </div>
                        </div>
                    </div>
                </div>
                
                <!-- Formulario de cambio de contraseña -->
                <div class="card shadow">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Cambiar Contraseña</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-section">
                            <div class="mb-3">
                                <label for="txtContrasenaActual" class="form-label">Contraseña Actual</label>
                                <asp:TextBox ID="txtContrasenaActual" runat="server" CssClass="form-control" 
                                             placeholder="Ingrese su contraseña actual" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvContrasenaActual" runat="server" 
                                    ControlToValidate="txtContrasenaActual" 
                                    ErrorMessage="La contraseña actual es obligatoria" 
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationGroup="ChangePassword">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtNuevaContrasena" class="form-label">Nueva Contraseña</label>
                                <asp:TextBox ID="txtNuevaContrasena" runat="server" CssClass="form-control" 
                                             placeholder="Ingrese su nueva contraseña" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNuevaContrasena" runat="server" 
                                    ControlToValidate="txtNuevaContrasena" 
                                    ErrorMessage="La nueva contraseña es obligatoria" 
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationGroup="ChangePassword">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revNuevaContrasena" runat="server" 
                                    ControlToValidate="txtNuevaContrasena" 
                                    ErrorMessage="La contraseña debe tener al menos 6 caracteres" 
                                    ValidationExpression=".{6,}" 
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationGroup="ChangePassword">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtConfirmarContrasena" class="form-label">Confirmar Nueva Contraseña</label>
                                <asp:TextBox ID="txtConfirmarContrasena" runat="server" CssClass="form-control" 
                                             placeholder="Confirme su nueva contraseña" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfirmarContrasena" runat="server" 
                                    ControlToValidate="txtConfirmarContrasena" 
                                    ErrorMessage="Debe confirmar la nueva contraseña" 
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationGroup="ChangePassword">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvContrasenas" runat="server" 
                                    ControlToValidate="txtConfirmarContrasena" 
                                    ControlToCompare="txtNuevaContrasena" 
                                    ErrorMessage="Las contraseñas no coinciden" 
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationGroup="ChangePassword">
                                </asp:CompareValidator>
                            </div>
                            <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                                <asp:Button ID="btnCambiarContrasena" runat="server" CssClass="btn btn-primary" 
                                           Text="Cambiar Contraseña" OnClick="btnCambiarContrasena_Click" ValidationGroup="ChangePassword" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>