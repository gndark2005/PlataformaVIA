namespace PlataformaVIA.Data.Repositories.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Core.Domain.AppMobile;
    using Data.Extensions;
    using Data.Repositories.Interfaces;

    public class AppMobileRepository : AData<ProductoComercial>, IAppMobileRepository
    {
        public IEnumerable<ProductoComercial> GetProductosByFiltro(string filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetProductosByFiltro";
                    command.Parameters.Add(command.CreateParameter("@TextoBusqueda", filtro));
                    return this.ToList<ProductoComercial>(command);
                }
            }
        }

        public ProductoComercialDetalle GetProducto(int id, string tipo)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "AppMobile_GetProductosByTipo";
                        command.Parameters.Add(command.CreateParameter("@id", id));
                        command.Parameters.Add(command.CreateParameter("@tipo", tipo));
                        ProductoComercialDetalle resultado = this.ToList<ProductoComercialDetalle>(command).FirstOrDefault();

                        return resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Banner GetBanner()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetBanner";

                    var result = this.ToList<Banner>(command).FirstOrDefault();

                    if (result != null)
                    {
                        if (!string.IsNullOrEmpty(result.Id_Banner.ToString()))
                            result.Articulo = GetImagenByCodInstructivo(result.Id_Banner);
                    }
                    
                    return result;
                }
            }
        }

        public IEnumerable<Categoria> GetCategoriasByFechaSincronizacion(DateTime pultimaFechaSincronizacion)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetCategoriasByFechaSincronizacion";
                    command.Parameters.Add(command.CreateParameter("@ultimaFechaSincronizacion", pultimaFechaSincronizacion));
                    return this.ToList<Categoria>(command);
                }
            }
        }

        public IEnumerable<InstructivoTerminal> GetInstructivosByTerminal(int tipoTerminal)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetInstructivosByTerminal";
                    command.Parameters.Add(command.CreateParameter("@tipoTerminal", tipoTerminal));
                    return this.ToList<InstructivoTerminal>(command);
                }
            }
        }

        public IEnumerable<InstructivoTerminal> GetInstructivosByProductoYTerminal(int tipoTerminal, int CodProducto)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetInstructivosByProductoYTerminal";
                    command.Parameters.Add(command.CreateParameter("@CodTipoTerminal", tipoTerminal));
                    command.Parameters.Add(command.CreateParameter("@CodProducto", CodProducto));
                    return this.ToList<InstructivoTerminal>(command);
                }
            }
        }

        public IEnumerable<Instructivo> GetInstructivos()
        {

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetInstructivos";
                    var ltResult = this.ToList<Instructivo>(command);
                    
                    return ltResult;
                }
            }
        }

        public Instructivo GetInstructivoById(int codInstructivo)
        {

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetInstructivoById";
                    command.Parameters.Add(command.CreateParameter("@codInstructivo", codInstructivo));
                    var Result = this.ToList<Instructivo>(command).FirstOrDefault();

                    Result.Articulo = GetImagenByCodInstructivo(codInstructivo);

                    return Result;
                }
            }
        }

        public Instructivo GetInstructivoByCodInstructivo(int codInstructivo)
        {

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetInstructivoByCodInstructivo";
                    command.Parameters.Add(command.CreateParameter("@CodInstructivo", codInstructivo));
                    var Result = this.ToList<Instructivo>(command).FirstOrDefault();

                    Result.Articulo = GetImagenByCodInstructivo(codInstructivo);

                    return Result;
                }
            }
        }

        public Instructivo GetInstructivoByTerminalYCategoria(int CodTipoTerminal, int CodCategoria)
        {

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetInstructivoByTerminalYCategoria";
                    command.Parameters.Add(command.CreateParameter("@CodTipoTerminal", CodTipoTerminal));
                    command.Parameters.Add(command.CreateParameter("@CodCategoria", CodCategoria));
                    var Result = this.ToList<Instructivo>(command).FirstOrDefault();

                    Result.Articulo = GetImagenByCodInstructivo(Result.Id_Instructivo);

                    return Result;
                }
            }
        }

        public Articulo GetImagenByCodInstructivoImagen(int CodInstructivoImagen)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetArticulosByCodInstructivo";
                    command.Parameters.Add(command.CreateParameter("@codInstructivo", CodInstructivoImagen));
                    return this.ToList<Articulo>(command).FirstOrDefault();
                }
            }
        }

        public IEnumerable<Articulo> GetImagenByCodInstructivo(int CodInstructivoImagen)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetImagenesByCodInstructivo";
                    command.Parameters.Add(command.CreateParameter("@codInstructivo", CodInstructivoImagen));
                    return this.ToList<Articulo>(command);
                }
            }
        }

        public Noticia GetNoticia()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_GetNoticia";
                    var ltResult = this.ToList<Noticia>(command).FirstOrDefault();

                    ltResult.Articulo = GetImagenByCodInstructivo(ltResult.Id_Noticia);

                    return ltResult;
                }
            }
        }

        public IEnumerable<Punto> GetPuntosCercanos(double longitud, double latitud)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AppMobile_ObtenerPuntosCercanos";
                    command.Parameters.Add(command.CreateParameter("@Latitud", latitud));
                    command.Parameters.Add(command.CreateParameter("@Longitud", longitud));
                    return this.ToList<Punto>(command);
                }
            }
        }
    }
}
