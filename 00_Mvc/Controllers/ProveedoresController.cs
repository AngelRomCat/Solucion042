using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _02_Services.ProveedoresServices;
using _04_Data.Data;

namespace _00_Mvc.Controllers
{
    public class ProveedoresController : Controller
    {
        //private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: Categorias
        public ActionResult Index(int? id)
        {
            //Necesitamos un IList<Categoria> para pasárselo a la View
            IList<Proveedor> proveedores = null;
            //Creamos un objeto de la Clase CategoriasService
            ProveedoresService service = null;
            service = new ProveedoresService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<Categoria> categorias
            proveedores = service.List(id);

            return View(proveedores);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //hasta aquí
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Proveedor proveedor = null;
            //Creamos un objeto de la Clase CategoriasService
            ProveedoresService service = null;
            service = new ProveedoresService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            proveedor = service.Detail(id.Value);
            //Fin Nuevo
            //Esto como estaba:
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
            //hasta aquí
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                ProveedoresService service = new ProveedoresService();
                bool ok = false;
                ok = service.Create(proveedor);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(proveedor);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Proveedor proveedor = null;
            //Creamos un objeto de la Clase CategoriasService
            ProveedoresService service = null;
            service = new ProveedoresService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            proveedor = service.Detail(id.Value);
            //Fin Nuevo
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            //Cogemos el objeto y se lo enviamos a la View
            //LEAMOS LO QUE PONE EN LA VISTA
            return View(proveedor);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                ProveedoresService service = new ProveedoresService();
                bool ok = false;
                //ESTE OBJETO categoria que ha entrado es NUEVO
                //para comprobarlo, buscamos el que está en la Tabla Categoria
                Proveedor buscada = service.Detail(proveedor.supplierID);
                //Vemos los valores de el objeto Categoria buscada
                //buscada.CategoryID = 9
                //buscada.CategoryName = Bicho
                //buscada.Description = Cambiamos la descripción
                //El registro de dentro de la Tabla Categoria NO HA CAMBIADO. PORQUE ES OTRO objeto

                ok = service.Edit(proveedor);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(proveedor);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Proveedor proveedor = null;
            //Creamos un objeto de la Clase CategoriasService
            ProveedoresService service = null;
            service = new ProveedoresService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            proveedor = service.Detail(id.Value);
            //Fin Nuevo
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Categorias/Delete/5
        //A pesar de que el método se llama "DeleteConfirmed"
        //Nosotros podremosacceder a él como "Delete"
        //Gracias a esta línea:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Proveedor proveedor = null;
            //Creamos un objeto de la Clase CategoriasService
            ProveedoresService service = null;
            service = new ProveedoresService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            proveedor = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(proveedor);

            return RedirectToAction("Index");
        }
        //Disposing, en principio, ya no es necesario.
        //Servía para liberar el DbContext, al cambiar de Clase
        protected override void Dispose(bool disposing)
        {
            //bool ok = false;
            ////Creamos un objeto de la Clase CategoriasService
            //CategoriasService service = null;
            //service = new CategoriasService();
            ////Lo utilizamos para llegar a su método Dispose 
            //ok = service.Dispose(disposing);

            //base.Dispose(disposing);
        }
    }
}
