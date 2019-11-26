﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;

        public SellersController(SellerService sellerService, DepartamentService departamentService)
        {
            _departamentService = departamentService;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.GetAll();

            return View(list);
        }
        public IActionResult Create()
        {
            var departaments = _departamentService.GetAll();
            var viewModel = new SellerFormViewModel { Departaments = departaments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller Seller)
        {
            _sellerService.AddSeller(Seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.GetbyId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var obj = _sellerService.GetbyId(id.Value);
            if (obj ==null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}