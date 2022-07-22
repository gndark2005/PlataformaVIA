namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain;
    using Core.Domain.ViaBaloto;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class ViaBalotoRepository : AData<Producto>, IViaBalotoRepository
    {

        public ResponseEO<Producto> ConsultarProductos(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina)
        {
            ResponseEO<Producto> respuesta = new ResponseEO<Producto>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarProductos";
                    command.Parameters.Add(command.CreateParameter("@TextoBusqueda", TextoBusqueda));
                    command.Parameters.Add(command.CreateParameter("@Pagina", NumeroPagina));
                    command.Parameters.Add(command.CreateParameter("@RegistrosPorPagina", RegistrosPorPagina));

                    SqlParameter outputIdParam = new SqlParameter("@totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Producto>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }
                    respuesta.NumeroPagina = NumeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)RegistrosPorPagina)));
                }
            }
            return respuesta;
        }

        public ResponseEO<Producto> ConsultarTodosProductos(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina)
        {
            ResponseEO<Producto> respuesta = new ResponseEO<Producto>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarTodosProductos";
                    command.Parameters.Add(command.CreateParameter("@TextoBusqueda", TextoBusqueda));
                    command.Parameters.Add(command.CreateParameter("@Pagina", NumeroPagina));
                    command.Parameters.Add(command.CreateParameter("@RegistrosPorPagina", RegistrosPorPagina));

                    SqlParameter outputIdParam = new SqlParameter("@totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Producto>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }
                    respuesta.NumeroPagina = NumeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)RegistrosPorPagina)));
                }
            }
            return respuesta;
        }

        public ResponseIndividualEO<ProductoDetalle> ConsultarProducto(string ClasificacionProducto, int IdProducto)
        {
            var respuesta = new ResponseIndividualEO<ProductoDetalle>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarProducto";
                    command.Parameters.Add(command.CreateParameter("@ClasificacionProducto", ClasificacionProducto));
                    command.Parameters.Add(command.CreateParameter("@id_producto", IdProducto));

                    respuesta.Entidad = this.ToList<ProductoDetalle>(command).FirstOrDefault();
                }
            }

            return respuesta;
        }

        public ResponseIndividualEO<ProductoEspecifico> ConsultarProductoGeneral(string ClasificacionProducto, int IdProducto)
        {
            var respuesta = new ResponseIndividualEO<ProductoEspecifico>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarProducto";
                    command.Parameters.Add(command.CreateParameter("@ClasificacionProducto", ClasificacionProducto));
                    command.Parameters.Add(command.CreateParameter("@id_producto", IdProducto));

                    respuesta.Entidad = this.ToList<ProductoEspecifico>(command).FirstOrDefault();
                }
            }

            return respuesta;
        }

        public ResponseEO<PuntoDeVenta> GetPuntosDeVentaCercanos(float latitud, float longitud, int numeroPagina, int registrosPorPagina)
        {
            var respuesta = new ResponseEO<PuntoDeVenta>();

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarPuntosCercanos";
                    command.Parameters.Add(command.CreateParameter("@Latitud", latitud));
                    command.Parameters.Add(command.CreateParameter("@Longitud", longitud));
                    command.Parameters.Add(command.CreateParameter("@NumeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@RegistrosPorPagina", registrosPorPagina));
                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<PuntoDeVenta>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    // respuesta.Entidades = this.ToList<PuntoDeVenta>(command).ToList();
                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }

            return respuesta;
        }

        public ResponseEO<PuntoDeVenta> GetPuntosDeVentaCercanosPorProducto(float latitud, float longitud, int numeroPagina, int registrosPorPagina, string clasificacionProducto, int idProducto)
        {
            var respuesta = new ResponseEO<PuntoDeVenta>();

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarPuntosCercanosPorProducto";
                    command.Parameters.Add(command.CreateParameter("@Latitud", latitud));
                    command.Parameters.Add(command.CreateParameter("@Longitud", longitud));
                    command.Parameters.Add(command.CreateParameter("@NumeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@RegistrosPorPagina", registrosPorPagina));
                    command.Parameters.Add(command.CreateParameter("@ClasificacionProducto", clasificacionProducto));
                    command.Parameters.Add(command.CreateParameter("@IdProducto", idProducto));
                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<PuntoDeVenta>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    //respuesta.Entidades = this.ToList<PuntoDeVenta>(command).ToList();
                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }
            return respuesta;
        }


        public ResponseEO<Transaccion> ConsultarTransacciones(string clasificacionProducto, int idProducto, int idCiudad, string textoReferencia, DateTime fechaInicio, DateTime fechaFin, int numeroPagina, int registrosPorPagina, string valor)
        {
            var respuesta = new ResponseEO<Transaccion>();

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarTransacciones";

                    command.Parameters.Add(command.CreateParameter("@ClasificacionProducto", clasificacionProducto));
                    command.Parameters.Add(command.CreateParameter("@IdProducto", idProducto));
                    command.Parameters.Add(command.CreateParameter("@IdCiudad", idCiudad));
                    command.Parameters.Add(command.CreateParameter("@TextoReferencia", textoReferencia));
                    command.Parameters.Add(command.CreateParameter("@Fechainicio", fechaInicio));
                    command.Parameters.Add(command.CreateParameter("@Fechafin", fechaFin));
                    command.Parameters.Add(command.CreateParameter("@ValorTransaccion", valor));
                    command.Parameters.Add(command.CreateParameter("@NumeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@RegistrosPorPagina", registrosPorPagina));

                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Transaccion>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    //respuesta.Entidades = this.ToList<Transaccion>(command).ToList();
                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }

            return respuesta;
        }


        public ResponseIndividualEO<TransaccionDetalle> ConsultarTransaccion(long IdTransaccion)
        {
            var respuesta = new ResponseIndividualEO<TransaccionDetalle>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarTransaccion";
                    command.Parameters.Add(command.CreateParameter("@Id_Transaccion", IdTransaccion));

                    respuesta.Entidad = this.ToList<TransaccionDetalle>(command).FirstOrDefault();
                }
            }

            return respuesta;
        }

        public ResponseEO<Ciudad> ConsultarCiudades(string textoBusqueda, int numeroPagina, int registrosPorPagina)
        {
            var respuesta = new ResponseEO<Ciudad>();

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ListarCiudades";
                    command.Parameters.Add(command.CreateParameter("@TextoConsulta", textoBusqueda));
                    command.Parameters.Add(command.CreateParameter("@NumeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@RegistrosPorPagina", registrosPorPagina));
                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Ciudad>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }
                    //respuesta.Entidades = this.ToList<Ciudad>(command).ToList();
                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));
                }
            }

            return respuesta;

        }

        public Sorteo ConsultarDatosSorteo()
        {
            var respuesta = new Sorteo();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ObtenerValoresSorteo";

                    respuesta = this.ToList<Sorteo>(command).FirstOrDefault();
                }
            }

            return respuesta;
        }

        public List<Categoria> GetSubCategorias()
        {
            var respuesta = new List<Categoria>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_GetSubCategorias";

                    respuesta = this.ToList<Categoria>(command).ToList();
                }
            }

            return respuesta;
        }

        public ResponseEO<Aliado> GetAliadosPorSubcategorias(Int64 IdSubcategoria, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            var respuesta = new ResponseEO<Aliado>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(command.CreateParameter("@IdSubcategoria", IdSubcategoria));
                    command.Parameters.Add(command.CreateParameter("@numeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@registrosPorPagina", registrosPorPagina));
                    command.CommandText = "ViaBaloto_GetAliadosPorSubcategorias";

                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Aliado>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }

            return respuesta;
        }


        public ResponseEO<Aliado> GetAliadosPorCategorias(string NombreCategoria, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            var respuesta = new ResponseEO<Aliado>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(command.CreateParameter("@NombreCategoria", NombreCategoria));
                    command.Parameters.Add(command.CreateParameter("@numeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@registrosPorPagina", registrosPorPagina));
                    command.CommandText = "ViaBaloto_GetAliadosPorCategorias";

                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                      
                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Aliado>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }

            return respuesta;
        }

        public ResponseEO<Producto> GetProductosPorAliadoCategoria(string NombreCategoria, int IdAliado, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            var respuesta = new ResponseEO<Producto>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(command.CreateParameter("@NombreCategoria", NombreCategoria));
                    command.Parameters.Add(command.CreateParameter("@IdAliado", IdAliado));
                    command.Parameters.Add(command.CreateParameter("@numeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@registrosPorPagina", registrosPorPagina));
                    command.CommandText = "ViaBaloto_GetProductosPorAliadoCategoria";

                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Producto>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }

            return respuesta;
        }

        public ResponseEO<Producto> GetProductosPorAliadoSubCategoria(int IdSubCategoria, int IdAliado, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            var respuesta = new ResponseEO<Producto>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(command.CreateParameter("@IdSubCategoria", IdSubCategoria));
                    command.Parameters.Add(command.CreateParameter("@IdAliado", IdAliado));
                    command.Parameters.Add(command.CreateParameter("@numeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@registrosPorPagina", registrosPorPagina));
                    command.CommandText = "ViaBaloto_GetProductosPorAliadoSubCategoria";

                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Producto>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }

            return respuesta;
        }

        public ResponseEO<Producto> ConsultarProductosGeneral(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina)
        {
            ResponseEO<Producto> respuesta = new ResponseEO<Producto>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarProductosGeneral";
                    command.Parameters.Add(command.CreateParameter("@TextoBusqueda", TextoBusqueda));
                    command.Parameters.Add(command.CreateParameter("@Pagina", NumeroPagina));
                    command.Parameters.Add(command.CreateParameter("@RegistrosPorPagina", RegistrosPorPagina));

                    SqlParameter outputIdParam = new SqlParameter("@totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Producto>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }
                    respuesta.NumeroPagina = NumeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)RegistrosPorPagina)));
                }
            }
            return respuesta;
        }



        public ResponseEO<Producto> GetProductosPorSubCategoria(int IdSubCategoria, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            var respuesta = new ResponseEO<Producto>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(command.CreateParameter("@IdSubCategoria", IdSubCategoria));
                    command.Parameters.Add(command.CreateParameter("@numeroPagina", numeroPagina));
                    command.Parameters.Add(command.CreateParameter("@registrosPorPagina", registrosPorPagina));
                    command.CommandText = "ViaBaloto_GetProductosPorSubCategoria";

                    SqlParameter outputIdParam = new SqlParameter("@Totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    respuesta.Entidades = this.ToList<Producto>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        respuesta.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        respuesta.TotalRegistros = 0;
                    }

                    respuesta.NumeroPagina = numeroPagina;
                    respuesta.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)respuesta.TotalRegistros / (decimal)registrosPorPagina)));

                }
            }

            return respuesta;
        }

    }
}
