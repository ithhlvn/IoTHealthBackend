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

namespace IOT.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceDataController : ControllerBase
    {
        private readonly ILogger<DeviceDataController> _logger;
        private readonly IDeviceDataService _service;

        // Inject ILogger vào constructor
        public DeviceDataController(ILogger<DeviceDataController> logger, IDeviceDataService service)
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
            try
            {
                var models = _service.LoadAll();
                if (models == null)
                    return NoContent();
                return Ok(models);
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
        public List<DeviceData> GetByDeviceId([FromQuery]int deviceId)
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
        public IActionResult Save(DeviceData model)
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
        public IActionResult SaveList(List<DeviceData> models)
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
