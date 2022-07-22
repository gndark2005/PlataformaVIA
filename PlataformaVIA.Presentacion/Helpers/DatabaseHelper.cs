namespace PlataformaVIA.Presentacion.Helpers
{
    using Core.Domain;
    using Presentacion.Models;
    using System;
    using System.Threading.Tasks;

    public class DatabaseHelper
    {
        public async static Task<Response> SaveChanges(LocalDataContext db)
        {
            try
            {
                await db.SaveChangesAsync();
                return new Response { Exitoso = true, };
            }
            catch (Exception ex)
            {
                var response = new Response { Exitoso = false, };
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                {
                    response.Mensaje = "!Ya existe un registro con el mismo valor!";
                }
                else if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    response.Mensaje = "¡No se puede eliminar el registro, tiene registros relacionados!";
                }
                else
                {
                    response.Mensaje = ex.Message;
                }

                return response;
            }
        }
    }
}