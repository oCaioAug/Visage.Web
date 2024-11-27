using Microsoft.AspNetCore.Mvc;
using Visage.Web.Models;
using Visage.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Visage.Web.Helpers;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Visage.Web.Models.Controllers
{
    public class AccountController : Controller
    {
        private readonly VisageContext _context;

        public AccountController(VisageContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            // Calcula o hash da senha fornecida pelo usuário
            string hashedPassword = PasswordHelper.HashPassword(model.Senha);

            // Simulação de autenticação sem persistir senhas no banco
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email && u.Senha == hashedPassword);
            Console.WriteLine($"Tipo: {usuario?.Email ?? "Não encotrado"}, Valor: {usuario?.Senha ?? "Não encotrado"}");

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(model);
            }

            // Cria dados de autenticação 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario?.Tipo ?? "Usuario") // Usa o TipoUsuario como role
            };

            foreach (var permissao in usuario.Permissoes)
            {
                claims.Add(new Claim("Permission", permissao.NomePermissao));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            Console.WriteLine("Claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }
            // Faz login do usuário
            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));
            Console.WriteLine("Usuário autenticado: " + User.Identity.IsAuthenticated);


            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var usuarioExistente = await _context.Usuarios.AnyAsync(u => u.Email == model.Email);
        //    if (usuarioExistente)
        //    {
        //        ModelState.AddModelError(string.Empty, "O email já está em uso.");
        //        return View(model);
        //    }

        //    var novoUsuario = new UsuariosController
        //    {
        //        Nome = model.Nome,
        //        Email = model.Email,
        //        Password = Usuario.HashPassword(model.Password), // Hash de senha
        //        TipoUsuarioId = 2 // Exemplo: Usuário comum
        //    };

        //    _context.Usuarios.Add(novoUsuario);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Login");
        //}

        //// GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
