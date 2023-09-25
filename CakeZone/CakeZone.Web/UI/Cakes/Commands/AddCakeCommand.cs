using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Cakes.Commands
{
    public record AddCakeCommand(CakeModel Cake) : IRequest<CakeModel>;
    public class AddCakeHandler : IRequestHandler<AddCakeCommand, CakeModel>
    {
        private readonly AppDbContext _context;

        public AddCakeHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CakeModel> Handle(AddCakeCommand request, CancellationToken cancellationToken)
        {
            CakeModel cake = request.Cake;
            await _context.Cakes.AddAsync(cake);
            await _context.SaveChangesAsync();
            return cake;
        }
    }
}
