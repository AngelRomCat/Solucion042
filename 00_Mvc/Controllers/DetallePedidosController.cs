using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using _05_Data.Data;
using _02_Services.DetallePedidosServices;
using _05_Data.ViewModels;

namespace _00_Mvc.Controllers
{
    public class DetallePedidosController : Controller
    {
        //private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: DetallePedidos
        public ActionResult Index(int? id, string madre, string nombreMadre)
        {
            //Necesitamos un IList<DetallePedido> para pasárselo a la View
            IList<DetallePedido> detallePedidos = null;
            //Creamos un objeto de la Clase DetallePedidosService
            DetallePedidosService service = null;
            service = new DetallePedidosService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<DetallePedido> detallePedidos
            detallePedidos = service.List(id, madre);
            ViewBag.Message = "";
            if (madre != null && madre != "")
            {
                ViewBag.Message = "DetallePedidos del " + madre + ": " + nombreMadre;
            }
            return View(detallePedidos);
        }

        // GET: DetallePedidos/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //hasta aquí
            //Nuevo
            //Necesitamos un objeto DetallePedido para pasárselo a la View
            DetallePedido detallePedido = null;
            //Creamos un objeto de la Clase DetallePedidosService
            DetallePedidosService service = null;
            service = new DetallePedidosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro DetallePedido detallePedido
            detallePedido = service.Detail(id.Value);
            //Fin Nuevo
            //Esto como estaba:
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
            //hasta aquí
        }

        // GET: DetallePedidos/Create
        public ActionResult Create()
        {
            PedidosProductosDetallePedidosViewModel viewModel = null;

            DetallePedidosService service = null;
            service = new DetallePedidosService();

            viewModel = service.RellenaViewModel();
            //ViewBag.CustomerID = SelectListsClienteRellenator(null);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(null);
            //ViewBag.shipperID = SelectListsNavieraRellenator(null);
            return View(viewModel);
        }

        // POST: DetallePedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PedidosProductosDetallePedidosViewModel viewModel)
        {
            DetallePedidosService service = null;
            if (ModelState.IsValid)
            {
                service = new DetallePedidosService();
                bool ok = false;
                ok = service.Create(viewModel.detallePedido);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";

            //ViewBag.CustomerID = SelectListsClienteRellenator(viewModel.detallePedido.CustomerID);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(viewModel.detallePedido.EmployeeID);
            //ViewBag.shipperID = SelectListsNavieraRellenator(viewModel.detallePedido.shipperID);

            //PedidosProductosDetallePedidosViewModel viewModel = null;
            viewModel = service.RellenaViewModel(viewModel);

            return View(viewModel);
        }

        // GET: DetallePedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nuevo
            //Necesitamos un objeto DetallePedido para pasárselo a la View
            DetallePedido detallePedido = null;
            //Creamos un objeto de la Clase DetallePedidosService
            DetallePedidosService service = null;
            service = new DetallePedidosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro DetallePedido detallePedido
            detallePedido = service.Detail(id.Value);

            PedidosProductosDetallePedidosViewModel viewModel = service.RellenaViewModel();
            viewModel.detallePedido = detallePedido;
            //Fin Nuevo
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerID = SelectListsClienteRellenator(detallePedido.CustomerID);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(detallePedido.EmployeeID);
            //ViewBag.shipperID = SelectListsNavieraRellenator(detallePedido.shipperID);

            return View(viewModel);
        }

        // POST: DetallePedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PedidosProductosDetallePedidosViewModel viewModel)
        {
            //ESTE OBJETO detallePedido que ha entrado es NUEVO
            //para comprobarlo, buscamos el que está en la Tabla DetallePedido
            if (ModelState.IsValid)
            {
                DetallePedidosService service = new DetallePedidosService();
                bool ok = false;
                //Vamos a testear el registro que hay en la tabla:
                DetallePedido buscada = service.Detail(viewModel.detallePedido.OrderID);
                //Vemos los valores de el objeto DetallePedido buscada
                //id = 9
                //name = Bicho
                //description = Cambiamos la descripción
                //El registro de dentro de la Tabla DetallePedido NO HA CAMBIADO. PORQUE ES OTRO

                ok = service.Edit(viewModel.detallePedido);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            //ViewBag.CustomerID = SelectListsClienteRellenator(detallePedido.CustomerID);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(detallePedido.EmployeeID);
            //ViewBag.shipperID = SelectListsNavieraRellenator(detallePedido.shipperID);

            return View(viewModel);
        }

        // GET: DetallePedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nuevo
            //Necesitamos un objeto DetallePedido para pasárselo a la View
            DetallePedido detallePedido = null;
            //Creamos un objeto de la Clase DetallePedidosService
            DetallePedidosService service = null;
            service = new DetallePedidosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro DetallePedido detallePedido
            detallePedido = service.Detail(id.Value);
            //Fin Nuevo
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            //Nuevo
                     //Necesitamos un objeto DetallePedido para pasárselo a la View
            DetallePedido detallePedido = null;
            //Creamos un objeto de la Clase DetallePedidosService
            DetallePedidosService service = null;
            service = new DetallePedidosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro DetallePedido detallePedido
            detallePedido = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(detallePedido);

            return RedirectToAction("Index");
        }
        ////SelectListsRellenators
        //private SelectList SelectListsClienteRellenator(int? id)
        //{
        //    ClientesService service = null;
        //    service = new ClientesService();
        //    IList<Cliente> clientes = null;
        //    clientes = service.List(null);
        //    SelectList selectList = null;
        //    if (id != null && id > 0)
        //    {
        //        selectList = new SelectList(clientes, "CustomerID", "CustomerName", id);
        //    }
        //    else
        //    {
        //        selectList = new SelectList(clientes, "CustomerID", "CustomerName");
        //    }

        //    return selectList;
        //}
        //private SelectList SelectListsEmpleadoRellenator(int? id)
        //{
        //    EmpleadosService service = null;
        //    service = new EmpleadosService();
        //    IList<Empleado> empleados = null;
        //    empleados = service.List(null);
        //    SelectList selectList = null;
        //    if (id != null && id > 0)
        //    {
        //        selectList = new SelectList(empleados, "EmployeeID", "FirstName", id);
        //    }
        //    else
        //    {
        //        selectList = new SelectList(empleados, "EmployeeID", "FirstName");
        //    }

        //    return selectList;
        //}
        //private SelectList SelectListsNavieraRellenator(int? id)
        //{
        //    NavierasService service = null;
        //    service = new NavierasService();
        //    IList<Naviera> navieras = null;
        //    navieras = service.List(null);
        //    SelectList selectList = null;
        //    if (id != null && id > 0)
        //    {
        //        selectList = new SelectList(navieras, "shipperID", "shipperName", id);
        //    }
        //    else
        //    {
        //        selectList = new SelectList(navieras, "shipperID", "shipperName");
        //    }

        //    return selectList;
        //}
        protected override void Dispose(bool disposing)
        {
            //bool ok = false;
            ////Creamos un objeto de la Clase DetallePedidosService
            //DetallePedidosService service = null;
            //service = new DetallePedidosService();
            ////Lo utilizamos para llegar a su método Dispose 
            //ok = service.Dispose(disposing);

            //base.Dispose(disposing);
        }
    }
}
