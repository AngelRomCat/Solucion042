using _05_Data.Data;
using System.Collections.Generic;
using System.Linq;

namespace _02_Services.ProveedoresServices
{
    public class ProveedoresService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public ProveedoresService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<Proveedor> List(int? id)
        {
            IList<Proveedor> proveedores = null;
            if (id == null || id < 1)
            {
                proveedores = _db.Proveedor.ToList();
            }
            else
            {
                proveedores = _db.Proveedor
                                .Where(x => x.supplierID == id)
                                .ToList();
            }

            return proveedores;
        }
    }
}
