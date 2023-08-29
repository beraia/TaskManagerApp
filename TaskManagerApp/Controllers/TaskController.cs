using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Commands;
using TaskManagerApp.Data;
using TaskManagerApp.Models;
using TaskManagerApp.Queries;

namespace TaskManagerApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public TaskController(ApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task <IActionResult> Index(int pg=1)
        {
            var tasks = _dbContext.Tasks.AsQueryable();

            const int pageSize = 3;
            if(pg < 1)
            {
                pg = 1;
            }

            int rescCount = tasks.Count();

            var pager = new Pager(rescCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = tasks.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel request)
        {
            var task = await _mediator.Send(new CreateTaskCommand() { Title = request.Title, Content = request.Content });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var task = await _mediator.Send(new TaskDetails(id));
            var model = new UpdateTaskViewModel { Id = task.Id, Title = task.Title, Content = task.Content, Status = task.Status };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTaskViewModel request)
        {
            var task = await _mediator.Send(new UpdateTaskCommand(request.Id, request.Title, request.Content, (TaskStatus)request.Status));
            return RedirectToAction("Details");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(request: new DeleteTaskCommand() { Id = id });
            return RedirectToAction("Index");
        }
    }
}
