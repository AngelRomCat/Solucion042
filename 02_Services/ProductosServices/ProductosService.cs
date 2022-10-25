using _05_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace _02_Services.ProductosServices
{
    public class ProductosService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public ProductosService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<Producto> List(int? id)
        {
            IList<Producto> productos = null;
            if (id == null || id < 1)
            {
                productos = _db.Producto
                            .Include(p => p.Categoria)
                            .Include(p => p.Proveedor)
                            .ToList();
            }
            else
            {
                productos = _db.Producto
                            .Include(p => p.Categoria)
                            .Include(p => p.Proveedor)
                            .Where(x => x.ProductID == id)
                            .ToList();
            }

            return productos;
        }
        //Details
        public Producto Detail(int id)
        {
            Producto producto = null;
            producto = _db.Producto
                                .Where(x => x.ProductID == id)
                                .FirstOrDefault();
            return producto;
        }
        //Create
        public bool Create(Producto producto)
        {
            bool ok = false;
            try
            {
                _db.Producto.Add(producto);
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
        public bool Edit(Producto producto)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla Producto que tiene el mismo id
                //que el objeto que ha creado la view
                Producto buscada = _db.Producto
                                    .Where(x => x.ProductID == producto.ProductID)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:
                //buscada.ProductID = producto.ProductID;
                buscada.ProductName = producto.ProductName;
                buscada.supplierID = producto.supplierID;
                buscada.CategoryID = producto.CategoryID;
                buscada.unit = producto.unit;
                buscada.Price = producto.Price;

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
        public bool Delete(Producto producto)
        {
            bool ok = false;
            try
            {
                _db.Producto.Remove(producto);
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
    }
}

