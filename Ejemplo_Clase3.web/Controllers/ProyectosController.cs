using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ejemplo_Clase3.web.Models;

namespace Ejemplo_Clase3.web.Controllers
{
    public class ProyectosController : Controller
    {
        private Ejemplo_Clase3_DB_Context db = new Ejemplo_Clase3_DB_Context();

        // GET: Proyectos
        public ActionResult Index()
        {
            var proyectos = db.Proyectos.Include(p => p.Personas).Include(p => p.Personas1);
            return View(proyectos.ToList());
        }

        // GET: Proyectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // GET: Proyectos/Create
        public ActionResult Create()
        {
            ViewBag.GerenteId = new SelectList(db.Personas, "PersonaId", "Nombre");
            ViewBag.SupervisorId = new SelectList(db.Personas, "PersonaId", "Nombre");
            return View();
        }

        // POST: Proyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProyectoId,Nombre,GerenteId,SupervisorId")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Proyectos.Add(proyectos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GerenteId = new SelectList(db.Personas, "PersonaId", "Nombre", proyectos.GerenteId);
            ViewBag.SupervisorId = new SelectList(db.Personas, "PersonaId", "Nombre", proyectos.SupervisorId);
            return View(proyectos);
        }

        // GET: Proyectos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            ViewBag.GerenteId = new SelectList(db.Personas, "PersonaId", "Nombre", proyectos.GerenteId);
            ViewBag.SupervisorId = new SelectList(db.Personas, "PersonaId", "Nombre", proyectos.SupervisorId);
            return View(proyectos);
        }

        // POST: Proyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProyectoId,Nombre,GerenteId,SupervisorId")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyectos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GerenteId = new SelectList(db.Personas, "PersonaId", "Nombre", proyectos.GerenteId);
            ViewBag.SupervisorId = new SelectList(db.Personas, "PersonaId", "Nombre", proyectos.SupervisorId);
            return View(proyectos);
        }

        // GET: Proyectos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proyectos proyectos = db.Proyectos.Find(id);
            db.Proyectos.Remove(proyectos);
            db.SaveChanges();
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
