namespace PlataformaVIA.Identity.Controllers
{
    using DataTables.Mvc;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize]
    public class RepresentanteLegalController : Controller
    {
        private IPuntoVentaService _puntoventaService;

        #region Constructores

        public RepresentanteLegalController(IPuntoVentaService puntoventaService)
        {
            this._puntoventaService = puntoventaService;
        }
        #endregion

        // POST api/Account/Register
        public async Task<ActionResult> ConsultarPuntosdeVenta(CriterioBusqueda filtro)
        {
            //TODO Lógica de negocio para Obtener Puntos de venta de acuerdo a un criterio de búsqueda y un NIT.
            //TODO Enviar paginado a los servicios y a los repositorios para consulta en la base de datos

            // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
            //var myObjectResponse = GetObjectFromDb(id);

            //if (myObjectResponse == null)
            //    return Request.CreateResponse(HttpStatusCode.NotFound);

            //return View(new List<PuntodeVenta> { new PuntodeVenta { CiudadPuntoVenta  = "Bogotá D.C.",CodigoCadena = "123456", CodigoPuntoVenta = "13245678", Nombre = "PDV 1" },
            //new PuntodeVenta { CiudadPuntoVenta  = "Pasto",CodigoCadena = "456789", CodigoPuntoVenta = "010145", Nombre = "PDV 2" }, });
            return View();
            //TODO Cambiar por objeto de BD
        }



        public ActionResult GetFiltroBasico([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            // Aplicamos filtros de búsqueda
            var valorfiltro = requestModel.Search.Value.Trim();

            //IQueryable<PuntodeVenta> query = DbContext.Assets;
            //var totalCount = query.Count();

            #region Ordenado
            // Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) +
                  (column.SortDirection ==
                  Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            //query = query.OrderBy(orderByString ==
            //string.Empty ? "BarCode asc" : orderByString);

            #endregion Ordenado

            // Paginación
            // query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var criterioBusqueda = new CriterioBusqueda { Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start, TamanoPagina = requestModel.Length } };


            var lstpuntosventa = _puntoventaService.GetPuntoVentaXRazonSocial(criterioBusqueda);



            //var data = lstpuntosventa.Select(puntoventa => new
            //{
            //    PDV = puntoventa.CodigoPuntoVenta,
            //    Cadena = puntoventa.CodigoCadena,
            //    puntoventa.Nombre,
            //    FechaCorte = puntoventa.FechaCorteFacturacion,
            //    puntoventa.SaldoActual,
            //    CupoFacturas = puntoventa.CupoPagoFacturas,
            //    CupoRecPines = puntoventa.CupoPines,
            //    EstadoPDV = puntoventa.Estado
            //}).ToList();

            //TODO Setear total registros
            return Json(new DataTablesResponse
            (requestModel.Draw, lstpuntosventa,lstpuntosventa.Count(), criterioBusqueda.Paginacion.TotalRegistros),
                        JsonRequestBehavior.AllowGet);
        }


        //// PUT api/Colocador/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutRepresentanteLegal(int id, RazonSocial razonsocial)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != razonsocial.IdRazonSocial)
        //    {
        //        return BadRequest();
        //    }

        //    //TODO Descomentarear lógica de la edición. Por ahora al representante legal solo se le debe editar  email y celular
        //    //db.Entry(usuario).State = EntityState.Modified;

        //    //try
        //    //{
        //    //    await db.SaveChangesAsync();
        //    //}
        //    //catch (DbUpdateConcurrencyException)
        //    //{
        //    //    if (!UsuarioExists(id))
        //    //    {
        //    //        return NotFound();
        //    //    }
        //    //    else
        //    //    {
        //    //        throw;
        //    //    }
        //    //}
        //    return StatusCode(HttpStatusCode.OK);
        //}

        //// POST api/Account/Register
        //[Route("ConsultarSaldoFacturacion")]
        //[HttpPost]
        //[ResponseType(typeof(IEnumerable<SaldoFacturacion>))]
        //public async Task<IHttpActionResult> ConsultarSaldoFacturacion(JObject form)
        //{
        //    var nit = string.Empty;
        //    dynamic jsonObject = form;

        //    try
        //    {
        //        nit = jsonObject.Nit.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Falta un parámetro");
        //    }

        //    //TODO Lógica de negocio para Obtener saldo de facturación de acuerdo al NIT ingresado

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<SaldoFacturacion> { new SaldoFacturacion { ConceptoPago = ParticipacionLineaNegocioEnum.Fiducia, FechaLimitePago = DateTime.Now.AddDays(911), SaldoTotalPagar = 75842578 },
        //                                            new SaldoFacturacion { ConceptoPago = ParticipacionLineaNegocioEnum.IGT, FechaLimitePago = DateTime.Now.AddDays(910), SaldoTotalPagar = 54787575 }
        //    });//TODO Cambiar por objeto de BD

        //}

        //// POST api/Account/Register
        //[Route("ConsultarNotificaciones")]
        //[HttpPost]
        //[ResponseType(typeof(IEnumerable<Notificacion>))]
        //public async Task<IHttpActionResult> ConsultarNotificaciones(JObject form)
        //{
        //    var Nit = string.Empty;
        //    dynamic jsonObject = form;

        //    try
        //    {
        //        Nit = jsonObject.Nit.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Falta un parámetro");
        //    }

        //    //TODO Lógica de negocio para Obtener notificaciones de acuerdo al NIT ingresado

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<Notificacion> { new Notificacion { Asunto = "Notificación 1" , Detalle = "Detalle de la notificación 1 ", Fecha = DateTime.Now },
        //                                            new Notificacion { Asunto = "Notificación 2" , Detalle = "Detalle de la notificación 2 ", Fecha = DateTime.Now }
        //    });//TODO Cambiar por objeto de BD

        //}

        //// POST api/Account/Register
        //[Route("ConsultarNoticias")]
        //[HttpGet]
        //[ResponseType(typeof(IEnumerable<Noticia>))]
        //public async Task<IHttpActionResult> ConsultarNoticias()
        //{


        //    //TODO Lógica de negocio para Obtener noticias

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<Noticia> { new Noticia { FechaPublicacion  = DateTime.Now, RutaImagen =  "http:ejemplo/ing/imagen.jpg"},
        //                                   new Noticia { FechaPublicacion  = DateTime.Now, RutaImagen =  "http:ejemplo/ing/imagen2.jpg" } });
        //    //TODO Cambiar por objeto de BD

        //}


        //// POST api/Account/Register
        //[Route("ConsultarCadenas")]
        //[HttpPost]
        //[ResponseType(typeof(IEnumerable<Cadena>))]
        //public async Task<IHttpActionResult> ConsultarCadenas(CriterioBusqueda filtro)
        //{
        //    //TODO Lógica de negocio para Obtener las cadenas de acuerdo a un criterio de búsqueda y un NIT.
        //    //TODO Enviar paginado a los servicios y a los repositorios para consulta en la base de datos

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<Cadena> { new Cadena { Nombre = "Cadena 1 ", CodigoCadena = "132", DiasMora = 1, ReferenciaPago = "123897#-423200"},
        //                                 new Cadena { Nombre = "Cadena 45 ", CodigoCadena = "78592", DiasMora = 3, ReferenciaPago = "878787#-423200"} });
        //    //TODO Cambiar por objeto de BD
        //}

        //// GET api/Cadena/ConsultarPagosXCicloFacturacion
        ///// <summary>
        ///// </summary>
        ///// <param name="form"></param>
        ///// <returns></returns>
        //[Route("ConsultarPagosXCicloFacturacion")]
        //[HttpGet]
        //[ResponseType(typeof(IEnumerable<PagoRazonSocial>))]
        //public async Task<IHttpActionResult> ConsultarPagosXCicloFacturacion(long id, int idciclo)
        //{
        //    //TODO Lógica de negocio para Obtener facturación de la cadena por determinado ciclo de facturación

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<PagoRazonSocial> {
        //                                            new PagoRazonSocial { Fecha = DateTime.Now.AddHours(-25),
        //                                                                  ValorPago = 157550,
        //                                                                  ReferenciaPago = "987989-$5450",
        //                                                                  FechaAplicacionPago = DateTime.Now,
        //                                                                   BancoTransaccion = new Banco { CodBanco = 1,
        //                                                                                                 Nit = "45648",
        //                                                                                                 Nombre = "Occidente"
        //                                                                                                },
        //                                                                  Distribuciones =  new List<DistribucionPagoRazonSocial> { new DistribucionPagoRazonSocial { CodigoCadena = "456798",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             },
        //                                                                                                                            new DistribucionPagoRazonSocial { CodigoCadena = "024578",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             }
        //                                                                                                                           }
        //                                                                },
        //                                            new PagoRazonSocial {  Fecha = DateTime.Now.AddHours(-25),
        //                                                                  ValorPago = 157550,
        //                                                                  ReferenciaPago = "987989-$5450",
        //                                                                  FechaAplicacionPago = DateTime.Now,
        //                                                                  BancoTransaccion = new Banco { CodBanco = 1,
        //                                                                                                 Nit = "45648",
        //                                                                                                 Nombre = "Banco Bogotá"
        //                                                                                                },
        //                                                                  Distribuciones =  new List<DistribucionPagoRazonSocial> { new DistribucionPagoRazonSocial { CodigoCadena = "124578",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             },
        //                                                                                                                            new DistribucionPagoRazonSocial { CodigoCadena = "154578",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             }
        //                                                                                                                           }
        //                                                                }

        //    });//TODO Cambiar por objeto de BD

        //}

        //// GET api/Cadena/ConsultarPagosXCicloFacturacion
        ///// <summary>
        ///// </summary>
        ///// <param name="form"></param>
        ///// <returns></returns>
        //[Route("ConsultarAjustesXCicloFacturacion")]
        //[HttpGet]
        //[ResponseType(typeof(IEnumerable<AjusteRazonSocial>))]
        //public async Task<IHttpActionResult> ConsultarAjustesXCicloFacturacion(long id, int idciclo)
        //{
        //    //TODO Lógica de negocio para Obtener facturación de la cadena por determinado ciclo de facturación

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<AjusteRazonSocial> {
        //                                            new AjusteRazonSocial { Fecha = DateTime.Now.AddHours(-10),
        //                                                                    Valor = 157550,
        //                                                                    Descripcion = "DISTRIBBUCIÓN DE SALDO",
        //                                                                   Distribuciones =  new List<DistribucionAjusteRazonSocial> { new DistribucionAjusteRazonSocial { CodigoCadena = "456798",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             },
        //                                                                                                                            new DistribucionAjusteRazonSocial { CodigoCadena = "024578",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             }
        //                                                                                                                           }
        //                                                                },
        //                                            new AjusteRazonSocial { Fecha = DateTime.Now.AddHours(-5),
        //                                                                    Valor = 157550,
        //                                                                    Descripcion = "RECLAMACIÓN DE USUARIO FINAL",
        //                                                                    Distribuciones =  new List<DistribucionAjusteRazonSocial> { new DistribucionAjusteRazonSocial { CodigoCadena = "456798",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             },
        //                                                                                                                            new DistribucionAjusteRazonSocial { CodigoCadena = "024578",
        //                                                                                                                                                              CodigoPuntoVenta = "545654",
        //                                                                                                                                                              ValorFacturasDepositos = 31231231,
        //                                                                                                                                                              ValorPinesRecargas = 75675,
        //                                                                                                                                                              ValorTotal = 89789699
        //                                                                                                                                                             }
        //                                                                                                                           }
        //                                                                }

        //    });//TODO Cambiar por objeto de BD

        //}

        //// POST api/Account/Register
        //[Route("ConsultarColocadores")]
        //[HttpPost]
        //[ResponseType(typeof(IEnumerable<Colocador>))]
        //public async Task<IHttpActionResult> ConsultarColocadores(CriterioBusqueda filtro)
        //{
        //    //TODO Lógica de negocio para Obtener Puntos de venta de acuerdo a un criterio de búsqueda y un NIT.
        //    //TODO Enviar paginado a los servicios y a los repositorios para consulta en la base de datos

        //    // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
        //    //var myObjectResponse = GetObjectFromDb(id);

        //    //if (myObjectResponse == null)
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Ok(new List<Colocador> { new Colocador { CiudadVendedor = "Bogotá D.C.", NombreCompleto = "Mía Pineda ", NumeroColocador = "67724578", NumeroIdentificacion = "123457897", UsuarioAsignado = "SI"  },
        //                                       new Colocador { CiudadVendedor = "Bogotá D.C.", NombreCompleto = "Mía Pineda ", NumeroColocador = "67724578", NumeroIdentificacion = "123457897", UsuarioAsignado = "SI"  }
        //    });
        //    //TODO Cambiar por objeto de BD
        //}
    }
}
