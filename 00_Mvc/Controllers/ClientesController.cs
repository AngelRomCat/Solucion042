using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using _02_Services.ClientesServices;
using _05_Data.Dtos;

namespace _00_Mvc.Controllers
{
    public class ClientesController : Controller
    {
        //private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: Clientes
        public ActionResult Index(int? id)
        {
            IList<ClienteDto> clienteDtos = null;
            ClientesService service = null;
            service = new ClientesService();
            clienteDtos = service.List(id);

            return View(clienteDtos);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClienteDto clienteDto = null;
            ClientesService service = null;
            service = new ClientesService();
            clienteDto = service.Detail(id.Value);
            if (clienteDto == null)
            {
                return HttpNotFound();
            }
            return View(clienteDto);
            //hasta aquí
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            //ClienteDto clienteDto = new ClienteDto();
            //return View(clienteDto);
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteDto clienteDto)
        {
            if (ModelState.IsValid)
            {
                ClientesService service = new ClientesService();
                bool ok = false;
                ok = service.Create(clienteDto);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(clienteDto);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDto clienteDto = null;
            ClientesService service = null;
            service = new ClientesService();
            clienteDto = service.Detail(id.Value);
            if (clienteDto == null)
            {
                return HttpNotFound();
            }
            return View(clienteDto);
        }



        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteDto clienteDto)
        {
            if (ModelState.IsValid)
            {
                ClientesService service = new ClientesService();
                bool ok = false;
                ClienteDto buscadaDto = service.Detail(clienteDto.CustomerID);

                ok = service.Edit(clienteDto);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(clienteDto);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDto clienteDto = null;
            ClientesService service = null;
            service = new ClientesService();
            clienteDto = service.Detail(id.Value);

            if (clienteDto == null)
            {
                return HttpNotFound();
            }
            return View(clienteDto);
        }

        // POST: Clientes/Delete/5
        //A pesar de que el método se llama "DeleteConfirmed"
        //Nosotros podremosacceder a él como "Delete"
        //Gracias a esta línea:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClienteDto clienteDto = null;
            ClientesService service = null;
            service = new ClientesService();
            clienteDto = service.Detail(id);

            bool ok = false;
            ok = service.Delete(clienteDto);

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
    }
}