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
    public class ContasPagarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContasPagar Ativo
        public ActionResult Index()
        {
            var contasPagar = db.ContasPagar.Include(c => c._Fornecedor).Include(c => c._Grupo).Where(x => x.Baixado.Equals(false) && x.Liquidado.Equals(false)).ToList();
            return View(contasPagar);
        }

        // GET: ContasPagar Baixado
        public ActionResult IndexBaixado()
        {
            var contasPagar = db.ContasPagar.Include(c => c._Fornecedor).Include(c => c._Grupo).Where(x => x.Baixado.Equals(true)).ToList();
            return View(contasPagar);
        }

        // GET: ContasPagar Liquidado
        public ActionResult IndexLiquidado()
        {
            var contasPagar = db.ContasPagar.Include(c => c._Fornecedor).Include(c => c._Grupo).Where(x => x.Liquidado.Equals(true)).ToList();
            return View(contasPagar);
        }

        // GET: ContasPagar/Details/5
        public ActionResult DetailsAtivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                return HttpNotFound();
            }
            return View(contaPagar);
        }

        public ActionResult DetailsLiquidado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                return HttpNotFound();
            }
            return View(contaPagar);
        }

        // GET: ContasPagar/Create
        public ActionResult Create()
        {
            ViewBag.FornecedorID = new SelectList(db.Fornecedores.Where(x => x.Inativo.Equals(false)).ToList(), "FornecedorID", "Nome");
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome");
            return View();
        }

        // POST: ContasPagar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContaPagar contaPagar)
        {
            contaPagar.Data_Inclusao = DateTime.Now;
            contaPagar.Data_Pagamento = DateTime.Today.AddDays(+1);
            contaPagar.Valor_Pago = 0;
            if (contaPagar.Data_Vencimento >= DateTime.Today)
            {
                if (ModelState.IsValid)
                {
                    db.ContasPagar.Add(contaPagar);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Response.Write("<script>alert('Não possivel criar uma Conta a Pagar com data de vencimento menor do que a data de hoje!');</script>");
            }

            ViewBag.FornecedorID = new SelectList(db.Fornecedores, "FornecedorID", "Nome", contaPagar.FornecedorID);
            ViewBag.GrupoID = new SelectList(db.Grupos, "GrupoID", "Nome", contaPagar.GrupoID);
            return View(contaPagar);
        }

        // GET: ContasPagar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                return HttpNotFound();
            }
            ViewBag.FornecedorID = new SelectList(db.Fornecedores.Where(x => x.Inativo.Equals(false)).ToList(), "FornecedorID", "Nome", contaPagar.FornecedorID);
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome", contaPagar.GrupoID);
            Session.Add("Data_Inclusao", contaPagar.Data_Inclusao);
            Session.Add("Data_Pagamento", contaPagar.Data_Pagamento);
            return View(contaPagar);
        }

        // POST: ContasPagar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContaPagar contaPagar)
        {
            contaPagar.Data_Inclusao = (DateTime)Session["Data_Inclusao"];
            contaPagar.Data_Pagamento = (DateTime)Session["Data_Pagamento"];
            if (contaPagar.Data_Vencimento >= DateTime.Today)
            {
                if (ModelState.IsValid)
            {
                db.Entry(contaPagar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            else
            {
                Response.Write("<script>alert('Não possivel editar uma Conta a Pagar com data de vencimento menor do que a data de hoje!');</script>");
            }
            ViewBag.FornecedorID = new SelectList(db.Fornecedores, "FornecedorID", "Nome", contaPagar.FornecedorID);
            ViewBag.GrupoID = new SelectList(db.Grupos, "GrupoID", "Nome", contaPagar.GrupoID);
            return View(contaPagar);
        }

        // GET: ContasPagar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                return HttpNotFound();
            }
            return View(contaPagar);
        }

        // POST: ContasPagar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            
            db.ContasPagar.Find(id).Baixado = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ContasPagar/Ativar/5
        public ActionResult Ativar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                return HttpNotFound();
            }
            return View(contaPagar);
        }

        // POST: ContasPagar/Ativar
        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public ActionResult AtivarConfirmed(int id)
        {
            db.ContasPagar.Find(id).Baixado = false;
            db.SaveChanges();
            return RedirectToAction("IndexBaixado");
        }

        // GET: ContasPagar/Liquidar
        public ActionResult Liquidar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                return HttpNotFound();
            }
            ViewBag.FornecedorID = new SelectList(db.Fornecedores.Where(x => x.Inativo.Equals(false)).ToList(), "FornecedorID", "Nome", contaPagar.FornecedorID);
            ViewBag.GrupoID = new SelectList(db.Grupos.Where(x => x.Inativo.Equals(false)).ToList(), "GrupoID", "Nome", contaPagar.GrupoID);            
            Session.Add("contaPagar", contaPagar);
            return View(contaPagar);
        }

        // POST: ContasPagar/Liquidar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Liquidar(ContaPagar contaPagar)
        {
            double valor_pago = contaPagar.Valor_Pago;
            contaPagar = (ContaPagar)Session["contaPagar"];
            contaPagar.Valor_Pago = valor_pago;
                if (ModelState.IsValid)
                {
                    db.Entry(contaPagar).State = EntityState.Modified;
                    db.ContasPagar.Find(contaPagar.ContaPagarID).Liquidado = true;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            ViewBag.FornecedorID = new SelectList(db.Fornecedores, "FornecedorID", "Nome", contaPagar.FornecedorID);
            ViewBag.GrupoID = new SelectList(db.Grupos, "GrupoID", "Nome", contaPagar.GrupoID);
            return View(contaPagar);
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
