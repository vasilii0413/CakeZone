using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using CakeZone.Web.UI.Fillings.Commands;
using CakeZone.Web.UI.Fillings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Web.UI.Fillings
{
    public class FillingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public FillingsController(AppDbContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var fillings = await _mediator.Send(new GetAllFillingsQuery());
            return View(fillings);
            
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FillingId,FillingType,FillingDescription")] FillingModel filling)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new AddFillingCommand(filling));
                return RedirectToAction(nameof(Index));
            }
            return View(filling);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Fillings == null)
            {
                return NotFound();
            }

            var filling = await _context.Fillings.FindAsync(id);

            if (filling == null)
            {
                return NotFound();
            }
            return View(filling);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("FillingId,FillingType,FillingDescription")] FillingModel filling)
        {
            if (id != filling.FillingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(new UpdateFillingCommand(filling));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FillingExists(filling.FillingId))
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
            return View(filling);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Fillings == null)
            {
                return NotFound();
            }

            var filling = await _mediator.Send(new GetSingleFillingQuery(id.Value));

            if (filling == null)
            {
                return NotFound();
            }

            return View(filling);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var filling = await _mediator.Send(new GetSingleFillingQuery(id));

            if (filling == null)
            {
                return NotFound();
            }
            await _mediator.Send(new DeleteFillingCommand(filling));

            return RedirectToAction(nameof(Index));
        }

        private bool FillingExists(byte id)
        {
            return (_context.Fillings?.Any(e => e.FillingId == id)).GetValueOrDefault();
        }
    }
}
