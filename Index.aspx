<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SeñaWeb.Index" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8" />
    <title>SeñaWeb</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="Free HTML Templates" name="keywords" />
    <meta content="Free HTML Templates" name="description" />

    <!-- Favicon -->
    <link href="Vista/kidkinder-1.0.0/img/favicon.ico" rel="icon" />

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link
      href="https://fonts.googleapis.com/css2?family=Handlee&family=Nunito&display=swap"
      rel="stylesheet"
    />

    <!-- Font Awesome -->
    <link
      href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css"
      rel="stylesheet"
    />

    <!-- Flaticon Font -->
    <link href="Vista/kidkinder-1.0.0/lib/flaticon/font/flaticon.css" rel="stylesheet" />

    <!-- Libraries Stylesheet -->
    <link href="Vista/kidkinder-1.0.0/lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet" />
    <link href="Vista/kidkinder-1.0.0/lib/lightbox/css/lightbox.min.css" rel="stylesheet" />

    <!-- Customized Bootstrap Stylesheet -->
    <link href="Vista/kidkinder-1.0.0/css/style.css" rel="stylesheet" />
  </head>

  <body>
    <!-- Navbar Start -->
    <div class="container-fluid bg-light position-relative shadow">
      <nav
        class="navbar navbar-expand-lg bg-light navbar-light py-3 py-lg-0 px-0 px-lg-5"
      >
        <a
          href=""
          class="navbar-brand font-weight-bold text-secondary"
          style="font-size: 50px"
        >
          <i class="flaticon-043-teddy-bear"></i>
          <span class="text-primary">SeñaWeb</span>
        </a>
        <button
          type="button"
          class="navbar-toggler"
          data-toggle="collapse"
          data-target="#navbarCollapse"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div
          class="collapse navbar-collapse justify-content-between"
          id="navbarCollapse"
        >
          <div class="navbar-nav font-weight-bold mx-auto py-0">
            <a href="Index.aspx" class="nav-item nav-link active">Inicio</a>
            <a href="about.html" class="nav-item nav-link">Sobre Nosotros</a>
          </div>
          <a href="Vista/Login.aspx" class="btn btn-primary px-4">Iniciar Sesion</a>
        </div>
      </nav>
    </div>
    <!-- Navbar End -->

    <!-- Header Start -->
<!-- Header Start -->
<div class="container-fluid bg-primary px-0 px-md-5 mb-5">
  <div class="row align-items-center px-3">
    <div class="col-lg-6 text-center text-lg-left">
      <h4 class="text-white mb-4 mt-5 mt-lg-0">Seña Web</h4>
      <h1 class="display-3 font-weight-bold text-white">
        Aprendiendo Lengua de Señas de Manera Innovadora
      </h1>
      <p class="text-white mb-4">
        Seña Web es una plataforma educativa diseñada para ayudar a los 
        estudiantes a aprender el lenguaje de señas de forma interactiva y dinámica.
        A través de recursos visuales, ejercicios prácticos y metodologías inclusivas, 
        buscamos fomentar la comunicación y la accesibilidad para todos.
      </p>
      <a href="" class="btn btn-secondary mt-1 py-3 px-5">Conoce Más</a>
    </div>
    <div class="col-lg-6 text-center text-lg-right">
     <img class="img-fluid mt-5" src="Vista/imagenes/Señas1.jpg" 
     alt="Seña Web" style="max-width: 100%; height: auto; clip-path: ellipse(60% 40% at 50% 50%);" />

    </div>
  </div>
</div>
<!-- Header End -->


    <!-- Header End -->

   <!-- Facilities Start -->
<div class="container-fluid pt-5">
  <div class="container pb-3">
    <div class="row">
      <div class="col-lg-4 col-md-6 pb-1">
        <div
          class="d-flex bg-light shadow-sm border-top rounded mb-4"
          style="padding: 30px"
        >
          <i class="flaticon-050-fence h1 font-weight-normal text-primary mb-3"></i>
          <div class="pl-4">
            <h4>Aprendizaje Interactivo</h4>
            <p class="m-0">
              Aprende lengua de señas a través de videos, ejercicios dinámicos y juegos educativos diseñados para una mejor comprensión.
            </p>
          </div>
        </div>
      </div>
      <div class="col-lg-4 col-md-6 pb-1">
        <div
          class="d-flex bg-light shadow-sm border-top rounded mb-4"
          style="padding: 30px"
        >
          <i class="flaticon-022-drum h1 font-weight-normal text-primary mb-3"></i>
          <div class="pl-4">
            <h4>Recursos Visuales</h4>
            <p class="m-0">
              Imágenes y animaciones que facilitan el aprendizaje de cada signo, mejorando la retención y comprensión.
            </p>
          </div>
        </div>
      </div>
      <div class="col-lg-4 col-md-6 pb-1">
        <div
          class="d-flex bg-light shadow-sm border-top rounded mb-4"
          style="padding: 30px"
        >
          <i class="flaticon-017-toy-car h1 font-weight-normal text-primary mb-3"></i>
          <div class="pl-4">
            <h4>Accesibilidad Total</h4>
            <p class="m-0">
              Plataforma optimizada para todos los dispositivos, con subtítulos, audios y transcripciones para mayor accesibilidad.
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>


    <!-- Facilities Start -->

   <!-- About Start -->
