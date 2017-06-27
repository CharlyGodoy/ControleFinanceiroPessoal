using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaseModel;
using WEB.Models;

namespace WEB.Controllers
{
    public class ContasReceberController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContasReceber Ativo
        public ActionResult Index()
        {
            var contasReceber = db.ContasReceber.Include(c => c._Grupo).Include(c => c.Cliente).Where(x => x.Baixado.Equals(false) && x.Liquidado.Equals(false)).ToList();
            return View(contasReceber);
        }

        // GET: ContasReceber Baixado
        public ActionResult IndexBaixado()
        {
            var contasReceber = db.ContasReceber.Include(c => c._Grupo).Include(c => c.Cliente).Where(x => x.Baixado.Equals(true)).ToList();
            return View(contasReceber);
        }

        // GET: ContasReceber Liquidado
        public ActionResult IndexLiquidado()
        {
            var contasReceber = db.ContasReceber.Include(c => c._Grupo).Include(c => c.Cliente).Where(x => x.Liquidado.Equals(true)).ToList();
            return View(contasReceber);
        }

        // GET: ContasReceber/Details/5
        public ActionResult DetailsAtivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                return HttpNotFound();
            }
            return View(contaReceber);
        }

        // GET: ContasReceber/Details/5
        public ActionResult DetailsLiquidado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                return HttpNotFound();
            }
            return View(contaReceber);
        }

        // GET: ContasReceber/Create
        public ActionResult Create()
        {
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome");
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Inativo.Equals(false)).ToList(), "ClienteID", "Nome");
            return View();
        }

        // POST: ContasReceber/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContaReceber contaReceber)
        {
            contaReceber.Data_Inclusao = DateTime.Now;
            contaReceber.Data_Recebimento = DateTime.Today.AddDays(+1);
            contaReceber.Valor_Recebido = 0;
            if (contaReceber.Data_PrevRecebimento >= DateTime.Today)
            {
                if (ModelState.IsValid)
                {
                    db.ContasReceber.Add(contaReceber);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }else
            {
                Response.Write("<script>alert('Não possivel criar uma Conta a Receber com data de vencimento menor do que a data de hoje!');</script>");
            }
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome", contaReceber.GrupoID);
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Inativo.Equals(false)).ToList(), "ClienteID", "Nome", contaReceber.ClienteID);
            return View(contaReceber);
        }

        // GET: ContasReceber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome", contaReceber.GrupoID);
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Inativo.Equals(false)).ToList(), "ClienteID", "Nome", contaReceber.ClienteID);
            Session.Add("Data_Inclusao", contaReceber.Data_Inclusao);
            Session.Add("Data_Recebimento", contaReceber.Data_Recebimento);
            return View(contaReceber);
        }

        // POST: ContasReceber/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContaReceber contaReceber)
        {
            contaReceber.Data_Inclusao = (DateTime)Session["Data_Inclusao"];
            contaReceber.Data_Recebimento = (DateTime)Session["Data_Recebimento"];
            if (contaReceber.Data_PrevRecebimento >= DateTime.Today)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(contaReceber).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }else
            {
                Response.Write("<script>alert('Não possivel editar uma Conta a Receber com data de vencimento menor do que a data de hoje!');</script>");
            }
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome", contaReceber.GrupoID);
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Inativo.Equals(false)).ToList(), "ClienteID", "Nome", contaReceber.ClienteID);
            return View(contaReceber);
        }

        // GET: ContasReceber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                return HttpNotFound();
            }
            return View(contaReceber);
        }

        // POST: ContasReceber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.ContasReceber.Find(id).Baixado = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ContasReceber/Ativar/5
        public ActionResult Ativar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                return HttpNotFound();
            }
            return View(contaReceber);
        }

        // POST: ContasReceber/Ativar/5
        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public ActionResult AtivarConfirmed(int id)
        {
            db.ContasReceber.Find(id).Baixado = false;
            db.SaveChanges();
            return RedirectToAction("IndexBaixado");
        }

        // GET: ContasReceber/Liquidar/5
        public ActionResult Liquidar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaReceber contaReceber = db.ContasReceber.Find(id);
            if (contaReceber == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome", contaReceber.GrupoID);
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Inativo.Equals(false)).ToList(), "ClienteID", "Nome", contaReceber.ClienteID);
            Session.Add("contaReceber", contaReceber);
            return View(contaReceber);
        }

        // POST: ContasReceber/Liquidar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Liquidar(ContaReceber contaReceber)
        {
            double valor_recebido = contaReceber.Valor_Recebido;
            contaReceber = (ContaReceber)Session["contaReceber"];
            contaReceber.Valor_Recebido = valor_recebido;            
            if (ModelState.IsValid)
            {
                db.Entry(contaReceber).State = EntityState.Modified;
                db.ContasReceber.Find(contaReceber.ContaReceberID).Liquidado = true;
                db.SaveChanges();
                return RedirectToAction("IndexLiquidado");
            }
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome", contaReceber.GrupoID);
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Inativo.Equals(false)).ToList(), "ClienteID", "Nome", contaReceber.ClienteID);
            return View(contaReceber);
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
