using _04_Persistencia.Repository.Class;
using _05_Data.Data;
using System;

namespace _04_Persistencia.UoW.Class
{
    public class UnitOfWork
    {
        private readonly NorthWindTuneadoDbContext _context;

        private GenericRepository<Cliente> _clienteRepository;
        private GenericRepository<Empleado> _empleadoRepository;
        private GenericRepository<Naviera> _navieraRepository;

        private bool _disposed;

        public UnitOfWork()
        {
            _context = new NorthWindTuneadoDbContext();
        }

        //Repositories

        public GenericRepository<Cliente> ClienteRepository => _clienteRepository ??
                                                             (_clienteRepository = new GenericRepository<Cliente>(_context));

        ////Esto también se puede poner así en forma de PROPERTY que da valor al 
        ////private Atributo clienteRepository 
        ////mediante un Getter:
        //GenericRepository<Cliente> ClienteRepository
        //{
        //    get
        //    {
        //        return _clienteRepository ?? (_clienteRepository = new GenericRepository<Cliente>(_context));
        //    }
        //}

        public GenericRepository<Empleado> EmpleadoRepository => _empleadoRepository ??
                                                             (_empleadoRepository = new GenericRepository<Empleado>(_context));
        public GenericRepository<Naviera> NavieraRepository => _navieraRepository ??
                                                             (_navieraRepository = new GenericRepository<Naviera>(_context));

        //public GenericRepository<Cliente> ClienteRepository => _clienteRepository ??
        //                                                     (_clienteRepository = new GenericRepository<Cliente>(_context));

        //public GenericRepository<Cliente> ClienteRepository => _clienteRepository ??
        //                                                     (_clienteRepository = new GenericRepository<Cliente>(_context));

        //public GenericRepository<Cliente> ClienteRepository => _clienteRepository ??
        //                                                     (_clienteRepository = new GenericRepository<Cliente>(_context));

        //public GenericRepository<Cliente> ClienteRepository => _clienteRepository ??
        //                                                     (_clienteRepository = new GenericRepository<Cliente>(_context));

        //public GenericRepository<Cliente> ClienteRepository => _clienteRepository ??
        //                                                     (_clienteRepository = new GenericRepository<Cliente>(_context));

        //public GenericRepository<Cliente> ClienteRepository => _clienteRepository ??
        //                                                     (_clienteRepository = new GenericRepository<Cliente>(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing) _context.Dispose();
            _disposed = true;
        }
    }

}
