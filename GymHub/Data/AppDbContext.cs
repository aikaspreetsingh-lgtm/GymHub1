using System.Diagnostics.Contracts;
using GymHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymHub.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
     
    
        public DbSet<NutritionPlan> NutritionPlans { get; set; }
       
        public DbSet<GymClass> GymClasses { get; set; }
       

        public DbSet<ProgressMetric> ProgressMetric { get; set; }
        public DbSet<WorkoutSession> WorkoutSession { get; set; }
        public DbSet<WorkoutSet> WorkoutSet { get; set; }
        public DbSet<ClassBooking> ClassBooking { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User -> WorkoutSessions
            builder.Entity<WorkoutSession>()
                .HasOne(ws => ws.User)
                .WithMany(u => u.WorkoutSession)
                .HasForeignKey(ws => ws.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> ProgressMetrics
            builder.Entity<ProgressMetric>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProgressMetric)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> ClassBookings
            builder.Entity<ClassBooking>()
                .HasOne(cb => cb.User)
                .WithMany(u => u.ClassBookings)
                .HasForeignKey(cb => cb.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> NutritionPlans
            builder.Entity<NutritionPlan>()
                .HasOne(np => np.User)
                .WithMany(u => u.NutritionPlans)
                .HasForeignKey(np => np.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> WorkoutPlans
            builder.Entity<WorkoutPlan>()
                .HasOne(wp => wp.User)
                .WithMany(u => u.WorkoutPlans)
                .HasForeignKey(wp => wp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // WorkoutSession -> WorkoutSets
            builder.Entity<WorkoutSet>()
                .HasOne(ws => ws.WorkoutSession)
                .WithMany(wsession => wsession.Sets)
                .HasForeignKey(ws => ws.WorkoutSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // WorkoutSet -> Exercises (Restrict to prevent deleting exercises in use)
            builder.Entity<WorkoutSet>()
                .HasOne(ws => ws.Exercise)
                .WithMany()
                .HasForeignKey(ws => ws.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            // GymClass -> ClassBookings
            builder.Entity<ClassBooking>()
                .HasOne(cb => cb.GymClass)
                .WithMany(gc => gc.Bookings)
                .HasForeignKey(cb => cb.GymClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
