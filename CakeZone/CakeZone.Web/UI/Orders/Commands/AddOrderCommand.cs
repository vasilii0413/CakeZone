using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Orders.Commands
{
    public record AddOrderCommand(OrderModel Order): IRequest<OrderModel>;
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, OrderModel>
    {
        private readonly AppDbContext _context;

        public AddOrderHandler(AppDbContext context)
        {
            _context = context;
        }
       
        public async Task<OrderModel> Handle(AddOrderCommand request,CancellationToken cancellationToken)
        {
            OrderModel order = request.Order;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
