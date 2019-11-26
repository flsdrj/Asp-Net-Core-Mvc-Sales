using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                return RedirectToAction(nameof(Error), new { message = "Registro não fornecido" });
            }

            var obj = _sellerService.GetbyId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Registro não encontrado" });
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return RedirectToAction(nameof(Error), new { message = "Registro não fornecido" });
            }

            var obj = _sellerService.GetbyId(id.Value);
            if (obj ==null)
            {
                return RedirectToAction(nameof(Error), new { message = "Registro não encontrado" });
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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Registro não fornecido" });
            }

            var obj = _sellerService.GetbyId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Registro não encontrado" });
            }

            List<Departament> departaments = _departamentService.GetAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departaments = departaments };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Registro incompativel" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };

            return View(viewModel);
        }
    }
}