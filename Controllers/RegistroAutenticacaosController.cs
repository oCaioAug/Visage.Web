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
    public class RegistroAutenticacaosController : Controller
    {
        private readonly VisageContext _context;

        public RegistroAutenticacaosController(VisageContext context)
        {
            _context = context;
        }

        // GET: RegistroAutenticacaos
        public async Task<IActionResult> Index()
        {
            var visageContext = _context.RegistroAutenticacaos.Include(r => r.Usuarios);
            return View(await visageContext.ToListAsync());
        }

        // GET: RegistroAutenticacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroAutenticacao = await _context.RegistroAutenticacaos
                .Include(r => r.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroAutenticacao == null)
            {
                return NotFound();
            }

            return View(registroAutenticacao);
        }

        // GET: RegistroAutenticacaos/Create
        public IActionResult Create()
        {
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            return View();
        }

        // POST: RegistroAutenticacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuariosId,DataHora,Status,TipoAcesso")] RegistroAutenticacao registroAutenticacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroAutenticacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Nome", registroAutenticacao.UsuariosId);
            return View(registroAutenticacao);
        }

        // GET: RegistroAutenticacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroAutenticacao = await _context.RegistroAutenticacaos.FindAsync(id);
            if (registroAutenticacao == null)
            {
                return NotFound();
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Nome", registroAutenticacao.UsuariosId);
            return View(registroAutenticacao);
        }

        // POST: RegistroAutenticacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuariosId,DataHora,Status,TipoAcesso")] RegistroAutenticacao registroAutenticacao)
        {
            if (id != registroAutenticacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroAutenticacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroAutenticacaoExists(registroAutenticacao.Id))
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
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Nome", registroAutenticacao.UsuariosId);
            return View(registroAutenticacao);
        }

        // GET: RegistroAutenticacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroAutenticacao = await _context.RegistroAutenticacaos
                .Include(r => r.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroAutenticacao == null)
            {
                return NotFound();
            }

            return View(registroAutenticacao);
        }

        // POST: RegistroAutenticacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroAutenticacao = await _context.RegistroAutenticacaos.FindAsync(id);
            if (registroAutenticacao != null)
            {
                _context.RegistroAutenticacaos.Remove(registroAutenticacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroAutenticacaoExists(int id)
        {
            return _context.RegistroAutenticacaos.Any(e => e.Id == id);
        }
    }
}
