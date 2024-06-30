using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskProject.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<UserTasks> UserTasks { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

       
           protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure soft delete for Project entity
            modelBuilder.Entity<Project>()
                .Property<bool>("IsDeleted")
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<Project>()
                .HasQueryFilter(p => !EF.Property<bool>(p, "IsDeleted"));

            // Configure soft delete for Task entity
            modelBuilder.Entity<Task>()
                .Property<bool>("IsDeleted")
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<Task>()
                .HasQueryFilter(t => !EF.Property<bool>(t, "IsDeleted"));

            // Configure soft delete for Subtask entity
            modelBuilder.Entity<Subtask>()
                .Property<bool>("IsDeleted")
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<Subtask>()
                .HasQueryFilter(s => !EF.Property<bool>(s, "IsDeleted"));

            // Project-Task relationship
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            // Task-Subtask relationship
            modelBuilder.Entity<Task>()
                .HasMany(t => t.Subtasks)
                .WithOne(s => s.Task)
                .HasForeignKey(s => s.TaskId)
                .OnDelete(DeleteBehavior.NoAction);

            // Task-UserTasks relationship
            modelBuilder.Entity<Task>()
                .HasMany(t => t.UserTasks)
                .WithOne(ut => ut.Task)
                .HasForeignKey(ut => ut.TaskId)
                .OnDelete(DeleteBehavior.NoAction);

            // ApplicationUser-UserTasks relationship
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserTasks)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}