using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class FornecedoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // --- Listar Fornecedores
        // GET
        public ActionResult Index()
        {
            var fornecedores = db.Fornecedores.Where(x => x.Inativo.Equals(false)).ToList();
            return View(fornecedores);
        }

        public ActionResult IndexAtivo()
        {
            var fornecedores = db.Fornecedores.Where(x => x.Inativo.Equals(true)).ToList();
            return View(fornecedores);
        }

        // --- Novo Fornecedor ---
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                if (db.Fornecedores.FirstOrDefault(x => x.Nome.Equals(fornecedor.Nome)) == null)
                {
                    db.Fornecedores.Add(fornecedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Response.Write("<script>alert('Já existe um fornecedor com o Nome: " + fornecedor.Nome + " cadastrado!');</script>");
                }
                
            }
            return View(fornecedor);
        }

        // --- Detalhes Fornecedor ---
        //GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // --- Editar ---
        //GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            Session.Add("NomeAntigo", fornecedor.Nome);
            return View(fornecedor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                if ((string)Session["NomeAntigo"] != fornecedor.Nome)
                {
                    if (db.Fornecedores.FirstOrDefault(x => x.Nome.Equals(fornecedor.Nome)) == null)
                    {
                        db.Entry(fornecedor).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.Write("<script>alert('Já existe um fornecedor com o Nome: " + fornecedor.Nome + " cadastrado!');</script>");
                        return View(fornecedor);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
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
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(fornecedor);
        }
        [HttpPost]
        [ActionName("Delete")]// Decide o nome da Action
        public ActionResult DeleteConfirmed(int? id)//O delete já foi confirmado
        {
            db.Fornecedores.Find(id).Inativo = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // --- Ativar Fornecedor ---
        //Get
        public ActionResult Ativar(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(fornecedor);
        }
        [HttpPost]
        [ActionName("Ativar")]// Decide o nome da Action
        public ActionResult AtivarConfirmed(int? id)
        {
            db.Fornecedores.Find(id).Inativo = false;
            db.SaveChanges();
            return RedirectToAction("IndexAtivo");
        }
    }
}