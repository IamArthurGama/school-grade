using Microsoft.AspNetCore.Mvc;
using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Services;

namespace SistemaNotasEscolar.Controllers;

public class DisciplinasController : Controller
{
    private readonly IDisciplinaService _disciplinaService;

    public DisciplinasController(IDisciplinaService disciplinaService)
    {
        _disciplinaService = disciplinaService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _disciplinaService.GetAllAsync());
    }

    public IActionResult Create()
    {
        return View(new Disciplina());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Disciplina disciplina)
    {
        if (!ModelState.IsValid)
        {
            return View(disciplina);
        }

        var result = await _disciplinaService.CreateAsync(disciplina);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View(disciplina);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var disciplina = await _disciplinaService.GetByIdAsync(id);
        return disciplina is null ? NotFound() : View(disciplina);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Disciplina disciplina)
    {
        if (id != disciplina.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(disciplina);
        }

        var result = await _disciplinaService.UpdateAsync(disciplina);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View(disciplina);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var disciplina = await _disciplinaService.GetByIdAsync(id);
        return disciplina is null ? NotFound() : View(disciplina);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _disciplinaService.DeleteAsync(id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.ErrorMessage;
        }

        return RedirectToAction(nameof(Index));
    }
}
