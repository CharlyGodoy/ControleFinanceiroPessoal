using BaseModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class ContasPagarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // --- Listar ContasPagar
        // GET
        public ActionResult Index()
        {
            var contasPagar = db.ContasPagar.ToList();
            return View(contasPagar);
        }

        // --- Nova Conta a Pagar ---
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ContaPagar contaPagar)
        {
            if (ModelState.IsValid)
            {
                db.ContasPagar.Add(contaPagar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contaPagar);
        }

        // --- Detalhes Conta a Pagar ---
        //GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(contaPagar);
        }

        // --- Editar Conta a Pagar ---
        //GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(contaPagar);
        }
        [HttpPost]
        public ActionResult Edit(ContaPagar contaPagar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaPagar).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contaPagar);
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
            ContaPagar contaPagar = db.ContasPagar.Find(id);
            if (contaPagar == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(contaPagar);
        }
        [HttpPost]
        [ActionName("Delete")]// Decide o nome da Action
        public ActionResult DeleteConfirmed(int? id)//O delete já foi confirmado
        {
            db.ContasPagar.Find(id).Baixado = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}