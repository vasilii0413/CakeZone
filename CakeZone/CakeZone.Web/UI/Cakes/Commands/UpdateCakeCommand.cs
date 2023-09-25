using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Cakes.Commands
{
    public record UpdateCakeCommand(CakeModel Cake) : IRequest<CakeModel>;
    public class UpdateCakeHandler : IRequestHandler<UpdateCakeCommand, CakeModel>
    {
        private readonly AppDbContext _context;

        public UpdateCakeHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CakeModel> Handle(UpdateCakeCommand request, CancellationToken cancellationToken)
        {
            CakeModel cake = request.Cake;
            _context.Cakes.Update(cake);
            await _context.SaveChangesAsync();
            return cake;
        }
    }
}
