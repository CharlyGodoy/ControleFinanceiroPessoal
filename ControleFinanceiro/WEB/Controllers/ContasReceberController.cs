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
    public class ContasReceberController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // --- Listar Contas A a Receber
        // GET
        public ActionResult Index()
        {
            var contasReceber = db.ContasReceber.ToList();
            return View(contasReceber);
        }

        // --- Nova Conta a Receber ---
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ContaReceber contaReceber)
        {
            if (ModelState.IsValid)
            {
                db.ContasReceber.Add(contaReceber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contaReceber);
        }

        // --- Detalhes Conta a Receber ---
        //GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(contaReceber);
        }

        // --- Editar Conta a Receber ---
        //GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(contaReceber);
        }
        [HttpPost]
        public ActionResult Edit(ContaReceber contaReceber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaReceber).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contaReceber);
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
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(contaReceber);
        }
        [HttpPost]
        [ActionName("Delete")]// Decide o nome da Action
        public ActionResult DeleteConfirmed(int? id)//O delete já foi confirmado
        {
            db.ContasReceber.Find(id).Baixado = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}