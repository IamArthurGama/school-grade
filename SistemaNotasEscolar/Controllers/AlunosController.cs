using Microsoft.AspNetCore.Mvc;
using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Services;

namespace SistemaNotasEscolar.Controllers;

public class AlunosController : Controller
{
    private readonly IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _alunoService.GetAllAsync());
    }

    public IActionResult Create()
    {
        return View(new Aluno());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Aluno aluno)
    {
        if (!ModelState.IsValid)
        {
            return View(aluno);
        }

        var result = await _alunoService.CreateAsync(aluno);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View(aluno);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var aluno = await _alunoService.GetByIdAsync(id);
        return aluno is null ? NotFound() : View(aluno);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Aluno aluno)
    {
        if (id != aluno.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(aluno);
        }

        var result = await _alunoService.UpdateAsync(aluno);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View(aluno);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var aluno = await _alunoService.GetByIdAsync(id);
        return aluno is null ? NotFound() : View(aluno);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _alunoService.DeleteAsync(id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.ErrorMessage;
        }

        return RedirectToAction(nameof(Index));
    }
}