<div class="container-fluid py-5">
  <div class="container">
    <div class="row align-items-center">
      <div class="col-lg-5">
        <img
          class="img-fluid rounded mb-5 mb-lg-0"
          src="Vista/imagenes/SobreNosotros.png"
          alt="Lengua de señas"
        />
      </div>
      <div class="col-lg-7">
        <p class="section-title pr-5">
          <span class="pr-2">Sobre Nosotros</span>
        </p>
        <h1 class="mb-4">Aprende Lengua de Señas de Manera Interactiva</h1>
        <p>
          Nuestra plataforma está diseñada para facilitar el aprendizaje de la lengua de señas a través de métodos dinámicos y accesibles. Creemos en la inclusión y en brindar herramientas efectivas para la comunicación.
        </p>
        <div class="row pt-2 pb-4">
          <div class="col-6 col-md-4">
            <img class="img-fluid rounded" src="Vista/imagenes/SobreNosotros2.jpg" alt="Aprendizaje de señas" />
          </div>
          <div class="col-6 col-md-8">
            <ul class="list-inline m-0">
              <li class="py-2 border-top border-bottom">
                <i class="fa fa-check text-primary mr-3"></i>Métodos visuales interactivos
              </li>
              <li class="py-2 border-bottom">
                <i class="fa fa-check text-primary mr-3"></i>Ejercicios prácticos y evaluaciones
              </li>
              <li class="py-2 border-bottom">
                <i class="fa fa-check text-primary mr-3"></i>Accesible para todos los dispositivos
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

    <!-- About End -->
    <!-- Team Start -->
<div class="container-fluid pt-5">
  <div class="container">
    <div class="text-center pb-2">
      <p class="section-title px-5">
        <span class="px-2">Our Developers</span>
      </p>
      <h1 class="mb-4">Meet Our Developers</h1>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-6 col-lg-4 text-center team mb-5">
        <div class="position-relative overflow-hidden mb-4" style="border-radius: 100%">
          <img class="img-fluid w-100" src="img/team-1.jpg" alt="Developer 1" />
        </div>
        <h4>Aquí debes poner tu nombre</h4>
        <i>Aquí debes poner tu rol</i>
      </div>
      <div class="col-md-6 col-lg-4 text-center team mb-5">
        <div class="position-relative overflow-hidden mb-4" style="border-radius: 100%">
          <img class="img-fluid w-100" src="img/team-2.jpg" alt="Developer 2" />
        </div>
        <h4>Aquí debes poner tu nombre</h4>
        <i>Aquí debes poner tu rol</i>
      </div>
    </div>
  </div>
</div>
<!-- Team End -->
    <!-- Testimonial Start -->
<div class="container-fluid py-5">
  <div class="container p-0">
    <div class="text-center pb-2">
      <p class="section-title px-5">
        <span class="px-2">Testimonios</span>
      </p>
      <h1 class="mb-4">Lo que dicen nuestros estudiantes</h1>
    </div>
    <div class="owl-carousel testimonial-carousel">
      <div class="testimonial-item px-3">
        <div class="bg-light shadow-sm rounded mb-4 p-4">
          <h3 class="fas fa-quote-left text-primary mr-3"></h3>
          "Aprender lengua de señas ha sido una experiencia increíble. Ahora puedo comunicarme mejor con mi hermano sordo y entender su mundo."
        </div>
        <div class="d-flex align-items-center">
          <img
            class="rounded-circle"
            src="Vista/kidkinder-1.0.0/img/testimonial-1.jpg"
            style="width: 70px; height: 70px"
            alt="Imagen 1"
          />
          <div class="pl-3">
            <h5>María López</h5>
            <i>Estudiante</i>
          </div>
        </div>
      </div>
      <div class="testimonial-item px-3">
        <div class="bg-light shadow-sm rounded mb-4 p-4">
          <h3 class="fas fa-quote-left text-primary mr-3"></h3>
          "Siempre quise aprender lengua de señas, y este curso me ha ayudado mucho. Ahora puedo ayudar a clientes sordos en mi trabajo."
        </div>
        <div class="d-flex align-items-center">
          <img
            class="rounded-circle"
            src="Vista/kidkinder-1.0.0/img/testimonial-2.jpg"
            style="width: 70px; height: 70px"
            alt="Imagen 2"
          />
          <div class="pl-3">
            <h5>Carlos Ramírez</h5>
            <i>Atención al cliente</i>
          </div>
        </div>
      </div>
      <div class="testimonial-item px-3">
        <div class="bg-light shadow-sm rounded mb-4 p-4">
          <h3 class="fas fa-quote-left text-primary mr-3"></h3>
          "Gracias a este curso, pude conectar con más personas de la comunidad sorda y mejorar mi comunicación en el aula."
        </div>
        <div class="d-flex align-items-center">
          <img
            class="rounded-circle"
            src="Vista/kidkinder-1.0.0/img/testimonial-3.jpg"
            style="width: 70px; height: 70px"
            alt="Imagen 3"
          />
          <div class="pl-3">
            <h5>Andrea Fernández</h5>
            <i>Docente</i>
          </div>
        </div>
      </div>
      <div class="testimonial-item px-3">
        <div class="bg-light shadow-sm rounded mb-4 p-4">
          <h3 class="fas fa-quote-left text-primary mr-3"></h3>
          "La lengua de señas me ha abierto muchas puertas. Ahora puedo comunicarme mejor con mis compañeros sordos en la universidad."
        </div>
        <div class="d-flex align-items-center">
          <img
            class="rounded-circle"
            src="Vista/kidkinder-1.0.0/img/testimonial-4.jpg"
            style="width: 70px; height: 70px"
            alt="Imagen 4"
          />
          <div class="pl-3">
            <h5>Fernando Gómez</h5>
            <i>Estudiante universitario</i>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<!-- Testimonial End -->

   <!-- Footer Start -->
