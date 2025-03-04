<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="BibliotecaSeñas.aspx.cs" Inherits="SeñaWeb.Vista.Usuario.BibliotecaSeñas" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos específicos para la biblioteca de señas */
        .modulo-card {
            transition: all 0.3s ease;
            cursor: pointer;
            border-left: 5px solid transparent;
        }
        
        .modulo-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0,0,0,0.1);
        }
        
        .modulo-card.active {
            border-left: 5px solid var(--magenta);
            background-color: rgba(212, 35, 116, 0.05);
        }
        
        .modulo-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        
        .senas-container {
            min-height: 200px;
            transition: all 0.3s ease;
        }
        
        .sena-card {
            height: 100%;
            transition: all 0.3s ease;
            border: 1px solid #e0e0e0;
        }
        
        .sena-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
        }
        
        .sena-header {
            background-color: #f8f9fa;
            padding: 10px 15px;
            border-bottom: 1px solid #e0e0e0;
        }
        
        .sena-visto {
            border-left: 4px solid var(--green);
        }
        
        .empty-state {
            text-align: center;
            padding: 40px 20px;
        }
        
        .filters-container {
            transition: all 0.3s ease;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- ScriptManager para habilitar AJAX -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div class="container-fluid">
        <!-- Sección de encabezado -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Biblioteca de Señas</h1>
        </div>
        
        <!-- Estadísticas y progreso -->
        <div class="row mb-4">
            <div class="col-md-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                    Módulos completados
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblModulosCompletados" runat="server" Text="0"></asp:Label> de 
                                    <asp:Label ID="lblTotalModulos" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="bi bi-collection fs-2 text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="col-md-4">
                <div class="card border-left-info shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-info text-uppercase mb-1">
                                    Señas aprendidas
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblSenasVistas" runat="server" Text="0"></asp:Label> de 
                                    <asp:Label ID="lblTotalSenas" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="bi bi-hand-thumbs-up fs-2 text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="col-md-4">
                <div class="card border-left-success shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                    Progreso global
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblPorcentajeGlobal" runat="server" Text="0%"></asp:Label>
                                </div>
                                <div class="progress progress-sm mr-2 mt-2">
                                    <asp:Literal ID="litProgressBar" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="bi bi-graph-up fs-2 text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Contenedor principal - Grid de dos columnas -->
        <div class="row">
            <!-- Columna de módulos -->
            <div class="col-md-4">
                <div class="card shadow mb-4">
                    <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                        <h6 class="m-0 font-weight-bold">Módulos</h6>
                        <div class="input-group input-group-sm" style="width: 150px;">
                            <asp:TextBox ID="txtBuscarModulo" runat="server" CssClass="form-control" placeholder="Buscar..." AutoPostBack="true" OnTextChanged="txtBuscarModulo_TextChanged"></asp:TextBox>
                            <span class="input-group-text">
                                <i class="bi bi-search"></i>
                            </span>
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <div class="list-group list-group-flush" id="modulosList">
                            <asp:Repeater ID="rptModulos" runat="server" OnItemCommand="rptModulos_ItemCommand">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkModulo" runat="server" CssClass='<%# "list-group-item list-group-item-action modulo-card " + (Convert.ToInt32(Eval("idModulo")) == SelectedModuloId ? "active" : "") %>'
                                        CommandName="SelectModulo" CommandArgument='<%# Eval("idModulo") %>'>
                                        <div class="modulo-header">
                                            <h6 class="mb-1"><%# Eval("nombreModulo") %></h6>
                                            <span class="badge bg-primary"><%# Eval("porcentajeCompletado") %>%</span>
                                        </div>
                                        <div class="progress my-2" style="height: 6px;">
                                            <div class="progress-bar" role="progressbar" style="width: <%# Eval("porcentajeCompletado") %>%;" 
                                                aria-valuenow="<%# Eval("porcentajeCompletado") %>" aria-valuemin="0" aria-valuemax="100">
                                            </div>
                                        </div>
                                        <small class="text-muted">
                                            <asp:Literal ID="litSenasInfo" runat="server" 
                                                Text='<%# string.Format("{0} tipos de señas", GetCantidadTiposSena(Convert.ToInt32(Eval("idModulo")))) %>'>
                                            </asp:Literal>
                                        </small>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Panel ID="pnlNoModulos" runat="server" CssClass="empty-state" Visible='<%# ((Repeater)Container.Parent).Items.Count == 0 %>'>
                                        <i class="bi bi-emoji-frown fs-1 text-muted mb-3"></i>
                                        <h5>No se encontraron módulos</h5>
                                        <p class="text-muted">No hay módulos disponibles que coincidan con tu búsqueda.</p>
                                    </asp:Panel>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Columna de señas -->
            <div class="col-md-8">
                <div class="card shadow mb-4">
                    <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                        <h6 class="m-0 font-weight-bold">
                            <asp:Literal ID="litTituloSenas" runat="server" Text="Selecciona un módulo para ver sus señas"></asp:Literal>
                        </h6>
                        <button type="button" class="btn btn-sm btn-outline-primary" id="btnToggleFilters" onclick="toggleFilters()">
                            <i class="bi bi-funnel"></i> Filtros
                        </button>
                    </div>
                    
                    <!-- Filtros de señas - Inicialmente oculto -->
                    <div class="card-body py-2 filters-container" id="filtersContainer" style="display: none;">
                        <div class="row g-2">
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlTipoSena" runat="server" CssClass="form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoSena_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Todos los tipos</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                    <asp:ListItem Value="todos">Todos los estados</asp:ListItem>
                                    <asp:ListItem Value="vistas">Solo vistas</asp:ListItem>
                                    <asp:ListItem Value="pendientes">Solo pendientes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-5">
                                <div class="input-group input-group-sm">
                                    <asp:TextBox ID="txtBuscarSena" runat="server" CssClass="form-control" placeholder="Buscar señas..."></asp:TextBox>
                                    <asp:Button ID="btnBuscarSena" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarSena_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="card-body senas-container">
                        <asp:Panel ID="pnlSeleccionaModulo" runat="server" CssClass="empty-state" Visible="true">
                            <i class="bi bi-arrow-left-circle fs-1 text-muted mb-3"></i>
                            <h5>Selecciona un módulo</h5>
                            <p class="text-muted">Escoge un módulo de la lista de la izquierda para ver las señas disponibles.</p>
                        </asp:Panel>
                        
                        <asp:UpdatePanel ID="upSenas" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row" id="senasGrid">
                                    <asp:Repeater ID="rptSenas" runat="server" OnItemCommand="rptSenas_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-6 col-lg-4 mb-3">
                                                <div class="card sena-card <%# Convert.ToBoolean(Eval("estado")) ? "sena-visto" : "" %>">
                                                    <div class="sena-header d-flex justify-content-between align-items-center">
                                                        <h6 class="mb-0 text-truncate" title='<%# Eval("nombreSeña") %>'>
                                                            <%# Eval("nombreSeña") %>
                                                        </h6>
                                                        <span class="badge <%# Convert.ToBoolean(Eval("estado")) ? "bg-success" : "bg-secondary" %>">
                                                            <%# Convert.ToBoolean(Eval("estado")) ? "Visto" : "Pendiente" %>
                                                        </span>
                                                    </div>
                                                    <div class="card-body p-3">
                                                        <p class="card-text text-muted small mb-2">
                                                            <i class="bi bi-tag me-1"></i> <%# Eval("tipoSeña") %>
                                                        </p>
                                                        <div class="d-grid gap-2">
                                                            <a href='<%# "SeñaDetalle.aspx?id=" + Eval("idSeña") %>' class="btn btn-sm btn-primary">
                                                                <i class="bi bi-play-circle me-1"></i> Ver Video
                                                            </a>
                                                            <asp:LinkButton ID="btnMarcarEstado" runat="server" 
                                                                CommandName='<%# Convert.ToBoolean(Eval("estado")) ? "MarcarPendiente" : "MarcarVisto" %>'
                                                                CommandArgument='<%# Eval("idSeña") %>'
                                                                CssClass='<%# Convert.ToBoolean(Eval("estado")) ? "btn btn-sm btn-outline-secondary" : "btn btn-sm btn-outline-success" %>'>
                                                                <i class='<%# Convert.ToBoolean(Eval("estado")) ? "bi bi-x-circle" : "bi bi-check-circle" %> me-1'></i>
                                                                <%# Convert.ToBoolean(Eval("estado")) ? "Marcar como pendiente" : "Marcar como visto" %>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Panel ID="pnlNoSenas" runat="server" CssClass="col-12 empty-state" Visible='<%# ((Repeater)Container.Parent).Items.Count == 0 %>'>
                                                <i class="bi bi-emoji-frown fs-1 text-muted mb-3"></i>
                                                <h5>No se encontraron señas</h5>
                                                <p class="text-muted">No hay señas disponibles que coincidan con los criterios seleccionados.</p>
                                                <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar filtros" CssClass="btn btn-primary" OnClick="btnLimpiarFiltros_Click" />
                                            </asp:Panel>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlTipoSena" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnBuscarSena" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        function toggleFilters() {
            var filtersContainer = document.getElementById('filtersContainer');
            if (filtersContainer.style.display === 'none') {
                filtersContainer.style.display = 'block';
            } else {
                filtersContainer.style.display = 'none';
            }
        }
    </script>
</asp:Content>