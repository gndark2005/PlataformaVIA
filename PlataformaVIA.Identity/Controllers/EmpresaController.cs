using PlataformaVIA.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Identity.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult RegistrarCuentaMaestra()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarCuentaMaestra(RepresentanteLegalRegistro model)
        {
            return View(model);
        }
    }
}