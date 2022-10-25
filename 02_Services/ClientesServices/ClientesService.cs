using _05_Data.Data;
using _05_Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Services.ClientesServices
{
    public class ClientesService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public ClientesService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }
        //Index
        public IList<ClienteDto> List(int? id)
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

            IList<ClienteDto> clienteDtos = new List<ClienteDto>();
            foreach (var cliente in clientes)
            {
                ClienteDto clienteDto = new ClienteDto(cliente);
                clienteDtos.Add(clienteDto);
            }

            return clienteDtos;
        }
        //Details
        public ClienteDto Detail(int id)
        {
            Cliente cliente = _db.Cliente
                                .Where(x => x.CustomerID == id)
                                .FirstOrDefault();
            ClienteDto clienteDto = new ClienteDto(cliente);
            return clienteDto;
        }
        //Create
        public bool Create(ClienteDto clienteDto)
        {
            bool ok = false;
            try
            {
                Cliente cliente = new Cliente();
                cliente.CustomerName = clienteDto.CustomerName;
                cliente.Address = clienteDto.Address;
                cliente.ContactName = clienteDto.ContactName;
                cliente.City = clienteDto.City;
                cliente.Country = clienteDto.Country;
                cliente.PostalCode = clienteDto.PostalCode;

                _db.Cliente.Add(cliente);
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
        public bool Edit(ClienteDto clienteDto)
        {
            bool ok = false;
            try
            {
                Cliente cliente = _db.Cliente
                                    .Where(x => x.CustomerID == clienteDto.CustomerID)
                                    .FirstOrDefault();
                ClienteDto buscada = new ClienteDto(cliente);

                buscada.CustomerName = clienteDto.CustomerName;
                buscada.ContactName = clienteDto.ContactName;
                buscada.City = clienteDto.City;
                buscada.Address = clienteDto.Address;
                buscada.PostalCode = clienteDto.PostalCode;
                buscada.Country = clienteDto.Country;

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
        public bool Delete(ClienteDto clienteDto)
        {
            bool ok = false;
            try
            {
                Cliente cliente = _db.Cliente.Where(x => x.CustomerID == clienteDto.CustomerID).FirstOrDefault();
                _db.Cliente.Remove(cliente);
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
