using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using CakeZone.Web.UI.Cakes.Commands;
using CakeZone.Web.UI.Cakes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Web.UI.Cakes
{
    public class CakesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public CakesController(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var cakes = await _mediator.Send(new GetAllCakesQuery());
            return View(cakes);
        }

        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await _mediator.Send(new GetSingleCakeQuery(id.Value));

            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }

        public IActionResult Create()
        {
            ViewData["Fillings"] = new SelectList(_context.Fillings, "FillingId", "FillingType");
            ViewData["Covers"] = new SelectList(_context.Covers, "CoverId", "CoverName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CakeId,CakeName,CakeDescription,Price,Weight,ImageURL,CoverId,FillingId")] CakeModel cake)
        {
            try
            {
                await _mediator.Send(new AddCakeCommand(cake));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Cakes == null)
            {
                return NotFound();
            }

            var cake = await _context.Cakes.FindAsync(id);
            if (cake == null)
            {
                return NotFound();
            }
            ViewData["Fillings"] = new SelectList(_context.Fillings, "FillingId", "FillingType");
            ViewData["Covers"] = new SelectList(_context.Covers, "CoverId", "CoverName");
            return View(cake);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("CakeId,CakeName,CakeDescription,Price,Weight,ImageURL,CoverId,FillingId")] CakeModel cake)
        {

            try
            {
                await _mediator.Send(new UpdateCakeCommand(cake));
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Cakes == null)
            {
                return NotFound();
            }

            var cake = await _mediator.Send(new GetSingleCakeQuery(id.Value));

            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                var cake = await _mediator.Send(new GetSingleCakeQuery(id));
                if (cake == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteCakeCommand(cake));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
