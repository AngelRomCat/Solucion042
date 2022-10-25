using _05_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Services.NavierasServices
{
    public class NavierasService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public NavierasService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<Naviera> List(int? id)
        {
            IList<Naviera> navieras = null;
            if (id == null || id < 1)
            {
                navieras = _db.Naviera.ToList();
            }
            else
            {
                navieras = _db.Naviera
                                .Where(x => x.shipperID == id)
                                .ToList();
            }

            return navieras;
        }
    }
}
