using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using CakeZone.Web.UI.Covers.Commands;
using CakeZone.Web.UI.Covers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Web.UI.Covers
{
    public class CoversController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public CoversController(AppDbContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var covers = await _mediator.Send(new GetAllCoversQuery());
            return View(covers);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoverId,CoverName,CoverDescription")] CoverModel cover)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new AddCoverCommand(cover));
                return RedirectToAction(nameof(Index));
            }
            return View(cover);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Covers == null)
            {
                return NotFound();
            }

            var cover = await _context.Covers.FindAsync(id);

            if (cover == null)
            {
                return NotFound();
            }
            return View(cover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("CoverId,CoverName,CoverDescription")] CoverModel cover)
        {
            if (id != cover.CoverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _mediator.Send(new UpdateCoverCommand(cover));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoverExists(cover.CoverId))
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
            return View(cover);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Covers == null)
            {
                return NotFound();
            }

            var cover = await _mediator.Send(new GetSingleCoverQuery(id.Value));

            if (cover == null)
            {
                return NotFound();
            }

            return View(cover);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var cover = await _mediator.Send(new GetSingleCoverQuery(id));

            if (cover == null)
            {
                return NotFound();
            }
            await _mediator.Send(new DeleteCoverCommand(cover));

            return RedirectToAction(nameof(Index));
        }

        private bool CoverExists(byte id)
        {
            return (_context.Covers?.Any(e => e.CoverId == id)).GetValueOrDefault();
        }

    }
}
