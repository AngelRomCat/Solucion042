using _05_Data.Data;
using _05_Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using _04_Persistencia;
using _04_Persistencia.UoW.Class;
using _04_Persistencia.Repository.Class;

namespace _02_Services.EmpleadosServices
{
    public class EmpleadosService
    {
        //
        //ESTO SE HA DE QUITAR:
        private static NorthWindTuneadoDbContext _db = null;

        public EmpleadosService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }
        private readonly UnitOfWork unit = new UnitOfWork();

        //Index
        //Si bool? madre == null: Devolvemos todos los registros de la Tabla
        //Si bool? madre != null: Devolvemos todos los registros de la Tabla
        //que pertenecen al Registro de la Madre cuya id es id
        //Por eso devuelve un IList<EmpleadoRepository>
        public IEnumerable<Empleado> List(int? id, string madre)
        {
            IEnumerable<Empleado> empleados = null;
            if (id == null || id < 1)
            {
                //empleados = _db.Empleado.ToList();
                empleados = unit.EmpleadoRepository.GetAll();
            }
            else
            {
                if (madre == null)
                {
                    //empleados = _db.Empleado
                    //                .Where(x => x.EmployeeID == id)
                    //                .ToList();
                }
                //if (madre == "TablaMadre01")
                //{
                //    empleados = _db.Empleado
                //                    .Where(x => x.TablaMadre01_id == id)
                //                    .ToList();
                //}
                //if (madre == "TablaMadre02")
                //{
                //    empleados = _db.Empleado
                //                    .Where(x => x.TablaMadre02_id == id)
                //                    .ToList();
                //}
            }

            return empleados;
        }
        public IEnumerable<Empleado> ListApi(int? id, string madre)
        {
            IEnumerable<Empleado> empleadosTabla = List(id, madre);
            IList<Empleado> empleados = new List<Empleado>();
            foreach (var empleadoTabla in empleadosTabla)
            {
                Empleado empleado = SacarInfoDeMadresEHijas(empleadoTabla);

                empleados.Add(empleado);
            }

            return empleados;
        }
        //Details
        public Empleado Detail(int id)
        {
            Empleado empleado = null;
            //empleado = _db.Empleado
            //                    .Where(x => x.EmployeeID == id)
            //                    .FirstOrDefault();
            empleado = unit.EmpleadoRepository.FindById(id);
            return empleado;
        }
        public Empleado DetailApi(int id)
        {
            Empleado empleadoTabla = Detail(id);
            Empleado empleado = null;
            if (empleadoTabla != null)
            {
                empleado = SacarInfoDeMadresEHijas(empleadoTabla);
            }

            return empleado;
        }
        public Empleado DetailApi(int? id, int? siguiente)
        {
            Empleado empleadoTabla = null;
            Empleado empleado = null;
            if (id != null)
            {
                if (siguiente != null)
                {
                    if (siguiente.Value == 1)
                    {
                        empleadoTabla = _db.Empleado.Where(x => x.EmployeeID > id).FirstOrDefault();
                    }
                    else
                    {
                        IList<Empleado> empleadosTabla = _db.Empleado.Where(x => x.EmployeeID < id).ToList();
                        if (empleadosTabla != null && empleadosTabla.Count() > 0)
                        {
                            int? idEmpleado = empleadosTabla.Max(x => x.EmployeeID);
                            empleadoTabla = empleadosTabla.Where(x => x.EmployeeID == idEmpleado.Value).FirstOrDefault();
                        }
                    }
                }
                else //if (siguiente == null)
                {
                    empleadoTabla = Detail(id.Value);
                }
                if (empleadoTabla == null)
                {
                    empleadoTabla = Detail(id.Value);
                }
                else //if (empleadoTabla != null)
                {
                    empleado = SacarInfoDeMadresEHijas(empleadoTabla);
                }
            }

            return empleado;
        }
        //Create
        public bool Create(Empleado empleado)
        {
            bool ok = false;
            try
            {
                _db.Empleado.Add(empleado);
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
        public bool Edit(EmpleadoViewModel viewModel)
        {
            bool ok = false;
            try
            {
                DateTime? birthDate = null;
                if (viewModel != null && viewModel.birthDate != null && viewModel.birthDate != "")
                {
                    if (DateTime.TryParse(viewModel.birthDate, out DateTime result) == true)
                    {
                        birthDate = result;
                    }
                }
                Empleado buscada = _db.Empleado
                                    .Where(x => x.EmployeeID == viewModel.EmployeeID)
                                    .FirstOrDefault();

                buscada.EmployeeID = viewModel.EmployeeID;
                buscada.FirstName = viewModel.FirstName;
                buscada.LastName = viewModel.LastName;
                buscada.birthDate = birthDate;
                buscada.Photo = viewModel.Photo;
                buscada.Notes = viewModel.Notes;


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
        public bool Delete(Empleado empleado)
        {
            bool ok = false;
            try
            {
                _db.Empleado.Remove(empleado);
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
        //SacarInfoDeMadresEHijas para la API
        private Empleado SacarInfoDeMadresEHijas(Empleado empleadoTabla)
        {
            Empleado empleado = new Empleado();
            empleado.EmployeeID = empleadoTabla.EmployeeID;
            empleado.LastName = empleadoTabla.LastName;
            empleado.FirstName = empleadoTabla.FirstName;
            empleado.birthDate = empleadoTabla.birthDate;
            empleado.Photo = empleadoTabla.Photo;
            empleado.Notes = empleadoTabla.Notes;

            return empleado;
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
