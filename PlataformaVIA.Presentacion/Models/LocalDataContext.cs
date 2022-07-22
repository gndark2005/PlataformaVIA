namespace PlataformaVIA.Presentacion.Models
{
    using Core.Domain;
    using Presentacion.ViewModels;

    /// <summary>
    /// Clase necesaria para poder migrar la base de datos (Code First)
    /// </summary>
    public class LocalDataContext : DataContext
    {
        //public System.Data.Entity.DbSet<Core.Domain.PruebasAzure.Categoria> Categorias { get; set; }

        //public System.Data.Entity.DbSet<Core.Domain.Seguridad.Usuario> Usuarios { get; set; }


        //public System.Data.Entity.DbSet<Core.Domain.Seguridad.TipoUsuario> TipoUsuarios { get; set; }

        public System.Data.Entity.DbSet<RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<UsuarioInfo> UsuarioInfo { get; set; }

        public System.Data.Entity.DbSet<ViewModels.UsuarioPuntoDeVenta> UsuarioPuntoDeVenta { get; set; }

        //public System.Data.Entity.DbSet<UserProfileInfo> UserProfileInfo { get; set; }



    }
}