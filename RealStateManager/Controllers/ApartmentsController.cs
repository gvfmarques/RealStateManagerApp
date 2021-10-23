using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    [Authorize(Roles = "Manager,ResidentManager")]
    public class ApartmentsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUserRepository _userRepository;

        public ApartmentsController(IWebHostEnvironment webHostEnvironment, IApartmentRepository apartmentRepository, IUserRepository userRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _userRepository.GetUserByName(User);

            if (await _userRepository.VerifyIfUserInFunction(user, "ResidentManager"))
            {
                return View(await _apartmentRepository.GetAll());
            }

            return View(await _apartmentRepository.GetApartmentByUser(user.Id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["ApartmentResidentId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName");
            ViewData["ApartmentOwnerId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,Number,Floor,Picture,ApartmentResidentId,ApartmentOwnerId")] Apartment apartment, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    string directory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string pictureName = Guid.NewGuid().ToString() + picture.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(directory, pictureName), FileMode.Create))
                    {
                        await picture.CopyToAsync(fileStream);
                        apartment.Picture = "~/Images/" + pictureName;
                    }
                }

                await _apartmentRepository.Insert(apartment);
                TempData["NewRegister"] = $"Apartment number {apartment.Number} registered successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentResidentId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName", apartment.ApartmentResidentId);
            ViewData["ApartmentOwnerId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName", apartment.ApartmentOwnerId);
            return View(apartment);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Apartment apartment = await _apartmentRepository.GetById(id);
            if (apartment == null)
            {
                return NotFound();
            }
            TempData["Picture"] = apartment.Picture;
            ViewData["ApartmentResidentId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName", apartment.ApartmentResidentId);
            ViewData["ApartmentOwnerId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName", apartment.ApartmentOwnerId);
            return View(apartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentId,Number,Floor,Picture,ApartmentResidentId,ApartmentOwnerId")] Apartment apartment, IFormFile picture)
        {
            if (id != apartment.ApartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    string directory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string namePicture = Guid.NewGuid().ToString() + picture.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(directory, namePicture), FileMode.Create))
                    {
                        await picture.CopyToAsync(fileStream);
                        apartment.Picture = "~/Images/" + namePicture;
                        System.IO.File.Delete(TempData["Picture"].ToString().Replace("~", "wwwroot"));
                    }
                }
                else
                    apartment.Picture = TempData["Picture"].ToString();

                await _apartmentRepository.Update(apartment);
                TempData["Updating"] = $"Apartmento number {apartment.Number} Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentResidentId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName", apartment.ApartmentResidentId);
            ViewData["ApartmentOwnerId"] = new SelectList(await _userRepository.GetAll(), "Id", "UserName", apartment.ApartmentOwnerId);
            return View(apartment);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            Apartment apartment = await _apartmentRepository.GetById(id);
            System.IO.File.Delete(apartment.Picture.Replace("~", "wwwroot"));
            await _apartmentRepository.Delete(apartment);
            TempData["Exclusion"] = $"Apartment number {apartment.Number} Deleted Successfully";
            return Json("Apartment Deleted Successfully");
        }
    }
}
