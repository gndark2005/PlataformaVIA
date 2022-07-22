namespace PlataformaVIA.Presentacion.Controllers
{
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using Services.Interfaces;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Web.Mvc;

    [Authorize]
    [NoCache]
    [SecurityLog]
    public class AjusteController : Controller
    {
        private IAjusteService _ajusteService;

        #region Constructores
        public AjusteController(IAjusteService ajusteService)
        {
            this._ajusteService = ajusteService;
        }
        #endregion

        // GET: Pago
        public ActionResult _DistribucionesAjuste(int id)
        {
            try
            {
                var distribucionajuste = _ajusteService.GetDistribucionAjuste(id);
                ViewBag.CodAjuste = id;
                return PartialView(distribucionajuste);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult ExportExcel(int codAjuste)
        {
            try
            {
                var fileDownloadName = "PlataformaVIA_DistribucionAjuste_" + codAjuste + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("PlataformaVIA_DistribucionPago_" + codAjuste);
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

                var distribucionajuste = _ajusteService.GetDistribucionAjuste(codAjuste);


                //string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now
                ws.Cells["A1"].Value = "Distribución de Ajuste";
                ws.Cells["A2"].Value = "Punto de Venta";
                ws.Cells["B2"].Value = "Código Cadena";
                ws.Cells["C2"].Value = "Facturas y Depósitos";
                ws.Cells["D2"].Value = "Pines y Recargas";
                ws.Cells["E2"].Value = "Total";

                int filainicio = 3;
                foreach (var item in distribucionajuste)
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