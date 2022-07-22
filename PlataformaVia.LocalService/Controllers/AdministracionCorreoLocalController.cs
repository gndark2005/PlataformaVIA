using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PlataformaVia.LocalService.Models;

namespace PlataformaVia.LocalService.Controllers
{
    public class AdministracionCorreoLocalController : ApiController
    {
        private PortalViaEntities db = new PortalViaEntities();

        // GET: api/AdministracionCorreoLocals
        public IQueryable<AdministracionCorreoLocal> GetAdministracionCorreoLocals()
        {
            return db.AdministracionCorreoLocals;
        }

        // GET: api/AdministracionCorreoLocals/5
        [ResponseType(typeof(AdministracionCorreoLocal))]
        public async Task<IHttpActionResult> GetAdministracionCorreoLocal(long id)
        {
            AdministracionCorreoLocal administracionCorreoLocal = await db.AdministracionCorreoLocals.FindAsync(id);
            if (administracionCorreoLocal == null)
            {
                return NotFound();
            }

            return Ok(administracionCorreoLocal);
        }

        // PUT: api/AdministracionCorreoLocals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAdministracionCorreoLocal(long id, AdministracionCorreoLocal administracionCorreoLocal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administracionCorreoLocal.ID_CORREO)
            {
                return BadRequest();
            }

            db.Entry(administracionCorreoLocal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministracionCorreoLocalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AdministracionCorreoLocals
        [ResponseType(typeof(AdministracionCorreoLocal))]
        public async Task<IHttpActionResult> PostAdministracionCorreoLocal(AdministracionCorreoLocal administracionCorreoLocal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdministracionCorreoLocals.Add(administracionCorreoLocal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = administracionCorreoLocal.ID_CORREO }, administracionCorreoLocal);
        }

        // DELETE: api/AdministracionCorreoLocals/5
        [ResponseType(typeof(AdministracionCorreoLocal))]
        public async Task<IHttpActionResult> DeleteAdministracionCorreoLocal(long id)
        {
            AdministracionCorreoLocal administracionCorreoLocal = await db.AdministracionCorreoLocals.FindAsync(id);
            if (administracionCorreoLocal == null) 
            {
                return NotFound();
            }

            db.AdministracionCorreoLocals.Remove(administracionCorreoLocal);
            await db.SaveChangesAsync();

            return Ok(administracionCorreoLocal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministracionCorreoLocalExists(long id)
        {
            return db.AdministracionCorreoLocals.Count(e => e.ID_CORREO == id) > 0;
        }
    }
}