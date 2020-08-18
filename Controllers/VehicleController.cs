using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class VehicleController : ControllerBase
    {
        private IVehicleRepository _vehicleRepository;
        private IHostingEnvironment _environment;

        public VehicleController(IVehicleRepository vehicleRepository, IHostingEnvironment environment)
        {
            this._vehicleRepository = vehicleRepository;
            this._environment = environment;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<ResultModel> GetVehicles()
        {
            try
            {
                var data = await _vehicleRepository.GetVehiclesAsync();
                return ResponceModel.GetResponse(data);
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return ResponceModel.GetExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResultModel> SearchVehicles(Vehicle vehicle, int PageSize=10, int PageNo=1 )
        {
            try
            {
                var data = await _vehicleRepository.GetSearchVehiclesAsync(PageSize, PageNo, vehicle);
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
        public async Task<ResultModel> AddVehicle([FromBody] Vehicle vehicle)
        {
            try
            {
                int response = await _vehicleRepository.AddVehicleAsync(vehicle);
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
        public async Task<ResultModel> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            try
            {
                bool response = await _vehicleRepository.UpdateVehicleAsync(id, vehicle);
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