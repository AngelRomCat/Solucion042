using _05_Data.Data;
using _05_Data.Dtos;
using System.Collections.Generic;

namespace _05_Data.ViewModels
{
    public class ClientesEmpleadosNavierasPedidoViewModel
    {
        //public IList<Cliente> clientes { get; set; }
        public IList<ClienteDto> clienteDtos { get; set; }
        public IList<Empleado> empleados { get; set; }
        public IList<Naviera> navieras { get; set; }
        public Pedido pedido { get; set; }
    }
}
