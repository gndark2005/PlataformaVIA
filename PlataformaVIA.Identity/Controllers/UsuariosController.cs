namespace PlataformaVIA.Identity.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Identity.Models;
    using PlataformaVIA.Identity.Helpers;

    [Authorize]
    public class UsuariosController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            //var usuarios = db.Usuarios.Include(u => u.TipoUsuario);
            var usuarios = db.Usuarios;
            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Id_TipoUsuario = new SelectList(db.TipoUsuarios, "Id_TipoUsuario", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsuarioViewModel usuarioview)
        {
            if (ModelState.IsValid)
            {
                var usuario = this.ToUser(usuarioview);
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                UsuariosHelper.CrearUsuarioIdentity(usuarioview.Email,"User",usuarioview.Contrasena);
                return RedirectToAction("Index");
            }

            //ViewBag.Id_TipoUsuario = new SelectList(db.TipoUsuarios, "Id_TipoUsuario", "Nombre", 
            //    usuarioview.Id_TipoUsuario
            //    ;
            return View(usuarioview);
        }

        private Usuario ToUser(UsuarioViewModel usuarioview)
        {
            return new Usuario
            {
                Email = usuarioview.Email,
                Apellidos = usuarioview.Apellidos,
                Nombres = usuarioview.Nombres,
                Telefono = usuarioview.Telefono,
                Id_Usuario = usuarioview.Id_Usuario,
                //Id_TipoUsuario = usuarioview.Id_TipoUsuario,
                //TipoUsuario = usuarioview.TipoUsuario,
                FechaHoraUltimoIngreso = DateTime.Now
            };
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id_TipoUsuario = new SelectList(db.TipoUsuarios, "Id_TipoUsuario", "Nombre", usuario.Id_TipoUsuario);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Usuario,Nombres,Apellidos,Email,Telefono,Id_TipoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.Id_TipoUsuario = new SelectList(db.TipoUsuarios, "Id_TipoUsuario", "Nombre", usuario.Id_TipoUsuario);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
