namespace PlataformaVIA.Presentacion.Controllers
{
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using System.Web.Mvc;

    [Authorize]
    [PasswordExpiredAuthorize]
    [NoCache]
    [SecurityLog]
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        [Authorize (Roles = "Funcionario, FuncionarioExterno")]
        public ActionResult Index()
        {
            return View();
        }
    }
}