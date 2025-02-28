<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DashboardAdmin.aspx.cs" Inherits="SeñaWeb.Vista.Admin.DashboardAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Sección de bienvenida -->
        <div class="welcome-section mb-5 text-center">
            <h1 class="welcome-title">¡Bienvenido, <asp:Label ID="lblAdminName" runat="server" Text="Administrador" />!</h1>
            <p class="welcome-subtitle">Panel de control de la aplicación de Señas</p>
            <div class="welcome-divider mx-auto"></div>
        </div>
        
        <!-- Tarjetas de estadísticas -->
        <div class="row row-cols-1 row-cols-md-3 g-4 dashboard-row">
            <!-- Módulos -->
            <div class="col">
                <div class="dashboard-card modules-card">
                    <h5><i class="bi bi-collection"></i> Módulos Registrados</h5>
                    <div class="count-number">
                        <asp:Label ID="lblModulosCount" runat="server" Text="0" />
                    </div>
                    <p class="card-description">Total de módulos en el sistema</p>
                    <i class="bi bi-collection bg-icon"></i>
                </div>
            </div>
            
            <!-- Tipos de Señas -->
            <div class="col">
                <div class="dashboard-card types-card">
                    <h5><i class="bi bi-tags"></i> Tipos de Señas</h5>
                    <div class="count-number">
                        <asp:Label ID="lblTiposSenaCount" runat="server" Text="0" />
                    </div>
                    <p class="card-description">Categorías de señas disponibles</p>
                    <i class="bi bi-tags bg-icon"></i>
                </div>
            </div>
            
            <!-- Señas -->
            <div class="col">
                <div class="dashboard-card signs-card">
                    <h5><i class="bi bi-camera-video"></i> Señas Registradas</h5>
                    <div class="count-number">
                        <asp:Label ID="lblSenasCount" runat="server" Text="0" />
                    </div>
                    <p class="card-description">Total de señas en la biblioteca</p>
                    <i class="bi bi-camera-video bg-icon"></i>
                </div>
            </div>
        </div>
    </div>
</asp:Content>