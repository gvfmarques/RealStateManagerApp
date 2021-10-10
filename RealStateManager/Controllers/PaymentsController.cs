using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRentRepository _rentRepository;
        private readonly IHistoricResourcesRepository _historicResourcesRepository;
        private readonly IUserRepository _userRepository;

        public PaymentsController(IPaymentRepository paymentRepository, IRentRepository rentRepository,
            IHistoricResourcesRepository historicResourcesRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _rentRepository = rentRepository;
            _historicResourcesRepository = historicResourcesRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _userRepository.GetUserByName(User);
            return View(await _paymentRepository.GetPaymentByUser(user.Id));
        }

        public async Task<IActionResult> MakePayment(int id)
        {
            Payment payment = await _paymentRepository.GetById(id);
            payment.DatePayment = DateTime.Now.Date;
            payment.Status = StatusPayment.PaidOut;
            await _paymentRepository.Update(payment);

            Rent rent = await _rentRepository.GetById(payment.RentId);

            HistoricResource hr = new HistoricResource
            {
                Value = rent.RentValue,
                MonthId = rent.MonthId,
                Day = DateTime.Now.Day,
                Year = rent.Year,
                Type = Types.Input
            };

            await _historicResourcesRepository.Insert(hr);
            TempData["NewRegister"] = $"Payment of {payment.Rent.RentValue} paid out.";
            return RedirectToAction(nameof(Index));
        }
    }
}
