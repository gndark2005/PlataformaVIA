namespace PlataformaVIA.Presentacion.Controllers
{
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using PlataformaVIA.Presentacion.ViewModels;
    using Services.Interfaces;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Web.Mvc;

    [Authorize]
    [NoCache]
    [SecurityLog]
    public class PagoController : Controller
    {
        private IPagoService _pagoService;

        #region Constructores
        public PagoController(IPagoService pagoService)
        {
            this._pagoService = pagoService;
        }
        #endregion

        // GET: Pago
        public ActionResult _DistribucionesPago(int id)
        {
            var distribucionpago = _pagoService.GetDistribucionPago(id);
            ViewBag.CodPago = id;
            return PartialView(distribucionpago);
        }

        public ActionResult _Barcodes(bool esCadena, int codigo) {
           int codUsuario = CrossController.Instance.GetUserInfoId();
            Barcode barcode = _pagoService.GetBarcodes(codUsuario, esCadena, codigo);
            ImageConverter converter = new ImageConverter();
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeViewModel barcodevm = new BarcodeViewModel();
            if (!string.IsNullOrEmpty(barcode.TextoBarcodeFiducia) && !string.IsNullOrEmpty(barcode.TextoBarcodeIGT)){
                Image imgFiducia = b.Encode(BarcodeLib.TYPE.CODE128, barcode.TextoBarcodeFiducia, Color.Black, Color.White, 300, 36);
                Image imgIGT = b.Encode(BarcodeLib.TYPE.CODE128, barcode.TextoBarcodeIGT, Color.Black, Color.White, 300, 36);
                barcodevm = new BarcodeViewModel { TextoBarcodeFiducia = barcode.TextoBarcodeFiducia, TextoBarcodeIGT = barcode.TextoBarcodeIGT, BarcodeFiducia = (byte[])converter.ConvertTo(imgFiducia, typeof(byte[])), BarcodeIGT = (byte[])converter.ConvertTo(imgIGT, typeof(byte[])) };
            }
           
            return View(barcodevm);
        }

        public ActionResult ExportExcel(int codPago)
        {
            try
            {
                var fileDownloadName = "PlataformaVIA_DistribucionPago_" + codPago + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("PlataformaVIA_DistribucionPago_" + codPago);
                //var headerCells = ws.Cells[1, 1, 1, 10];
                //var headerFont = headerCells.Style.Font;

                using (var range = ws.Cells[1, 1, 1, 5])  //Address "A1:A5"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                using (var range = ws.Cells[2, 1, 2, 5])  //Address "B1:B5"
                {
                    range.Style.Font.Bold = true;
                }

                var distribucionpago = _pagoService.GetDistribucionPago(codPago);


                //string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now
                ws.Cells["A1"].Value = "Distribución de Pago";
                ws.Cells["A2"].Value = "Punto de Venta";
                ws.Cells["B2"].Value = "Código Cadena";
                ws.Cells["C2"].Value = "Facturas y Depósitos";
                ws.Cells["D2"].Value = "Pines y Recargas";
                ws.Cells["E2"].Value = "Total";

                int filainicio = 3;
                foreach (var item in distribucionpago)
                {
                    ws.Cells[string.Format("A{0}", filainicio)].Value = item.CodigoCadena;
                    ws.Cells[string.Format("B{0}", filainicio)].Value = item.CodigoCadena;
                    ws.Cells[string.Format("C{0}", filainicio)].Value = item.FacturasDepositos;
                    ws.Cells[string.Format("D{0}", filainicio)].Value = item.PinesRecargas;
                    ws.Cells[string.Format("E{0}", filainicio)].Value = item.Total;
                    filainicio++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();
                //Response.Clear();
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment: filename=" + "PlataformaVIA_DistribucionPago_" + string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now) + ".xlsx");
                //Response.BinaryWrite(package.GetAsByteArray());
                //Response.End();


                var fileStream = new MemoryStream();
                package.SaveAs(fileStream);
                fileStream.Position = 0;

                var fsr = new FileStreamResult(fileStream, contentType)
                {
                    FileDownloadName = fileDownloadName
                };

                return fsr;


            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }
    }
}