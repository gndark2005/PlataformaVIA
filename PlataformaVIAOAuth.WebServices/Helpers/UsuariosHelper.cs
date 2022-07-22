namespace PlataformaVIAOAuth.WebServices.Helpers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIAOAuth.WebServices.Models;
    using System;
    using System.Threading.Tasks;
    using System.Web.Configuration;


    /// <summary>
    /// Clase encargada de administrar la seguridad de Plataforma VIA
    /// </summary>
    public class UsuariosHelper : IDisposable
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static DataContext db = new DataContext();

        public static bool BorrarUsuario(string nombreusuario, string nombrerol)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(nombreusuario);
            if (userASP == null)
            {
                return false;
            }

            var response = userManager.RemoveFromRole(userASP.Id, nombrerol);
            return response.Succeeded;
        }

        public static bool ActualizarNombreUsuario(string currentUserName, string newUserName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(currentUserName);
            if (userASP == null)
            {
                return false;
            }

            userASP.UserName = newUserName;
            userASP.Email = newUserName;
            var response = userManager.Update(userASP);
            return response.Succeeded;
        }

        public static void VerificarRol(string nombrerol)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // Verifica si el rol existe,  de lo contrario lo crea.
            if (!roleManager.RoleExists(nombrerol))
            {
                roleManager.Create(new IdentityRole(nombrerol));
            }
        }

        public static void VerificarSuperUsuario()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {
                CrearUsuarioIdentity(email, "Admin", password);
                return;
            }
        }

        public static void CrearUsuarioIdentity(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                userASP = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                };

                userManager.Create(userASP, email);
            }

            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CrearUsuarioIdentity(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            var result = userManager.Create(userASP, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(userASP.Id, roleName);
            }
        }

        public void Dispose()
        {
            userContext.Dispose();
            db.Dispose();
        }
    }
}