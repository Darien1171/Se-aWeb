<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="SeñaDetalle.aspx.cs" Inherits="SeñaWeb.Vista.Usuario.SeñaDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .video-container {
            position: relative;
            padding-top: 56.25%; /* 16:9 Aspect Ratio */
            height: 0;
            overflow: hidden;
            border-radius: 10px;
            margin-bottom: 20px;
            background: #f8f9fa;
        }
        
        .video-container video {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            border-radius: 10px;
        }
        
        .sena-title {
            font-size: 1.8rem;
            font-weight: 700;
            color: var(--blue);
            margin-bottom: 0.5rem;
        }
        
        .sena-type {
            display: inline-block;
            padding: 0.25rem 0.75rem;
            border-radius: 20px;
            font-size: 0.9rem;
            margin-bottom: 1rem;
            background-color: #e9ecef;
        }
        
        .related-sena-card {
            transition: all 0.3s ease;
            height: 100%;
        }
        
        .related-sena-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0,0,0,0.1);
        }
        
        .back-btn {
            margin-bottom: 1rem;
        }
        
        .action-buttons {
            margin-top: 1.5rem;
        }
        
        .action-buttons .btn {
            margin-right: 0.5rem;
            margin-bottom: 0.5rem;
        }
        
        .module-link {
            margin-bottom: 1.5rem;
        }
        
        .progress-circle {
            width: 70px;
            height: 70px;
            border-radius: 50%;
            background: #f8f9fa;
            border: 6px solid #e9ecef;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
        }
        
        .progress-number {
            font-size: 1.2rem;
            font-weight: 700;
            color: var(--blue);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Botón de regreso -->
        <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-outline-secondary back-btn" OnClick="btnRegresar_Click">
            <i class="bi bi-arrow-left"></i> Volver
        </asp:LinkButton>
        
        <div class="row">
            <!-- Columna principal - Video y detalles -->
            <div class="col-lg-8">
                <div class="card shadow mb-4">
                    <div class="card-body">
                        <!-- Mensajes de alerta -->
                        <div id="alertSuccess" runat="server" class="alert alert-success alert-dismissible fade show mb-4" role="alert" visible="false">
                            <i class="bi bi-check-circle-fill me-2"></i>
                            <span id="successMessage" runat="server"></span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>
                        
                        <!-- Contenedor de video -->
                        <div class="video-container">
                            <video id="videoPlayer" runat="server" controls="controls">
                                Su navegador no soporta la etiqueta de video.
                            </video>
                        </div>
                        
                        <!-- Título y tipo -->
                        <h1 class="sena-title" id="lblNombreSena" runat="server">Nombre de la Seña</h1>
                        <span class="sena-type" id="lblTipoSena" runat="server">Tipo de Seña</span>
                        
                        <!-- Módulo al que pertenece -->
                        <div class="module-link">
                            <i class="bi bi-collection"></i> Módulo: 
                            <asp:HyperLink ID="lnkModulo" runat="server" NavigateUrl="#">Nombre del Módulo</asp:HyperLink>
                        </div>
                        
                        <!-- Descripción -->
                        <p class="mb-4" id="lblDescripcion" runat="server">
                            Descripción detallada de la seña, si existe...
                        </p>
                        
                        <!-- Botones de acción -->
                        <div class="action-buttons">
                            <asp:LinkButton ID="btnMarcarVisto" runat="server" 
                                CssClass="btn btn-success" OnClick="btnMarcarVisto_Click" Visible="true">
                                <i class="bi bi-check-circle"></i> Marcar como Visto
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="btnMarcarPendiente" runat="server" 
                                CssClass="btn btn-outline-secondary" OnClick="btnMarcarPendiente_Click" Visible="false">
                                <i class="bi bi-x-circle"></i> Marcar como Pendiente
                            </asp:LinkButton>
                            
                            <asp:HyperLink ID="lnkVerBiblioteca" runat="server" CssClass="btn btn-outline-primary"
                                NavigateUrl="~/Vista/Usuario/BibliotecaSeñas.aspx">
                                <i class="bi bi-collection"></i> Ver Biblioteca de Señas
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Columna lateral - Señas relacionadas -->
            <div class="col-lg-4">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Señas Relacionadas</h6>
                    </div>
                    <div class="card-body">
                        <asp:ListView ID="lvSenasRelacionadas" runat="server" OnItemCommand="lvSenasRelacionadas_ItemCommand">
                            <EmptyDataTemplate>
                                <div class="text-center py-4">
                                    <i class="bi bi-info-circle fs-1 text-muted"></i>
                                    <p class="mt-2">No hay señas relacionadas disponibles.</p>
                                </div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div class="card related-sena-card mb-3">
                                    <div class="card-body p-3">
                                        <h6 class="card-title mb-1"><%# Eval("nombreSeña") %></h6>
                                        <p class="card-text small text-muted mb-2"><%# Eval("tipoSeña") %></p>
                                        <div class="d-flex justify-content-between">
                                            <span class="badge <%# ConvertToBoolean(Eval("estado")) ? "bg-success" : "bg-secondary" %>">
                                                <%# ConvertToBoolean(Eval("estado")) ? "Visto" : "Pendiente" %>
                                            </span>
                                            <asp:LinkButton ID="btnVerSena" runat="server" CssClass="btn btn-sm btn-outline-primary"
                                                CommandName="VerSena" CommandArgument='<%# Eval("idSeña") %>'>
                                                Ver <i class="bi bi-eye"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                
                <!-- Estado de progreso -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Tu Progreso</h6>
                    </div>
                    <div class="card-body">
                        <div class="d-flex align-items-center">
                            <div class="flex-shrink-0">
                                <div class="progress-circle">
                                    <span id="lblPorcentajeProgreso" runat="server" class="progress-number">0%</span>
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <p class="mb-1">Progreso en este módulo:</p>
                                <div class="progress" style="height: 8px;">
                                    <div class="progress-bar bg-success" id="progressBar" runat="server" 
                                         role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                    </div>
                                </div>
                                <p class="small text-muted mt-2" id="lblProgresoDetalle" runat="server">
                                    Has visto 0 de 0 señas
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>