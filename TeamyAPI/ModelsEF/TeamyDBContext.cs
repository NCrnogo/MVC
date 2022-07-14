using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamyAPI.ModelsEF
{
    public partial class TeamyDBContext : DbContext
    {
        public TeamyDBContext()
        {
        }

        public TeamyDBContext(DbContextOptions<TeamyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Daily> Dailys { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectUserMapping> ProjectUserMappings { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamInvite> TeamInvites { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRollMapping> UserRollMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.;database=TeamyDB;uid=sa;pwd=SQL");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Idactivities)
                    .HasName("PK__Activiti__55539B7802649CE0");

                entity.Property(e => e.Idactivities).HasColumnName("IDActivities");

                entity.Property(e => e.DailyFk).HasColumnName("DailyFK");

                entity.Property(e => e.End)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Start)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.DailyFkNavigation)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.DailyFk)
                    .HasConstraintName("FK__Activitie__Daily__3B75D760");
            });

            modelBuilder.Entity<Daily>(entity =>
            {
                entity.HasKey(e => e.Iddaily)
                    .HasName("PK__Dailys__8C9480AC9D71A9D8");

                entity.Property(e => e.Iddaily).HasColumnName("IDDaily");

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Details).HasMaxLength(500);

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.Dailies)
                    .HasForeignKey(d => d.UserFk)
                    .HasConstraintName("FK__Dailys__UserFK__38996AB5");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Idproject)
                    .HasName("PK__Projects__B05299551798A3C5");

                entity.Property(e => e.Idproject).HasColumnName("IDProject");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Project1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Project");

                entity.Property(e => e.TeamLeadFk).HasColumnName("TeamLeadFK");

                entity.HasOne(d => d.TeamLeadFkNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.TeamLeadFk)
                    .HasConstraintName("FK__Projects__TeamLe__3E52440B");
            });

            modelBuilder.Entity<ProjectUserMapping>(entity =>
            {
                entity.HasKey(e => e.IdprojectUserMapping)
                    .HasName("PK__ProjectU__3C900E93CF8218DB");

                entity.Property(e => e.IdprojectUserMapping).HasColumnName("IDProjectUserMapping");

                entity.Property(e => e.ProjectFk).HasColumnName("ProjectFK");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.ProjectFkNavigation)
                    .WithMany(p => p.ProjectUserMappings)
                    .HasForeignKey(d => d.ProjectFk)
                    .HasConstraintName("FK__ProjectUs__Proje__4222D4EF");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.ProjectUserMappings)
                    .HasForeignKey(d => d.UserFk)
                    .HasConstraintName("FK__ProjectUs__UserF__412EB0B6");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Idteam)
                    .HasName("PK__Teams__B1AB4A7BBFD9B81F");

                entity.Property(e => e.Idteam).HasColumnName("IDTeam");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.Team1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Team");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.TeamOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teams__OwnerID__276EDEB3");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeamTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Teams__TeacherID__286302EC");
            });

            modelBuilder.Entity<TeamInvite>(entity =>
            {
                entity.HasKey(e => e.IdteamInvites)
                    .HasName("PK__TeamInvi__89DBB2F45181C691");

                entity.Property(e => e.IdteamInvites).HasColumnName("IDTeamInvites");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamInvites)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TeamInvit__TeamI__2F10007B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamInvites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TeamInvit__UserI__300424B4");
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.HasKey(e => e.IdteamMembers)
                    .HasName("PK__TeamMemb__3D00DD36D4F556D3");

                entity.Property(e => e.IdteamMembers).HasColumnName("IDTeamMembers");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TeamMembe__TeamI__2B3F6F97");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TeamMembe__UserI__2C3393D0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__Users__EAE6D9DF8C21B457");

                entity.HasIndex(e => e.LoginName, "UQ__Users__DB8464FFFABA306D")
                    .IsUnique();

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(36);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.IduserRole)
                    .HasName("PK__UserRole__5A7AF78198A6F276");

                entity.Property(e => e.IduserRole).HasColumnName("IDUserRole");

                entity.Property(e => e.Roll)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserRollMapping>(entity =>
            {
                entity.HasKey(e => e.IduserRoleMapping)
                    .HasName("PK__UserRoll__7BE270B1B2707400");

                entity.Property(e => e.IduserRoleMapping).HasColumnName("IDUserRoleMapping");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.Property(e => e.UserRoleFk).HasColumnName("UserRoleFK");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.UserRollMappings)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRollM__UserF__34C8D9D1");

                entity.HasOne(d => d.UserRoleFkNavigation)
                    .WithMany(p => p.UserRollMappings)
                    .HasForeignKey(d => d.UserRoleFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRollM__UserR__35BCFE0A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
