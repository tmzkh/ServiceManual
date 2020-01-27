using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace EtteplanMORE.ServiceManual.ApplicationCore.AppContext {
    /// <summary>
    /// App database context
    /// </summary>
    public class FactoryContext : DbContext {

        public FactoryContext(DbContextOptions<FactoryContext> options) : base(options) { }

        public DbSet<FactoryDevice> FactoryDevices { get; set; }

        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FactoryDevice>()
                .Property(mt => mt.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<FactoryDevice>()
                .Property(mt => mt.Type)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<MaintenanceTask>()
                .HasKey(mt => mt.Id);
            modelBuilder.Entity<MaintenanceTask>()
                .HasOne(mt => mt.FactoryDevice);
            modelBuilder.Entity<MaintenanceTask>()
                .Property(mt => mt.FactoryDeviceId)
                .IsRequired();
            modelBuilder.Entity<MaintenanceTask>()
                .Property(mt => mt.Description)
                .HasMaxLength(500)
                .IsRequired();
            modelBuilder.Entity<MaintenanceTask>()
                .Property(mt => mt.Done)
                .HasDefaultValue(0)
                .HasColumnType("TINYINT(4)")
                .IsRequired();
            modelBuilder.Entity<MaintenanceTask>()
                .Property(mt => mt.Criticality)
                .IsRequired();

        }
    }
}
