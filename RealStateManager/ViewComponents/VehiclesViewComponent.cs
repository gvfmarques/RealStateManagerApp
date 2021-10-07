using Microsoft.AspNetCore.Mvc;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.ViewComponents
{
    public class VehiclesViewComponent : ViewComponent
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesViewComponent(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return View(await _vehicleRepository.GetVehiclesByUser(id));
        }
    }
}
