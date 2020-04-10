using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MrPiattoWAPI.Model
{
    public partial class MrPiattoDBContext : DbContext
    {
        public MrPiattoDBContext()
        {
        }

        public MrPiattoDBContext(DbContextOptions<MrPiattoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<DayStatistics> DayStatistics { get; set; }
        public virtual DbSet<HourStatistics> HourStatistics { get; set; }
        public virtual DbSet<LockedHours> LockedHours { get; set; }
        public virtual DbSet<PaymentOptions> PaymentOptions { get; set; }
        public virtual DbSet<Policies> Policies { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<RestaurantPhotos> RestaurantPhotos { get; set; }
        public virtual DbSet<RestaurantStrikes> RestaurantStrikes { get; set; }
        public virtual DbSet<Surveys> Surveys { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
        public virtual DbSet<TextFloor> TextFloor { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPhotos> UserPhotos { get; set; }
        public virtual DbSet<UserRestaurant> UserRestaurant { get; set; }
        public virtual DbSet<UserStrikes> UserStrikes { get; set; }
        public virtual DbSet<Waiters> Waiters { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=SRVMRPIATTO;Database=MrPiattoDB;User Id=sql_user;Password=1234;");
                optionsBuilder.UseSqlServer(@"Server=MFARFAN\MSSQLSERVER01;Database=MrPiattoDB;User Id=sql_user;Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Idcategories);

                entity.Property(e => e.Idcategories).HasColumnName("IDCategories");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categories_Restaurant");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.Idcomments);

                entity.Property(e => e.Idcomments).HasColumnName("IDComments");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Restaurant");
            });

            modelBuilder.Entity<DayStatistics>(entity =>
            {
                entity.HasKey(e => e.IddayStatistics);

                entity.Property(e => e.IddayStatistics).HasColumnName("IDDayStatistics");

                entity.Property(e => e.AverageFriday).HasColumnName("averageFriday");

                entity.Property(e => e.AverageMonday).HasColumnName("averageMonday");

                entity.Property(e => e.AverageSaturday).HasColumnName("averageSaturday");

                entity.Property(e => e.AverageSunday).HasColumnName("averageSunday");

                entity.Property(e => e.AverageThursday).HasColumnName("averageThursday");

                entity.Property(e => e.AverageTuesday).HasColumnName("averageTuesday");

                entity.Property(e => e.AverageWednesday).HasColumnName("averageWednesday");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.DayStatistics)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DayStatistics_Restaurant");
            });

            modelBuilder.Entity<HourStatistics>(entity =>
            {
                entity.HasKey(e => e.IdhourStatistics);

                entity.Property(e => e.IdhourStatistics).HasColumnName("IDHourStatistics");

                entity.Property(e => e.Average0000).HasColumnName("average0000");

                entity.Property(e => e.Average0100).HasColumnName("average0100");

                entity.Property(e => e.Average0200).HasColumnName("average0200");

                entity.Property(e => e.Average0300).HasColumnName("average0300");

                entity.Property(e => e.Average0400).HasColumnName("average0400");

                entity.Property(e => e.Average0500).HasColumnName("average0500");

                entity.Property(e => e.Average0600).HasColumnName("average0600");

                entity.Property(e => e.Average0700).HasColumnName("average0700");

                entity.Property(e => e.Average0800).HasColumnName("average0800");

                entity.Property(e => e.Average0900).HasColumnName("average0900");

                entity.Property(e => e.Average1000).HasColumnName("average1000");

                entity.Property(e => e.Average1100).HasColumnName("average1100");

                entity.Property(e => e.Average1200).HasColumnName("average1200");

                entity.Property(e => e.Average1300).HasColumnName("average1300");

                entity.Property(e => e.Average1400).HasColumnName("average1400");

                entity.Property(e => e.Average1500).HasColumnName("average1500");

                entity.Property(e => e.Average1600).HasColumnName("average1600");

                entity.Property(e => e.Average1700).HasColumnName("average1700");

                entity.Property(e => e.Average1800).HasColumnName("average1800");

                entity.Property(e => e.Average1900).HasColumnName("average1900");

                entity.Property(e => e.Average2000).HasColumnName("average2000");

                entity.Property(e => e.Average2100).HasColumnName("average2100");

                entity.Property(e => e.Average2200).HasColumnName("average2200");

                entity.Property(e => e.Average2300).HasColumnName("average2300");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.HourStatistics)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HourStatistics_Restaurant");
            });

            modelBuilder.Entity<LockedHours>(entity =>
            {
                entity.HasKey(e => e.IdlockedHours);

                entity.Property(e => e.IdlockedHours).HasColumnName("IDLockedHours");

                entity.Property(e => e.EndDate)
                    .HasColumnName("endDate")
                    .HasColumnType("date");

                entity.Property(e => e.EndHour).HasColumnName("endHour");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("date");

                entity.Property(e => e.StartHour).HasColumnName("startHour");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.LockedHours)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LockedHours_Restaurant");
            });

            modelBuilder.Entity<PaymentOptions>(entity =>
            {
                entity.HasKey(e => e.IdpaymentOptions);

                entity.Property(e => e.IdpaymentOptions).HasColumnName("IDPaymentOptions");

                entity.Property(e => e.Amex)
                    .HasColumnName("AMEX")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cash).HasDefaultValueSql("((1))");

                entity.Property(e => e.Checks).HasDefaultValueSql("((0))");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.MasterCard).HasDefaultValueSql("((0))");

                entity.Property(e => e.Transfers).HasDefaultValueSql("((0))");

                entity.Property(e => e.Visa).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.PaymentOptions)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentOptions_Restaurant");
            });

            modelBuilder.Entity<Policies>(entity =>
            {
                entity.HasKey(e => e.Idpolicies);

                entity.Property(e => e.Idpolicies).HasColumnName("IDPolicies");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.MaxTimeArr).HasColumnName("maxTimeArr");

                entity.Property(e => e.MaxTimeRes).HasColumnName("maxTimeRes");

                entity.Property(e => e.MinTimeRes).HasColumnName("minTimeRes");

                entity.Property(e => e.ModTime).HasColumnName("modTime");

                entity.Property(e => e.StrikeType).HasColumnName("strikeType");

                entity.Property(e => e.Strikes).HasColumnName("strikes");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Policies_Restaurant");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Idreservation);

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.AmountOfPeople).HasColumnName("amountOfPeople");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Idtable).HasColumnName("IDTable");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Restaurant");

                entity.HasOne(d => d.Idrestaurant1)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_User");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.Idrestaurant);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingTime).HasColumnName("closingTime");

                entity.Property(e => e.Confirmation).HasColumnName("confirmation");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Dress)
                    .HasColumnName("dress")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin)
                    .HasColumnName("lastLogin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningTime).HasColumnName("openingTime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.SeverityLevel).HasColumnName("severityLevel");
            });

            modelBuilder.Entity<RestaurantPhotos>(entity =>
            {
                entity.HasKey(e => e.IdrestaurantPhotos);

                entity.Property(e => e.IdrestaurantPhotos).HasColumnName("IDRestaurantPhotos");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.RestaurantPhotos)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestaurantPhotos_Restaurant");
            });

            modelBuilder.Entity<RestaurantStrikes>(entity =>
            {
                entity.HasKey(e => e.IdrestaurantStrikes);

                entity.Property(e => e.IdrestaurantStrikes).HasColumnName("IDRestaurantStrikes");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Severity).HasColumnName("severity");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.RestaurantStrikes)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestaurantStrikes_Restaurant");
            });

            modelBuilder.Entity<Surveys>(entity =>
            {
                entity.HasKey(e => e.Idsurveys);

                entity.Property(e => e.Idsurveys).HasColumnName("IDSurveys");

                entity.Property(e => e.ComfortRating).HasColumnName("comfortRating");

                entity.Property(e => e.FoodRating).HasColumnName("foodRating");

                entity.Property(e => e.Idclient).HasColumnName("IDClient");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Idwaiter).HasColumnName("IDWaiter");

                entity.Property(e => e.ServiceRating).HasColumnName("serviceRating");

                entity.Property(e => e.ServiceType)
                    .HasColumnName("serviceType")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Surveys_Restaurant");
            });

            modelBuilder.Entity<Tables>(entity =>
            {
                entity.HasKey(e => e.Idtables);

                entity.Property(e => e.Idtables).HasColumnName("IDTables");

                entity.Property(e => e.AverageUse).HasColumnName("averageUse");

                entity.Property(e => e.CoordenateX).HasColumnName("coordenateX");

                entity.Property(e => e.CoordenateY).HasColumnName("coordenateY");

                entity.Property(e => e.FloorName)
                    .HasColumnName("floorName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Tables)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tables_Reservation");

                entity.HasOne(d => d.Idrestaurant1)
                    .WithMany(p => p.Tables)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tables_Restaurant");
            });

            modelBuilder.Entity<TextFloor>(entity =>
            {
                entity.HasKey(e => e.IdtextFloor);

                entity.Property(e => e.IdtextFloor).HasColumnName("IDTextFloor");

                entity.Property(e => e.CoordenateX).HasColumnName("coordenateX");

                entity.Property(e => e.CoordenateY).HasColumnName("coordenateY");

                entity.Property(e => e.FloorName)
                    .HasColumnName("floorName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.TextFloor)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TextFloor_Restaurant");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser);

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasColumnName("userType")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPhotos>(entity =>
            {
                entity.HasKey(e => e.IduserPhotos);

                entity.Property(e => e.IduserPhotos).HasColumnName("IDUserPhotos");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.UserPhotos)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhotos_Restaurant");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserPhotos)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhotos_User");
            });

            modelBuilder.Entity<UserRestaurant>(entity =>
            {
                entity.HasKey(e => e.IduserRestaurant);

                entity.Property(e => e.IduserRestaurant).HasColumnName("IDUserRestaurant");

                entity.Property(e => e.Favorite).HasColumnName("favorite");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.MailSubscription).HasColumnName("mailSubscription");

                entity.Property(e => e.Visited).HasColumnName("visited");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.UserRestaurant)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRestaurant_Restaurant");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserRestaurant)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRestaurant_User");
            });

            modelBuilder.Entity<UserStrikes>(entity =>
            {
                entity.HasKey(e => e.IduserStrikes);

                entity.Property(e => e.IduserStrikes).HasColumnName("IDUserStrikes");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserStrikes)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserStrikes_User");
            });

            modelBuilder.Entity<Waiters>(entity =>
            {
                entity.HasKey(e => e.Idwaiters);

                entity.Property(e => e.Idwaiters).HasColumnName("IDWaiters");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.WaiterFirstName)
                    .HasColumnName("waiterFirstName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.WaiterLastName)
                    .HasColumnName("waiterLastName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.WaiterRating).HasColumnName("waiterRating");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Waiters)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Waiters_Restaurant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
