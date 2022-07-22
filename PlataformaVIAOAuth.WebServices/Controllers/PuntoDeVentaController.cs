namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using PlataformaVIAOAuth.WebServices.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    [RoutePrefix("api/PuntoVenta")]
    public class PuntoDeVentaController : ApiController
    {

        private IPuntoVentaService puntoventaService;

        #region Constructores
        public PuntoDeVentaController(IPuntoVentaRepository puntoventaRepository)
        {
            this.puntoventaService = new PuntoVentaService(puntoventaRepository);
        }
        #endregion


        /// <summary>
        /// Puntos de venta - Detalle Punto de venta - Informacion General
        /// </summary>
        /// <param name="id">Id del punto de venta</param>
        /// <returns></returns>
        [Route("ConsultarInformacionGeneral")]
        [HttpGet]
        [ResponseType(typeof(PuntodeVenta))]
        public async Task<IHttpActionResult> ConsultarInformacionGeneral(int id)
        {
            return Ok(puntoventaService.GetDetallePuntoVenta(id));
        }


        /// <summary>
        /// Puntos de venta - Detalle Punto de venta - Presupuestos por Linea de Negocio
        /// </summary>
        /// <param name="id">Id del punto de venta</param>
        /// <returns></returns>
        [Route("ConsultarEstadoCuentaPorLineaDeNegocio")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<PresupuestoXLineaDeNegocio>))]
        public async Task<IHttpActionResult> ConsultarEstadoCuentaPorLineaDeNegocio(int id)
        {
            return Ok(puntoventaService.GetPresupuestoLineaNegocio(id));
        }


        /// <summary>
        /// Puntos de venta - Detalle Punto de venta - Presupuestos por linea de negocio
        /// </summary>
        /// <param name="id">Id del punto de venta</param>
        /// <returns></returns>
        [Route("ConsultarPrespuestosPorLineaDeNegocio")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EstadoCuentaXLineaDeNegocio>))]
        public async Task<IHttpActionResult> ConsultarPrespuestosPorLineaDeNegocio(int id)
        {
            return Ok(puntoventaService.GetPresupuestoLineaNegocio(id));
        }

        // GET api/PuntoVenta/ConsultarDetalleEstadoCuenta
        [Route("ConsultarDetalleEstadoCuenta")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EstadoDeCuentaGeneral>))]
        public async Task<IHttpActionResult> ConsultarDetalleEstadoCuenta(ResponseEO<EstadoDeCuentaGeneral> response) //long id, int ciclo)
        {
            try
            {
                return Ok(puntoventaService.GetDetalleEstadoDeCuenta(response));
            }
            catch (Exception ex)
            {
                return BadRequest("Falta un parámetro");
            }

        }

        // POST api/PuntoVenta/ConsultarProductos
        [Route("ConsultarProductos")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ProductoComercial>))]
        public async Task<IHttpActionResult> ConsultarProductos(CriterioBusqueda filtro)
        {
            return Ok(puntoventaService.GetProductosComerciales(filtro));
        }

        // POST api/PuntoVenta/ConsultarTransacciones
        [Route("ConsultarTransacciones")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<Transaccion>))]
        public async Task<IHttpActionResult> ConsultarTransacciones(CriterioBusquedaTransaccionesPDV filtro)
        {
            return Ok(puntoventaService.GetTransacciones(filtro));
        }

        /// <summary>
        /// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ciclo"></param>
        /// <returns></returns>
        // GET api/PuntoVenta/ConsultarResumenPagoTotal
        //[Route("ConsultarResumenPagoTotal")]
        //[HttpGet]
        //[ResponseType(typeof(IEnumerable<ResumenPago>))]
        //public async Task<IHttpActionResult> ConsultarResumenPagoTotal(JObject form) //long id, int ciclo
        //{
        //    try
        //    {
        //        var request = JsonConvert.DeserializeObject<ResumenPago>(form.ToString());

        //        return Ok(puntoventaService.GetResumenPagoTotal(request. id, ciclo));

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}


        /// <summary>
        /// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        /// </summary>
        /// <param name="id">ID del puntod e venta</param>
        /// <param name="ciclo"></param>
        /// <returns></returns>
        // GET api/Account/Register
        [Route("ConsultarResumenCuadreSemanalPagoTotal")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResumenCuadreSemanal>))]
        public async Task<IHttpActionResult> ConsultarResumenCuadreSemanalPagoTotal(ResumenCuadreSemanal request)
        {
            try
            {
                return Ok(this.puntoventaService.GetResumenCuadreSemanalPagoTotal(request));
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
        }

        // <summary>
        /// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        /// </summary>
        /// <param name="id">ID del puntod e venta</param>
        /// <param name="ciclo"></param>
        /// <returns></returns>
        // GET api/Account/Register
        [Route("ConsultarResumenVentasAjustesSemanaActualPagoTotal")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResumenCuadreSemanal>))]
        public async Task<IHttpActionResult> ConsultarResumenVentasAjustesSemanaActualPagoTotal(ResumenCuadreSemanal request)
        {
            try
            {
                return Ok(this.puntoventaService.GetResumenVentasAjustesSemanaActualPagoTotal(request));
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
            //return Ok(puntoventaService.GetResumenVentasAjustesSemanaActualPagoTotal(id, ciclo));
        }

        // <summary>
        /// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        /// </summary>
        /// <param name="id">ID del puntod e venta</param>
        /// <param name="ciclo"></param>
        /// <returns></returns>
        // GET api/Account/Register
        [Route("ConsultarResumenPagosRetirosSemanaActualPagoTotal")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResumenPagosRetirosSemanaActual>))]
        public async Task<IHttpActionResult> ConsultarResumenPagosRetirosSemanaActualPagoTotal(CicloFacturacion request)
        {
            try
            {
                return Ok(this.puntoventaService.GetResumenPagosRetirosSemanaActualPagoTotal(request));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        // <summary>
        /// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        /// </summary>
        /// <param name="id">ID del puntod e venta</param>
        /// <param name="ciclo"></param>
        /// <returns></returns>
        // GET api/Account/Register

        [Route("ConsultarResumenAjustesCarteraPagoTotal")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResumenAjustesCartera>))]
        public async Task<IHttpActionResult> ConsultarResumenAjustesCarteraPagoTotal(ResumenAjustesCartera request)
        {
            try
            {
                return Ok(this.puntoventaService.GetResumenAjustesCarteraPagoTotal(request));
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
            //return Ok(puntoventaService.GetResumenAjustesCarteraPagoTotal(id, ciclo));//TODO Cambiar por objeto de BD
        }


        /// <summary>
        /// Hace parte de la información que se muestra al oprimir el botón Detalle pago mínimo a la fecha
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ciclo"></param>
        /// <returns></returns>
        // GET api/Account/Register
        [Route("ConsultarResumenPagoMinimo")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResumenPago>))]
        public async Task<IHttpActionResult> ConsultarResumenPagoMinimo(ResumenPago request)
        {
            try
            {
                return Ok(this.puntoventaService.GetResumenPagoMinimo(request));
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }


        ///// <summary>
        ///// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        ///// </summary>
        ///// <param name="id">ID del puntod e venta</param>
        ///// <param name="ciclo"></param>
        ///// <returns></returns>
        //// GET api/Account/Register
        //[Route("ConsultarResumenCuadreSemanalPagoMinimo")]
        //[HttpGet]
        //[ResponseType(typeof(IEnumerable<ResumenSemanal>))]
        //public async Task<IHttpActionResult> ConsultarResumenCuadreSemanalPagoMinimo(long id, int ciclo)
        //{
        //    //TODO Lógica de negocio para Obtener saldo de facturación de acuerdo al NIT ingresado

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<ResumenSemanal> { new ResumenSemanal { Valor = 154500, LineaDeNegocio = LineadeNegocioEnum.Instalaciones,Fecha = DateTime.Now,Tipo = "CUADRE SEMANAL"  },
        //                                         new ResumenSemanal { Valor = 78500, LineaDeNegocio = LineadeNegocioEnum.Retiros,Fecha = DateTime.Now,Tipo = "CUADRE SEMANAL"  }
        //    });//TODO Cambiar por objeto de BD

        //}

        //// <summary>
        ///// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        ///// </summary>
        ///// <param name="id">ID del puntod e venta</param>
        ///// <param name="ciclo"></param>
        ///// <returns></returns>
        //// GET api/Account/Register
        //[Route("ConsultarResumenVentasAjustesSemanaActualPagoTotal")]
        //[HttpGet]
        //[ResponseType(typeof(IEnumerable<ResumenSemanal>))]
        //public async Task<IHttpActionResult> ConsultarResumenVentasAjustesSemanaActualPagoTotal(long id, int ciclo)
        //{
        //    //TODO Lógica de negocio para Obtener saldo de facturación de acuerdo al NIT ingresado

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<ResumenSemanal> { new ResumenSemanal { Valor = 154500, LineaDeNegocio = LineadeNegocioEnum.Instalaciones,Fecha = DateTime.Now,Tipo = "VENTA"  },
        //                                         new ResumenSemanal { Valor = 78500, LineaDeNegocio = LineadeNegocioEnum.Retiros,Fecha = DateTime.Now,Tipo = "PREMIOS"  }
        //    });//TODO Cambiar por objeto de BD

        //}

        ////// <summary>
        /////// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        /////// </summary>
        /////// <param name="id">ID del puntod e venta</param>
        /////// <param name="ciclo"></param>
        /////// <returns></returns>
        ////// GET api/Account/Register
        ////[Route("ConsultarResumenPagosRetirosSemanaActualPagoTotal")]
        ////[HttpGet]
        ////[ResponseType(typeof(IEnumerable<ResumenPagosRetirosSemanaActual>))]
        ////public async Task<IHttpActionResult> ConsultarResumenPagosRetirosSemanaActualPagoTotal(long id, int ciclo)
        ////{
        ////    //TODO Lógica de negocio para Obtener saldo de facturación de acuerdo al NIT ingresado

        ////    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        ////    //var myObjectResponse = GetObjectFromDb(id);

        ////    //if (myObjectResponse == null)
        ////    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        ////    return Ok(new List<ResumenPagosRetirosSemanaActual> { new ResumenPagosRetirosSemanaActual { Valor = 45879, CuentaBancaria= "IGT Banco Bogotá", FechaPago = DateTime.Now, Referencia = "00045834", TipoIdentificacion = "Pago"},
        ////                                                          new ResumenPagosRetirosSemanaActual { Valor = 45879, CuentaBancaria= "IGT Banco Bogotá", FechaPago = DateTime.Now.AddDays(-4), Referencia = "00045834", TipoIdentificacion = "Retiro"  }
        ////    });//TODO Cambiar por objeto de BD

        ////}

        ////// <summary>
        /////// Hace parte de la información que se muestra al oprimir el botón Detalle Pago Total
        /////// </summary>
        /////// <param name="id">ID del puntod e venta</param>
        /////// <param name="ciclo"></param>
        /////// <returns></returns>
        ////// GET api/Account/Register

        ////[Route("ConsultarResumenAjustesCarteraPagoTotal")]
        ////[HttpGet]
        ////[ResponseType(typeof(IEnumerable<ResumenAjustesCartera>))]
        ////public async Task<IHttpActionResult> ConsultarResumenAjustesCarteraPagoTotal(long id, int ciclo)
        ////{
        ////    //TODO Lógica de negocio para Obtener saldo de facturación de acuerdo al NIT ingresado

        ////    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        ////    //var myObjectResponse = GetObjectFromDb(id);

        ////    //if (myObjectResponse == null)
        ////    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        ////    return Ok(new List<ResumenAjustesCartera> { new ResumenAjustesCartera { Valor = 45879, CategoriaAjuste = "Castigo", FechaAplicacion = DateTime.Now.AddDays(3), Observacion = "Pruebas"},
        ////                                                new ResumenAjustesCartera { Valor = 1504, CategoriaAjuste = "Castigo 2", FechaAplicacion = DateTime.Now.AddDays(3), Observacion = "Pruebas"}
        ////    });//TODO Cambiar por objeto de BD

        ////}


        /// <summary>
        /// Hace parte de la información que se muestra al oprimir el botón Detalle pago mínimo a la fecha
        /// </summary>
        /// <param name="id">ID del punto de venta</param>
        /// <param name="ciclo"></param>
        /// <returns></returns>
        // GET api/Account/Register
        [Route("ConsultarTirillaCuadreSemanal")]
        [HttpGet]
        [ResponseType(typeof(TirillaCuadreSemanal))]
        public async Task<IHttpActionResult> ConsultarTirillaCuadreSemanal(CriterioBusquedaCicloFacturacion request)
        {
            try
            {
                return Ok(this.puntoventaService.GetTirillaCuadreSemanal(request));
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
            //return Ok(puntoventaService.GetTirillaCuadreSemanal(id,ciclo));
        }

        // POST api/Account/ConsultarVentas
        [Route("ConsultarVentas")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<Ventas>))]
        public async Task<IHttpActionResult> ConsultarVentas(CriterioBusquedaFechas request)//CriterioBusquedaFechas filtro)
        {
            try
            {
                ResponseEO<Ventas> response = new ResponseEO<Ventas> { FiltrosFechas = request };

                return Ok(this.puntoventaService.GetMisVentas(response));
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
        }

        // GET api/PuntoVenta/ConsultarPagosXCicloFacturacion
        /// <summary>
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarPagosXCicloFacturacion")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Pago>))]
        public async Task<IHttpActionResult> ConsultarPagosXCicloFacturacion(CicloFacturacion request)
        {
            try
            {
                return Ok(this.puntoventaService.GetPagosXCicloFacturacion(request));
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
        }

        // GET api/Cadena/ConsultarPagosXCicloFacturacion
        /// <summary>
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarAjustesXCicloFacturacion")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Ajuste>))]
        public async Task<IHttpActionResult> ConsultarAjustesXCicloFacturacion(JObject form)
        {
            try
            {
                var request = JsonConvert.DeserializeObject<ResponseEO<Ajuste>>(form.ToString());

                return Ok(puntoventaService.GetAjustesXCicloFacturacion(request));
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
            //return Ok(puntoventaService.GetAjustesXCicloFacturacion(id, idciclo));
        }

        /// <summary>
        /// Método encargado de retornar información completa de la cadena (Información General,  Estado de Cartera por LDN, y Estado Parámetro de Ventas)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarPoliticaPago")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultarPoliticaPagoXPuntoVenta(JObject form)
        {
            //TODO Generar imagen
            using (MemoryStream ms = new MemoryStream())
            {
                Image img = Image.FromFile(@"E:\Politicas\PoliticaPagoSemanal.jpg");
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

                return result;
            }

        }

    }
}
