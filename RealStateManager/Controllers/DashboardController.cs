using RealStateManager.DAL.Interface;
using RealStateManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    [Authorize(Roles = "Manager,ResidentManager")]
    public class DashboardController : Controller
    {
        private readonly IRentRepository _rentRepository;
        private readonly IHistoricResourcesRepository _historicResourcesRepository;

        public DashboardController(IRentRepository rentRepository, IHistoricResourcesRepository historicResourcesRepository)
        {
            _rentRepository = rentRepository;
            _historicResourcesRepository = historicResourcesRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Years"] = new SelectList(await _rentRepository.GetAllYears());
            return View();
        }

        public JsonResult GraphicDataGains(int year)
        {
            return Json(_historicResourcesRepository.GetHistoricIncomes(year));
        }

        public JsonResult GraphicDataExpenses(int year)
        {
            return Json(_historicResourcesRepository.GetHistoricExpense(year));
        }

        public async Task<JsonResult> GraphicDataExpensesTotalGains()
        {
            int year = DateTime.Now.Year;
            IncomesExpensesViewModel model = new IncomesExpensesViewModel
            {
                Expenses = await _historicResourcesRepository.GetSumExpenses(year),
                Incomes = await _historicResourcesRepository.GetSumIncomes(year)
            };

            return Json(model);

        }
    }
}
