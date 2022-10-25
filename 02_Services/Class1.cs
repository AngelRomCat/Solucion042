using _05_Data.Data;
using _05_Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Services
{
    public class Class1
    {
        private static NorthWindTuneadoDbContext _db = null;
        public Class1()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }
        //Index
        public IList<IList<Cliente>> List(int? id)
        {
            IList<Cliente> clientes = null;
            if (id == null || id < 1)
            {
                clientes = _db.Cliente.ToList();
            }
            else
            {
                clientes = _db.Cliente
                                .Where(x => x.CustomerID == id)
                                .ToList();
            }
            IList<IList<Cliente>> GruposDeGruposDeClientes = new List<IList<Cliente>>();
            foreach (int id_site in clientes.Select(x => x.id_site))
            {
                IList<Cliente> GrupoDeClientes = new List<Cliente>();
                foreach (Cliente cliente in clientes)
                {
                    if (cliente.id_site == id_site)
                    {
                        GrupoDeClientes.Add(cliente);
                    }
                }
                GruposDeGruposDeClientes.Add(GrupoDeClientes);
            }

            return GruposDeGruposDeClientes;
        }
    }
}
