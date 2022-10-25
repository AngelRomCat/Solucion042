using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using _05_Data.Data;
using _02_Services.ProductosServices;
using _02_Services.CategoriasServices;
using _02_Services.ProveedoresServices;
using _05_Data.Dtos;

namespace _00_Mvc.Controllers
{
    public class ProductosController : Controller
    {
        //private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: Productos
        public ActionResult Index(int? id)
        {
            //Necesitamos un IList<Producto> para pasárselo a la View
            IList<Producto> productos = null;
            //Creamos un objeto de la Clase ProductosService
            ProductosService service = null;
            service = new ProductosService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<Producto> productos
            productos = service.List(id);

            return View(productos);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //hasta aquí
            //Nuevo
            //Necesitamos un objeto Producto para pasárselo a la View
            Producto producto = null;
            //Creamos un objeto de la Clase ProductosService
            ProductosService service = null;
            service = new ProductosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Producto producto
            producto = service.Detail(id.Value);
            //Fin Nuevo
            //Esto como estaba:
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
            //hasta aquí
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = SelectListsCategoriaRellenator(null);
            ViewBag.supplierID = SelectListsProveedorRellenator(null);
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                ProductosService service = new ProductosService();
                bool ok = false;
                ok = service.Create(producto);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";

            ViewBag.CategoryID = SelectListsCategoriaRellenator(producto.CategoryID);
            ViewBag.supplierID = SelectListsProveedorRellenator(producto.supplierID);
            
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            //Nuevo
            //Necesitamos un objeto Producto para pasárselo a la View
            Producto producto = null;
            //Creamos un objeto de la Clase ProductosService
            ProductosService service = null;
            service = new ProductosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Producto producto
            producto = service.Detail(id.Value);
            //Fin Nuevo
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = SelectListsCategoriaRellenator(producto.CategoryID);
            ViewBag.supplierID = SelectListsProveedorRellenator(producto.supplierID);

            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            //ESTE OBJETO producto que ha entrado es NUEVO
            //para comprobarlo, buscamos el que está en la Tabla Producto
            if (ModelState.IsValid)
            {
                ProductosService service = new ProductosService();
                bool ok = false;
                //Vamos a testear el registro que hay en la tabla:
                Producto buscada = service.Detail(producto.ProductID);
                //Vemos los valores de el objeto Producto buscada
                //id = 9
                //name = Bicho
                //description = Cambiamos la descripción
                //El registro de dentro de la Tabla Producto NO HA CAMBIADO. PORQUE ES OTRO

                ok = service.Edit(producto);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            ViewBag.CategoryID = SelectListsCategoriaRellenator(producto.CategoryID);
            ViewBag.supplierID = SelectListsProveedorRellenator(producto.supplierID);

            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            //Nuevo
            //Necesitamos un objeto Producto para pasárselo a la View
            Producto producto = null;
            //Creamos un objeto de la Clase ProductosService
            ProductosService service = null;
            service = new ProductosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Producto producto
            producto = service.Detail(id.Value);
            //Fin Nuevo
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            //Nuevo
            //Necesitamos un objeto Producto para pasárselo a la View
            Producto producto = null;
            //Creamos un objeto de la Clase ProductosService
            ProductosService service = null;
            service = new ProductosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Producto producto
            producto = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(producto);

            return RedirectToAction("Index");
        }
        //SelectListsRellenators
        private SelectList SelectListsCategoriaRellenator(int? id)
        {
            CategoriasService service = null;
            service = new CategoriasService();
            IList<CategoriaDto> categoriaDtos = null;
            categoriaDtos = service.List(null);
            SelectList selectList = null;
            if (id != null && id > 0)
            {
                selectList = new SelectList(categoriaDtos, "CategoryID", "CategoryName", id);
            }
            else
            {
                selectList = new SelectList(categoriaDtos, "CategoryID", "CategoryName");
            }

            return selectList;
        }
        private SelectList SelectListsProveedorRellenator(int? id)
        {
            ProveedoresService service = null;
            service = new ProveedoresService();
            IList<Proveedor> proveedores = null;
            proveedores = service.List(null);
            SelectList selectList = null;
            if (id != null && id > 0)
            {
                selectList = new SelectList(proveedores, "supplierID", "supplierName", id);
            }
            else
            {
                selectList = new SelectList(proveedores, "supplierID", "supplierName");
            }

            return selectList;
        }

        protected override void Dispose(bool disposing)
        {
            //bool ok = false;
            ////Creamos un objeto de la Clase ProductosService
            //ProductosService service = null;
            //service = new ProductosService();
            ////Lo utilizamos para llegar a su método Dispose 
            //ok = service.Dispose(disposing);

            //base.Dispose(disposing);
        }
    }
}
