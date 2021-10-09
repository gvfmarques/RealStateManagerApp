using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using RealStateManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceBuildingRepository _serviceBuildingRepository;
        private readonly IHistoricResourcesRepository _historicResourcesRepository;

        public ServicesController(IServiceRepository serviceRepository, IUserRepository userRepository, IServiceBuildingRepository serviceBuildingRepository, IHistoricResourcesRepository historicResourcesRepository)
        {
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _serviceBuildingRepository = serviceBuildingRepository;
            _historicResourcesRepository = historicResourcesRepository;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            User user = await _userRepository.GetUserByName(User);
            if (await _userRepository.VerifyIfUserInFunction(user, "Resident"))
            {
                return View(await _serviceRepository.GetServiceByUser(user.Id));
            }

            return View(await _serviceRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            User user = await _userRepository.GetUserByName(User);
            Service service = new Service
            {
                UserId = user.Id
            };

            return View(service);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Name,Value,Status,UserId")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.Status = StatusService.Pending;
                await _serviceRepository.Insert(service);
                TempData["NewRegister"] = $"Requested {service.Name} service";
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Service service = await _serviceRepository.GetById(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,Name,Value,Status,UserId")] Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _serviceRepository.Update(service);
                TempData["Updating"] = $"Updated {service.Name} Service";
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _serviceRepository.Delete(id);
            TempData["Exclusion"] = $"Deleted Service";
            return Json("Deleted Service");
        }

        [Authorize(Roles = "Manager,ResidentManager")]
        [HttpGet]
        public async Task<IActionResult> ApproveService(int id)
        {
            Service service = await _serviceRepository.GetById(id);
            ServiceApprovedViewModel viewModel = new ServiceApprovedViewModel
            {
                ServiceId = service.ServiceId,
                Name = service.Name
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Manager,ResidentManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveService(ServiceApprovedViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Service service = await _serviceRepository.GetById(viewModel.ServiceId);
                service.Status = StatusService.Accepted;
                await _serviceRepository.Update(service);

                ServiceBuilding serviceBuilding = new ServiceBuilding
                {
                    ServiceId = viewModel.ServiceId,
                    DateExecution = viewModel.Date
                };

                await _serviceBuildingRepository.Insert(serviceBuilding);

                HistoricResource hr = new HistoricResource
                {
                    Value = service.Value,
                    MonthId = serviceBuilding.DateExecution.Month,
                    Day = serviceBuilding.DateExecution.Day,
                    Year = serviceBuilding.DateExecution.Year,
                    Type = Types.Output
                };

                await _historicResourcesRepository.Insert(hr);
                TempData["NewRegister"] = $"{service.Name} successfully scaled";

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }


        [Authorize(Roles = "Manager,ResidentManager")]
        public async Task<IActionResult> RefuseService(int id)
        {
            Service service = await _serviceRepository.GetById(id);
            if (service == null)
                return NotFound();

            service.Status = StatusService.Refused;
            await _serviceRepository.Update(service);
            TempData["Exclusion"] = $"{service.Name} refused";

            return RedirectToAction(nameof(Index));
        }

    }
}
