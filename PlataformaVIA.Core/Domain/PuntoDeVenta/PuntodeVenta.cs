namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using Core.Domain.Media;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PuntodeVenta
    {
        public int Id { get; set; }
        [Display(Name = "Código")]
        public string CodigoPuntoVenta { get; set; }
        public string CodigoCadena { get; set; }
        [Display(Name = "Cadena")]
        public string NombreCadena { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Ciudad")]
        public string CiudadPuntoVenta { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Referencia de Pago")]
        public string ReferenciaPago { get; set; }
        [Display(Name = "Rep. Ventas")]
        public string RepresentanteVentas { get; set; }
        public string RepresentanteVentasTelefono { get; set; }
        [Display(Name = "Celular Rep. Ventas")]
        public string RepresentanteVentasCelular { get; set; }
        [Display(Name = "Email Rep. Ventas")]
        public string RepresentanteVentasEmail { get; set; }
        public decimal SaldoActual { get; set; }
        public int DiasMora { get; set; }
        public DateTime FechaCorteFacturacion { get; set; }
        public decimal CupoPagoFacturas { get; set; }
        public decimal CupoPines { get; set; }
        public string PorcentajePagoFacturas { get; set; }
        public string PorcentajePines { get; set; }
        public string Estado { get; set; }
        public string Banco { get; set; }
        [Display(Name = "Razón Social")]
        public string NombreRazonSocial { get; set; }
        public string RazonSocial { get; set; }
        [Display(Name = "NIT")]
        public string NITRazonSocial { get; set; }
        
        public string Nit{ get; set; }
        public bool AsignadoAColocador { get; set; }

        //List<DetalleCarteraPuntoVenta> DetallesCarteraPuntoVenta { get; set; }
        //List<DetalleCupoPuntodeVenta> DetallesCupoPuntodeVenta { get; set; }
        //List<Notificacion> Notificaciones { get; set; }
        List<Noticia> Noticias { get; set; }
        List<ProductoComercial> ProductosComerciales { get; set; }

    }
}

