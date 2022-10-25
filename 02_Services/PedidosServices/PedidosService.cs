using _05_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using _05_Data.ViewModels;
using _02_Services.ClientesServices;
using _02_Services.EmpleadosServices;
using _02_Services.NavierasServices;

namespace _02_Services.PedidosServices
{
    public class PedidosService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public PedidosService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<Pedido> List(int? id, string madre)
        {
            IList<Pedido> pedidos = null;
            if (id == null || id < 1)
            {
                pedidos = _db.Pedido
                            .Include(p => p.Cliente)
                            .Include(p => p.Empleado)
                            .Include(p => p.Naviera)
                            .ToList();
            }
            else
            {
                if (madre != null && madre != "")
                {
                    if (madre == "Cliente")
                    {
                        pedidos = _db.Pedido
                                    .Include(p => p.Cliente)
                                    .Include(p => p.Empleado)
                                    .Include(p => p.Naviera)
                                    .Where(x => x.CustomerID == id)
                                    .ToList();
                    }
                    if (madre == "Empleado")
                    {
                        pedidos = _db.Pedido
                                    .Include(p => p.Cliente)
                                    .Include(p => p.Empleado)
                                    .Include(p => p.Naviera)
                                    .Where(x => x.EmployeeID == id)
                                    .ToList();
                    }
                    if (madre == "Naviera")
                    {
                        pedidos = _db.Pedido
                                    .Include(p => p.Cliente)
                                    .Include(p => p.Empleado)
                                    .Include(p => p.Naviera)
                                    .Where(x => x.shipperID == id)
                                    .ToList();
                    }
                }
            }

            return pedidos;
        }
        //Details
        public Pedido Detail(int id)
        {
            Pedido pedido = null;
            pedido = _db.Pedido
                                .Where(x => x.OrderID == id)
                                .FirstOrDefault();
            return pedido;
        }
        //Create
        public bool Create(Pedido pedido)
        {
            bool ok = false;
            try
            {
                _db.Pedido.Add(pedido);
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Edit
        public bool Edit(Pedido pedido)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla Pedido que tiene el mismo id
                //que el objeto que ha creado la view
                Pedido buscada = _db.Pedido
                                    .Where(x => x.OrderID == pedido.OrderID)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:
                //buscada.OrderID = pedido.OrderID;
                buscada.OrderID = pedido.OrderID;
                buscada.OrderDate = pedido.OrderDate;
                buscada.CustomerID = pedido.CustomerID;
                buscada.EmployeeID = pedido.EmployeeID;
                buscada.shipperID = pedido.shipperID;

        //Guardamos cambios:
        ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Delete
        public bool Delete(Pedido pedido)
        {
            bool ok = false;
            try
            {
                _db.Pedido.Remove(pedido);
                //Guardamos cambios:
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //SaveChanges
        public bool SaveChanges()
        {
            bool ok = false;
            try
            {
                int retorno = 0;
                retorno = _db.SaveChanges();
                if (retorno > 0)
                {
                    ok = true;
                }
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Dispose
        public bool Dispose(bool ok)
        {
            if (ok == true)
            {
                _db.Dispose();
            }

            return ok;
        }
        //Creamos y Rellenamos el ViewModel
        public ClientesEmpleadosNavierasPedidoViewModel RellenaViewModel()
        {
            //Creamos el viewModel
            ClientesEmpleadosNavierasPedidoViewModel viewModel = null;
            viewModel = new ClientesEmpleadosNavierasPedidoViewModel();
            //Rellenamos solamente los Clientes, Empleados y Navieras del ViewModel
            viewModel = RellenaViewModel(viewModel);
            //Creamos un nuevo Pedido
            Pedido pedido = new Pedido();
            //Rellenamos los 3 campos IMPRESCINDIBLES de el nuevo objeto Pedido pedido 
            //Con el id del primer elemento de cada una de las tres listas
            //pedido.CustomerID = viewModel.clientes.FirstOrDefault().CustomerID;
            pedido.CustomerID = viewModel.clienteDtos.FirstOrDefault().CustomerID;
            pedido.EmployeeID = viewModel.empleados.FirstOrDefault().EmployeeID;
            pedido.shipperID = viewModel.navieras.FirstOrDefault().shipperID;

            viewModel.pedido = pedido;

            return viewModel;
        }
        //Rellenamos solamente los Clientes, Empleados y Navieras del ViewModel
        public ClientesEmpleadosNavierasPedidoViewModel RellenaViewModel(ClientesEmpleadosNavierasPedidoViewModel viewModel)
        {
            ClientesService clientesService = null;
            clientesService = new ClientesService();
            //viewModel.clientes = clientesService.List(null);
            viewModel.clienteDtos = clientesService.List(null);
            EmpleadosService empleadosService = null;
            empleadosService = new EmpleadosService();
            viewModel.empleados = empleadosService.List(null,null);
            NavierasService navierasService = null;
            navierasService = new NavierasService();
            viewModel.navieras = navierasService.List(null);

            return viewModel;
        }
    }
}

