<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RegistrarSeña.aspx.cs" Inherits="SeñaWeb.Vista.Admin.RegistrarSeña" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Título de la página -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0">Registrar Seña</h1>
        </div>

        <!-- Formulario de registro -->
        <div class="row">
            <div class="col-lg-8">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Información de la Seña</h6>
                    </div>
                    <div class="card-body">
                        <!-- Mensajes de alerta -->
                        <div id="alertSuccess" runat="server" class="alert alert-success alert-dismissible fade show" role="alert" visible="false">
                            <i class="bi bi-check-circle-fill me-2"></i>
                            <span id="successMessage" runat="server">La seña ha sido registrada correctamente.</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>

                        <div id="alertError" runat="server" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false">
                            <i class="bi bi-exclamation-triangle-fill me-2"></i>
                            <span id="errorMessage" runat="server">Ha ocurrido un error al registrar la seña.</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>

                        <!-- Formulario -->
                        <div class="mb-3">
                            <label for="ddlTipoSena" class="form-label">Tipo de Seña</label>
                            <asp:DropDownList ID="ddlTipoSena" runat="server" CssClass="form-select">
                            </asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label for="txtNombreSena" class="form-label">Nombre de la Seña</label>
                            <asp:TextBox ID="txtNombreSena" runat="server" CssClass="form-control" placeholder="Ingrese el nombre de la seña"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="fileVideo" class="form-label">Video de la Seña</label>
                            <asp:FileUpload ID="fileVideo" runat="server" CssClass="form-control" />
                            <small class="text-muted">Formatos permitidos: MP4, AVI, MOV. Tamaño máximo: 50 MB.</small>
                        </div>

                        <!-- Vista previa del video -->
                        <div class="mb-3" id="videoPreviewContainer" runat="server" visible="false">
                            <label class="form-label">Vista previa:</label>
                            <video id="videoPreview" runat="server" controls="controls" style="max-width: 100%; max-height: 300px;">
                                Su navegador no soporta la etiqueta de video.
                            </video>
                        </div>

                        <!-- Botones de acción -->
                        <div class="mt-4 d-flex justify-content-between">
                            <button type="button" class="btn btn-secondary" 
                                onclick="location.href='<%= ResolveUrl("~/Vista/Admin/DashboardAdmin.aspx") %>'">
                                Cancelar
                            </button>
                            
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Seña" 
                                CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Panel lateral de ayuda -->
            <div class="col-lg-4">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Información de Ayuda</h6>
                    </div>
                    <div class="card-body">
                        <p>Las señas son representaciones visuales de un concepto en lenguaje de señas:</p>
                        <ol>
                            <li>Seleccione el módulo y tipo de seña correspondiente.</li>
                            <li>Ingrese un nombre descriptivo y claro para la seña.</li>
                            <li>Suba un video que muestre claramente la realización de la seña.</li>
                        </ol>
                        <hr />
                        <p class="mb-0">
                            <i class="bi bi-info-circle me-2"></i>
                            Procure que el video tenga buena iluminación, enfoque claro en las manos y sea grabado contra un fondo neutro para mejor visibilidad.
                        </p>
                    </div>
                </div>

                <!-- Tabla de señas recientes -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Señas Recientes</h6>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvSenasRecientes" runat="server" 
                            CssClass="table table-hover table-sm" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No hay señas registradas recientemente"
                            GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="nombreSeña" HeaderText="Seña" />
                                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                <asp:TemplateField HeaderText="Video">
                                    <ItemTemplate>
                                        <a href='<%# ResolveUrl(Eval("urlVideo").ToString()) %>' target="_blank">
                                            <i class="bi bi-play-circle"></i> Ver
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
