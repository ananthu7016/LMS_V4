using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem_V3_API.Model;

public partial class LmsV3DbContext : DbContext
{
    public LmsV3DbContext()
    {
    }

    public LmsV3DbContext(DbContextOptions<LmsV3DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<LoanDeatil> LoanDeatils { get; set; }

    public virtual DbSet<LoanRequest> LoanRequests { get; set; }

    public virtual DbSet<LoanType> LoanTypes { get; set; }

    public virtual DbSet<LoanVerification> LoanVerifications { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UploadedDocument> UploadedDocuments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =HP_Ananthu\\SQLEXPRESS; Initial Catalog =CMS_V3_db; Integrated Security = True;\nTrusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("PK__Customer__A1B71F9029E94383");

            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.CustAadhar)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("cust_aadhar");
            entity.Property(e => e.CustAddress)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("cust_address");
            entity.Property(e => e.CustAnnualIncome)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cust_annual_income");
            entity.Property(e => e.CustDob)
                .HasColumnType("date")
                .HasColumnName("cust_dob");
            entity.Property(e => e.CustEmploymentStatus).HasColumnName("cust_employment_status");
            entity.Property(e => e.CustFirstName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("cust_first_name");
            entity.Property(e => e.CustGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cust_gender");
            entity.Property(e => e.CustLastName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cust_last_name");
            entity.Property(e => e.CustMaritalStatus).HasColumnName("cust_marital_status");
            entity.Property(e => e.CustNationality)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cust_nationality");
            entity.Property(e => e.CustOccupation)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("cust_occupation");
            entity.Property(e => e.CustPhone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cust_phone");
            entity.Property(e => e.CustStatus).HasColumnName("cust_status");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RegistredDateTime)
                .HasColumnType("datetime")
                .HasColumnName("registred_date_time");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.DocTypeId).HasName("PK__Document__85153F05FAFEF07F");

            entity.HasIndex(e => e.DocTypeName, "UQ__Document__D1FDC469F01DB235").IsUnique();

            entity.Property(e => e.DocTypeId).HasColumnName("doc_type_id");
            entity.Property(e => e.DocTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("doc_type_name");
        });

        modelBuilder.Entity<LoanDeatil>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__LoanDeat__38E9A224741AAE98");

            entity.Property(e => e.DetailId).HasColumnName("detail_id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.DocumentUploadedStatus).HasColumnName("document_uploaded_status");
            entity.Property(e => e.LatePaymentPenalty)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("late_payment_penalty");
            entity.Property(e => e.LoanAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("loan_amount");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.LoanPurpose)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("loan_purpose");
            entity.Property(e => e.LoanRequestDateTime)
                .HasColumnType("datetime")
                .HasColumnName("loan_request_date_time");
            entity.Property(e => e.LoanSanctionDateTime)
                .HasColumnType("datetime")
                .HasColumnName("loan_sanction_date_time");
            entity.Property(e => e.LoanStatus).HasColumnName("loan_status");
            entity.Property(e => e.OutstandingBalance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("outstanding_balance");
            entity.Property(e => e.RepaymentFrequency).HasColumnName("repayment_frequency");
            entity.Property(e => e.TotalAmountRepaid)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_amount_repaid");
            entity.Property(e => e.VerifiedBy).HasColumnName("verified_by");

            entity.HasOne(d => d.Cust).WithMany(p => p.LoanDeatils)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__LoanDeati__cust___5CD6CB2B");

            entity.HasOne(d => d.Loan).WithMany(p => p.LoanDeatils)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK__LoanDeati__loan___5BE2A6F2");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.LoanDeatils)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("FK__LoanDeati__verif__5DCAEF64");
        });

        modelBuilder.Entity<LoanRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__LoanRequ__18D3B90F53FAB9BA");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.DocumentUploadedStatus).HasColumnName("document_uploaded_status");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.LoanPurpose)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("loan_purpose");
            entity.Property(e => e.LoanRequestDateTime)
                .HasColumnType("datetime")
                .HasColumnName("loan_request_date_time");
            entity.Property(e => e.RepaymentFrequency).HasColumnName("repayment_frequency");
            entity.Property(e => e.RequestDetails)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("request_details");
            entity.Property(e => e.RequestStatus).HasColumnName("request_status");
            entity.Property(e => e.RequestedAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("requested_amount");

            entity.HasOne(d => d.Cust).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__LoanReque__cust___619B8048");

            entity.HasOne(d => d.Loan).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK__LoanReque__loan___60A75C0F");
        });

        modelBuilder.Entity<LoanType>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__LoanType__A1F79554A7109FB7");

            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.CollateralRequired).HasColumnName("collateral_required");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("created_date_time");
            entity.Property(e => e.EmployementStatusRequired).HasColumnName("employement_status_required");
            entity.Property(e => e.GracePeriod).HasColumnName("grace_period");
            entity.Property(e => e.LatePaymentPenalty)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("late_payment_penalty");
            entity.Property(e => e.LoanDescription)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("loan_description");
            entity.Property(e => e.LoanIntrestRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("loan_intrest_rate");
            entity.Property(e => e.LoanMaximumAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("loan_maximum_amount");
            entity.Property(e => e.LoanMinimumAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("loan_minimum_amount");
            entity.Property(e => e.LoanName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("loan_name");
            entity.Property(e => e.LoanStatus).HasColumnName("loan_status");
            entity.Property(e => e.LoanTerm).HasColumnName("loan_term");
            entity.Property(e => e.ProcessingFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("processing_fee");
            entity.Property(e => e.RepaymentFrequency).HasColumnName("repayment_frequency");
            entity.Property(e => e.TaxPercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tax_percentage");
        });

        modelBuilder.Entity<LoanVerification>(entity =>
        {
            entity.HasKey(e => e.VerificationId).HasName("PK__LoanVeri__24F1796943B310B3");

            entity.Property(e => e.VerificationId).HasColumnName("verification_id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.StatusChangedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("status_changed_date_time");
            entity.Property(e => e.VerificationReview)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("verification_review");
            entity.Property(e => e.VerificationStatus).HasColumnName("verification_status");
            entity.Property(e => e.VerifiedBy).HasColumnName("verified_by");

            entity.HasOne(d => d.Request).WithMany(p => p.LoanVerifications)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__LoanVerif__reque__6477ECF3");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.LoanVerifications)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("FK__LoanVerif__verif__656C112C");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Logs__9E2397E0AC3D7F99");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.EventType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("event_type");
            entity.Property(e => e.LogDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("log_description");
            entity.Property(e => e.LogStatus).HasColumnName("log_status");
            entity.Property(e => e.TimeStamp)
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CCF1BE87B9");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("role_name");
            entity.Property(e => e.RoleStatus).HasColumnName("role_status");
        });

        modelBuilder.Entity<UploadedDocument>(entity =>
        {
            entity.HasKey(e => e.UploadId).HasName("PK__Uploaded__A13DEF589793D3C2");

            entity.HasIndex(e => e.DocPath, "UQ__Uploaded__048063CF192B7287").IsUnique();

            entity.Property(e => e.UploadId).HasColumnName("upload_id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.DocPath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("doc_path");
            entity.Property(e => e.DocTypeId).HasColumnName("doc_type_id");

            entity.HasOne(d => d.Cust).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__UploadedD__cust___6E01572D");

            entity.HasOne(d => d.DocType).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.DocTypeId)
                .HasConstraintName("FK__UploadedD__doc_t__6EF57B66");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F49CBEE55");

            entity.HasIndex(e => e.Password, "UQ__Users__6E2DBEDE7D325FCD").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("created_date_time");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__role_id__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
