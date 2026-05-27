using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Data;
using EmployeesManagement.Models;

namespace EmployeesManagement.Controllers;

[Authorize]
public class InstrumentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public InstrumentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index() => View(await _context.Instruments.ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var instrument = await _context.Instruments.FirstOrDefaultAsync(m => m.Id == id);
        return instrument == null ? NotFound() : View(instrument);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Instrument instrument)
    {
        if (ModelState.IsValid)
        {
            _context.Add(instrument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(instrument);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var instrument = await _context.Instruments.FindAsync(id);
        return instrument == null ? NotFound() : View(instrument);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Instrument instrument)
    {
        if (id != instrument.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(instrument);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentExists(id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(instrument);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var instrument = await _context.Instruments.FirstOrDefaultAsync(m => m.Id == id);
        return instrument == null ? NotFound() : View(instrument);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var instrument = await _context.Instruments.FindAsync(id);
        if (instrument != null) _context.Instruments.Remove(instrument);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InstrumentExists(int id) => _context.Instruments.Any(e => e.Id == id);
}
