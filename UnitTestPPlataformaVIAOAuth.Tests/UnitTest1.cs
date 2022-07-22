namespace UnitTestPPlataformaVIAOAuth.Tests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain.Cadena;
    using PlataformaVIAOAuth.WebServices.Controllers;
    using UnitTestPPlataformaVIAOAuth.Tests.Helpers;

    [TestClass]
    public class UnitTest1
    {
        #region Services
        #endregion

        //[TestMethod]
        //public void GetAllDetallesCupoPuntodeVenta1_DeberiaRetornarTodosLosDetalles()
        //{
        //    var registrosprueba = ObtenerDetallesCupoPrueba();
        //    var controlador = new CadenaController(registrosprueba);

        //    var resultado = controlador.GetAllDetallesCupoPuntodeVenta(1) as List<DetalleCupoPuntodeVenta>;

        //    Assert.AreEqual(registrosprueba.Count, resultado.Count);
        //}

        //[TestMethod]
        //public async Task GetAllDetallesCupoPuntodeVenta1Async_DeberiaRetornarTodosLosDetalles()
        //{
        //    var registrosprueba = ObtenerDetallesCupoPrueba();
        //    var controlador = new CadenaController(registrosprueba);

        //    //var payload = new 
        //    //{
        //    //  id = 1
        //    //};


        //    //var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

        //    //// Wrap our JSON inside a StringContent which then can be used by the HttpClient class
        //    //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //    //using (var httpClient = new HttpClient())
        //    //{

        //    //    // Do the actual request and await the response
        //    //    var httpResponse = await httpClient.PostAsync("http://localhost/api/path", httpContent);

        //    //    // If the response contains content we want to read it!
        //    //    if (httpResponse.Content != null)
        //    //    {
        //    //        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        //    //        // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
        //    //    }
        //    //}


        //    JObject form = new JObject(new JProperty("id", 1));

        //    var resultado = await controlador.ConsultarDetalleCupoPuntodeVentaAsync(form) as List<DetalleCupoPuntodeVenta>;

        //}

        private List<DetalleCupoPuntodeVenta> ObtenerDetallesCupoPrueba()
        {
            return new List<DetalleCupoPuntodeVenta>
            {
                new DetalleCupoPuntodeVenta {
                   CodigoPuntoVenta = "007279",
                   NombrePuntoVenta = "Supermercado",
                   CupoConsumidoBP = 965000,
                   PorcentajeDisponibleBP = 7,
                   Ciudad = "Bogotá"
                },
                new DetalleCupoPuntodeVenta {
                   CodigoPuntoVenta = "007279",
                   NombrePuntoVenta = "Droguería",
                   CupoConsumidoBP = 965000,
                   PorcentajeDisponibleBP = 7,
                   Ciudad = "Cali"
                },
            };//TODO Cambiar por objeto de BD
        }
    }
}
