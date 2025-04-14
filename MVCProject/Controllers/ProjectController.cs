using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Services;
using MVCProject.Dtos;
using System.Threading.Tasks;

public class ProjectController : Controller
{
    
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }
    

    // Visa alla projekt
    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetProjectsAsync();
        return View(projects);
    }

    // Visa detaljer för ett projekt
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
            return BadRequest("Projekt-ID saknas.");

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound("Projektet hittades inte.");

        return View(project);
    }

    // Visa formulär för att skapa ett projekt
    public IActionResult Create()
    {
        return View();
    }

    // Skapa ett nytt projekt
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




    // Visa formulär för att redigera ett projekt
    public async Task<IActionResult> Edit(string id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound("Projektet hittades inte.");
            return View(project);
 
    }

    // Uppdatera ett projekt
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, ProjectUpdateForm projectUpdateForm)
    {
        if (!ModelState.IsValid)
            return View(projectUpdateForm);

        await _projectService.UpdateProjectAsync(id, projectUpdateForm);
        return RedirectToAction(nameof(Index));
    }




    // Visa bekräftelsevy för borttagning
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index)); // Omdirigera till projektlistan om raderingen lyckas
            }
            return NotFound(); // Om något går fel
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
