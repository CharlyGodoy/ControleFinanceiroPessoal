using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class GruposController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // --- Grupo ---
        // GET: Grupos
        public ActionResult Index()
        {
            var grupos = db.Grupos.Where(x => x.Inativo == false).ToList();
            return View(grupos);
        }
        public ActionResult IndexAtivarGrupo()
        {
            var grupos = db.Grupos.Where(x => x.Inativo == true).ToList();
            return View(grupos);
        }

        // --- Cadastrar Novo Grupo ---
        //GET
        public ActionResult CreateGrupo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGrupo(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                if (db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)) == null)
                {
                    db.Grupos.Add(grupo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {    
                        Response.Write("<script>alert('Já existe um grupo com o Nome: " + grupo.Nome + " cadastrado!');</script>"); 
                }
            }
            return View(grupo);
        }

        // --- Detalhes Grupo ---
        //GET
        public ActionResult DetailsGrupo(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(grupo);
        }

        // --- Editar Grupo---
        //GET
        public ActionResult EditGrupo(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            Session.Add("NomeAntigo", grupo.Nome);
            return View(grupo);
        }
        [HttpPost]
        public ActionResult EditGrupo(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                if ((string)Session["NomeAntigo"] != grupo.Nome)
                {
                    if (db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)) == null)
                    {
                        db.Entry(grupo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                            Response.Write("<script>alert('Já existe um grupo com o Nome: " + grupo.Nome + " cadastrado!');</script>");
                            return View(grupo);      
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupo);
        }

        // --- Exclusão logica ---
        //Get
        public ActionResult DeleteGrupo(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(grupo);
        }
        [HttpPost]
        [ActionName("DeleteGrupo")]// Decide o nome da Action
        public ActionResult DeleteGrupoConfirmed(int? id)//O delete já foi confirmado
        {
            db.Grupos.Find(id).Inativo = true;
            string id_Pai = Convert.ToString(id);
            try
            {
                foreach (SubGrupo item in db.SubGrupos.Where(x => x.GrupoID == id).ToList())
                {
                    db.SubGrupos.Find(item.SubGrupoID).Inativo = true;
                }
            }
            finally
            {
                db.SaveChanges();
            }            
            return RedirectToAction("Index");
        }

        // --- Ativar Grupo ---
        //Get
        public ActionResult AtivarGrupo(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(grupo);
        }
        [HttpPost]
        [ActionName("AtivarGrupo")]// Decide o nome da Action
        public ActionResult AtivarGrupoConfirmed(int? id)
        {
            db.Grupos.Find(id).Inativo = false;
            db.SaveChanges();
            return RedirectToAction("IndexAtivarGrupo");
        }
    }
}