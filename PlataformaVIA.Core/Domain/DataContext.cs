namespace PlataformaVIA.Core.Domain
{
    using System.Data.Entity;

    /// <summary>
    /// Clase emcargada de realizar las conexiones a la base de datos de la nUBE
    /// </summary>
    public class DataContext : DbContext
    {
        //Llamamos al constructor de la clase DbContext y le pasamos como parámetro el nombre de la conexión del app config.
        public DataContext() : base("ConexionPlataformaVIA")
        {
        }

        public DbSet<PruebasAzure.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<PlataformaVIA.Core.Domain.Seguridad.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<PlataformaVIA.Core.Domain.Seguridad.TipoUsuario> TipoUsuarios { get; set; }
    }
}
