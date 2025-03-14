<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RegistrarModulo.aspx.cs" Inherits="SeñaWeb.Vista.Admin.RegistrarModulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0">Registrar Módulo</h1>
        </div>

        
        <div class="row">
            <div class="col-lg-8">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Información del Módulo</h6>
                    </div>
                    <div class="card-body">
                        
                        <div id="alertSuccess" runat="server" class="alert alert-success alert-dismissible fade show" role="alert" visible="false">
                            <i class="bi bi-check-circle-fill me-2"></i>
                            <span id="successMessage" runat="server">El módulo ha sido registrado correctamente.</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>

                        <div id="alertError" runat="server" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false">
                            <i class="bi bi-exclamation-triangle-fill me-2"></i>
                            <span id="errorMessage" runat="server">Ha ocurrido un error al registrar el módulo.</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>

                        
                        <div class="mb-3">
                            <label for="txtNombreModulo" class="form-label">Nombre del Módulo</label>
                            <asp:TextBox ID="txtNombreModulo" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del módulo"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtDescripcion" class="form-label">Descripción</label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" 
                                placeholder="Ingrese una descripción detallada del módulo" 
                                TextMode="MultiLine" Rows="4">
                            </asp:TextBox>
                        </div>

                        
                        <div class="mt-4 d-flex justify-content-between">
                            <button type="button" class="btn btn-secondary" 
                                onclick="location.href='<%= ResolveUrl("~/Vista/Admin/DashboardAdmin.aspx") %>'">
                                Cancelar
                            </button>
                            
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Módulo" 
                                CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                        </div>
                    </div>
                </div>
            </div>

            
            <div class="col-lg-4">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Información de Ayuda</h6>
                    </div>
                    <div class="card-body">
                        <p>Los módulos son categorías principales que agrupan tipos de señas relacionadas.</p>
                        <p>Ejemplos de módulos:</p>
                        <ul>
                            <li>Saludos y presentaciones</li>
                            <li>Números y matemáticas</li>
                            <li>Familia y relaciones</li>
                            <li>Alimentos y bebidas</li>
                        </ul>
                        <hr />
                        <p class="mb-0">
                            <i class="bi bi-info-circle me-2"></i>
                            Para obtener mejores resultados, proporcione nombres claros y descripciones detalladas que ayuden a los usuarios a comprender el propósito del módulo.
                        </p>
                    </div>
                </div>

                
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold">Módulos Recientes</h6>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvModulosRecientes" runat="server" 
                            CssClass="table table-hover table-sm" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No hay módulos registrados recientemente"
                            GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="nombreModulo" HeaderText="Nombre" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>