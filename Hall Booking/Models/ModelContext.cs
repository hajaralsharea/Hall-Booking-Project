using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        
        public virtual DbSet<ContactU> ContactUs { get; set; }

        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<MailRequest> MailRequests { get; set; }
        
        public virtual DbSet<Payment> Payments { get; set; }
        
        public virtual DbSet<Role> Roles { get; set; }
        
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<User> Users { get; set; }
       
        public virtual DbSet<UsersLogin> UsersLogins { get; set; }
        public virtual DbSet<HallCategory> HallCategories { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<AboutUs> AboutUses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=JOR15_User45;Password=hajar12345;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR15_USER45")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("BOOKING");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.BookingDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BOOKING_DATE");

                entity.Property(e => e.BookingNotes)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BOOKING_NOTES");

                entity.Property(e => e.EndDate)
                    .HasPrecision(6)
                    .HasColumnName("END_DATE");

                entity.Property(e => e.HallId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("HALL_ID");

                entity.Property(e => e.StartDate)
                    .HasPrecision(6)
                    .HasColumnName("START_DATE");


                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.StatusId)
                   .HasColumnType("NUMBER")
                   .HasColumnName("STATUS_ID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00271053");


                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.HallId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK1_HALL_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK1_USER_ID");
            });

            

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.ToTable("CONTACT_US");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.Message)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContactUs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERS_ID");
            });

            modelBuilder.Entity<HallCategory>(entity =>
            {
                entity.ToTable("HALL_CATEGORY");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.HallDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("HALL_DESCRIPTION");


                entity.Property(e => e.ImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");
            });



            modelBuilder.Entity<Hall>(entity =>
            {
                entity.ToTable("HALL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Capacity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CAPACITY");

                entity.Property(e => e.HallAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HALL_ADDRESS");

                entity.Property(e => e.HallArea)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HALL_AREA");

                entity.Property(e => e.HallName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HALL_NAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORY_ID");

                entity.HasOne(d => d.HallCategory)
                   .WithMany(p => p.Halls)
                   .HasForeignKey(d => d.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("HALL_CATEGORY_ID");
            });

            modelBuilder.Entity<MailRequest>(entity =>
            {
                entity.ToTable("MAIL_REQUEST");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Body)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BODY");

                entity.Property(e => e.BookingId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BOOKING_ID");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FILE_PATH");

                entity.Property(e => e.Subject)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");

                entity.Property(e => e.ToEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TO_EMAIL");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.MailRequests)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("BOOKING_ID");
            });

           

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("PAYMENT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CardBalance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_BALANCE");

                entity.Property(e => e.CardName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CARD_NAME");

                entity.Property(e => e.CardSequanceNumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_SEQUANCE_NUMBER");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAYMENT_DATE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USER_ID");
            });

            

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

           

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Commint)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COMMINT");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK3_USERS_ID");

                entity.Property(e => e.StatusId)
                  .HasColumnType("NUMBER")
                  .HasColumnName("STATUS_ID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00271051");


            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");
            });

            

            modelBuilder.Entity<UsersLogin>(entity =>
            {
                entity.ToTable("USERS_LOGIN");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Passwordd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORDD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsersLogins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ROLE_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERSS_ID");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("STATUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_NAME");
            });
            modelBuilder.Entity<AboutUs>(entity =>
            {
                entity.ToTable("ABOUT_US");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Text)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.Property(e => e.Text2)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2");

            });

            modelBuilder.HasSequence("DEPTID");

            modelBuilder.HasSequence("SQ1").IncrementsBy(3);

            modelBuilder.HasSequence("SQ2")
                .IncrementsBy(3)
                .IsCyclic();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
