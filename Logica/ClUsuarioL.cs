﻿using SeñaWeb.Datos;
using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Logica
{
    public class ClUsuarioL
    {
        public ClUsuarioE login(ClUsuarioE oSesionUsuario)
        {
            ClUsuarioD loginDatos = new ClUsuarioD();
            return loginDatos.loginUsuario(oSesionUsuario);
        }



        // Obtener datos del usuario por ID
        public ClUsuarioE MtdObtenerUsuarioPorId(int idUsuario)
        {
            try
            {
                // Validar
                if (idUsuario <= 0)
                {
                    throw new ArgumentException("ID de usuario no válido");
                }

                // Llamar a la capa de datos
                ClUsuarioD datosUsuario = new ClUsuarioD();
                return datosUsuario.MtdObtenerUsuarioPorId(idUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerUsuarioPorId (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }

        // Actualizar datos del usuario
        public bool MtdActualizarUsuario(ClUsuarioE oUsuario)
        {
            try
            {
                // Validaciones
                if (oUsuario == null)
                {
                    throw new ArgumentNullException("El objeto usuario no puede ser nulo");
                }

                if (oUsuario.idUsuario <= 0)
                {
                    throw new ArgumentException("ID de usuario no válido");
                }

                if (string.IsNullOrWhiteSpace(oUsuario.Nombre))
                {
                    throw new ArgumentException("El nombre es obligatorio");
                }

                if (string.IsNullOrWhiteSpace(oUsuario.Apellido))
                {
                    throw new ArgumentException("El apellido es obligatorio");
                }

                if (string.IsNullOrWhiteSpace(oUsuario.Email))
                {
                    throw new ArgumentException("El email es obligatorio");
                }

                // Validar formato de email (simple)
                if (!oUsuario.Email.Contains("@") || !oUsuario.Email.Contains("."))
                {
                    throw new ArgumentException("Email no válido");
                }

                // Llamar a la capa de datos
                ClUsuarioD datosUsuario = new ClUsuarioD();
                return datosUsuario.MtdActualizarUsuario(oUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdActualizarUsuario (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }

        // Cambiar contraseña
        public bool MtdCambiarContrasena(int idUsuario, string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            try
            {
                // Validaciones
                if (idUsuario <= 0)
                {
                    throw new ArgumentException("ID de usuario no válido");
                }

                if (string.IsNullOrWhiteSpace(contrasenaActual))
                {
                    throw new ArgumentException("La contraseña actual es obligatoria");
                }

                if (string.IsNullOrWhiteSpace(nuevaContrasena))
                {
                    throw new ArgumentException("La nueva contraseña es obligatoria");
                }

                if (nuevaContrasena != confirmarContrasena)
                {
                    throw new ArgumentException("La nueva contraseña y su confirmación no coinciden");
                }

                if (nuevaContrasena.Length < 6)
                {
                    throw new ArgumentException("La nueva contraseña debe tener al menos 6 caracteres");
                }

                // Llamar a la capa de datos
                ClUsuarioD datosUsuario = new ClUsuarioD();
                return datosUsuario.MtdCambiarContrasena(idUsuario, contrasenaActual, nuevaContrasena);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdCambiarContrasena (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }


        // Añadir estos métodos a la clase ClUsuarioL existente en SeñaWeb.Logica

        // Método para validar si un email ya está registrado
        public bool MtdVerificarEmailExistente(string email)
        {
            try
            {
                // Validar formato de email básico
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                {
                    throw new ArgumentException("El formato del correo electrónico no es válido");
                }

                // Llamar a la capa de datos
                ClUsuarioD datosUsuario = new ClUsuarioD();
                return datosUsuario.MtdVerificarEmailExistente(email);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdVerificarEmailExistente (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }

        // Método para registrar un nuevo usuario
        public int MtdRegistrarUsuario(ClUsuarioE oUsuario)
        {
            try
            {
                // Validaciones
                if (oUsuario == null)
                {
                    throw new ArgumentNullException("El objeto usuario no puede ser nulo");
                }

                if (string.IsNullOrWhiteSpace(oUsuario.Nombre))
                {
                    throw new ArgumentException("El nombre es obligatorio");
                }

                if (string.IsNullOrWhiteSpace(oUsuario.Apellido))
                {
                    throw new ArgumentException("El apellido es obligatorio");
                }

                if (string.IsNullOrWhiteSpace(oUsuario.Email))
                {
                    throw new ArgumentException("El email es obligatorio");
                }

                if (string.IsNullOrWhiteSpace(oUsuario.Contraseña) || oUsuario.Contraseña.Length < 6)
                {
                    throw new ArgumentException("La contraseña debe tener al menos 6 caracteres");
                }

                // Validar formato de email (simple)
                if (!oUsuario.Email.Contains("@") || !oUsuario.Email.Contains("."))
                {
                    throw new ArgumentException("Email no válido");
                }

                // Verificar si el email ya existe
                if (MtdVerificarEmailExistente(oUsuario.Email))
                {
                    return -1; // Indicar que el usuario ya existe
                }

                // Si pasa todas las validaciones, proceder con el registro
                ClUsuarioD datosUsuario = new ClUsuarioD();
                return datosUsuario.MtdRegistrarUsuario(oUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarUsuario (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }
    }
}