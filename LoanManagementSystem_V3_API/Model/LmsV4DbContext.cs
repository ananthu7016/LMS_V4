using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem_V3_API.Model;

public partial class LmsV4DbContext : DbContext
{
    public LmsV4DbContext()
    {
    }

    public LmsV4DbContext(DbContextOptions<LmsV4DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<LoanDetail> LoanDetails { get; set; }

    public virtual DbSet<LoanRequest> LoanRequests { get; set; }

    public virtual DbSet<LoanType> LoanTypes { get; set; }

    public virtual DbSet<LoanVerification> LoanVerifications { get; set; }

    public virtual DbSet<LoanVerificationSummary> LoanVerificationSummaries { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =HP_Ananthu\\SQLEXPRESS; Initial Catalog =LMS_V4_db; Integrated Security = True;\nTrusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D844BCD941");

            entity.Property(e => e.Aadhar)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnType("date");
            entity.Property(e => e.FullName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Occupation)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RegisteredDateTime).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LoanDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__LoanDeta__135C316DE3B9EEED");

            entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoanPurpose)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.LoanRequestDateTime).HasColumnType("datetime");
            entity.Property(e => e.LoanSanctionDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Cust).WithMany(p => p.LoanDetails)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__LoanDetai__CustI__17F790F9");

            entity.HasOne(d => d.LoanType).WithMany(p => p.LoanDetails)
                .HasForeignKey(d => d.LoanTypeId)
                .HasConstraintName("FK__LoanDetai__LoanT__17036CC0");

            entity.HasOne(d => d.User).WithMany(p => p.LoanDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__LoanDetai__UserI__18EBB532");
        });

        modelBuilder.Entity<LoanRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__LoanRequ__33A8517AA4C57C2B");

            entity.Property(e => e.LoanPurpose)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.LoanRequestDateTime).HasColumnType("datetime");
            entity.Property(e => e.RequestDetails)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RequestedAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cust).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__LoanReque__CustI__1CBC4616");

            entity.HasOne(d => d.LoanType).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.LoanTypeId)
                .HasConstraintName("FK__LoanReque__LoanT__1BC821DD");
        });

        modelBuilder.Entity<LoanType>(entity =>
        {
            entity.HasKey(e => e.LoanTypeId).HasName("PK__LoanType__19466BAF352CA81F");

            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.LoanDescription)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.LoanInterestRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LoanMaximumAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoanMinimumAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoanTypeName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.ProcessingFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<LoanVerification>(entity =>
        {
            entity.HasKey(e => e.VerificationId).HasName("PK__LoanVeri__306D49070599F605");

            entity.Property(e => e.VerificationReview)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Request).WithMany(p => p.LoanVerifications)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__LoanVerif__Reque__22751F6C");

            entity.HasOne(d => d.User).WithMany(p => p.LoanVerifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__LoanVerif__UserI__236943A5");
        });

        modelBuilder.Entity<LoanVerificationSummary>(entity =>
        {
            entity.HasKey(e => e.SummaryId).HasName("PK__LoanVeri__DAB10E2F5BFB44D0");

            entity.ToTable("LoanVerificationSummary");

            entity.Property(e => e.StatusChangedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Summary)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Request).WithMany(p => p.LoanVerificationSummaries)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__LoanVerif__Reque__1F98B2C1");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Logs__5E548648AEE77DBC");

            entity.Property(e => e.EventType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LogDescription)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A3D072BB3");

            entity.Property(e => e.RoleName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C1E6A03B1");

            entity.HasIndex(e => e.Password, "UQ__Users__87909B15E159D59F").IsUnique();

            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.FullName)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__74AE54BC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
