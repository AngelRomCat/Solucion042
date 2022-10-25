using _05_Data.Data;

namespace _05_Data.Dtos
{
    public class ClienteDto
    {
        //Constructor que recibe un Objeto entity
        public ClienteDto(Cliente entity)
        {
            //Como vemos:
            //La conversión entre la Entity y el ObjetoDto. Está codificada dentro del mismo ObjetoDto.
            if (entity == null) return;
            CustomerID = entity.CustomerID;
            CustomerName = entity.CustomerName;
            ContactName = entity.ContactName;
            Address = entity.Address;
            City = entity.City;
            PostalCode = entity.PostalCode;
            Country = entity.Country;
        }
        //Constructor que recibe un Objeto vacío
        public ClienteDto()
        {
        }
        //Atributos del Dto que se llaman igual que los de la Entity
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
