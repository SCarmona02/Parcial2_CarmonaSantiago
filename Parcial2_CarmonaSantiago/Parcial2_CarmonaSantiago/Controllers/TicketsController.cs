using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial2_CarmonaSantiago.DAL;
using Parcial2_CarmonaSantiago.DAL.Entities;

namespace Parcial2_CarmonaSantiago.Controllers
{
    public class TicketsController : Controller
    {
        #region Constructor

        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }

        #endregion

        #region Private Methods

        private async Task<Ticket> GetTicketById(Guid? id)
        {
            Ticket ticket = await _context.Tickets
                .FirstOrDefaultAsync(ticket => ticket.Id == id);
            return ticket;
        }

        #endregion
        // GET: Tickets
        public async Task<IActionResult> Index()
        {
              return _context.Tickets != null ? 
                          View(await _context.Tickets.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.Tickets'  is null.");
        }

        public async Task<IActionResult> Consultation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Consultation(Guid? id)
        {

            Ticket ticket = await GetTicketById(id);

            if (ticket == null)
            {
                ModelState.AddModelError(string.Empty, "Boleta no válida");
                return View(ticket);
            }

            else if (ticket.IsUsed == true)
            {
                ModelState.AddModelError(string.Empty, $"Esta boleta ya fue usada el {ticket.UseDate}, en la porteria {ticket.EntranceGate}");
                return View(ticket);
            }
            return RedirectToAction(nameof(Edit), new { ticket.Id });
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        private bool TicketExists(Guid id)
        {
          return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
