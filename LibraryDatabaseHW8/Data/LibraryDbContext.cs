using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibraryDatabaseHW8.Data
{
    public partial class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Checkout> Checkout { get; set; }
        public virtual DbSet<CheckoutItem> CheckoutItem { get; set; }
        public virtual DbSet<LibraryCard> LibraryCard { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomReservation> RoomReservation { get; set; }
        public virtual DbSet<SpecialCollection> SpecialCollection { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("name=DATABASE_URL");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorName)
                    .HasColumnName("author_name")
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.BookTitle)
                    .HasColumnName("book_title")
                    .HasMaxLength(80);

                entity.Property(e => e.PublishedYear).HasColumnName("published_year");

                entity.Property(e => e.ShelfPosition)
                    .HasColumnName("shelf_position")
                    .HasMaxLength(20);

                entity.Property(e => e.SpecialCollectionId).HasColumnName("special_collection_id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("book_author_id_fkey");

                entity.HasOne(d => d.SpecialCollection)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.SpecialCollectionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("book_special_collection_id_fkey");
            });

            modelBuilder.Entity<Checkout>(entity =>
            {
                entity.ToTable("checkout", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CheckoutDate)
                    .HasColumnName("checkout_date")
                    .HasColumnType("date");

                entity.Property(e => e.ReturnDate)
                    .HasColumnName("return_date")
                    .HasColumnType("date");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Checkout)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("checkout_staff_id_fkey");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Checkout)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("checkout_student_id_fkey");
            });

            modelBuilder.Entity<CheckoutItem>(entity =>
            {
                entity.ToTable("checkout_item", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CheckoutId).HasColumnName("checkout_id");

                entity.Property(e => e.Returned)
                    .HasColumnName("returned")
                    .HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CheckoutItem)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("checkout_item_book_id_fkey");

                entity.HasOne(d => d.Checkout)
                    .WithMany(p => p.CheckoutItem)
                    .HasForeignKey(d => d.CheckoutId)
                    .HasConstraintName("checkout_item_checkout_id_fkey");
            });

            modelBuilder.Entity<LibraryCard>(entity =>
            {
                entity.ToTable("library_card", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnName("expiration_date")
                    .HasColumnType("date");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LibraryCard)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("library_card_student_id_fkey");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.FloorNumber).HasColumnName("floor_number");

                entity.Property(e => e.RoomName)
                    .HasColumnName("room_name")
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<RoomReservation>(entity =>
            {
                entity.ToTable("room_reservation", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomReservation)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("room_reservation_room_id_fkey");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RoomReservation)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("room_reservation_student_id_fkey");
            });

            modelBuilder.Entity<SpecialCollection>(entity =>
            {
                entity.ToTable("special_collection", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CollectionName)
                    .HasColumnName("collection_name")
                    .HasMaxLength(80);

                entity.Property(e => e.ContactInstructions)
                    .HasColumnName("contact_instructions")
                    .HasMaxLength(800);

                entity.Property(e => e.Restrictions)
                    .HasColumnName("restrictions")
                    .HasMaxLength(800);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.StaffName)
                    .HasColumnName("staff_name")
                    .HasMaxLength(80);

                entity.Property(e => e.StaffPassword)
                    .HasColumnName("staff_password")
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student", "library");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.StudentName)
                    .HasColumnName("student_name")
                    .HasMaxLength(80);

                entity.Property(e => e.StudentPassword)
                    .HasColumnName("student_password")
                    .HasMaxLength(80);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
