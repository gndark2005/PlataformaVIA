namespace UnitTestPPlataformaVIAOAuth.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PlataformaVIA.Core.Domain.RepresentanteLegal;
    using PlataformaVIA.Data;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIAOAuth.WebServices.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    [TestClass]
    public class TestColocadorController
    {

        [TestMethod]
        public void OtorgarAcceso_DeberiaRetornarMismoObjetoAcceso()
        {
            //var controlador = new ColocadorController(new ColocadorService(new ColocadorRepository(new PlataformaVIA.Data.DbContext(new DbConnectionFactory()))));

            //var item = ObtenerAccesoDemo();

            //var result = controlador.OtorgarAcceso(item) as CreatedAtRouteNegotiatedContentResult<AccesoColocador>;

            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.RouteName, "DefaultApi");
            //Assert.AreEqual(result.RouteValues["id"], result.Content.IdAccesoColocador);
            //Assert.AreEqual(result.Content.CodColocador, item.CodColocador);
            //Assert.AreEqual(result.Content.CodPuntosVenta, item.CodPuntosVenta);

        }

        AccesoColocador ObtenerAccesoDemo()
        {
            return new AccesoColocador
            {
                CodColocador = 1,
                CodPuntosVenta = new List<PuntosVentaAcceso> { new PuntosVentaAcceso { IdPuntoVenta = 2548, Seleccionada = true },
                                                                                                          new PuntosVentaAcceso { IdPuntoVenta = 32568, Seleccionada = true },
                                                                                                          new PuntosVentaAcceso { IdPuntoVenta = 4578, Seleccionada = true }
                                                                                              },
                CodRazonSocial = 132465
            };
        }
    }
}
