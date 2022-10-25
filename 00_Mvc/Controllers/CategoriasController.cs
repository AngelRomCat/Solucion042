using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using _02_Services.CategoriasServices;
using _02_Services.ClientesServices;
using _05_Data.Data;
using _05_Data.Dtos;

namespace _00_Mvc.Controllers
{
    public class CategoriasController : Controller
    {
        //private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: Categorias
        public ActionResult Index(int? id)
        {
            //Necesitamos un IList<Categoria> para pasárselo a la View
            IList<CategoriaDto> categoriaDtos = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<Categoria> categorias
            categoriaDtos = service.List(id);

            return View(categoriaDtos);
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
            CategoriaDto categoriaDto = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoriaDto = service.Detail(id.Value);
            //Fin Nuevo
            //Esto como estaba:
            if (categoriaDto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaDto);
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
        public ActionResult Create(CategoriaDto categoriaDto)
        {
            if (ModelState.IsValid)
            {
                CategoriasService service = new CategoriasService();
                bool ok = false;
                ok = service.Create(categoriaDto);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(categoriaDto);
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
            CategoriaDto categoriaDto = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoriaDto = service.Detail(id.Value);
            //Fin Nuevo
            if (categoriaDto == null)
            {
                return HttpNotFound();
            }
            //Cogemos el objeto y se lo enviamos a la View
            //LEAMOS LO QUE PONE EN LA VISTA
            return View(categoriaDto);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaDto categoriaDto)
        {
            if (ModelState.IsValid)
            {
                CategoriasService service = new CategoriasService();
                bool ok = false;
                //ESTE OBJETO categoria que ha entrado es NUEVO
                //para comprobarlo, buscamos el que está en la Tabla Categoria
                CategoriaDto buscada = service.Detail(categoriaDto.CategoryID);
                //Vemos los valores de el objeto Categoria buscada
                //buscada.CategoryID = 9
                //buscada.CategoryName = Bicho
                //buscada.Description = Cambiamos la descripción
                //El registro de dentro de la Tabla Categoria NO HA CAMBIADO. PORQUE ES OTRO objeto

                ok = service.Edit(categoriaDto);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(categoriaDto);
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
            CategoriaDto categoriaDto = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoriaDto = service.Detail(id.Value);
            //Fin Nuevo
            if (categoriaDto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaDto);
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
            CategoriaDto categoriaDto = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoriaDto = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(categoriaDto);

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
