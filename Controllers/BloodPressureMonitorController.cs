/********************************************************************************/
/* COPYRIGHT 										                            */
/* Authors reserve all rights even in the event of industrial property rights.  */
/* We reserve all rights of disposal such as copying and passing			    */
/* on to third parties. 										                */
/*													                            */
/* Description : Controller Api                                                 */
/* Developers : Leo Ho , Vietnam                                                */
/* -----------------------------------------------------------------------------*/
/* History 											                            */
/*													                            */
/* Started on : 10 Dec 2024  							                        */
/* Revision : 1.0.0.0 									  	                    */
/* Changed by :     									                        */
/* Change date :                                                                */
/* Changes : 								                                    */
/* Reasons :  										                            */
/* -----------------------------------------------------------------------------*/

using IOT.Models;
using IOT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IOT.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;

namespace IOT.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]  // Đảm bảo rằng chỉ người dùng có token hợp lệ mới có thể truy cập
    public class BloodPressureMonitorController : ControllerBase
    {
        private readonly ILogger<BloodPressureMonitorController> _logger;
        private readonly IBloodPressureMonitorService _service;

        // Inject ILogger vào constructor
        public BloodPressureMonitorController(ILogger<BloodPressureMonitorController> logger, IBloodPressureMonitorService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LoadAll()
        {
            // Log thông tin với Serilog Api
            //using (LogContext.PushProperty("UserId", userId))
            //{
            //    Log.Information("User {UserId} performed an action.", userId);
            //}
            _logger.LogInformation("Getting all blood pressure monitors data...");

            // Log thông tin với Serilog MongoDB
            Log.Information("This is an informational log message.");
            Log.Warning("This is a warning log message.");
            Log.Error("This is an error log message.");
            try
            {
                var model = _service.LoadAll();
                if (model == null)
                    return NoContent();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult LoadAllV1()
        {
            // Log thông tin với Serilog Api
            //using (LogContext.PushProperty("UserId", userId))
            //{
            //    Log.Information("User {UserId} performed an action.", userId);
            //}
            _logger.LogInformation("Getting all blood pressure monitors data...");

            // Log thông tin với Serilog MongoDB
            Log.Information("This is an informational log message.");
            Log.Warning("This is a warning log message.");
            Log.Error("This is an error log message.");

            _logger.LogInformation("Getting all blood pressure monitors data...");
            try
            {
                var model = _service.LoadAll();
                if (model == null)
                    return NoContent();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult LoadAllV2()
        {
            // Log thông tin với Serilog Api
            //using (LogContext.PushProperty("UserId", userId))
            //{
            //    Log.Information("User {UserId} performed an action.", userId);
            //}
            _logger.LogInformation("Getting all blood pressure monitors data...");

            // Log thông tin với Serilog MongoDB
            Log.Information("This is an informational log message.");
            Log.Warning("This is a warning log message.");
            Log.Error("This is an error log message.");

            _logger.LogInformation("Getting all blood pressure monitors data...");
            try
            {
                var model = _service.LoadAll();
                if (model == null)
                    return NoContent();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var model = _service.GetById(id);
                if (model == null)
                    return NoContent();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// The GetByDeviceId
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public List<BloodPressureMonitor> GetByDeviceId([FromQuery]int deviceId)
        {
            return _service.GetByDeviceId(deviceId);
        }

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Save(BloodPressureMonitor model)
        {
            try
            {
                var response = _service.Save(model);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// The SaveList (Insert & Update)
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SaveList(List<BloodPressureMonitor> models)
        {
            try
            {
                var response = _service.SaveList(models);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// The Delete  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _service.Delete(id);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
