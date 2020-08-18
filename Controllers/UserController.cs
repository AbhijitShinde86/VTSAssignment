using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VTS_User_Service.Models;
using VTS_User_Service.Repositories;

namespace VTS_User_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IHostingEnvironment _environment;

        public UserController(IUserRepository userRepository, IHostingEnvironment environment)
        {
            this._userRepository = userRepository;
            this._environment = environment;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<ResultModel> GetUsers()
        {
            try
            {
                var data = await _userRepository.GetUsersAsync();
                return ResponceModel.GetResponse(data);
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return ResponceModel.GetExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultModel> AddUser([FromBody] User user)
        {
            try
            {
                int response = await _userRepository.AddUserAsync(user);
                return ResponceModel.GetSavedResponse(response > 0, response);
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return ResponceModel.GetExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<ResultModel> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                bool response = await _userRepository.UpdateUserAsync(id, user);
                return ResponceModel.GetUpdatedResponse(response, id);
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return ResponceModel.GetExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ResultModel> UploadProfileImage(int id, [FromForm] IFormFile file)
        {
            try
            {
                string fName = file.FileName;
                string imagesPath = @"ProfileImages/" + file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, imagesPath);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                bool response = await _userRepository.UploadProfileImageAsync(id, imagesPath);
                return ResponceModel.GetUpdatedResponse(response, id);

            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return ResponceModel.GetExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }

    }
}