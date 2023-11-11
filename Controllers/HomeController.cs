using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly TaskService _taskService;

    public HomeController(TaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _taskService.GetAsync();

        return View(tasks);
    }

    public IActionResult CreatePage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TodoTaskModel task)
    {
        if (ModelState.IsValid)
        {
            await _taskService.CreateAsync(task);
            return RedirectToAction("Index");
        }
        else
        {
            Console.WriteLine("Invalid Task");
        }

        return View("Index", await _taskService.GetAsync());
    }
}
