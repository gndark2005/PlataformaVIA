namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Core.Domain.ViaBaloto;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using PlataformaVIAOAuth.WebServices.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;


    [Authorize]
    [RoutePrefix("api/ViaBaloto")]
    public class ViaBalotoController : ApiController
    {

        private IViaBalotoService ViaBalotoService;

        #region "Constructores"
        public ViaBalotoController(IViaBalotoRepository ViaBalotoRepository)
        {
            this.ViaBalotoService = new ViaBalotoService(ViaBalotoRepository);
        }
        #endregion

        #region "Metodos para pagina VIA y Baloto"

        ///GET api/ViaBaloto/ConsultarProductos
        /// <summary>
        ///Metodo usado para consultar los productos que contengan en el nombre una cadena de caracteres dados.  
        /// </summary>
        /// <param name="textoBusqueda" ></param>
        /// <param name="numeroPagina" ></param>
        /// <param name="registrosPorPagina" ></param>
        /// <returns>Objeto de tipo ResponseEO<Producto></returns>
        [Route("ConsultarProductos")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<Producto>))]
        public async Task<IHttpActionResult> ConsultarProductos(JObject form)
        {
            string textoBusqueda = string.Empty;
            var respuesta = new ResponseEO<Producto>();
            int numeroPagina = 0;
            int registrosPorPagina = 0;

            dynamic jsonobject = form;
            try
            {
                textoBusqueda = jsonobject.textoBusqueda;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;
                if (string.IsNullOrEmpty(textoBusqueda) || numeroPagina <= 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = ViaBalotoService.ConsultarProductos(textoBusqueda, numeroPagina, registrosPorPagina);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };

            }
            catch (ArgumentException aex)
            {
                ResponseEO<Producto> respuestaError = new ResponseEO<Producto>();
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = aex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                ResponseEO<Producto> respuestaError = new ResponseEO<Producto>();
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = ex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        ///GET api/ViaBaloto/ConsultaTodosProductos
        /// <summary>
        ///Metodo usado para consultar todos los productos que contengan en el nombre una cadena de caracteres dados.  
        /// </summary>
        /// <param name="textoBusqueda" ></param>
        /// <param name="numeroPagina" ></param>
        /// <param name="registrosPorPagina" ></param>
        /// <returns>Objeto de tipo ResponseEO<Producto></returns>
        [Route("ConsultaTodosProductos")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<Producto>))]
        public async Task<IHttpActionResult> ConsultaTodosProductos(JObject form)
        {
            string textoBusqueda = string.Empty;
            var respuesta = new ResponseEO<Producto>();
            int numeroPagina = 0;
            int registrosPorPagina = 0;

            dynamic jsonobject = form;
            try
            {
                textoBusqueda = jsonobject.textoBusqueda;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;
                if (string.IsNullOrEmpty(textoBusqueda) || numeroPagina <= 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = ViaBalotoService.ConsultarTodosProductos(textoBusqueda, numeroPagina, registrosPorPagina);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };

            }
            catch (ArgumentException aex)
            {
                ResponseEO<Producto> respuestaError = new ResponseEO<Producto>();
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = aex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                ResponseEO<Producto> respuestaError = new ResponseEO<Producto>();
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = ex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Método usado para consultar un producto o subproducto especifico.
        /// </summary>
        /// <param name="ClasificacionProducto"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("ConsultarProducto")]
        [HttpGet]
        [ResponseType(typeof(ResponseIndividualEO<ProductoDetalle>))]
        //public async Task<IHttpActionResult> ConsultarProducto(string ClasificacionProducto, int IdProducto)
        public async Task<IHttpActionResult> ConsultarProducto(JObject form)
        {
            var respuesta = new ResponseIndividualEO<ProductoDetalle>();
            string clasificacionProducto = string.Empty;
            int idProducto = 0;
            dynamic jsonobject = form;
            try
            {
                clasificacionProducto = jsonobject.clasificacionProducto;
                idProducto = jsonobject.idProducto;

                if (string.IsNullOrEmpty(clasificacionProducto) || idProducto == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = this.ViaBalotoService.ConsultarProducto(clasificacionProducto, idProducto);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = ex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Método usado para consultar un producto o subproducto especifico.
        /// </summary>
        /// <param name="ClasificacionProducto"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("ConsultarProductoGeneral")]
        [HttpGet]
        [ResponseType(typeof(ResponseIndividualEO<ProductoEspecifico>))]
        //public async Task<IHttpActionResult> ConsultarProducto(string ClasificacionProducto, int IdProducto)
        public async Task<IHttpActionResult> ConsultarProductoGeneral(JObject form)
        {
            var respuesta = new ResponseIndividualEO<ProductoEspecifico>();
            string clasificacionProducto = string.Empty;
            int idProducto = 0;
            dynamic jsonobject = form;
            try
            {
                clasificacionProducto = jsonobject.clasificacionProducto;
                idProducto = jsonobject.idProducto;

                if (string.IsNullOrEmpty(clasificacionProducto) || idProducto == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = this.ViaBalotoService.ConsultarProductoGeneral(clasificacionProducto, idProducto);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = ex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }


        /// <summary>
        /// Método usado para buscar el punto de venta mas cercano a una ubicacion.
        /// </summary>
        /// <param name="Latitud"></param>
        /// <param name="Longitud"></param>
        /// <param name="Cantidad"></param>
        /// <returns></returns>
        [Route("ConsultaPuntosDeVentaCercanos")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<PuntoDeVenta>))]
        public async Task<IHttpActionResult> ConsultaPuntosDeVentaCercanos(JObject form)
        {
            float latitud = 0.0F;
            float longitud = 0.0F;
            var respuesta = new ResponseEO<PuntoDeVenta>();
            int numeroPagina = 0;
            int registrosPorPagina = 0;

            dynamic jsonobject = form;

            try
            {
                latitud = jsonobject.latitud;
                longitud = jsonobject.longitud;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (latitud == 0.0F || longitud == 0.0F || numeroPagina == 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = ViaBalotoService.GetPuntosDeVentaCercanos(latitud, longitud, numeroPagina, registrosPorPagina);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };

            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }


        /// <summary>
        /// Método usado para buscar el punto de venta mas cercano a una ubicacion que tenga asignado un producto especifico.
        /// </summary>
        /// <param name="Latitud"></param>
        /// <param name="Longitud"></param>
        /// <param name="Cantidad"></param>
        /// <returns></returns>
        [Route("ConsultaPuntosDeVentaCercanosPorProducto")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<PuntoDeVenta>))]
        public async Task<IHttpActionResult> ConsultaPuntosDeVentaCercanosPorProducto(JObject form)
        {
            var respuesta = new ResponseEO<PuntoDeVenta>();
            float latitud = 0.0F;
            float longitud = 0.0F;
            string clasificacionProducto = string.Empty;
            int idProducto = -1;
            int numeroPagina = -1;
            int registrosPorPagina = 0;

            dynamic jsonobject = form;

            try
            {
                latitud = jsonobject.latitud;
                longitud = jsonobject.longitud;
                clasificacionProducto = jsonobject.clasificacionProducto;
                idProducto = jsonobject.idProducto;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (latitud == 0.0F || longitud == 0.0F || string.IsNullOrEmpty(clasificacionProducto) || idProducto == -1 || numeroPagina == -1 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = ViaBalotoService.GetPuntosDeVentaCercanosPorProducto(latitud, longitud, numeroPagina, registrosPorPagina, clasificacionProducto, idProducto);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };

            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        #endregion

        #region "Metodos para SAC"

        [Route("ConsultarTransacciones")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<Transaccion>))]
        //public async Task<IHttpActionResult> ConsultarTransacciones(string ClasificacionProducto, int IdProducto, int IdCiudad, string TextoReferencia, DateTime FechaInicio, DateTime FechaFin)
        public async Task<IHttpActionResult> ConsultarTransacciones(JObject form)
        {
            var respuesta = new ResponseEO<Transaccion>();
            string clasificacionProducto = string.Empty;
            int idProducto = -1;
            int idCiudad = -1;
            string textoReferencia = string.Empty;
            DateTime fechaInicio = DateTime.MinValue;
            DateTime fechaFin = DateTime.MinValue;
            string valor;
            int numeroPagina = 0;
            int registrosPorPagina = 0;

            dynamic jsonobject = form;

            try
            {
                clasificacionProducto = jsonobject.clasificacionProducto;
                idProducto = jsonobject.idProducto;
                idCiudad = jsonobject.idCiudad;
                valor = jsonobject.valor;
                textoReferencia = jsonobject.textoReferencia;
                fechaInicio = jsonobject.fechaInicio;
                fechaFin = jsonobject.fechaFin;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;
                
                if (string.IsNullOrEmpty(clasificacionProducto) ||
                    idProducto == -1 || 
                    idCiudad == -1 || 
                    string.IsNullOrEmpty(textoReferencia)  ||
                    fechaInicio == DateTime.MinValue ||
                    fechaFin == DateTime.MinValue ||
                    numeroPagina == 0 || 
                    registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                RegistroEventos.RegistrarParametros(TipoRegistroEvento.Registro, "clasificacion: " + clasificacionProducto + "; idProducto: " + idProducto + "; idCiudad: " + idCiudad + "; textoReferencia: " + textoReferencia + "; fechaInicio: " + fechaInicio + "; fechaFin: " + fechaFin + "; numeroPagina: " + numeroPagina + "; registrosPorPagina: " + registrosPorPagina + "; valor: " + valor);

                respuesta = this.ViaBalotoService.ConsultarTransacciones(clasificacionProducto, idProducto, idCiudad, textoReferencia, fechaInicio, fechaFin, numeroPagina, registrosPorPagina, valor);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };

            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("ConsultarTransaccion")]
        [HttpGet]
        [ResponseType(typeof(ResponseIndividualEO<TransaccionDetalle>))]
        public async Task<IHttpActionResult> ConsultarTransaccion(JObject form)
        {
            var respuesta = new ResponseIndividualEO<TransaccionDetalle>();
            long idTransaccion = 0;
            dynamic jsonobject = form;
            try
            {
                idTransaccion = jsonobject.idTransaccion;

                if (idTransaccion == 0)
                {
                    throw new ArgumentException("Los parametros no son validos");
                }

                RegistroEventos.RegistrarParametros(TipoRegistroEvento.Registro, "Id_Transaccion: " + idTransaccion);

                respuesta = this.ViaBalotoService.ConsultarTransaccion(idTransaccion);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }


        [Route("ConsultarCiudades")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<Ciudad>))]
        public async Task<IHttpActionResult> ConsultarCiudades(JObject form)
        {
            var respuesta = new ResponseEO<Ciudad>();
            int numeroPagina = 0;
            int registrosPorPagina = 0;
            string textoBusqueda = string.Empty;
            dynamic jsonobject = form;

            try
            {
                if (jsonobject.textoBusqueda == null || jsonobject.numeroPagina == null || jsonobject.registrosPorPagina == null) {
                    throw new ArgumentException("Los parametros no son validos");
                }

                textoBusqueda = jsonobject.textoBusqueda;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (string.IsNullOrEmpty(textoBusqueda) || numeroPagina == 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = this.ViaBalotoService.ConsultarCiudades(textoBusqueda,  numeroPagina, registrosPorPagina);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };

            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("GetSubCategorias")]
        [HttpPost]
        [ResponseType(typeof(List<Categoria>))]
        public async Task<IHttpActionResult> GetSubCategorias(JObject form)
        {
            var respuesta = new List<Categoria>();
            dynamic jsonobject = form;
            try
            {
                
                respuesta = this.ViaBalotoService.GetSubCategorias();
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("GetAliadosPorSubcategorias")]
        [HttpPost]
        [ResponseType(typeof(ResponseEO<Aliado>))]
        public async Task<IHttpActionResult> GetAliadosPorSubcategorias(JObject form)
        {
            var respuesta = new ResponseEO<Aliado>();
            int numeroPagina = 0;
            int registrosPorPagina = 0;
            long IdSubcategoria = 0;
            dynamic jsonobject = form;
            try
            {
                IdSubcategoria = jsonobject.IdSubcategoria;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (IdSubcategoria == 0 || numeroPagina == 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los parametros no son validos");
                }

                RegistroEventos.RegistrarParametros(TipoRegistroEvento.Registro, "GetAliadosPorSubcategorias _ IdSubcategoria: " + IdSubcategoria);

                respuesta = this.ViaBalotoService.GetAliadosPorSubcategorias(IdSubcategoria, numeroPagina, registrosPorPagina);
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("GetAliadosPorCategorias")]
        [HttpPost]
        [ResponseType(typeof(ResponseEO<Aliado>))]
        public async Task<IHttpActionResult> GetAliadosPorCategorias(JObject form)
        {
            var respuesta = new ResponseEO<Aliado>();
            string NombreCategoria = "";
            int numeroPagina = 0;
            int registrosPorPagina = 0;
            dynamic jsonobject = form;
            try
            {
                NombreCategoria = jsonobject.NombreCategoria;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (String.IsNullOrEmpty(NombreCategoria) || numeroPagina == 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los parametros no son validos");
                }

                RegistroEventos.RegistrarParametros(TipoRegistroEvento.Registro, "GetAliadosPorCategorias _ NombreCategoria: " + NombreCategoria);

                respuesta = this.ViaBalotoService.GetAliadosPorCategorias(NombreCategoria, numeroPagina, registrosPorPagina);
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("GetProductosPorAliadoCategoria")]
        [HttpPost]
        [ResponseType(typeof(ResponseEO<Producto>))]
        public async Task<IHttpActionResult> GetProductosPorAliadoCategoria(JObject form)
        {
            var respuesta = new ResponseEO<Producto>();
            string NombreCategoria = "";
            int numeroPagina = 0;
            int registrosPorPagina = 0;
            int IdAliado = 0;
            dynamic jsonobject = form;
            try
            {
                NombreCategoria = jsonobject.NombreCategoria;
                IdAliado = jsonobject.IdAliado;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (NombreCategoria == "" || IdAliado == 0 || numeroPagina == 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los parametros no son validos");
                }

                RegistroEventos.RegistrarParametros(TipoRegistroEvento.Registro, "GetProductosPorAliadoCategoria _ NombreCategoria: " + NombreCategoria + ", IdAliado: " + IdAliado);

                respuesta = this.ViaBalotoService.GetProductosPorAliadoCategoria(NombreCategoria, IdAliado, numeroPagina, registrosPorPagina);
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("GetProductosPorAliadoSubCategoria")]
        [HttpPost]
        [ResponseType(typeof(List<Producto>))]
        public async Task<IHttpActionResult> GetProductosPorAliadoSubCategoria(JObject form)
        {
            var respuesta = new ResponseEO<Producto>();
            int IdSubCategoria = 0;
            int IdAliado = 0;
            int numeroPagina = 0;
            int registrosPorPagina = 0;
            dynamic jsonobject = form;
            try
            {
                IdSubCategoria = jsonobject.IdSubCategoria;
                IdAliado = jsonobject.IdAliado;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (IdSubCategoria == 0 || IdAliado == 0 || numeroPagina == 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los parametros no son validos");
                }

                RegistroEventos.RegistrarParametros(TipoRegistroEvento.Registro, "GetProductosPorAliadoCategoria _ IdSubCategoria: " + IdSubCategoria + ", IdAliado: " + IdAliado);

                respuesta = this.ViaBalotoService.GetProductosPorAliadoSubCategoria(IdSubCategoria, IdAliado, numeroPagina, registrosPorPagina);
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("ConsultarProductosGeneral")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<Producto>))]
        public async Task<IHttpActionResult> ConsultarProductosGeneral(JObject form)
        {
            string textoBusqueda = string.Empty;
            var respuesta = new ResponseEO<Producto>();
            int numeroPagina = 0;
            int registrosPorPagina = 0;

            dynamic jsonobject = form;
            try
            {
                textoBusqueda = jsonobject.textoBusqueda;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;
                if (string.IsNullOrEmpty(textoBusqueda) || numeroPagina <= 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los valores de los parametros no son validos");
                }

                respuesta = ViaBalotoService.ConsultarProductosGeneral(textoBusqueda, numeroPagina, registrosPorPagina);
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };

            }
            catch (ArgumentException aex)
            {
                ResponseEO<Producto> respuestaError = new ResponseEO<Producto>();
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = aex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                ResponseEO<Producto> respuestaError = new ResponseEO<Producto>();
                respuesta.Mensaje = new Message { Error = true, ErrorMessage = ex.Message };
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }



        [Route("GetProductosPorSubCategoria")]
        [HttpPost]
        [ResponseType(typeof(List<Producto>))]
        public async Task<IHttpActionResult> GetProductosPorSubCategoria(JObject form)
        {
            var respuesta = new ResponseEO<Producto>();
            int IdSubCategoria = 0;
            int numeroPagina = 0;
            int registrosPorPagina = 0;
            dynamic jsonobject = form;
            try
            {
                IdSubCategoria = jsonobject.IdSubCategoria;
                numeroPagina = jsonobject.numeroPagina;
                registrosPorPagina = jsonobject.registrosPorPagina;

                if (IdSubCategoria == 0 ||  numeroPagina == 0 || registrosPorPagina == 0)
                {
                    throw new ArgumentException("Los parametros no son validos");
                }

                RegistroEventos.RegistrarParametros(TipoRegistroEvento.Registro, "GetProductosPorAliadoCategoria _ IdSubCategoria: " + IdSubCategoria );

                respuesta = this.ViaBalotoService.GetProductosPorSubCategoria(IdSubCategoria,  numeroPagina, registrosPorPagina);
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }



        #endregion

    }
}