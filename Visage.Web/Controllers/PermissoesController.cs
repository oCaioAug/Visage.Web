using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Visage.Web.Models;

namespace Visage.Web
{
    public class PermissoesController : Controller
    {
        private readonly VisageContext _context;

        public PermissoesController(VisageContext context)
        {
            _context = context;
        }

        // GET: Permissoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Permissoes.ToListAsync());
        }

        // GET: Permissoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisso = await _context.Permissoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permisso == null)
            {
                return NotFound();
            }

            return View(permisso);
        }

        // GET: Permissoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Permissoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomePermissao")] Permisso permisso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permisso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permisso);
        }

        // GET: Permissoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisso = await _context.Permissoes.FindAsync(id);
            if (permisso == null)
            {
                return NotFound();
            }
            return View(permisso);
        }

        // POST: Permissoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomePermissao")] Permisso permisso)
        {
            if (id != permisso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permisso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissoExists(permisso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(permisso);
        }

        // GET: Permissoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisso = await _context.Permissoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permisso == null)
            {
                return NotFound();
            }

            return View(permisso);
        }

        // POST: Permissoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permisso = await _context.Permissoes.FindAsync(id);
            if (permisso != null)
            {
                _context.Permissoes.Remove(permisso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissoExists(int id)
        {
            return _context.Permissoes.Any(e => e.Id == id);
        }
    }
}
