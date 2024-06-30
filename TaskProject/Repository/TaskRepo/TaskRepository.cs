﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskProject.Repository.ProjectRepo;
using Task = TaskProject.Models.Task;

namespace TaskProject.Repository.TaskRepo
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        private readonly Context context;

        public TaskRepository(Context _context) : base(_context)
        {
            this.context = _context;
        }
        public async Task<List<Task>> GetAllAsync()
        {
            List<Task> tasks =
             await context.Tasks
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Subtasks)
                .ToListAsync();
        
            
            return tasks;
        }

  
        public async Task<Task> GetSpecificAsync(int id)
        {
            return await context.Tasks
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Subtasks)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //public async Task<bool> DeleteTask(int id)
        //{
        //    Task task = await GetSpecificAsync(id);
        //    if (task != null)
        //    {
        //        task.IsDeleted = true;
        //        foreach (var t in task.Subtasks)
        //        { t.IsDeleted = true; }
        //        context.Tasks.Remove(task);
        //        //await context.SaveChangesAsync(); // Ensure changes are saved asynchronously
        //        return true;
        //    }
        //    return false;
        //}

        public async Task<bool> DeleteTask(int taskId)
        {
            var task = await context.Tasks
                .Include(t => t.Subtasks)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task != null)
            {
                task.IsDeleted = true;

                // Mark all related Subtasks as deleted
                if (task.Subtasks != null)
                {
                    foreach (var subtask in task.Subtasks)
                    {
                        subtask.IsDeleted = true;
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertTaskAsync(Task task)
        {
            if (task != null)
            {
                await context.Tasks.AddAsync(task);
                return true;
            }
            return false;
        }


    }
}
