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
using System.ComponentModel.DataAnnotations;
using IOT.Interfaces;

namespace IOT.Services
{
    public class DeviceDataService : IDeviceDataService
    {
        private IoTHealthBackendContext _context;
        public DeviceDataService(IoTHealthBackendContext context) => _context = context;

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        public List<DeviceData> LoadAll()
        {
            List<DeviceData> rltList;
            try
            {
                rltList = _context.Set<DeviceData>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return rltList;
        }

        /// <summary>
        /// The BetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DeviceData GetById(int id)
        {
            Models.DeviceData model;
            try
            {
                model = _context.Find<DeviceData>(id);
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
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public List<DeviceData> GetByDeviceId(int deviceId)
        {
            return _context.Set<DeviceData>()?.ToList()?.Where(p => p.DeviceId == deviceId)?.ToList();
        }

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="DeviceDataModel"></param>
        /// <returns></returns>
        public ApiResult Save(DeviceData model)
        {
            ApiResult response = new();
            try
            {
                DeviceData existingRecord = GetById(model.Id);
                if (existingRecord != null)
                {
                    existingRecord.CopyNotNullFrom(model);

                    _context.Update<DeviceData>(existingRecord);
                    response.Message = "DeviceData Update Successfully";
                }
                else
                {
                    _context.Add<DeviceData>(model);
                    response.Message = "DeviceData Inserted Successfully";
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
        public ApiResult SaveList(List<DeviceData> models)
        {
            ApiResult response = new();
            try
            {
                // Lấy tất cả các Id từ danh sách models
                var ids = models.Select(m => m.Id).ToList();

                // Truy vấn cơ sở dữ liệu để lấy các đối tượng có Id tồn tại trong danh sách models
                var existingRecords = _context.Set<DeviceData>()
                                              .Where(x => ids.Contains(x.Id))
                                              .ToList();

                // Tạo dictionary để tìm kiếm nhanh đối tượng trong cơ sở dữ liệu
                var existingRecordsDict = existingRecords.ToDictionary(x => x.Id);

                // Tạo danh sách đối tượng cần cập nhật và thêm mới
                var toUpdate = new List<DeviceData>();
                var toInsert = new List<DeviceData>();

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

                response.Message = "DeviceData Processed Successfully";
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
        public ApiResult Delete(int id)
        {
            ApiResult model = new ApiResult();
            try
            {
                DeviceData existingRecord = GetById(id);
                if (existingRecord != null)
                {
                    _context.Remove<DeviceData>(existingRecord);
                    _context.SaveChanges();
                    model.Message = "DeviceData Deleted Successfully";
                }
                else
                {
                    model.Message = "DeviceData Not Found";
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
