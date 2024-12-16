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

namespace IOT.Services
{
    public class DeviceService : IDeviceService
    {
        private IoTHealthBackendContext _context;
        public DeviceService(IoTHealthBackendContext context) => _context = context;

        /// <summary>
        /// The Randomize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Randomize(byte value)
        {
            Random random = new();
            int randomNumber = random.Next(0, 9); // Generates a number Deviceween 0 (inclusive) and 10 (exclusive)
            return value == randomNumber;
        }

        /// <summary>
        /// The LoadAll
        /// </summary>
        /// <returns></returns>
        public List<Device> LoadAll()
        {
            List<Device> rltList;
            try
            {
                rltList = _context.Set<Device>().ToList();
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
        public Device GetById(int id)
        {
            Models.Device Device;
            try
            {
                Device = _context.Find<Device>(id);
            }
            catch (Exception)
            {
                throw;
            }
            return Device;
        }

        /// <summary>
        /// The GetByType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Device> GetByType(short type)
        {
            return _context.Set<Device>()?.ToList()?.Where(x => x.Type == (DeviceType?)type)?.ToList();
        }

        /// <summary>
        /// The Save (Insert & Update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResult Save(Device model)
        {
            ApiResult response = new();
            try
            {
                Device existingRecord = GetById(model.Id);
                if (existingRecord != null)
                {
                    existingRecord.CopyNotNullFrom(model);
                    _context.Update<Device>(existingRecord);
                    response.Message = "Device Update Successfully";
                }
                else
                {
                    _context.Add<Device>(model);
                    response.Message = "Device Inserted Successfully";
                }
                _context.SaveChanges();
                response.Message = "Device saved Successfully";
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
        public ApiResult SaveList(List<Device> models)
        {
            ApiResult response = new ApiResult();
            try
            {
                // Lấy tất cả các Id từ danh sách models
                var ids = models.Select(m => m.Id).ToList();

                // Truy vấn cơ sở dữ liệu để lấy các đối tượng có Id tồn tại trong danh sách models
                var existingRecords = _context.Set<Device>()
                                              .Where(x => ids.Contains(x.Id))
                                              .ToList();

                // Tạo dictionary để tìm kiếm nhanh đối tượng trong cơ sở dữ liệu
                var existingRecordsDict = existingRecords.ToDictionary(x => x.Id);

                // Tạo danh sách đối tượng cần cập nhật và thêm mới
                var toUpdate = new List<Device>();
                var toInsert = new List<Device>();

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
                Device existingRecord = GetById(id);
                if (existingRecord != null)
                {
                    _context.Remove<Device>(existingRecord);
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
