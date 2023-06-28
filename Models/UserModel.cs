using System;

namespace Haulmer_3.Models
{
    public class UserModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Actualizacion { get; set; }
    }
}