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
using Microsoft.AspNetCore.Http.HttpResults;
using IOT.Data;
using IOT.Models;
using IOT.Interfaces;
using IOT.Libs;
using Microsoft.EntityFrameworkCore;

namespace IOT.Services
{
    public class BloodPressureMonitorService : IBloodPressureMonitorService
    {
        private IoTHealthBackendContext _context;
        public BloodPressureMonitorService(IoTHealthBackendContext context) => _context = context;

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        public List<BloodPressureMonitor> LoadAll()
        {
            List<BloodPressureMonitor> rltList;
            try
            {
                rltList = _context.Set<BloodPressureMonitor>().ToList();
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
        public BloodPressureMonitor GetById(int id)
        {
            Models.BloodPressureMonitor model;
            try
            {
                model = _context.Find<BloodPressureMonitor>(id);
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        /// <summary>
        /// The GetByDeviceId
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<BloodPressureMonitor> GetByDeviceId(int deviceId)
        {
            return _context.Set<BloodPressureMonitor>()?.ToList()?.Where(x => x.DeviceId == deviceId)?.ToList();
        }

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResult Save(BloodPressureMonitor model)
        {
            ApiResult response = new();
            try
            {
                BloodPressureMonitor existingRecord = GetById(model.Id);
                if (existingRecord != null)
                {
                    existingRecord.CopyNotNullFrom(model);

                    _context.Update<BloodPressureMonitor>(existingRecord);
                    response.Message = "Device Update Successfully";
                }
                else
                {
                    _context.Add<BloodPressureMonitor>(model);
                    response.Message = "Device Inserted Successfully";
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
        public ApiResult SaveList(List<BloodPressureMonitor> models)
        {
            ApiResult response = new();
            try
            {
                // Lấy tất cả các Id từ danh sách models
                var ids = models.Select(m => m.Id).ToList();

                // Truy vấn cơ sở dữ liệu để lấy các đối tượng có Id tồn tại trong danh sách models
                var existingRecords = _context.Set<BloodPressureMonitor>()
                                              .Where(x => ids.Contains(x.Id))
                                              .ToList();

                // Tạo dictionary để tìm kiếm nhanh đối tượng trong cơ sở dữ liệu
                var existingRecordsDict = existingRecords.ToDictionary(x => x.Id);

                // Tạo danh sách đối tượng cần cập nhật và thêm mới
                var toUpdate = new List<BloodPressureMonitor>();
                var toInsert = new List<BloodPressureMonitor>();

                // Lặp qua danh sách models và cập nhật hoặc thêm mới
                foreach (var model in models)
                {
                    if (existingRecordsDict.TryGetValue(model.Id, out var existingRecord))
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
                if (toInsert.Any())
                {
                    _context.AddRange(toInsert);
                }

                // Cập nhật các đối tượng đã tồn tại
                if (toUpdate.Any())
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

        #region The SaveList Using EFCore.BulkExtensions (Insert & Update)
        /*
        /// <summary>
        /// The SaveList Using EFCore.BulkExtensions (Insert & Update)
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public ResponseModel SaveLisBulk(List<BloodPressureMonitor> models)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // Lấy tất cả các ID từ danh sách models
                var ids = models.Select(m => m.Id).ToList();

                // Truy vấn cơ sở dữ liệu để lấy các đối tượng có Id tồn tại trong danh sách models
                var existingRecords = _context.Set<BloodPressureMonitor>()
                                              .Where(x => ids.Contains(x.Id))
                                              .ToList();

                // Tạo dictionary để tìm kiếm nhanh đối tượng trong cơ sở dữ liệu
                var existingRecordsDict = existingRecords.ToDictionary(x => x.Id);

                // Tạo danh sách đối tượng cần cập nhật và thêm mới
                var toUpdate = new List<BloodPressureMonitor>();
                var toInsert = new List<BloodPressureMonitor>();

                // Lặp qua danh sách models và cập nhật hoặc thêm mới
                foreach (var model in models)
                {
                    if (existingRecordsDict.TryGetValue(model.Id, out var existingRecord))
                    {
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

                // Sử dụng BulkExtensions để chèn hoặc cập nhật đối tượng
                if (toInsert.Any())
                {
                    // Bulk insert các đối tượng mới
                    _context.BulkInsert(toInsert);
                }

                if (toUpdate.Any())
                {
                    // Bulk update các đối tượng đã tồn tại
                    _context.BulkUpdate(toUpdate);
                }

                // Lưu tất cả thay đổi vào cơ sở dữ liệu
                response.IsSuccess = true;
                response.Message = "Devices Processed Successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error : " + ex.Message;
            }
            return response;
        }
        */
        #endregion The SaveList Using EFCore.BulkExtensions (Insert & Update)

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResult Delete(int id)
        {
            ApiResult model = new();
            try
            {
                BloodPressureMonitor existingRecord = GetById(id);
                if (existingRecord != null)
                {
                    _context.Remove<BloodPressureMonitor>(existingRecord);
                    _context.SaveChanges();
                    model.Message = "Device Deleted Successfully";
                }
                else
                {
                    model.Message = "Device Not Found";
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
