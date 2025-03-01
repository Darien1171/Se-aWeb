<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RegistrarTipoSeña.aspx.cs" Inherits="SeñaWeb.Vista.Admin.RegistrarTipoSeña" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Título de la página -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0">Registrar Tipo de Seña</h1>
        </div>

        <!-- Formulario de registro -->
        <div class="row">
            <div class="col-lg-8">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Información del Tipo de Seña</h6>
                    </div>
                    <div class="card-body">
                        <!-- Mensajes de alerta -->
                        <div id="alertSuccess" runat="server" class="alert alert-success alert-dismissible fade show" role="alert" visible="false">
                            <i class="bi bi-check-circle-fill me-2"></i>
                            <span id="successMessage" runat="server">El tipo de seña ha sido registrado correctamente.</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>

                        <div id="alertError" runat="server" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false">
                            <i class="bi bi-exclamation-triangle-fill me-2"></i>
                            <span id="errorMessage" runat="server">Ha ocurrido un error al registrar el tipo de seña.</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>

                        <!-- Formulario -->
                        <div class="mb-3">
                            <label for="ddlModulo" class="form-label">Módulo</label>
                            <asp:DropDownList ID="ddlModulo" runat="server" CssClass="form-select" required="true">
                            </asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label for="txtTipo" class="form-label">Nombre del Tipo de Seña</label>
                            <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del tipo de seña"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtDescripcion" class="form-label">Descripción</label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" 
                                placeholder="Ingrese una descripción detallada del tipo de seña" 
                                TextMode="MultiLine" Rows="4">
                            </asp:TextBox>
                        </div>

                        <!-- Botones de acción -->
                        <div class="mt-4 d-flex justify-content-between">
                            <button type="button" class="btn btn-secondary" 
                                onclick="location.href='<%= ResolveUrl("~/Vista/Admin/DashboardAdmin.aspx") %>'">
                                Cancelar
                            </button>
                            
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Tipo de Seña" 
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
                        <p>Los tipos de señas son categorías específicas dentro de un módulo que agrupan señas relacionadas.</p>
                        <p>Ejemplos de tipos de señas:</p>
                        <ul>
                            <li>Saludos formales</li>
                            <li>Saludos informales</li>
                            <li>Números del 1 al 10</li>
                            <li>Miembros de la familia</li>
                        </ul>
                        <hr />
                        <p class="mb-0">
                            <i class="bi bi-info-circle me-2"></i>
                            Seleccione primero el módulo al que pertenecerá este tipo de seña, luego especifique un nombre claro y una descripción detallada.
                        </p>
                    </div>
                </div>

                <!-- Tabla de tipos de seña recientes -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Tipos de Seña Recientes</h6>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvTiposSenaRecientes" runat="server" 
                            CssClass="table table-hover table-sm" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No hay tipos de seña registrados recientemente"
                            GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="tipo" HeaderText="Nombre" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="nombreModulo" HeaderText="Módulo" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>