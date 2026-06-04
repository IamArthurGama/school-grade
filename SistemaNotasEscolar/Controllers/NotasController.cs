using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Services;

namespace SistemaNotasEscolar.Controllers;

public class NotasController : Controller
{
    private readonly IAlunoService _alunoService;
    private readonly IDisciplinaService _disciplinaService;
    private readonly INotaService _notaService;

    public NotasController(
        IAlunoService alunoService,
        IDisciplinaService disciplinaService,
        INotaService notaService)
    {
        _alunoService = alunoService;
        _disciplinaService = disciplinaService;
        _notaService = notaService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _notaService.GetAllAsync());
    }

    public async Task<IActionResult> Create()
    {
        await PopulateSelectListsAsync();
        return View(new Nota());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Nota nota)
    {
        ModelState.Remove(nameof(Nota.Aluno));
        ModelState.Remove(nameof(Nota.Disciplina));

        if (!ModelState.IsValid)
        {
            await PopulateSelectListsAsync(nota.AlunoId, nota.DisciplinaId);
            return View(nota);
        }

        var result = await _notaService.CreateAsync(nota);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            await PopulateSelectListsAsync(nota.AlunoId, nota.DisciplinaId);
            return View(nota);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var nota = await _notaService.GetByIdAsync(id);
        if (nota is null)
        {
            return NotFound();
        }

        await PopulateSelectListsAsync(nota.AlunoId, nota.DisciplinaId);
        return View(nota);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Nota nota)
    {
        if (id != nota.Id)
        {
            return NotFound();
        }

        ModelState.Remove(nameof(Nota.Aluno));
        ModelState.Remove(nameof(Nota.Disciplina));

        if (!ModelState.IsValid)
        {
            await PopulateSelectListsAsync(nota.AlunoId, nota.DisciplinaId);
            return View(nota);
        }

        var result = await _notaService.UpdateAsync(nota);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            await PopulateSelectListsAsync(nota.AlunoId, nota.DisciplinaId);
            return View(nota);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var nota = await _notaService.GetByIdAsync(id);
        return nota is null ? NotFound() : View(nota);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _notaService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(int? alunoId = null, int? disciplinaId = null)
    {
        var alunos = await _alunoService.GetAllAsync();
        var disciplinas = await _disciplinaService.GetAllAsync();

        ViewBag.AlunoId = new SelectList(alunos, nameof(Aluno.Id), nameof(Aluno.Nome), alunoId);
        ViewBag.DisciplinaId = new SelectList(disciplinas, nameof(Disciplina.Id), nameof(Disciplina.Nome), disciplinaId);
    }
}
