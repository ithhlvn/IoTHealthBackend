using Microsoft.AspNetCore.Mvc;
using IOT.Models;
using IOT.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOT.Interfaces
{
    public interface IBloodPressureMonitorService
    {
        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        List<BloodPressureMonitor> LoadAll();

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BloodPressureMonitor GetById(int id);

        /// <summary>
        /// The GetByDeviceId
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        List<BloodPressureMonitor> GetByDeviceId(int deviceId);

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult Save(BloodPressureMonitor model);

        /// <summary>
        /// The SaveList (Insert & Update)
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        ApiResult SaveList(List<BloodPressureMonitor> models);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult Delete(int id);
    }
}
