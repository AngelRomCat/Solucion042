using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using _02_Services.EmpleadosServices;
using _05_Data.Data;
using _05_Data.ViewModels;

namespace _00_Mvc.Controllers
{
    public class EmpleadosController : Controller
    {
        //private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: Empleados
        public ActionResult Index(int? id)
        {
            IList<Empleado> empleados = null;
            EmpleadosService service = null;
            service = new EmpleadosService();
            empleados = service.List(id,null);

            return View(empleados);
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Empleado empleado = null;
            EmpleadosService service = null;
            service = new EmpleadosService();
            empleado = service.Detail(id.Value);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
            //hasta aquí
        }
        // GET: Empleados/Details/5
        public ActionResult DetailsAjax(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Empleado empleado = null;
            EmpleadosService service = null;
            service = new EmpleadosService();
            empleado = service.Detail(id.Value);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
            //hasta aquí
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                EmpleadosService service = new EmpleadosService();
                bool ok = false;
                ok = service.Create(empleado);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Empleado empleado = null;

            EmpleadosService service = null;
            service = new EmpleadosService();

            empleado = service.Detail(id.Value);

            if (empleado == null)
            {
                return HttpNotFound();
            }

            string birthDate = "";

            if (empleado != null && empleado.birthDate != null)
            {
                birthDate = empleado.birthDate.Value.ToString("dd/MM/yyyy");
            }
            EmpleadoViewModel viewModel = new EmpleadoViewModel();

            viewModel.EmployeeID = empleado.EmployeeID;
            viewModel.FirstName = empleado.FirstName;
            viewModel.LastName = empleado.LastName;
            viewModel.birthDate = birthDate;
            viewModel.Photo = empleado.Photo;
            viewModel.Notes = empleado.Notes;

            return View(viewModel);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpleadoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                EmpleadosService service = new EmpleadosService();
                bool ok = false;

                Empleado buscada = service.Detail(viewModel.EmployeeID);

                ok = service.Edit(viewModel);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(viewModel);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = null;

            EmpleadosService service = null;
            service = new EmpleadosService();

            empleado = service.Detail(id.Value);

            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        //A pesar de que el método se llama "DeleteConfirmed"
        //Nosotros podremosacceder a él como "Delete"
        //Gracias a esta línea:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = null;

            EmpleadosService service = null;
            service = new EmpleadosService();

            empleado = service.Detail(id);

            bool ok = false;
            ok = service.Delete(empleado);

            return RedirectToAction("Index");
        }
        //Disposing, en principio, ya no es necesario.
        //Servía para liberar el DbContext, al cambiar de Clase
        protected override void Dispose(bool disposing)
        {
            //bool ok = false;
            //CategoriasService service = null;
            //service = new CategoriasService();
            //ok = service.Dispose(disposing);

            //base.Dispose(disposing);
        }
        // POST: Ejercicio/_EmpleadoMvcOtraPartialView/5
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult _EmpleadoMvcOtraPartialView(Empleado empleado)
        {
            return View("_EmpleadoMvcOtraPartialView", empleado);
        }
    }
}
