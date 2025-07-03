using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProgramPLC.DTOs;
using ProgramPLC.Models;

namespace ProgramPLC.Database;

public partial class AnalysisPlcContext : DbContext
{
    public AnalysisPlcContext()
    {
    }

    public AnalysisPlcContext(DbContextOptions<AnalysisPlcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Engine> Engines { get; set; }

    public virtual DbSet<InitMonitor> InitMonitors { get; set; }

    public virtual DbSet<MonitoringEngine> MonitoringEngines { get; set; }

    public virtual DbSet<InitMonitorReadDTO> InitMonitorDTO {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-P64FGUS\\SQLEXPRESS; Initial Catalog=AnalysisPLC;\n                                          integrated security=True;MultipleActiveResultSets=True;\n                                          Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Devices__49E12331D13ED74B");

            entity.Property(e => e.DeviceId).HasColumnName("DeviceID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DeviceValue).IsUnicode(false);
            entity.Property(e => e.EngineId).HasColumnName("EngineID");

            entity.HasOne(d => d.Engine).WithMany(p => p.Devices)
                .HasForeignKey(d => d.EngineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DevicesInitMon");          
        });

        modelBuilder.Entity<InitMonitorReadDTO>().HasNoKey();

        modelBuilder.Entity<Engine>(entity =>
        {
            entity.HasKey(e => e.EngineId).HasName("PK__Engines__7BBCE92485E3F3F0");

            entity.Property(e => e.EngineId).HasColumnName("EngineID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EngineDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EngineName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InitMonitor>(entity =>
        {
            entity.HasKey(e => e.InitMonitorId).HasName("PK__InitMoni__511F1B41AC1EC85E");

            entity.ToTable("InitMonitor", tb => tb.HasTrigger("NotifyNewInsertInitMonitor"));

            entity.Property(e => e.InitMonitorId).HasColumnName("InitMonitorID");
            entity.Property(e => e.Amper)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Engine)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Hertz)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HertzSetup)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.StatusM).HasDefaultValue(true);
            entity.Property(e => e.Volts)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MonitoringEngine>(entity =>
        {
            entity.HasKey(e => e.MonitorId).HasName("PK__Monitori__DF5D95D842ED1040");

            entity.ToTable("MonitoringEngine");

            entity.Property(e => e.MonitorId).HasColumnName("MonitorID");
            entity.Property(e => e.AmperValue).HasColumnType("decimal(10, 5)");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HertzValue).HasColumnType("decimal(10, 5)");
            entity.Property(e => e.InitMonitorId).HasColumnName("InitMonitorID");
            entity.Property(e => e.VoltsValue).HasColumnType("decimal(10, 5)");

            entity.HasOne(d => d.InitMonitor).WithMany(p => p.MonitoringEngines)
                .HasForeignKey(d => d.InitMonitorId)
                .HasConstraintName("FK__Monitorin__InitM__7D439ABD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
