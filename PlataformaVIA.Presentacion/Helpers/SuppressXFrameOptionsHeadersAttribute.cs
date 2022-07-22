namespace PlataformaVIA.Presentacion.Helpers
{
    using System;
    using System.Web.Helpers;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class SuppressXFrameOptionsHeadersAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
        }
    }
}