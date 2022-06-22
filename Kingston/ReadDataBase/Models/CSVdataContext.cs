using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReadDataBase.Models
{
    public partial class CSVdataContext : DbContext
    {
        
        public CSVdataContext()
        {
        }

        public CSVdataContext(DbContextOptions<CSVdataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CSV_Result> CSV_Results { get; set; } = null!;


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        optionsBuilder.UseSqlServer("Server=hsinyu.database.windows.net;Database=CSVdata;User Id=cala;Password=ZxcZxc33;");
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer("Server=hsinyu.database.windows.net;Database=CSVdata;User Id=cala;Password=ZxcZxc33;");
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CSV_Result>(entity =>
            {
                entity.HasKey(e => e.num)
                    .HasName("PK__CSV_Resu__DF908D655541286B");

                entity.ToTable("CSV_Result");

                entity.Property(e => e.num)
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.age)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.school)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

         

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
