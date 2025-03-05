<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="DashboardUsuario.aspx.cs" Inherits="SeñaWeb.Vista.Usuario.DashboardUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos para las tarjetas de progreso */
        .progress-card {
            display: flex;
            flex-direction: column;
            padding: 1.5rem;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            background-color: #fff;
        }
        
        .progress-card h5 {
            margin-bottom: 1rem;
            display: flex;
            align-items: center;
            gap: 0.5rem;
        }
        
        .progress-card .bi {
            font-size: 1.2rem;
        }
        
        .module-card h5 .bi {
            color: #003A59;
        }
        
        .signs-card h5 .bi {
            color: #D42374;
        }
        
        .eval-card h5 .bi {
            color: #28a745;
        }
        
        .progress-stats {
            margin-top: auto;
        }
        
        .stat-box {
            height: 100%;
            display: flex;
            flex-direction: column;
            align-items: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Control HiddenField para almacenar el progreso general -->
        <asp:HiddenField ID="hdnProgresoGeneral" runat="server" Value="0" />
        
        <!-- Sección de bienvenida -->
        <div class="welcome-section mb-5 text-center">
            <h1 class="welcome-title">¡Bienvenido,
                <asp:Label ID="lblUserName" runat="server" Text="Estudiante" />!</h1>
            <p class="welcome-subtitle">Plataforma de aprendizaje de lenguaje de señas</p>
            <div class="welcome-divider mx-auto"></div>
        </div>

        <!-- Tarjetas de progreso -->
        <div class="row row-cols-1 row-cols-md-3 g-4 mb-5">
            <!-- Módulos -->
            <div class="col">
                <div class="progress-card module-card h-100">
                    <h5><i class="bi bi-collection"></i>Módulos Completados</h5>
                    <div class="d-flex align-items-center justify-content-center mb-3">
                        <div class="position-relative" style="width: 120px; height: 120px;">
                            <asp:Label ID="lblModulosProgress" runat="server" CssClass="position-absolute top-50 start-50 translate-middle fs-4 fw-bold" Text="0%"></asp:Label>
                            <canvas id="modulosChart" width="120" height="120"></canvas>
                        </div>
                    </div>
                    <p class="text-center">
                        <asp:Label ID="lblModulosDetail" runat="server" Text="0 de 0 módulos completados"></asp:Label>
                    </p>
                    <a href="Modulos.aspx" class="btn btn-sm btn-primary w-100 mt-2">Ver módulos</a>
                </div>
            </div>

            <!-- Señas -->
            <div class="col">
                <div class="progress-card signs-card h-100">
                    <h5><i class="bi bi-camera-video"></i>Señas Aprendidas</h5>
                    <div class="d-flex align-items-center justify-content-center mb-3">
                        <div class="position-relative" style="width: 120px; height: 120px;">
                            <asp:Label ID="lblSenasProgress" runat="server" CssClass="position-absolute top-50 start-50 translate-middle fs-4 fw-bold" Text="0%"></asp:Label>
                            <canvas id="senasChart" width="120" height="120"></canvas>
                        </div>
                    </div>
                    <p class="text-center">
                        <asp:Label ID="lblSenasDetail" runat="server" Text="0 de 0 señas aprendidas"></asp:Label>
                    </p>
                    <a href="Señas.aspx" class="btn btn-sm btn-primary w-100 mt-2">Ver biblioteca de señas</a>
                </div>
            </div>

            <!-- Evaluación -->
            <div class="col">
                <div class="progress-card eval-card h-100">
                    <h5><i class="bi bi-award"></i>Progreso General</h5>
                    <div class="d-flex align-items-center justify-content-center mb-3">
                        <div class="position-relative" style="width: 120px; height: 120px;">
                            <asp:Label ID="lblPuntajeGeneral" runat="server" CssClass="position-absolute top-50 start-50 translate-middle fs-4 fw-bold" Text="0%"></asp:Label>
                            <canvas id="progresoGeneralChart" width="120" height="120"></canvas>
                        </div>
                    </div>
                    
                    <!-- Detalles de progreso adicionales -->
                    <div class="progress-stats text-center mb-3">
                        <div class="row g-2">
                            <div class="col-6">
                                <div class="stat-box border rounded p-2">
                                    <i class="bi bi-calendar-check text-success"></i>
                                    <div class="stat-title small">Última sesión</div>
                                    <div class="stat-value">
                                        <asp:Label ID="lblUltimaSesion" runat="server" Text="Hoy"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="stat-box border rounded p-2">
                                    <i class="bi bi-lightning-charge text-warning"></i>
                                    <div class="stat-title small">Racha actual</div>
                                    <div class="stat-value">
                                        <asp:Label ID="lblRachaActual" runat="server" Text="0 días"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Últimos módulos estudiados -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-header py-3 d-flex justify-content-between align-items-center">
                        <h6 class="m-0 font-weight-bold">Últimos Módulos Estudiados</h6>
                        <a href="Modulos.aspx" class="btn btn-sm btn-outline-primary">Ver todos</a>
                    </div>
                    <div class="card-body">
                        <asp:ListView ID="lvModulosRecientes" runat="server">
                            <EmptyDataTemplate>
                                <div class="text-center py-4">
                                    <i class="bi bi-info-circle fs-1 text-muted"></i>
                                    <p class="mt-2">No has estudiado ningún módulo aún. ¡Comienza ahora!</p>
                                    <a href="Modulos.aspx" class="btn btn-primary">Explorar módulos</a>
                                </div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div class="d-flex align-items-center p-3 border-bottom">
                                    <div class="flex-shrink-0">
                                        <div class="bg-primary text-white rounded-circle d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
                                            <i class="bi bi-collection-fill"></i>
                                        </div>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="mb-1"><%# Eval("nombreModulo") %></h6>
                                        <div class="progress" style="height: 6px;">
                                            <div class="progress-bar" role="progressbar" style="width: <%# Eval("porcentajeCompletado") %>%;" aria-valuenow="<%# Eval("porcentajeCompletado") %>" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                        <small class="text-muted"><%# Eval("porcentajeCompletado") %>% completado</small>
                                    </div>
                                    <div class="ms-auto">
                                        <a href='<%# "ModuloDetalle.aspx?id=" + Eval("idModulo") %>' class="btn btn-sm btn-outline-primary">Continuar</a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>

        <!-- Señas recientemente practicadas -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow">
                    <div class="card-header py-3 d-flex justify-content-between align-items-center">
                        <h6 class="m-0 font-weight-bold">Señas Recientemente Practicadas</h6>
                        <a href="Señas.aspx" class="btn btn-sm btn-outline-primary">Ver todas</a>
                    </div>
                    <div class="card-body">
                        <asp:ListView ID="lvSenasRecientes" runat="server">
                            <EmptyDataTemplate>
                                <div class="text-center py-4">
                                    <i class="bi bi-camera-video fs-1 text-muted"></i>
                                    <p class="mt-2">No has practicado ninguna seña aún. ¡Empieza a aprender!</p>
                                    <a href="Señas.aspx" class="btn btn-primary">Explorar señas</a>
                                </div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div class="d-flex align-items-center p-3 border-bottom">
                                    <div class="flex-shrink-0">
                                        <div class="bg-info text-white rounded-circle d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
                                            <i class="bi bi-camera-video-fill"></i>
                                        </div>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="mb-1"><%# Eval("nombreSeña") %></h6>
                                        <small class="text-muted"><%# Eval("tipoSeña") %> | <%# Eval("fechaVisto", "{0:dd/MM/yyyy}") %></small>
                                    </div>
                                    <div class="ms-auto">
                                        <span class="badge rounded-pill <%# ConvertToBoolean(Eval("estado")) ? "bg-success" : "bg-warning text-dark" %>">
                                            <%# ConvertToBoolean(Eval("estado")) ? "Visto" : "Pendiente" %>
                                        </span>
                                        <a href='<%# "SeñaDetalle.aspx?id=" + Eval("idSeña") %>' class="btn btn-sm btn-outline-primary ms-2">Ver</a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Scripts para las gráficas -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Configuración de colores
            const moduleColor = '#003A59';
            const senasColor = '#D42374';
            const progresoColor = '#28a745';

            // Gráfica de Módulos
            const modulosCtx = document.getElementById('modulosChart');
            const modulosProgress = parseInt('<%= hdnProgresoGeneral.Value %>');

            new Chart(modulosCtx, {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [modulosProgress, 100 - modulosProgress],
                        backgroundColor: [moduleColor, '#E9ECEF'],
                        borderWidth: 0,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            display: false
                        },
                        tooltip: {
                            enabled: false
                        }
                    }
                }
            });

            // Gráfica de Señas
            const senasCtx = document.getElementById('senasChart');
            const senasProgress = parseInt('<%= hdnProgresoGeneral.Value %>');

            new Chart(senasCtx, {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [senasProgress, 100 - senasProgress],
                        backgroundColor: [senasColor, '#E9ECEF'],
                        borderWidth: 0,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            display: false
                        },
                        tooltip: {
                            enabled: false
                        }
                    }
                }
            });

            // Gráfica de Progreso General
            const progresoCtx = document.getElementById('progresoGeneralChart');
            const progresoGeneral = parseInt('<%= hdnProgresoGeneral.Value %>');

            new Chart(progresoCtx, {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [progresoGeneral, 100 - progresoGeneral],
                        backgroundColor: [progresoColor, '#E9ECEF'],
                        borderWidth: 0,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            display: false
                        },
                        tooltip: {
                            enabled: false
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>