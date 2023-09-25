using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using CakeZone.Web.UI.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CakeZone.Web.UI.Orders
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public OrdersController(AppDbContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public IActionResult Create(byte CakeId)
        {
            ViewData["CakeId"] = new SelectList(_context.Set<CakeModel>(), "CakeId", "CakeId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CakeId,UserFullName,UserEmail,PhoneNumber")] OrderModel order)
        {
            try
            {
                order.CakeId = Convert.ToByte(Request.Form["CakeId"]);
                
                await _mediator.Send(new AddOrderCommand(order));
                return RedirectToAction("Index", "Cakes");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
