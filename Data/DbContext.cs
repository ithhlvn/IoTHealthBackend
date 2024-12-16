using Microsoft.EntityFrameworkCore;
using IOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibs.Libs;

namespace IOT.Data
{
    public class IoTHealthBackendContext : DbContext
    {
        public IoTHealthBackendContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Device> Device { get; set; }
        public DbSet<BloodPressureMonitor> BloodPressureMonitor { get; set; }
        public DbSet<DeviceData> DeviceData { get; set; }
        public DbSet<DeviceLog> DeviceLog { get; set; }
        public DbSet<Patient> Patient { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                //optionsBuilder.UseSqlServer(Libs.Database.HIS_System);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Models.Sys.ApiKey>(entity =>
            //{
            //    entity.HasKey(e => new { e.Type, e.SecKey });
            //});
            //modelBuilder.Entity<Models.Sys.SysKey>(entity =>
            //{
            //    entity.HasKey(e => e.Code);
            //});
            //modelBuilder.Entity<Models.Sys.RptConfigName>(entity =>
            //{
            //    entity.HasKey(e => e.RptId);
            //});
            //modelBuilder.Entity<Models.Sys.RptConfigColumn>(entity =>
            //{
            //    entity.HasKey(e => new { e.RptId, e.ColumnId });
            //});
            //modelBuilder.Entity<Models.Sys.RptConfigValue>(entity =>
            //{
            //    entity.HasKey(e => new { e.RptId, e.ServiceId });
            //});
            //modelBuilder.Entity<Models.Sys.RptConfigDev>(entity =>
            //{
            //    entity.HasKey(e => new { e.Id });
            //});
            //modelBuilder.Entity<Models.Sys.RptConfigValueDrug>(entity =>
            //{
            //    entity.HasKey(e => new { e.RptId, e.ItemId });
            //});
        }
    }
}
