using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phone_book.Models;
using Phone_book.Repositories;

namespace Phone_book.Controllers
{
    public class PersonController : Controller
    {
        private SourceManager _repo = new SourceManager();
        [HttpGet]
        public IActionResult Index(int page = 1,string filterLastName = "")
        {
            var repo = new SourceManager();
            List<PersonModel> list = _repo.Get();

            if (!String.IsNullOrWhiteSpace(filterLastName))
            {
                list = list.Where(q => q.LastName.StartsWith(filterLastName)).ToList();
            }

            var pageElements = 5;
            var pages = Math.Ceiling((decimal)list.Count() / pageElements);

            list = list.Skip((page - 1) * pageElements).Take(pageElements).ToList();

            ViewBag.Page = page;
            ViewBag.Pages = pages;
            ViewBag.FilterLastName = filterLastName;

            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PersonModel model)
        {
            var manager = new SourceManager();
            manager.Add(model);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var _repo = new SourceManager();
            var model = _repo.GetByID(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PersonModel model)
        {
            var _repo = new SourceManager();
            _repo.Update(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var _repo= new SourceManager();
            PersonModel model = _repo.GetByID(id);
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var _repo = new SourceManager();
            _repo.Remove(id);
            return RedirectToAction("Index");
        }
    }
}