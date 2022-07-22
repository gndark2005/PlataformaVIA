namespace PlataformaVIA.Presentacion.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Presentacion.ViewModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            PasswordHistory = new List<PasswordHistory>();
        }

        // public int DiasPassword { get; set; }

        //public int UsuarioInfoId { get; set; }
        //public virtual ICollection<UsuarioInfo> UsuariosInfo { get; set; }
        public virtual UsuarioInfo UsuarioInfo { get; set; }
        //public virtual UserProfileInfo UserProfileInfo { get; set; }

        public virtual List<PasswordHistory> PasswordHistory { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }

    //Clase creada para personalizar la creación de los roles
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("ConexionPlataformaVIA", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // //modelBuilder.HasDefaultSchema("Seguridad");

            modelBuilder.Entity<UsuarioInfo>()
            .Property(c => c.ID_USUARIOINFO)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Configure Id as FK for UserProfileInfo
            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(s => s.UsuarioInfo)
                .WithRequired(ad => ad.ApplicationUser);

            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<UsuarioInfo>()
            .HasKey<string>(e => e.CODASPNETUSER);

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}