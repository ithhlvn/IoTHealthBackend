using System.ComponentModel;

namespace IOT.Libs
{
    public enum GenderType : byte { Male = 1, Female = 2, Unspecified = 3 }

    public enum BloodType : byte
    {
        [Description("A Dương")]
        A_Plus = 1,
        [Description("A Âm")]
        A_Minus = 2,
        [Description("B Dương")]
        B_Plus = 3,
        [Description("B Âm")]
        B_Minus = 4,
        [Description("O Dương")]
        O_Plus = 5,
        [Description("O Âm")]
        O_Minus = 6,
        [Description("AB Dương")]
        AB_Plus = 7,
        [Description("AB Âm")]
        AB_Minus = 8
    }
    public enum CommonAttribute : short { None = 1, New = 2, InProgress = 4, Done = 8, Approved = 16, Sync = 32, Delete = 64, CanceledApproved = 4096}
    public enum CommonStatus : byte { New = 0, Enable = 1, Disable = 2 }

    public enum PatientStatus : byte { Alive = 1, Dead = 2, Deleted = 3 }

    public enum PatientAttribute : byte // Bitwise
    {
        Merged = 1,                 //
        FinalizedSysCode = 2,       //Đã được cấp định danh y tế duy nhất (Mã BHXH)
        IsDefaultInPharmacy = 4,    //
        Simulation = 8,              //Bệnh nhân tạm cấp ở Kiosk
    }

    public enum DeviceType : short
    {
        //1. 10 - Nhóm thiết bị chẩn đoán
        [Description("Máy siêu âm")]
        Ultrasound = 10001,

        [Description("Máy X-quang")]
        XRay = 10002,

        [Description("Máy chụp cộng hưởng từ (MRI)")]
        MRI = 10003,

        [Description("Máy chụp cắt lớp vi tính (CT Scan)")]
        CTScan = 10004,

        [Description("Máy điện tim (ECG)")]
        ECG = 10005,

        [Description("Máy đo huyết áp")]
        BloodPressureMonitor = 10006,

        [Description("Máy đo nồng độ oxy trong máu")]
        PulseOximeter = 10007,

        [Description("Máy đo đường huyết")]
        GlucoseMeter = 10008,

        [Description("Máy nội soi")]
        Endoscopy = 10009,

        [Description("Máy siêu âm Doppler")]
        DopplerUltrasound = 10010,

        //2. 11 - Nhóm thiết bị điều trị
        [Description("Máy thở (Ventilator)")]
        Ventilator = 11001,

        [Description("Máy xạ trị")]
        RadiotherapyMachine = 11002,

        [Description("Máy hâm sữa")]
        MilkWarmer = 11003,

        [Description("Máy điện trị liệu")]
        ElectrotherapyMachine = 11004,

        [Description("Máy tạo oxy")]
        OxygenConcentrator = 11005,

        [Description("Máy xông mũi họng")]
        Nebulizer = 11006,

        [Description("Máy chiếu ánh sáng trị liệu")]
        LightTherapyMachine = 11007,

        [Description("Thiết bị trị liệu sóng xung kích")]
        ShockwaveTherapyDevice = 11008,
        
        //3. 12 - Nhóm thiết bị theo dõi sức khỏe (patient monitoring devices)
        [Description("Máy theo dõi bệnh nhân B155M")]
        PatientMonitorB155M = 12001,

        //4. 13 - Nhóm thiết bị hỗ trợ chăm sóc sức khỏe
        [Description("Máy trợ thính")]
        HearingAid = 13001,

        [Description("Máy trợ thở CPAP/BiPAP")]
        CPAP = 13002,

        [Description("Máy theo dõi nhịp tim")]
        HeartRateMonitor = 13003,

        [Description("Bộ hỗ trợ di chuyển (Walker)")]
        Walker = 13004,

        [Description("Giường bệnh thông minh")]
        SmartHospitalBed = 13005,

        [Description("Mặt nạ oxy")]
        OxygenMask = 13006,

        [Description("Ghế massage y tế")]
        MedicalMassageChair = 13007,

        //5. 14 - Nhóm thiết bị phẫu thuật
        [Description("Dao mổ điện")]
        ElectrosurgicalScalpel = 14001,

        [Description("Kính hiển vi phẫu thuật")]
        SurgicalMicroscope = 14002,

        [Description("Robot phẫu thuật")]
        SurgicalRobot = 14003,

        [Description("Máy mổ nội soi")]
        LaparoscopyEquipment = 14004,

        [Description("Máy mổ laser")]
        LaserSurgeryEquipment = 14005,

        [Description("Thiết bị cắt và may tự động")]
        Stapler = 14006,

        [Description("Máy gây mê kèm thở Carestation 650 A1")]
        AnesthesiaMachineCarestation650A1 = 14007,

        //6. 15 - Nhóm thiết bị phòng thí nghiệm
        [Description("Máy phân tích huyết học")]
        HematologyAnalyzer = 15001,

        [Description("Máy phân tích nước tiểu")]
        UrineAnalyzer = 15002,

        [Description("Thiết bị xét nghiệm vi sinh")]
        MicrobiologyLabEquipment = 15003,

        [Description("Máy PCR")]
        PCRMachine = 15004,

        [Description("Máy xét nghiệm ELISA")]
        ELISAMachine = 15005,

        //7. 16 - Nhóm thiết bị y tế gia đình
        [Description("Nhiệt kế điện tử")]
        DigitalThermometer = 16001,

        [Description("Máy massage")]
        MassageMachine = 16002,

        [Description("Bộ thử đường huyết")]
        BloodGlucoseTestKit = 16003,

        [Description("Máy đo huyết áp điện tử")]
        DigitalBloodPressureMonitor = 16004,

        [Description("Máy xông mũi họng cá nhân")]
        PersonalNebulizer = 16005,

        //8. 17 - Nhóm thiết bị hỗ trợ chăm sóc người cao tuổi
        [Description("Giường bệnh điều chỉnh")]
        AdjustableHospitalBed = 17001,

        [Description("Thiết bị theo dõi bệnh nhân từ xa")]
        RemotePatientMonitoringDevice = 17002,

        [Description("Bộ hỗ trợ ăn uống")]
        FeedingAids = 17003,

        [Description("Ghế vệ sinh y tế")]
        MedicalToiletChair = 17004,

        [Description("Hệ thống phát hiện ngã")]
        FallDetectionSystem = 17005,

        //9. 18 - Nhóm thiết bị phục hồi chức năng
        [Description("Xe lăn")]
        Wheelchair = 18001,

        [Description("Máy trị liệu bằng sóng siêu âm")]
        UltrasoundTherapyMachine = 18002,

        [Description("Thiết bị trị liệu TENS")]
        TENSDevice = 18003,

        [Description("Máy kéo dài cột sống")]
        SpinalTractionMachine = 18004,

        [Description("Băng tải tập đi")]
        GaitTrainer = 18005,

        //10. 19 - Nhóm thiết bị y tế khác
        [Description("Máy tiệt trùng")]
        Sterilizer = 19001,

        [Description("Bộ dụng cụ sơ cứu")]
        FirstAidKit = 19002,

        [Description("Máy tạo độ ẩm")]
        Humidifier = 19003,

        [Description("Nhiệt kế hồng ngoại không tiếp xúc")]
        NonContactInfraredThermometer = 19004
    }
}
