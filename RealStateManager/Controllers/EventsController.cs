using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using RealStateManager.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public EventsController(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            User user = await _userRepository.GetUserByName(User);

            if (await _userRepository.VerifyIfUserInFunction(user, "Resident"))
            {
                return View(await _eventRepository.GetEventById(user.Id));
            }
            return View(await _eventRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            User user = await _userRepository.GetUserByName(User);
            Event evento = new Event
            {
                UserId = user.Id
            };

            return View(evento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,Date,UserId")] Event evento)
        {
            if (ModelState.IsValid)
            {
                await _eventRepository.Insert(evento);
                TempData["NewRegister"] = $"Event {evento.Name} inserted successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Event evento = await _eventRepository.GetById(id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Name,Date,UserId")] Event evento)
        {
            if (id != evento.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eventRepository.Update(evento);
                TempData["Updating"] = $"Event {evento.Name} updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }


        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _eventRepository.Delete(id);
            TempData["Deletion"] = $"Event deleted successfully!";
            return Json("Event deleted");
        }
    }
}
