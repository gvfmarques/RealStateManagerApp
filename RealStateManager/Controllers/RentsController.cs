using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
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
    public class RentsController : Controller
    {

        private readonly IRentRepository _rentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMonthRepository _monthRepository;

        public RentsController(IRentRepository rentRepository, IUserRepository userRepository, IPaymentRepository paymentRepository, IMonthRepository monthRepository)
        {
            _rentRepository = rentRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _monthRepository = monthRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _rentRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,RentValue,MonthId,Year")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                if (!_rentRepository.RentExists(rent.MonthId, rent.Year))
                {
                    await _rentRepository.Insert(rent);
                    IEnumerable<User> users = await _userRepository.GetAll();
                    Payment payment;
                    List<Payment> payments = new List<Payment>();

                    foreach (User u in users)
                    {
                        payment = new Payment
                        {
                            RentId = rent.RentId,
                            UserId = u.Id,
                            DatePayment = null,
                            Status = StatusPayment.Pending
                        };

                        payments.Add(payment);
                    }

                    await _paymentRepository.Insert(payments);
                    TempData["NewRegister"] = $"Rent payment {rent.RentValue} for {rent.MonthId} year {rent.Year} added";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Rent Already exists");
                    ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", rent.MonthId);
                    return View(rent);
                }

            }
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", rent.MonthId);
            return View(rent);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Rent rent = await _rentRepository.GetById(id);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", rent.MonthId);
            return View(rent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentId,RentValue,MonthId,Year")] Rent rent)
        {
            if (id != rent.RentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _rentRepository.Update(rent);
                TempData["Updating"] = $"Rent for {rent.MonthId} year {rent.Year} updated";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(await _monthRepository.GetAll(), "MonthId", "Name", rent.MonthId);
            return View(rent);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _rentRepository.Delete(id);
            TempData["Exclusion"] = $"Rent Deleted";
            return Json("Rent deleted");
        }
    }
}
