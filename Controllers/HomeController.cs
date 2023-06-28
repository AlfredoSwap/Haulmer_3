using Haulmer_3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Haulmer_3.Controllers
{
    public class HomeController : Controller
    {

        private readonly HaulmerDbContext _dbContext;

        public HomeController(HaulmerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult CreateUser()
        {
            try
            {
                // Recuperar los valores de los campos del formulario
                string nombre = Request.Form["nombre"];
                string apellido = Request.Form["apellido"];
                string correo = Request.Form["correo"];
                string telefono = Request.Form["telefono"];
                string contrasena = Request.Form["contrasena"];
                var newUser = new UserModel
                {
                    Nombre = nombre,
                    Correo = correo,
                    Contrasena = contrasena,
                    Apellido = apellido,
                    Telefono = telefono,
                    Fecha_Creacion = DateTime.Now,
                    Fecha_Actualizacion = DateTime.Now,

                };
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                // Flujo exitoso
                return Json(new { success = true, message = "El usuario se creó correctamente." });
            }
            catch 
            {
                // Flujo negativo
                return Json(new { success = false, message = "El usuario no se creó correctamente." });
            }
        }
        public ActionResult GetUsers()
        {
            var users = _dbContext.Users.Select(u => new
            {
                u.Nombre,
                u.Apellido,
                u.Correo,
                u.Telefono,
            }).ToList();
            
            return Json(users);
        }
        public ActionResult EncriptText()
        {
            string text = Request.Form["text"];


            //byte[] keyBytes = new byte[24]; // Tamaño de clave para TripleDES
            //using (var rng = new RNGCryptoServiceProvider())
            //{
            //    rng.GetBytes(keyBytes);
            //}
            //string key = Convert.ToBase64String(keyBytes);
            //byte[] ivBytes = new byte[8]; // Tamaño de IV para TripleDES
            //using (var rng = new RNGCryptoServiceProvider())
            //{
            //    rng.GetBytes(ivBytes);
            //}
            //string iv = Convert.ToBase64String(ivBytes);
            //Para ambitos de prueba se deja la key y iv estaticas
            string key = "PGkKf+Wak5FU06JWDKFGb1s2/EdnEANA";
            string iv = "lP+LE/dZpEs=";

            string encryptedText = TDESEncryptor.Encrypt(text, key, iv);

            Console.WriteLine(encryptedText);
            // Si se guardaron los datos correctamente, se retorna una respuesta con un mensaje de éxito
            return Json(new { success = true, encryptedText = encryptedText });
        }
        public ActionResult DescriptText()
        {
            string text = Request.Form["text"];

            string key = "PGkKf+Wak5FU06JWDKFGb1s2/EdnEANA";
            string iv = "lP+LE/dZpEs=";

            string encryptedText = TDESEncryptor.Decrypt(text, key, iv);

            Console.WriteLine(encryptedText);
            return Json(new { success = true, encryptedText = encryptedText });
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}