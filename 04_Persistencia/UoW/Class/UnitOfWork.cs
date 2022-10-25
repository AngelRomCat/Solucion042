using _04_Persistencia.Repository.Class;
using _05_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Persistencia.UoW.Class
{
    public class UnitOfWork
    {
        private readonly NorthWindTuneadoDbContext _context;

        private GenericRepository<Cliente> ClienteRepository;

        private bool _disposed;

        public UnitOfWork()
        {
            _context = new NorthWindTuneadoDbContext();
        }

        //Repositories

        public GenericRepository<Cliente> ClienteRepository => clienteRepository ??
                                                             (clienteRepository = new GenericRepository<Cliente>(_context));

        ////Esto también se puede poner así en forma de PROPERTY que da valor al 
        ////private Atributo clienteRepository 
        ////mediante un Getter:
        //GenericRepository<Cliente> ClienteRepository
        //{
        //    get
        //    {
        //        return clienteRepository ?? (clienteRepository = new GenericRepository<Cliente>(_context));
        //    }
        //}

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
