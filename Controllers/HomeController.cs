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
        await _taskService.CreateAsync(task);
        return RedirectToAction("Index");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(string id, [FromBody] TodoTaskModel editetTask) {
        var toUpdateTask = await _taskService.GetAsync(id); 
        if (toUpdateTask == null) {
            return NotFound(); 
        }

        toUpdateTask.Name = editetTask.Name; 
        await _taskService.UpdateAsync(id, toUpdateTask);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(string id) {
        await _taskService.RemoveAsync(id);
        return Ok();
    }
}
