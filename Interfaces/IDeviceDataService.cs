using Microsoft.AspNetCore.Mvc;
using IOT.Models;
using IOT.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOT.Interfaces
{
    public interface IDeviceDataService
    {
        /// <summary>
        /// The Load All
        /// </summary>
        /// <returns></returns>
        List<DeviceData> LoadAll();

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DeviceData GetById(int id);

        /// <summary>
        /// The GetByDeviceId
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        List<DeviceData> GetByDeviceId(int deviceId);

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult Save(DeviceData model);

        /// <summary>
        /// The SaveList (Insert & Update)
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        ApiResult SaveList(List<DeviceData> models);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult Delete(int id);
    }
}
