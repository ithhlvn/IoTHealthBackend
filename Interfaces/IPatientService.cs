using Microsoft.AspNetCore.Mvc;
using IOT.Models;
using IOT.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOT.Interfaces
{
    public interface IPatientService
    {
        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        List<Patient> LoadAll();

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Patient GetById(int id);

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult Save(Patient model);

        /// <summary>
        /// The SaveList (Insert & Update)
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        ApiResult SaveList(List<Patient> models);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult Delete(int id);
    }
}
