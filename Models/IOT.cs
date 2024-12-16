/********************************************************************************/
/* COPYRIGHT 										                            */
/* Authors reserve all rights even in the event of industrial property rights.  */
/* We reserve all rights of disposal such as copying and passing			    */
/* on to third parties. 										                */
/*													                            */
/* Description : IOT Models                                                     */
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

using CoreLibs.Libs;
using IOT.Libs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IOT.Models
{
    public class Device : BaseNone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2)]
        public string Code { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DeviceType Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreateOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CreateById { get; set; }

        [Required]
        public CommonStatus Status { get; set; }

        [Required]
        public CommonAttribute Attribute { get; set; }

        public ICollection<DeviceData> DeviceData { get; set; }

        public ICollection<DeviceLog> DeviceLog { get; set; }

        public void CopyFrom(Device model)
        {
            Id = model.Id;
            Code = model.Code;
            Name = model.Name;
            Type = model.Type;

            CreateOn = model.CreateOn;
            CreateById = model.CreateById;
            Status = model.Status;
            Attribute = model.Attribute;
            DeviceData = model.DeviceData;
            DeviceLog = model.DeviceLog;
        }
        public void CopyNotNullFrom(Device info)
        {
            base.CopyNotNullFrom(info);
            if (info.Id != null) this.Id = info.Id;
            if (info.Code != null) this.Code = info.Code;
            if (info.Name != null) this.Name = info.Name;
            if (info.Type != null) this.Type = info.Type;
            if (info.CreateOn != null) this.CreateOn = info.CreateOn;
            if (info.CreateById != null) this.CreateById = info.CreateById;

            if (info.Status != null) this.Status = info.Status;
            if (info.Attribute != null) this.Attribute = info.Attribute;
            if (info.DeviceData != null) this.DeviceData = info.DeviceData;
        }
        public override bool Validate()
        {
            if (this.Name == null) return false;
            if (this.Type == null) return false;
            return base.Validate();
        }
    }

    public class BloodPressureMonitor : BaseNone
    {
        public BloodPressureMonitor()
        {
        }

        //Key
        [Key]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        // ID của thiết bị
        [Required]
        public int DeviceId { get; set; }

        // Chiều cao
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Height { get; set; }
        
        // Cân nặng
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Weight { get; set; }

        // Huyết áp tâm thu (Systolic Pressure)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double Systolic { get; set; }

        // Huyết áp tâm trương (Diastolic Pressure)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double Diastolic { get; set; }

        // Nhịp tim (Heart Rate)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int HeartRate { get; set; }

        // Nhiệt độ
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Temperature { get; set; }

        // SpO2
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SpO2 { get; set; }

        // Thời gian đo
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime MeasurementTime { get; set; }

        // Value Added
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        // Huyết áp trung bình (Mean Arterial Pressure)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double MeanArterialPressure { get; set; }

        // Tình trạng kết quả (Lỗi hoặc không chính xác)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }

        // Tình trạng kết quả (Lỗi hoặc không chính xác)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        //Ngày phát sinh bản ghi
        [Required]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreateOn { get; set; }

        //Người phát sinh
        [Required]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CreateById { get; set; }

        [Required]
        public CommonStatus Status { get; set; }

        [Required]
        public CommonAttribute Attribute { get; set; }

        // Phương thức tính Huyết áp trung bình
        public void CalculateMeanArterialPressure()
        {
            // Công thức tính MAP: MAP = Diastolic + (Systolic - Diastolic) / 3
            MeanArterialPressure = this.Diastolic + ((this.Systolic - this.Diastolic) / 3);
        }

        // Phương thức kiểm tra kết quả huyết áp có bình thường không
        public bool IsBloodPressureNormal()
        {
            return Systolic < 120 && Diastolic < 80;
        }

        // Phương thức kiểm tra kết quả nhịp tim có bình thường không
        public bool IsHeartRateNormal()
        {
            return HeartRate >= 60 && HeartRate <= 100;
        }

        public void CopyFrom(BloodPressureMonitor model)
        {
            Id = model.Id;
            DeviceId = model.DeviceId;
            Value = model.Value;
            Height = model.Height;
            Weight = model.Weight;
            Systolic = model.Systolic;
            Diastolic = model.Diastolic;
            HeartRate = model.HeartRate;
            MeanArterialPressure = model.MeanArterialPressure;
            Temperature = model.Temperature;
            SpO2 = model.SpO2;
            MeasurementTime = model.MeasurementTime;

            ErrorCode = model.ErrorCode;
            ErrorMessage = model.ErrorMessage;

            CreateOn = model.CreateOn;
            CreateById = model.CreateById;
            Status = model.Status;
            Attribute = model.Attribute;
        }
        public void CopyNotNullFrom(BloodPressureMonitor info)
        {
            base.CopyNotNullFrom(info);
            if (info.Id > 0) this.Id = info.Id;
            if (info.DeviceId != null) this.DeviceId = info.DeviceId;
            if (info.Value != null) this.Value = info.Value;
            if (info.Height != null) this.Height = info.Height;
            if (info.Weight != null) this.Weight = info.Weight;
            if (info.Systolic != null) this.Systolic = info.Systolic;
            if (info.Diastolic != null) this.Diastolic = info.Diastolic;
            if (info.HeartRate != null) this.HeartRate = info.HeartRate;
            if (info.Temperature != null) this.Temperature = info.Temperature;
            if (info.SpO2 != null) this.SpO2 = info.SpO2;
            if (info.MeasurementTime != null) this.MeasurementTime = info.MeasurementTime;

            if (info.ErrorCode != null) this.ErrorCode = info.ErrorCode;
            if (info.ErrorMessage != null) this.ErrorMessage = info.ErrorMessage;

            if (info.CreateOn != null) this.CreateOn = info.CreateOn;
            if (info.CreateById != null) this.CreateById = info.CreateById;

            if (info.Status != null) this.Status = info.Status;
            if (info.Attribute != null) this.Attribute = info.Attribute;
        }
        public override bool Validate()
        {
            if (this.DeviceId == null) return false;
            if (this.MeasurementTime == null) return false;
            return base.Validate();
        }
    }

    public class DeviceData : BaseNone
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DeviceId { get; set; }  // ID của thiết bị
        public string Value { get; set; }
        public double Temperature { get; set; }  // Dữ liệu nhiệt độ
        public double BloodPressure { get; set; }  // Dữ liệu huyết áp
        public double HeartRate { get; set; }  // Dữ liệu nhịp tim
        [Required]
        public DateTime Timestamp { get; set; }  // Thời gian thu thập dữ liệu

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreateOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CreateById { get; set; }

        [Required]
        public CommonStatus Status { get; set; }

        [Required]
        public CommonAttribute Attribute { get; set; }
        public Device Device { get; set; }

        public void CopyFrom(DeviceData model)
        {
            Id = model.Id;
            DeviceId = model.DeviceId;
            Value = model.Value;
            Temperature = model.Temperature;
            BloodPressure = model.BloodPressure;
            HeartRate = model.HeartRate;
            Timestamp = model.Timestamp;
            Status = model.Status;
            Attribute = model.Attribute;
            Device = model.Device;
        }
    }

    public class DeviceLog : BaseNone
    {
        public DeviceLog()
        {
        }
       
        [Key]
        public int Id { get; set; }
        [Required]
        public int DeviceId { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? OnDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? LabReqById { get; set; }

        [Required]
        public CommonStatus Status { get; set; }
        [Required]
        public CommonAttribute Attribute { get; set; }
        public Device Device { get; set; }

        public void CopyFrom(DeviceLog model)
        {
            Id = model.Id;
            DeviceId = model.DeviceId;
            Timestamp = model.Timestamp;
            Device = model.Device;
            Status = model.Status;
            Attribute = model.Attribute;
        }
    }

    public class Patient : BaseNone
    {
        public int PatientId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PatientCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SrcFullName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Dob { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public GenderType? Gender { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BloodType? BloodType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IdCardNo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MobileNo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string HomePhone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string WorkPhone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Nationality { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Ethnic { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string District { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Ward { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public short? Occupation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EmployerName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EmployerAddr { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TaxCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RelativeName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RelativeAddr { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RelativePhone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public short? RelativeType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MedHistPerson { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MedHistFamily { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PatientStatus? Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? ClientId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string HID { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SysCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PatientAttribute? Attribute { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? MergePatientId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? IdCardOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IdCardAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? EHRId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EHRUserName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? EHRCreateOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? EHRCreateById { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BloodDonationCard { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public short? BloodDonationCount { get; set; }

        public void CopyFrom(Patient info)
        {
            base.CopyFrom(info);
            this.PatientId = info.PatientId;
            this.PatientCode = info.PatientCode;

            this.Title = info.Title;
            this.FirstName = info.FirstName;
            this.LastName = info.LastName;
            this.SrcFullName = info.SrcFullName;
            this.Dob = info.Dob;
            this.Gender = info.Gender;
            this.BloodType = info.BloodType;

            this.IdCardNo = info.IdCardNo;
            this.Email = info.Email;
            this.MobileNo = info.MobileNo;
            this.HomePhone = info.HomePhone;
            this.WorkPhone = info.WorkPhone;

            this.Ethnic = info.Ethnic;
            this.Nationality = info.Nationality;
            this.Country = info.Country;
            this.City = info.City;
            this.District = info.District;
            this.Ward = info.Ward;
            this.Address = info.Address;

            this.Occupation = info.Occupation;
            this.TaxCode = info.TaxCode;

            this.EmployerName = info.EmployerName;
            this.EmployerAddr = info.EmployerAddr;

            this.RelativeName = info.RelativeName;
            this.RelativeAddr = info.RelativeAddr;
            this.RelativePhone = info.RelativePhone;
            this.RelativeType = info.RelativeType;

            this.Notes = info.Notes;
            this.MedHistPerson = info.MedHistPerson;
            this.MedHistFamily = info.MedHistFamily;

            this.Status = info.Status;

            this.ClientId = info.ClientId;

            this.HID = info.HID;
            this.SysCode = info.SysCode;

            this.Attribute = info.Attribute;
            this.MergePatientId = info.MergePatientId;
            this.IdCardOn = info.IdCardOn;
            this.IdCardAt = info.IdCardAt;

            this.EHRId = info.EHRId;
            this.EHRUserName = info.EHRUserName;
            this.EHRCreateOn = info.EHRCreateOn;
            this.EHRCreateById = info.EHRCreateById;

            this.BloodDonationCard = info.BloodDonationCard;
            this.BloodDonationCount = info.BloodDonationCount;
        }
        public void CopyNotNullFrom(Patient info)
        {
            base.CopyNotNullFrom(info);
            if (info.PatientId != null) this.PatientId = info.PatientId;
            if (info.PatientCode != null) this.PatientCode = info.PatientCode;

            if (info.FirstName != null) this.FirstName = info.FirstName;
            if (info.LastName != null) this.LastName = info.LastName;
            if (info.SrcFullName != null) this.SrcFullName = info.SrcFullName;
            if (info.Dob != null) this.Dob = info.Dob;
            if (info.Title != null) this.Title = info.Title;
            if (info.Gender != null) this.Gender = info.Gender;
            if (info.BloodType != null) this.BloodType = info.BloodType;

            if (info.IdCardNo != null) this.IdCardNo = info.IdCardNo;
            if (info.Email != null) this.Email = info.Email;
            if (info.MobileNo != null) this.MobileNo = info.MobileNo;
            if (info.HomePhone != null) this.HomePhone = info.HomePhone;
            if (info.WorkPhone != null) this.WorkPhone = info.WorkPhone;

            if (info.Ethnic != null) this.Ethnic = info.Ethnic;
            if (info.Nationality != null) this.Nationality = info.Nationality;
            if (info.Country != null) this.Country = info.Country;
            if (info.City != null) this.City = info.City;
            if (info.District != null) this.District = info.District;
            if (info.Ward != null) this.Ward = info.Ward;
            if (info.Address != null) this.Address = info.Address;

            if (info.Occupation != null) this.Occupation = info.Occupation;
            if (info.TaxCode != null) this.TaxCode = info.TaxCode;

            if (info.EmployerName != null) this.EmployerName = info.EmployerName;
            if (info.EmployerAddr != null) this.EmployerAddr = info.EmployerAddr;

            if (info.RelativeName != null) this.RelativeName = info.RelativeName;
            if (info.RelativeAddr != null) this.RelativeAddr = info.RelativeAddr;
            if (info.RelativePhone != null) this.RelativePhone = info.RelativePhone;
            if (info.RelativeType != null) this.RelativeType = info.RelativeType;

            if (info.Notes != null) this.Notes = info.Notes;
            if (info.MedHistPerson != null) this.MedHistPerson = info.MedHistPerson;
            if (info.MedHistFamily != null) this.MedHistFamily = info.MedHistFamily;

            if (info.Status != null) this.Status = info.Status;

            if (info.ClientId != null) this.ClientId = info.ClientId;

            if (info.HID != null) this.HID = info.HID;
            if (info.SysCode != null) this.SysCode = info.SysCode;

            if (info.Attribute != null) this.Attribute = info.Attribute;
            if (info.MergePatientId != null) this.MergePatientId = info.MergePatientId;
            if (info.IdCardOn != null) this.IdCardOn = info.IdCardOn;
            if (info.IdCardAt != null) this.IdCardAt = info.IdCardAt;

            if (info.EHRId != null) this.EHRId = info.EHRId;
            if (info.EHRUserName != null) this.EHRUserName = info.EHRUserName;
            if (info.EHRCreateOn != null) this.EHRCreateOn = info.EHRCreateOn;
            if (info.EHRCreateById != null) this.EHRCreateById = info.EHRCreateById;

            if (info.BloodDonationCard != null) this.BloodDonationCard = info.BloodDonationCard;
            if (info.BloodDonationCount != null) this.BloodDonationCount = info.BloodDonationCount;
        }

        public override bool Validate()
        {
            //if (string.IsNullOrEmpty(this.PatientCode)) return false;
            if (string.IsNullOrEmpty(this.FirstName)) return false;
            //if (string.IsNullOrEmpty(this.LastName)) return false; // Bug 12839: [Tiếp nhận] Bỏ ràng buộc đầy đủ họ và tên - cuongtk - 20231215
            if (string.IsNullOrEmpty(this.Country)) return false;
            if (string.IsNullOrEmpty(this.City)) return false;
            if (string.IsNullOrEmpty(this.Address)) return false;

            //if (this.LastName == null) return false; // Bug 12839: [Tiếp nhận] Bỏ ràng buộc đầy đủ họ và tên - cuongtk - 20231215
            if (this.Dob == null) return false;
            if (this.Gender == null) return false;
            if (this.Nationality == null) return false;
            //if (this.RelativeType == null) return false;
            if (this.Status == null) return false;
            return base.Validate();
        }
    }
}
