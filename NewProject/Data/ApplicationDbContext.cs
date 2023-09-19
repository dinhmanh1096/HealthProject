using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;
using System.Security.AccessControl;

namespace NewProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        #region DbSet
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        #endregion
       
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);

            builder.Entity<Sport>(e =>
            {
                e.ToTable("Sport");
                e.HasKey(sp => sp.SportID);
            });

            builder.Entity<Role>(r =>
            {
                r.ToTable("Role");
                r.HasKey(rl => rl.RoleID);
            });

            builder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserID);
                entity.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleID)
                .HasConstraintName("FK_role_user");

            });

            builder.Entity<Workout>(w =>
            {
                w.ToTable("Workout");
                w.HasKey(e => e.WorkoutID);

                w.HasOne(e => e.sport)
                .WithMany(e => e.Workouts)
                .HasForeignKey(e => e.SportID)
                .HasConstraintName("FK_Sport_Workout");
                w.HasOne(e => e.user)
                .WithMany(e => e.Workouts)
                .HasForeignKey(e => e.UserID)
                .HasConstraintName("Fk_User_workout");
            });
           
        }
        private static void SeedRoles(ModelBuilder model)
        {
            model.Entity<Role>().HasData
            (
                new Role() {RoleID=1, RoleName = "Admin"},
                new Role() {RoleID=2, RoleName = "User"}
            );
            
        }
    }
}
