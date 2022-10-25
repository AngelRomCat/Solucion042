using _05_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using _05_Data.ViewModels;
using _02_Services.ProductosServices;
using _02_Services.PedidosServices;
using _02_Services.NavierasServices;

namespace _02_Services.DetallePedidosServices
{
    public class DetallePedidosService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public DetallePedidosService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<DetallePedido> List(int? id, string madre)
        {
            IList<DetallePedido> detallePedidos = null;
            if (id == null || id < 1)
            {
                detallePedidos = _db.DetallePedido
                            .Include(p => p.Producto)
                            .Include(p => p.Pedido)
                            .ToList();
            }
            else
            {
                if (madre != null && madre != "")
                {
                    if (madre == "Pedido")
                    {
                        detallePedidos = _db.DetallePedido
                                    .Include(p => p.Producto)
                                    .Include(p => p.Pedido)
                                    .Where(x => x.OrderID == id)
                                    .ToList();
                    }
                    if (madre == "Producto")
                    {
                        detallePedidos = _db.DetallePedido
                                    .Include(p => p.Producto)
                                    .Include(p => p.Pedido)
                                    .Where(x => x.ProductID == id)
                                    .ToList();
                    }
                }
            }

            return detallePedidos;
        }
        //Details
        public DetallePedido Detail(int id)
        {
            DetallePedido detallePedido = null;
            detallePedido = _db.DetallePedido
                                .Where(x => x.OrderDetailID == id)
                                .FirstOrDefault();
            return detallePedido;
        }
        //Create
        public bool Create(DetallePedido detallePedido)
        {
            bool ok = false;
            try
            {
                _db.DetallePedido.Add(detallePedido);
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
        public bool Edit(DetallePedido detallePedido)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla DetallePedido que tiene el mismo id
                //que el objeto que ha creado la view
                DetallePedido buscada = _db.DetallePedido
                                    .Where(x => x.OrderDetailID == detallePedido.OrderDetailID)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:
                //buscada.OrderDetailID = detallePedido.OrderDetailID;
                buscada.OrderID = detallePedido.OrderID;
                buscada.ProductID = detallePedido.ProductID;
                buscada.Quantity = detallePedido.Quantity;

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
        public bool Delete(DetallePedido detallePedido)
        {
            bool ok = false;
            try
            {
                _db.DetallePedido.Remove(detallePedido);
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
        public PedidosProductosDetallePedidosViewModel RellenaViewModel()
        {
            //Creamos el viewModel
            PedidosProductosDetallePedidosViewModel viewModel = null;
            viewModel = new PedidosProductosDetallePedidosViewModel();
            //Rellenamos solamente los Productos, Pedidos y Navieras del ViewModel
            viewModel = RellenaViewModel(viewModel);
            //Creamos un nuevo DetallePedido
            DetallePedido detallePedido = new DetallePedido();
            //Rellenamos los 3 campos IMPRESCINDIBLES de el nuevo objeto DetallePedido detallePedido 
            //Con el id del primer elemento de cada una de las tres listas
            detallePedido.OrderID = viewModel.pedidos.FirstOrDefault().OrderID;
            detallePedido.ProductID = viewModel.productos.FirstOrDefault().ProductID;

            viewModel.detallePedido = detallePedido;

            return viewModel;
        }
        //Rellenamos solamente los Productos, Pedidos y Navieras del ViewModel
        public PedidosProductosDetallePedidosViewModel RellenaViewModel(PedidosProductosDetallePedidosViewModel viewModel)
        {
            PedidosService pedidosService = null;
            pedidosService = new PedidosService();
            viewModel.pedidos = pedidosService.List(null, null);
            ProductosService productosService = null;
            productosService = new ProductosService();
            viewModel.productos = productosService.List(null);

            return viewModel;
        }
    }
}

