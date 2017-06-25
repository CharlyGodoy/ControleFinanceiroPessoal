﻿using BaseModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class SubGruposController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubGrupos
        public ActionResult Index()
        {
            var subGrupos = db.SubGrupos.Where(x => x.Inativo.Equals(false)).ToList();
            return View(subGrupos);
        }
        public ActionResult IndexAtivarSubGrupo()
        {
            var subGrupos = db.SubGrupos.Where(x => x.Inativo.Equals(true)).ToList();
            return View(subGrupos);
        }

        // --- Cadastrar Novo Grupo ---
        //GET
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SubGrupo subGrupo)
        {
            if (ModelState.IsValid)
            {
                if (db.SubGrupos.FirstOrDefault(x => x.Nome.Equals(subGrupo.Nome)) == null)
                {
                    db.SubGrupos.Add(subGrupo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {                    
                        Response.Write("<script>alert('Já existe um sub grupo com o Nome: " + subGrupo.Nome + " cadastrado!');</script>");                    
                }
            }
            return View(subGrupo);
        }

        // --- Detalhes Grupo ---
        //GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubGrupo subGrupo = db.SubGrupos.Find(id);
            if (subGrupo == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(subGrupo);
        }

        // --- Editar Grupo---
        //GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubGrupo subGrupo = db.SubGrupos.Find(id);
            if (subGrupo == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            Session.Add("NomeAntigo", subGrupo.Nome);
            return View(subGrupo);
        }
        [HttpPost]
        public ActionResult Edit(SubGrupo subGrupo)
        {
            if (ModelState.IsValid)
            {
                if ((string)Session["NomeAntigo"] != subGrupo.Nome)
                {
                    if (db.SubGrupos.FirstOrDefault(x => x.Nome.Equals(subGrupo.Nome)) == null)
                    {
                        db.Entry(subGrupo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {                       
                            Response.Write("<script>alert('Já existe um SubGrupo com o Nome: " + subGrupo.Nome + " cadastrado!');</script>");
                            return View(subGrupo);                      
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(subGrupo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subGrupo);
        }

        // --- Exclusão logica ---
        //Get
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubGrupo subGrupo = db.SubGrupos.Find(id);
            if (subGrupo == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(subGrupo);
        }
        [HttpPost]
        [ActionName("Delete")]// Decide o nome da Action
        public ActionResult DeleteConfirmed(int? id)//O delete já foi confirmado
        {
            db.SubGrupos.Find(id).Inativo = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// --- Ativar SubGrupo ---
        ////Get
        //public ActionResult AtivarSubGrupo(int? id)
        //{
        //    if (id == null)
        //    {
        //        //ERRO HTTP 400
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Grupo grupo = db.Grupos.Find(id);
        //    if (grupo == null)
        //    {
        //        // ERRO HTTP 404
        //        return HttpNotFound();
        //    }
        //    return View(grupo);
        //}
        //[HttpPost]
        //[ActionName("AtivarSubGrupo")]// Decide o nome da Action
        //public ActionResult AtivarSubGrupoConfirmed(int? id)
        //{
        //    db.Grupos.Find(id).Inativo = false;
        //    db.SaveChanges();
        //    return RedirectToAction("IndexAtivarSubGrupo");
        //}
    }
}