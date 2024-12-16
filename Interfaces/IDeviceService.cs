using Microsoft.AspNetCore.Mvc;
using IOT.Models;
using IOT.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOT.Interfaces
{
    public interface IDeviceService
    {
        /// <summary>
        /// The Randomize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Randomize(byte value);

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        List<Device> LoadAll();

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Device GetById(int id);

        /// <summary>
        /// The GetByType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<Device> GetByType(short type);

        /// <summary>
        /// The save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult Save(Device model);

        /// <summary>
        /// The SaveList (Insert & Update)
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        ApiResult SaveList(List<Device> models);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult Delete(int id);
    }
}
