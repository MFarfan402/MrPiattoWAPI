using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MrPiattoWAPI.Model
{
    public partial class MrPiattoDB2Context : DbContext
    {
        public MrPiattoDB2Context()
        {
        }

        public MrPiattoDB2Context(DbContextOptions<MrPiattoDB2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AuxiliarReservation> AuxiliarReservation { get; set; }
        public virtual DbSet<AuxiliarTables> AuxiliarTables { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Complaints> Complaints { get; set; }
        public virtual DbSet<DayStatistics> DayStatistics { get; set; }
        public virtual DbSet<HourStatistics> HourStatistics { get; set; }
        public virtual DbSet<LockedHours> LockedHours { get; set; }
        public virtual DbSet<LockedRestaurants> LockedRestaurants { get; set; }
        public virtual DbSet<LockedTables> LockedTables { get; set; }
        public virtual DbSet<PaymentOptions> PaymentOptions { get; set; }
        public virtual DbSet<Policies> Policies { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<RestaurantPhotos> RestaurantPhotos { get; set; }
        public virtual DbSet<RestaurantStrikes> RestaurantStrikes { get; set; }
        public virtual DbSet<RestaurantTables> RestaurantTables { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Surveys> Surveys { get; set; }
        public virtual DbSet<TableDistribution> TableDistribution { get; set; }
        public virtual DbSet<TableStatistics> TableStatistics { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPhotos> UserPhotos { get; set; }
        public virtual DbSet<UserRestaurant> UserRestaurant { get; set; }
        public virtual DbSet<UserStrikes> UserStrikes { get; set; }
        public virtual DbSet<Waiters> Waiters { get; set; }
        public virtual DbSet<WaiterStatistics> WaiterStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=PC;Database=MrPiattoDB2;User Id=sql_user;Password=1234;");
                optionsBuilder.UseSqlServer("Server=MFARFAN\\MSSQLSERVER01;Database=MrPiattoDB2;User Id=sql_user;Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuxiliarReservation>(entity =>
            {
                entity.HasKey(e => e.IdauxiliarReservation);

                entity.Property(e => e.IdauxiliarReservation).HasColumnName("IDAuxiliarReservation");

                entity.Property(e => e.AmountOfPeople).HasColumnName("amountOfPeople");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdauxiliarTable).HasColumnName("IDAuxiliarTable");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.ReservationStatus).HasColumnName("reservationStatus");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdauxiliarTableNavigation)
                    .WithMany(p => p.AuxiliarReservation)
                    .HasForeignKey(d => d.IdauxiliarTable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuxiliarReservation_AuxiliarTable");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.AuxiliarReservation)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuxiliarReservation_User");
            });

            modelBuilder.Entity<AuxiliarTables>(entity =>
            {
                entity.HasKey(e => e.IdauxiliarTable)
                    .HasName("PK_AuxiliarTable");

                entity.Property(e => e.IdauxiliarTable).HasColumnName("IDAuxiliarTable");

                entity.Property(e => e.AvarageUse).HasColumnName("avarageUse");

                entity.Property(e => e.CoordenateX).HasColumnName("coordenateX");

                entity.Property(e => e.CoordenateY).HasColumnName("coordenateY");

                entity.Property(e => e.FloorName)
                    .HasColumnName("floorName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.StringIdtables)
                    .IsRequired()
                    .HasColumnName("stringIDTables")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.AuxiliarTables)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuxiliarTables_Restaurant");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Idcategory);

                entity.Property(e => e.Idcategory).HasColumnName("IDCategory");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.Idcomment);

                entity.Property(e => e.Idcomment).HasColumnName("IDComment");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Restaurant");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_User");
            });

            modelBuilder.Entity<Complaints>(entity =>
            {
                entity.HasKey(e => e.Idcomplaints)
                    .HasName("PK_Table_1");

                entity.Property(e => e.Idcomplaints).HasColumnName("IDComplaints");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaints_Restaurant");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaints_User");
            });

            modelBuilder.Entity<DayStatistics>(entity =>
            {
                entity.HasKey(e => e.IddaysStatistics);

                entity.Property(e => e.IddaysStatistics).HasColumnName("IDDaysStatistics");

                entity.Property(e => e.AverageFriday).HasColumnName("averageFriday");

                entity.Property(e => e.AverageMonday).HasColumnName("averageMonday");

                entity.Property(e => e.AverageSaturday).HasColumnName("averageSaturday");

                entity.Property(e => e.AverageSunday).HasColumnName("averageSunday");

                entity.Property(e => e.AverageThursday).HasColumnName("averageThursday");

                entity.Property(e => e.AverageTuesday).HasColumnName("averageTuesday");

                entity.Property(e => e.AverageWednesday).HasColumnName("averageWednesday");

                entity.Property(e => e.DateStatistics)
                    .HasColumnName("dateStatistics")
                    .HasColumnType("datetime");

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

                entity.Property(e => e.DateStatistics)
                    .HasColumnName("dateStatistics")
                    .HasColumnType("datetime");

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
                    .HasColumnType("datetime");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.LockedHours)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LockedHours_Restaurant");
            });

            modelBuilder.Entity<LockedRestaurants>(entity =>
            {
                entity.HasKey(e => e.IdlockedRestaurants);

                entity.Property(e => e.IdlockedRestaurants).HasColumnName("IDLockedRestaurants");

                entity.Property(e => e.Idrestaurants).HasColumnName("IDRestaurants");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.UnlockedDate)
                    .HasColumnName("unlockedDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdrestaurantsNavigation)
                    .WithMany(p => p.LockedRestaurants)
                    .HasForeignKey(d => d.Idrestaurants)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LockedRestaurants_Restaurant");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.LockedRestaurants)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LockedRestaurants_User");
            });

            modelBuilder.Entity<LockedTables>(entity =>
            {
                entity.HasKey(e => e.IdlockedTables);

                entity.Property(e => e.IdlockedTables).HasColumnName("IDLockedTables");

                entity.Property(e => e.EndDate)
                    .HasColumnName("endDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idtables).HasColumnName("IDTables");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdtablesNavigation)
                    .WithMany(p => p.LockedTables)
                    .HasForeignKey(d => d.Idtables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LockedTables_RestaurantTables");
            });

            modelBuilder.Entity<PaymentOptions>(entity =>
            {
                entity.HasKey(e => e.IdpaymentOptions);

                entity.Property(e => e.IdpaymentOptions).HasColumnName("IDPaymentOptions");

                entity.Property(e => e.PaymentOption)
                    .IsRequired()
                    .HasColumnName("paymentOption")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Policies>(entity =>
            {
                entity.HasKey(e => e.Idpolicies);

                entity.Property(e => e.Idpolicies).HasColumnName("IDPolicies");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.MaxTimeArr).HasColumnName("maxTimeArr");

                entity.Property(e => e.MaxTimeArrPer).HasColumnName("maxTimeArrPer");

                entity.Property(e => e.MaxTimePer).HasColumnName("maxTimePer");

                entity.Property(e => e.MaxTimeRes).HasColumnName("maxTimeRes");

                entity.Property(e => e.MinTimePer).HasColumnName("minTimePer");

                entity.Property(e => e.MinTimeRes).HasColumnName("minTimeRes");

                entity.Property(e => e.ModTimeDays).HasColumnName("modTimeDays");

                entity.Property(e => e.ModTimeHours).HasColumnName("modTimeHours");

                entity.Property(e => e.ModTimeSeats).HasColumnName("modTimeSeats");

                entity.Property(e => e.StrikeType).HasColumnName("strikeType");

                entity.Property(e => e.StrikeTypePer).HasColumnName("strikeTypePer");

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

                entity.Property(e => e.Idtable).HasColumnName("IDTable");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdtableNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.Idtable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_RestaurantTables");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_User");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.Idrestaurant);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Confirmation).HasColumnName("confirmation");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Dress)
                    .IsRequired()
                    .HasColumnName("dress")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idcategories).HasColumnName("IDCategories");

                entity.Property(e => e.Idpayment).HasColumnName("IDPayment");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("lastLogin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Long).HasColumnName("long");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.SeverityLevel).HasColumnName("severityLevel");

                entity.HasOne(d => d.IdcategoriesNavigation)
                    .WithMany(p => p.Restaurant)
                    .HasForeignKey(d => d.Idcategories)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restaurant_Categories");

                entity.HasOne(d => d.IdpaymentNavigation)
                    .WithMany(p => p.Restaurant)
                    .HasForeignKey(d => d.Idpayment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restaurant_PaymentOptions");
            });

            modelBuilder.Entity<RestaurantPhotos>(entity =>
            {
                entity.HasKey(e => e.IdrestaurantPhotos);

                entity.Property(e => e.IdrestaurantPhotos).HasColumnName("IDRestaurantPhotos");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(100)
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
                    .HasColumnType("datetime");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Severity).HasColumnName("severity");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.RestaurantStrikes)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestaurantStrikes_Restaurant");
            });

            modelBuilder.Entity<RestaurantTables>(entity =>
            {
                entity.HasKey(e => e.Idtables);

                entity.Property(e => e.Idtables).HasColumnName("IDTables");

                entity.Property(e => e.AvarageUse).HasColumnName("avarageUse");

                entity.Property(e => e.CoordenateX).HasColumnName("coordenateX");

                entity.Property(e => e.CoordenateY).HasColumnName("coordenateY");

                entity.Property(e => e.FloorIndex).HasColumnName("floorIndex");

                entity.Property(e => e.FloorName)
                    .IsRequired()
                    .HasColumnName("floorName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.IsJoin).HasColumnName("isJoin");

                entity.Property(e => e.Seats)
                    .HasColumnName("seats");

                entity.Property(e => e.TableName)
                    .HasColumnName("tableName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.RestaurantTables)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestaurantTables_Restaurant");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.Idschedule);

                entity.Property(e => e.Idschedule).HasColumnName("IDSchedule");

                entity.Property(e => e.Ctfriday).HasColumnName("CTFriday");

                entity.Property(e => e.Ctmonday).HasColumnName("CTMonday");

                entity.Property(e => e.Ctsaturday).HasColumnName("CTSaturday");

                entity.Property(e => e.Ctsunday).HasColumnName("CTSunday");

                entity.Property(e => e.Ctthursday).HasColumnName("CTThursday");

                entity.Property(e => e.Cttuestday).HasColumnName("CTTuestday");

                entity.Property(e => e.Ctwednesday).HasColumnName("CTWednesday");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Otfriday).HasColumnName("OTFriday");

                entity.Property(e => e.Otmonday).HasColumnName("OTMonday");

                entity.Property(e => e.Otsaturday).HasColumnName("OTSaturday");

                entity.Property(e => e.Otsunday).HasColumnName("OTSunday");

                entity.Property(e => e.Otthursday).HasColumnName("OTThursday");

                entity.Property(e => e.Ottuesday).HasColumnName("OTTuesday");

                entity.Property(e => e.Otwednesday).HasColumnName("OTWednesday");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Restaurant");
            });

            modelBuilder.Entity<Surveys>(entity =>
            {
                entity.HasKey(e => e.Idsurvey);

                entity.Property(e => e.Idsurvey).HasColumnName("IDSurvey");

                entity.Property(e => e.ComfortRating).HasColumnName("comfortRating");

                entity.Property(e => e.FoodRating).HasColumnName("foodRating");

                entity.Property(e => e.GeneralScore).HasColumnName("generalScore");

                entity.Property(e => e.Idcomment).HasColumnName("IDComment");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Idwaiter).HasColumnName("IDWaiter");

                entity.Property(e => e.ServiceRating).HasColumnName("serviceRating");

                entity.Property(e => e.DateStatistics).HasColumnName("dateStatistics");

                entity.HasOne(d => d.IdcommentNavigation)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.Idcomment)
                    .HasConstraintName("FK_Surveys_Comments");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Surveys_Restaurant");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Surveys_User");

                entity.HasOne(d => d.IdwaiterNavigation)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.Idwaiter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Surveys_Waiters");
            });

            modelBuilder.Entity<TableDistribution>(entity =>
            {
                entity.HasKey(e => e.IdtableDistribution)
                    .HasName("PK_Table_1_1");

                entity.Property(e => e.IdtableDistribution).HasColumnName("IDTableDistribution");

                entity.Property(e => e.CoordenateX).HasColumnName("coordenateX");

                entity.Property(e => e.CoordenateY).HasColumnName("coordenateY");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idtables).HasColumnName("IDTables");

                entity.HasOne(d => d.IdtablesNavigation)
                    .WithMany(p => p.TableDistribution)
                    .HasForeignKey(d => d.Idtables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TableDistribution_RestaurantTables");
            });

            modelBuilder.Entity<TableStatistics>(entity =>
            {
                entity.HasKey(e => e.IDTableStatistics);

                entity.Property(e => e.IDTableStatistics).HasColumnName("IDTableStatistics");

                entity.Property(e => e.IDRestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.IDTable).HasColumnName("IDTable");

                entity.Property(e => e.AvarageUse).HasColumnName("avarageUse"); 

                entity.Property(e => e.DateStatistics)
                    .HasColumnName("dateStatistics")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.TableStatistics)
                    .HasForeignKey(d => d.IDRestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TableStatistics_Restaurant");

                entity.HasOne(d => d.IdrestaurantTablesNavigation)
                    .WithMany(p => p.TableStatistics)
                    .HasForeignKey(d => d.IDTable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TableStatistics_RestaurantTables");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser);

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnlockedDay)
                    .HasColumnName("unlockedDay")
                    .HasColumnType("date");

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasColumnName("userType")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPhotos>(entity =>
            {
                entity.HasKey(e => e.IduserPhoto);

                entity.Property(e => e.IduserPhoto).HasColumnName("IDUserPhoto");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Url)
                    .IsRequired()
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
                    .IsRequired()
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
                entity.HasKey(e => e.Idwaiter);

                entity.Property(e => e.Idwaiter).HasColumnName("IDWaiter");

                entity.Property(e => e.Idrestaurant).HasColumnName("IDRestaurant");

                entity.Property(e => e.WaiterFirstName)
                    .IsRequired()
                    .HasColumnName("waiterFirstName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.WaiterLasName)
                    .IsRequired()
                    .HasColumnName("waiterLasName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrestaurantNavigation)
                    .WithMany(p => p.Waiters)
                    .HasForeignKey(d => d.Idrestaurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Waiters_Restaurant");
            });

            modelBuilder.Entity<WaiterStatistics>(entity =>
            {
                entity.HasKey(e => e.IdwaiterStatistics);

                entity.Property(e => e.IdwaiterStatistics).HasColumnName("IDWaiterStatistics");

                entity.Property(e => e.Idwaiter).HasColumnName("IDWaiter");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.DateStatistics).HasColumnName("dateStatistics");

                entity.HasOne(d => d.IdwaitersNavigation)
                    .WithMany(p => p.WaiterStatistics)
                    .HasForeignKey(d => d.Idwaiter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WaiterStatistics_Waiter");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
