using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Services;
using MVCProject.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


[Authorize]

public class ProjectController : Controller
{
    
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }


    [Authorize]

    public async Task<IActionResult> Index(string status)
    {
        IEnumerable<Project> projects;

        if (string.IsNullOrEmpty(status) || status.ToLower() == "all")
        {
            projects = await _projectService.GetProjectsAsync();
        }
        else
        {
            projects = await _projectService.GetProjectsByStatus(status);
        }

        return View(projects);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
            return BadRequest("Projekt-ID saknas.");

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound("Projektet hittades inte.");

        return View(project);
    }

    public IActionResult Create()
    {
        return View();
    }

    // Skapa ett nytt projekt fick hjälp av ChatGPT genom att skapa en ny projektregistrering 
    [HttpPost]
    public async Task<IActionResult> Create(ProjectRegisteration projectRegisteration)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState är ogiltig");
            return View(projectRegisteration);
        }

        try
        {
            var project = await _projectService.CreateProjectAsync(projectRegisteration);
            Console.WriteLine("skickadesss");
            return RedirectToAction(nameof(Index));


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid skapande av projekt: {ex.Message}");
            ModelState.AddModelError("", "Ett fel uppstod vid skapande av projektet.");
            return View(projectRegisteration);
        }
    }


    // GET: Edit project fick hjälp av ChatGPT för att hantera editering av projekt genom att skicka ett ID
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
            return BadRequest("Projekt-ID saknas.");

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound("Projektet hittades inte.");

        // fick hjälp av ChatGPT för att skapa en ny projektuppdateringsformulär
        var form = new ProjectUpdateForm
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            ClientName = project.ClientName,
            Description = project.Description,
            Budget = project.Budget,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status,
            ImageUrl = project.ImageUrl
        };

        return View(form);
    }

    // POST: Update project
    [HttpPost]
    public async Task<IActionResult> Edit(string id, ProjectUpdateForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        try
        {
            await _projectService.UpdateProjectAsync(id, form);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Ett fel uppstod: {ex.Message}");
            return View(form);
        }
    }




    // fick hjälp av ChatGPT
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index)); // fick hjälp av chatgpt för att komma till projektlistan om raderingen lyckas
            }
            return NotFound(); 
        }
        catch (KeyNotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index)); // Om projektet inte hittades eller kunde raderas
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Ett fel inträffade: " + ex.Message;
            return RedirectToAction(nameof(Index)); // Om något annat fel inträffar
        }
    }

}
