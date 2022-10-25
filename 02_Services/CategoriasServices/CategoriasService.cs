using _05_Data.Data;
using _05_Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Services.CategoriasServices
{
    public class CategoriasService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public CategoriasService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<CategoriaDto> List(int? id)
        {
            IList<Categoria> categorias = null;
            if (id == null || id < 1)
            {
                categorias = _db.Categoria.ToList();
            }
            else
            {
                categorias = _db.Categoria
                                .Where(x => x.CategoryID == id)
                                .ToList();
            }

            IList<CategoriaDto> categoriaDtos = new List<CategoriaDto>();
            foreach (var categoria in categorias)
            {
                CategoriaDto categoriaDto = new CategoriaDto(categoria);
                categoriaDtos.Add(categoriaDto);
            }

            return categoriaDtos;
        }
        //Details
        public CategoriaDto Detail(int id)
        {
            Categoria categoria = null;
            categoria = _db.Categoria
                                .Where(x => x.CategoryID == id)
                                .FirstOrDefault();
            CategoriaDto categoriaDto = new CategoriaDto(categoria);
            return categoriaDto;
        }
        //Create
        public bool Create(CategoriaDto categoriaDto)
        {
            bool ok = false;
            try
            {
                Categoria categoria = new Categoria();
                categoria.CategoryID = categoriaDto.CategoryID;
                categoria.CategoryName = categoriaDto.CategoryName;
                categoria.Description = categoriaDto.Description;

                _db.Categoria.Add(categoria);
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
        public bool Edit(CategoriaDto categoriaDto)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla Categoria que tiene el mismo id
                //que el objeto que ha creado la view
                Categoria buscada = _db.Categoria
                                    .Where(x => x.CategoryID == categoriaDto.CategoryID)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:
                //buscada.CategoryID = categoria.CategoryID;
                buscada.CategoryName = categoriaDto.CategoryName;
                buscada.Description = categoriaDto.Description;

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
        public bool Delete(CategoriaDto categoriaDto)
        {
            bool ok = false;
            try
            {
                Categoria categoria = _db.Categoria.Where(x => x.CategoryID == categoriaDto.CategoryID).FirstOrDefault();

                _db.Categoria.Remove(categoria);
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
