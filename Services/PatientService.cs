/********************************************************************************/
/* COPYRIGHT 										                            */
/* Authors reserve all rights even in the event of industrial property rights.  */
/* We reserve all rights of disposal such as copying and passing			    */
/* on to third parties. 										                */
/*													                            */
/* Description : Service Access                                                 */
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

using System;
using System.Collections.Generic;
using System.Linq;
using IOT.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IOT.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using IOT.Data;
using IOT.Interfaces;
using CoreLibs.Libs;
using ApiResult = IOT.ViewModels.ApiResult;

namespace IOT.Services
{
    public class PatientService : IPatientService
    {
        private IoTHealthBackendContext _context;
        public PatientService(IoTHealthBackendContext context) => _context = context;

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        /// <copyright>LeoHo, (c) 20241212</copyright>
        public List<Patient> LoadAll()
        {
            List<Patient> rltList;
            try
            {
                rltList = _context.Set<Patient>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return rltList;
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <copyright>LeoHo, (c) 20241212</copyright>
        public Patient GetById(int id)
        {
            Models.Patient Patient;
            try
            {
                Patient = _context.Find<Patient>(id);
            }
            catch (Exception)
            {
                throw;
            }
            return Patient;
        }

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <copyright>LeoHo, (c) 20241212</copyright>
        public ApiResult Save(Patient model)
        {
            ApiResult response = new();
            try
            {
                Patient existingRecord = GetById(model.PatientId);
                if (existingRecord != null)
                {
                    existingRecord.CopyNotNullFrom(model);
                    _context.Update<Patient>(existingRecord);
                    response.Message = "Patient Update Successfully";
                }
                else
                {
                    _context.Add<Patient>(model);
                    response.Message = "Patient Inserted Successfully";
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = "Error : " + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// The SaveList (Insert & Update)
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <copyright>LeoHo, (c) 20241212</copyright>
        public ApiResult SaveList(List<Patient> models)
        {
            ApiResult response = new();
            try
            {
                // Lấy tất cả các Id từ danh sách models
                var ids = models.Select(m => m.PatientId).ToList();

                // Truy vấn cơ sở dữ liệu để lấy các đối tượng có Id tồn tại trong danh sách models
                var existingRecords = _context.Set<Patient>()
                                              .Where(x => ids.Contains(x.PatientId))
                                              .ToList();

                // Tạo dictionary để tìm kiếm nhanh đối tượng trong cơ sở dữ liệu
                var existingRecordsDict = existingRecords.ToDictionary(x => x.PatientId);

                // Tạo danh sách đối tượng cần cập nhật và thêm mới
                var toUpdate = new List<Patient>();
                var toInsert = new List<Patient>();

                // Lặp qua danh sách models và cập nhật hoặc thêm mới
                foreach (var model in models)
                {
                    if (existingRecordsDict.TryGetValue(model.PatientId, out var existingRecord))
                    {
                        // Cập nhật thông tin nếu đối tượng đã tồn tại
                        existingRecord.CopyNotNullFrom(model);

                        // Thêm vào danh sách cần cập nhật
                        toUpdate.Add(existingRecord);
                    }
                    else
                    {
                        // Thêm mới đối tượng nếu chưa tồn tại
                        toInsert.Add(model);
                    }
                }

                // Thêm mới các đối tượng vào cơ sở dữ liệu
                if (toInsert.HasValue())
                {
                    _context.AddRange(toInsert);
                }

                // Cập nhật các đối tượng đã tồn tại
                if (toUpdate.HasValue())
                {
                    _context.UpdateRange(toUpdate);
                }

                // Lưu tất cả thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                response.Message = "Devices Processed Successfully";
            }
            catch (Exception ex)
            {
                response.Message = "Error : " + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <copyright>LeoHo, (c) 20241212</copyright>
        public ApiResult Delete(int id)
        {
            ApiResult model = new();
            try
            {
                Patient existingRecord = GetById(id);
                if (existingRecord != null)
                {
                    _context.Remove<Patient>(existingRecord);
                    _context.SaveChanges();
                    model.Message = "Patient Deleted Successfully";
                }
                else
                {
                    model.Message = "Patient Not Found";
                }
            }
            catch (Exception ex)
            {
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}
