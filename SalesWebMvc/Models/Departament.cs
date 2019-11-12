﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Departament
    {
        [Display (Name = "Identificador")]
        public int Id { get; set; }

        [Display (Name = "Nome") ]
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Departament()
        {
        }

        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller Seller)
        {
            Sellers.Add(Seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(Seller => Seller.TotalSales(initial, final));
        }
    }
}