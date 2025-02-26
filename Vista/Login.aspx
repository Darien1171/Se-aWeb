<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SeñaWeb.Vista.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
    <link href="Vista/css/login.css" rel="stylesheet" />
    <link rel="icon" type="image/x-icon" href="../recursos/assets/ASeI.ico" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        .btn-succes {
            background: linear-gradient(90deg, #444e4d, #305641, #245c15, #35721c);
            color: #000;
            border: none;
            padding: 10px 20px; /* Espaciado interno */

            transition: background-color 0.3s, transform 0.2s, box-shadow 0.3s, border 0.2s;
        }

        .btn {
            --bs-btn-padding-x: 0.75rem;
            --bs-btn-padding-y: 0.375rem;
            --bs-btn-font-family: Merriweather Sans, -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica Neue, Arial, Noto Sans, sans-serif, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji;
            --bs-btn-font-size: 1rem;
            --bs-btn-font-weight: 400;
            --bs-btn-line-height: 1.5;
            --bs-btn-bg: #4e73df;
            --bs-btn-border-width: 1px;
            --bs-btn-border-color: transparent;
            --bs-btn-border-radius: 0.375rem;
            --bs-btn-hover-border-color: transparent;
            --bs-btn-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.15), 0 1px 1px rgba(0, 0, 0, 0.075);
            --bs-btn-disabled-opacity: 0.65;
            --bs-btn-focus-box-shadow: 0 0 0 0.25rem rgba(var(--bs-btn-focus-shadow-rgb), .5);
            display: inline-block;
            padding: var(--bs-btn-padding-y) var(--bs-btn-padding-x);
            font-family: var(--bs-btn-font-family);
            font-size: var(--bs-btn-font-size);
            font-weight: var(--bs-btn-font-weight);
            line-height: var(--bs-btn-line-height);
            text-align: center;
            text-decoration: none;
            vertical-align: middle;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            user-select: none;
            border: var(--bs-btn-border-width) solid var(--bs-btn-border-color);
            border-radius: var(--bs-btn-border-radius);
            text-transform: capitalize;
        }

        /*.btn-succes:hover {
                background: linear-gradient(90deg, #35721c, #245c15, #305641, #444e4d);*/ /* Cambio de colores */
        /*transform: scale(1.05);*/ /* Ampliación ligera */
        /*color: #e8e8e8;
                box-shadow: 0 0 10px 3px #ffffff, 0 0 20px 6px #00ff00;
                border: 2px solid #00ff00;
            }*/

        /* Efecto al hacer clic */
        /*.btn-succes:active {
                transform: scale(0.95);*/ /* Efecto de presión */
        /*box-shadow: 0 0 10px 3px #ffffff, 0 0 20px 6px #00ff00;
                border: 2px solid #007bff;*/ /* Borde resaltado */
        /*}*/

        .gradient-custom-2 {
            /* Degradado de fondo */
            background: linear-gradient(180deg, #2A4B8D, #4A7FCF);
            border-top-right-radius: 0.3rem;
            border-bottom-right-radius: 0.3rem;
        }

        .gradient-form {
            height: 98vh; /* Un poco menos que 100vh para que no quede pegado */
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 10px 0; /* Agrega un pequeño margen arriba y abajo */
        }

        .card {
            max-height: 95%; /* Evita que el contenido se extienda fuera de la pantalla */
            overflow: auto; /* Añade scroll interno si es necesario */
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.7); /* Sombra uniforme */
        }


        img {
            max-height: 100px; /* Restringe el tamaño del logo para que no desborde */
        }

        @media (max-width: 768px) {
            .gradient-custom-2 {
                border-radius: revert-layer; /* Ajusta para pantallas pequeñas */
            }

            .mover-derecha {
                position: relative !important;
                left: 0 !important; /* Ajusta la posición del botón para móviles */
                margin-top: 15px;
            }

            .gradient-form .card {
                width: 100%; /* Asegura que la tarjeta ocupe todo el ancho en pantallas pequeñas */
                border-radius: 0 !important; /* Sin bordes redondeados en móviles */
            }

            .col-lg-6 {
                padding-left: 15px;
                padding-right: 15px;
            }

            .text-center img {
                max-width: 120px; /* Redimensiona el logo en pantallas más pequeñas */
            }
        }
    </style>
</head>

<body>


    <section class="h-100 gradient-form" style="background-color: white;">
        <div class="container h-100">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-xl-10">
                    <div class="card rounded-3 text-black">
                        <div class="row g-0">
                            <!-- Sección izquierda: Formulario -->
                            <div class="col-lg-6">
                                <div class="card-body p-md-5 mx-md-4">
                                    <div class="text-center">
                                        <img src="../recursos/assets/img/LogoSeñas.jpg" class="img-fluid mb-4" alt="logo señas" style="max-height" />
                                        <h4 class="mt-1 mb-5 pb-1">𝓛𝓸𝓰𝓲𝓷</h4>
                                    </div>
                                    <form runat="server">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <p>Por favor inicie sesión en su cuenta!</p>
                                        <div data-mdb-input-init class="form-outline mb-4">
                                            <%--<label class="form-label" for="txtEmail">Email</label>--%>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email " required="true"></asp:TextBox>

                                        </div>
                                        <div data-mdb-input-init class="form-outline mb-4">
                                            <%--<label class="form-label" for="txtPassword">Password</label>--%>
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password " required="true"></asp:TextBox>


                                        </div>
                                        <div class="text-center pt-1 mb-5 pb-1">
                                            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-secondary btn-block" OnClick="btnLogin_Click" />
                                        </div>
                                        <small>Dale click si no te has registrado <a href="Registrarse.aspx" class="text-blue text-center">¡Registrate!</a></small>




                                    </form>
                                </div>
                            </div>
                            <!-- Sección derecha: Bienvenida -->
                            <div class="col-lg-6 d-flex align-items-center gradient-custom-2">
                                <div class="text-white px-3 py-4 p-md-5 mx-md-4">
                                    <h2 class="mb-4">Bienvenido</h2>
                                    <p class="small mb-0">
                                        <h5>"El poder de tu voz no está en el sonido, sino en tus acciones, tus gestos y la fuerza de tu corazón. 
                                            Cada movimiento que haces, cada sonrisa que compartes, puede inspirar al mundo. Nunca subestimes tu capacidad de cambiar vidas, 
                                            porque en tu silencio hay un mensaje que grita esperanza, resiliencia y amor"
                                        </h5>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</body>
</html>
