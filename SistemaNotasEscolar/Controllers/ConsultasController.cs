using Microsoft.AspNetCore.Mvc;
using SistemaNotasEscolar.Services;

namespace SistemaNotasEscolar.Controllers;

public class ConsultasController : Controller
{
    private readonly IConsultaService _consultaService;

    public ConsultasController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> NotasPorAlunoDisciplina()
    {
        return View(await _consultaService.GetNotasPorAlunoDisciplinaAsync());
    }

    public async Task<IActionResult> MediasPorDisciplina()
    {
        return View(await _consultaService.GetMediasPorDisciplinaAsync());
    }

    public async Task<IActionResult> Filtros()
    {
        ViewBag.NotasAprovadas = await _consultaService.GetNotasAprovadasAsync();
        ViewBag.MediasAprovadas = await _consultaService.GetDisciplinasComMediaMinimaAsync();
        return View();
    }
}
