using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Cakes.Commands
{
    public record DeleteCakeCommand(CakeModel Cake) : IRequest<CakeModel>;
    public class DeleteCakeHandler : IRequestHandler<DeleteCakeCommand, CakeModel>
    {
        private readonly AppDbContext _context;

        public DeleteCakeHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CakeModel> Handle(DeleteCakeCommand request, CancellationToken cancellationToken)
        {
            CakeModel cake = request.Cake;
            _context.Cakes.Remove(cake);
            await _context.SaveChangesAsync();
            return cake;
        }
    }
}
