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
    public class ClientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // --- Listar Clientes
        // GET
        public ActionResult Index()
        {
            var clientes = db.Clientes.Where(x => x.Inativo.Equals(false)).ToList();
            return View(clientes);
        }

        public ActionResult IndexAtivo()
        {
            var clientes = db.Clientes.Where(x => x.Inativo.Equals(true)).ToList();
            return View(clientes);
        }

        // --- Novo Cliente ---
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (db.Clientes.FirstOrDefault(x => x.Nome.Equals(cliente.Nome)) == null)
                {
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Response.Write("<script>alert('Já existe um cliente com o Nome: " + cliente.Nome + " cadastrado!');</script>");
                }
            }
            return View(cliente);
        }

        // --- Detalhes Cliente ---
        //GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(cliente);
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
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            Session.Add("NomeAntigo", cliente.Nome);
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if ((string)Session["NomeAntigo"] != cliente.Nome)
                {
                    if (db.Clientes.FirstOrDefault(x => x.Nome.Equals(cliente.Nome)) == null)
                    {
                        db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.Write("<script>alert('Já existe um cliente com o Nome: " + cliente.Nome + " cadastrado!');</script>");
                        return View(cliente);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
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
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(cliente);
        }
        [HttpPost]
        [ActionName("Delete")]// Decide o nome da Action
        public ActionResult DeleteConfirmed(int? id)//O delete já foi confirmado
        {
            if (db.ContasReceber.FirstOrDefault(x => x.Baixado.Equals(false)) == null)
            {
                if (db.ContasReceber.FirstOrDefault(x => x.Liquidado.Equals(true)) == null)
                {
                    db.Clientes.Find(id).Inativo = true;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Response.Write("<script>alert('Não foi possivel excluir esse Cliente pois ele possui Contas a Receber Liquidadas!');</script>");
                    return View();
                }
            }
            else
            {
                Response.Write("<script>alert('Não foi possivel excluir esse Cliente pois ele possui Contas a Receber Ativas!');</script>");
                return View();
            }


        }

        // --- Ativar Cliente ---
        //Get
        public ActionResult Ativar(int? id)
        {
            if (id == null)
            {
                //ERRO HTTP 400
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                // ERRO HTTP 404
                return HttpNotFound();
            }
            return View(cliente);
        }
        [HttpPost]
        [ActionName("Ativar")]// Decide o nome da Action
        public ActionResult AtivarConfirmed(int? id)
        {
            db.Clientes.Find(id).Inativo = false;
            db.SaveChanges();
            return RedirectToAction("IndexAtivo");
        }
    }
}