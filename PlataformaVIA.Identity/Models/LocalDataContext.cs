namespace PlataformaVIA.Identity.Models
{
    using Core.Domain;

    /// <summary>
    /// Clase necesaria para poder migrar la base de datos (Code First)
    /// </summary>
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Core.Domain.PruebasAzure.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<PlataformaVIA.Core.Domain.Seguridad.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<PlataformaVIA.Core.Domain.Seguridad.TipoUsuario> TipoUsuarios { get; set; }

        public System.Data.Entity.DbSet<PlataformaVIA.Identity.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}