using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using RealStateManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironmnet;

        public UserController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _webHostEnvironmnet = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(ViewModelRegister model, IFormFile picture)
        {
            if(ModelState.IsValid)
            {
                if(picture != null)
                {
                    string directoryFolder = Path.Combine(_webHostEnvironmnet.WebRootPath, "Images");
                    string namePicture = Guid.NewGuid().ToString() + picture.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(directoryFolder, namePicture), FileMode.Create))
                    {
                        await picture.CopyToAsync(fileStream);
                        model.Picture = "~/Images/" + namePicture;
                    }
                }

                User user = new User();
                IdentityResult userCreated;

                if(_userRepository.VerifyIfRegisterExist() == 0)
                {
                    user.UserName = model.Name;
                    user.Identification = model.Identification;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Phone;
                    user.Picture = model.Picture;
                    user.FirstAccess = false;
                    user.Status = StatusAccount.Approved;

                    userCreated = await _userRepository.CreateUser(user, model.Password);

                    if(userCreated.Succeeded)
                    {
                        await _userRepository.IncludeUserInFunction(user, "Manager");
                        await _userRepository.LoginUser(user, false);
                        return RedirectToAction("Index", "User");
                    }
                }

                user.UserName = model.Name;
                user.Identification = model.Identification;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.Picture = model.Picture;
                user.FirstAccess = true;
                user.Status = StatusAccount.Analysing;

                userCreated = await _userRepository.CreateUser(user, model.Password);

                if(userCreated.Succeeded)
                {
                    return View("Analyse", user.UserName);
                }

                else
                {
                    foreach(IdentityError error in userCreated.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

            }
            return View(model);
        }

        public IActionResult Analyse(string name)
        {
            return View(name);
        }
    }
}
