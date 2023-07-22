using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var tasks = await _dbContext.Tasks.ToListAsync();
            return View(tasks);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel request)
        {
            Models.Domain.Task task = new ()
            {
                Title = request.Title,
                Content = request.Content
            };

            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if(task != null)
            {
                var viewModel = new UpdateTaskViewModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Content = task.Content
                };

                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateTaskViewModel request)
        {
            var task = await _dbContext.Tasks.FindAsync(request.Id);

            if(task != null)
            {
                task.Title = request.Title;
                task.Content = request.Content;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateTaskViewModel request)
        {
            var task = await _dbContext.Tasks.FindAsync(request.Id);

            if(task != null)
            {
                _dbContext.Tasks.Remove(task);

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
