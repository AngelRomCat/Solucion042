using _05_Data.Data;
using System.Collections.Generic;

namespace _05_Data.ViewModels
{
    public class EmpleadoViewModel
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string birthDate { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }

        public ICollection<Pedido> Pedido { get; set; }
    }
}