<!-- Footer Start -->
<div class="container-fluid bg-secondary text-white mt-5 py-5 px-sm-3 px-md-5">
  <div class="row pt-5">
    <!-- Seña Web -->
    <div class="col-lg-3 col-md-6 mb-5">
      <a href="Index.aspx" class="navbar-brand font-weight-bold text-primary m-0 mb-4 p-0 d-block text-center" 
        style="font-size: 40px; line-height: 40px">
        <img src="Vista/imagenes/logo.png" alt="Seña Web" style="height: 50px; margin-bottom: 10px; display: block; margin: auto;">
        <span class="text-white d-block text-center">Seña Web</span>
      </a>
      <p class="text-center">
        Seña Web es un espacio dedicado a la enseñanza y aprendizaje del lenguaje de señas, 
        promoviendo la inclusión y accesibilidad para todas las personas.
      </p>
    </div>

    <!-- Contacto -->
    <div class="col-lg-3 col-md-6 mb-5 text-center">
      <h3 class="text-primary mb-4">Contacto</h3>
      <div class="mb-3">
        <h5 class="text-white mb-1">Email</h5>
        <p class="mb-0">contacto@señaweb.com</p>
      </div>
      <div>
        <h5 class="text-white mb-1">Teléfono</h5>
        <p class="mb-0">+57 123 456 7890</p>
      </div>
    </div>

    <!-- Enlaces Rápidos -->
    <div class="col-lg-3 col-md-6 mb-5 text-center">
      <h3 class="text-primary mb-4">Enlaces Rápidos</h3>
      <div class="d-flex flex-column align-items-center">
        <a class="text-white mb-2" href="Index.aspx"><i class="fa fa-angle-right mr-2"></i>Inicio</a>
        <a class="text-white mb-2" href="SobreNosotros.aspx"><i class="fa fa-angle-right mr-2"></i>Sobre Nosotros</a>
      </div>
    </div>

    <!-- Redes Sociales -->
    <div class="col-lg-3 col-md-6 mb-5 text-center">
      <h3 class="text-primary mb-4">Síguenos</h3>
      <div class="d-flex justify-content-center">
        <a class="btn btn-outline-primary rounded-circle text-center mr-2 px-0" 
          style="width: 38px; height: 38px" href="#"><i class="fab fa-twitter"></i></a>
        <a class="btn btn-outline-primary rounded-circle text-center mr-2 px-0" 
          style="width: 38px; height: 38px" href="#"><i class="fab fa-facebook-f"></i></a>
        <a class="btn btn-outline-primary rounded-circle text-center px-0" 
          style="width: 38px; height: 38px" href="#"><i class="fab fa-instagram"></i></a>
      </div>
    </div>
  </div>

  <!-- Derechos de Autor -->
  <div class="container-fluid pt-5 text-center" style="border-top: 1px solid rgba(23, 162, 184, 0.2);">
    <p class="m-0 text-white">
      &copy; <a class="text-primary font-weight-bold" href="#">Seña Web</a>. Todos los derechos reservados.
    </p>
  </div>
</div>


    <!-- Footer End -->

    <!-- Back to Top -->
    <a href="#" class="btn btn-primary p-3 back-to-top"
      ><i class="fa fa-angle-double-up"></i
    ></a>

    <!-- JavaScript Libraries -->
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.bundle.min.js"></script>
    <script src="Vista/kidkinder-1.0.0/lib/easing/easing.min.js"></script>
    <script src="Vista/kidkinder-1.0.0/lib/owlcarousel/owl.carousel.min.js"></script>
    <script src="Vista/kidkinder-1.0.0/lib/isotope/isotope.pkgd.min.js"></script>
    <script src="Vista/kidkinder-1.0.0/lib/lightbox/js/lightbox.min.js"></script>

    <!-- Contact Javascript File -->
    <script src="Vista/kidkinder-1.0.0/mail/jqBootstrapValidation.min.js"></script>
    <script src="Vista/kidkinder-1.0.0/mail/contact.js"></script>

    <!-- Template Javascript -->
    <script src="Vista/kidkinder-1.0.0/js/main.js"></script>
  </body>
</html>
