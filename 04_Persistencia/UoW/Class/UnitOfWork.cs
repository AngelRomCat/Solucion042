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
        private GenericRepository<Categoria> _categoriaRepository;
        private GenericRepository<DetallePedido> _detallepedidoRepository;
        private GenericRepository<Pedido> _pedidoRepository;
        private GenericRepository<Producto> _productoRepository;
        private GenericRepository<Proveedor> _proveedorRepository;

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
        public GenericRepository<Categoria> CategoriaRepository => _categoriaRepository ??
                                                             (_categoriaRepository = new GenericRepository<Categoria>(_context));
        public GenericRepository<DetallePedido> DetallePedidoRepository => _detallepedidoRepository ??
                                                             (_detallepedidoRepository = new GenericRepository<DetallePedido>(_context));
        public GenericRepository<Pedido> PedidoRepository => _pedidoRepository ??
                                                            (_pedidoRepository = new GenericRepository<Pedido>(_context));
        public GenericRepository<Producto> ProductoRepository => _productoRepository ??
                                                            (_productoRepository = new GenericRepository<Producto>(_context));
        public GenericRepository<Proveedor> ProveedorRepository => _proveedorRepository ??
                                                            (_proveedorRepository = new GenericRepository<Proveedor>(_context));


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
