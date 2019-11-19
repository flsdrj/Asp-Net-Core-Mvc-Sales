﻿using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;


namespace SalesWebMvc.Services
{
    public class DepartamentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartamentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Departament> GetAll()
        {
            return _context.Departament.OrderBy(x => x.Name).ToList();
        }
    }
}
