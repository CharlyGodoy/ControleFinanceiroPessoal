using BaseModel;
using System;
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
            var grupos = db.Grupos.Where(x => x.Grupo_ID_Pai.Equals("-1") && (x.Inativo == false)).ToList();
            return View(grupos);
        }
        public ActionResult IndexAtivarGrupo()
        {
            var grupos = db.Grupos.Where(x => x.Grupo_ID_Pai.Equals("-1") && (x.Inativo == true)).ToList();
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
                    grupo.Grupo_ID_Pai = "-1";
                    db.Grupos.Add(grupo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (Convert.ToInt32(db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)).Grupo_ID_Pai) != -1)
                    {
                        grupo.Grupo_ID_Pai = "-1";
                        db.Grupos.Add(grupo);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.Write("<script>alert('Já existe um grupo com o Nome: " + grupo.Nome + " cadastrado!');</script>");
                    }
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
            Session.Add("Grupo_ID_Pai", grupo.Grupo_ID_Pai);
            return View(grupo);
        }
        [HttpPost]
        public ActionResult EditGrupo(Grupo grupo)
        {
            grupo.Grupo_ID_Pai = (string)Session["Grupo_ID_Pai"];
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
                        if (db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)).Grupo_ID_Pai != "-1")
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
            foreach (Grupo item in db.Grupos.Where(x => x.Grupo_ID_Pai.Equals(id_Pai)).ToList())
            {
                db.Grupos.Find(item.GrupoID).Inativo = true;
            }
            db.SaveChanges();
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



        // --- SubGrupo ---
        //GET - SubGrupos
        public ActionResult IndexSubGrupo()
        {
            var subGrupos = db.Grupos.Where(x => x.Grupo_ID_Pai != "-1" && x.Inativo.Equals(false)).ToList();
            return View(subGrupos);
        }
        public ActionResult IndexAtivarSubGrupo()
        {
            var subGrupos = db.Grupos.Where(x => x.Grupo_ID_Pai != "-1" && x.Inativo.Equals(true)).ToList();
            return View(subGrupos);
        }

        //// --- Cadastrar Novo Grupo ---
        ////GET
        //public ActionResult CreateSubGrupo()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateSubGrupo(Grupo grupo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)) == null)
        //        {
        //            db.Grupos.Add(grupo);
        //            db.SaveChanges();
        //            return RedirectToAction("IndexGrupo");
        //        }
        //        else
        //        {
        //            if (db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)).ID_Grupo == null)
        //            {
        //                db.Grupos.Add(grupo);
        //                db.SaveChanges();
        //                return RedirectToAction("IndexGrupo");
        //            }
        //            else
        //            {
        //                Response.Write("<script>alert('Já existe um sub grupo com o Nome: " + grupo.Nome + " cadastrado!');</script>");
        //            }
        //        }
        //    }
        //    return View(grupo);
        //}

        // --- Detalhes Grupo ---
        //GET
        public ActionResult DetailsSubGrupo(int? id)
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

        //// --- Editar Grupo---
        ////GET
        //public ActionResult EditSubGrupo(int? id)
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
        //    Session.Add("NomeAntigo", grupo.Nome);
        //    return View(grupo);
        //}
        //[HttpPost]
        //public ActionResult EditSubGrupo(Grupo grupo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if ((string)Session["NomeAntigo"] != grupo.Nome)
        //        {
        //            if (db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)) == null)
        //            {
        //                db.Entry(grupo).State = System.Data.Entity.EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("IndexGrupo");
        //            }
        //            else
        //            {
        //                if (db.Grupos.FirstOrDefault(x => x.Nome.Equals(grupo.Nome)).ID_Grupo == null)
        //                {
        //                    db.Entry(grupo).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();
        //                    return RedirectToAction("IndexGrupo");
        //                }
        //                else
        //                {
        //                    Response.Write("<script>alert('Já existe um sub grupo com o Nome: " + grupo.Nome + " cadastrado!');</script>");
        //                    return View(grupo);
        //                }
        //            }
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(grupo).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("IndexGrupo");
        //    }
        //    return View(grupo);
        //}

        // --- Exclusão logica ---
        //Get
        public ActionResult DeleteSubGrupo(int? id)
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
        [ActionName("DeleteSubGrupo")]// Decide o nome da Action
        public ActionResult DeleteSubGrupoConfirmed(int? id)//O delete já foi confirmado
        {
            db.Grupos.Find(id).Inativo = true;
            db.SaveChanges();
            return RedirectToAction("IndexSubGrupo");
        }

        // --- Ativar SubGrupo ---
        //Get
        public ActionResult AtivarSubGrupo(int? id)
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
        [ActionName("AtivarSubGrupo")]// Decide o nome da Action
        public ActionResult AtivarSubGrupoConfirmed(int? id)
        {
            db.Grupos.Find(id).Inativo = false;
            db.SaveChanges();
            return RedirectToAction("IndexAtivarSubGrupo");
        }
    }
}