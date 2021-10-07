using Microsoft.AspNetCore.Mvc;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RealStateManager.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;

        public VehiclesController(IVehicleRepository vehicleRepository, IUserRepository userRepository)
        {
            _vehicleRepository = vehicleRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("vehicleId,Name,brand,Color,Plate,UserId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                User user = await _userRepository.GetUserByName(User);
                vehicle.UserId = user.Id;
                await _vehicleRepository.Insert(vehicle);
                return RedirectToAction("MyInformations", "Users");
            }

            return View(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleRepository.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,Name,Brand,Color,Plate,UserId")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _vehicleRepository.Update(vehicle);
                return RedirectToAction("MyInformations", "Users");
            }

            return View(vehicle);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _vehicleRepository.Delete(id);
            return Json("Vehicle deleted uccessfully");
        }
    }
}
