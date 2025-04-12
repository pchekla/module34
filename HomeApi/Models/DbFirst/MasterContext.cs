using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Models.DbFirst;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NetworkUser> NetworkUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Если контекст уже настроен, не требуется дополнительной настройки
        if (!optionsBuilder.IsConfigured)
        {
            // Этот код будет запускаться только для инструментов EF Core, например при миграциях
            // В реальном приложении контекст должен быть настроен через DI в Startup.cs
            // с использованием строки подключения из конфигурации
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NetworkUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NetworkU__3214EC075D45664B");

            entity.ToTable("NetworkUser");

            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
