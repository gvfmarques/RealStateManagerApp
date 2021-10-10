using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    [Authorize(Roles = "Manager")]
    public class FunctionsController : Controller
    {
        private readonly IFunctionRepository _functionRepository;

        public FunctionsController(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        // GET: Functions
        public async Task<IActionResult> Index()
        {
            return View(await _functionRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Id,Name,NormalizedName,ConcurrencyStamp")] Function function)
        {
            if (ModelState.IsValid)
            {
                await _functionRepository.AddFunction(function);
                TempData["NewRegister"] = $"Function {function.Name} added";
                return RedirectToAction(nameof(Index));
            }
            return View(function);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            Function function = await _functionRepository.GetById(id);
            if (function == null)
            {
                return NotFound();
            }
            return View(function);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,Id,Name,NormalizedName,ConcurrencyStamp")] Function function)
        {
            if (id != function.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _functionRepository.Update(function);
                TempData["Updating"] = $"Function {function.Name} Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(function);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            await _functionRepository.Delete(id);
            TempData["Exclusion"] = $"Function Deleted";
            return Json("Function Deleted");
        }
    }
}
