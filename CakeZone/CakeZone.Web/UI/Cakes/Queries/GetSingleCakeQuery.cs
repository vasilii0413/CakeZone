using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Web.UI.Cakes.Queries
{
    public record GetSingleCakeQuery(byte CakeId) : IRequest<CakeModel>;

    public class GetSingleCakeHandler : IRequestHandler<GetSingleCakeQuery, CakeModel>
    {
        private readonly AppDbContext _context;
        public GetSingleCakeHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CakeModel> Handle(GetSingleCakeQuery request, CancellationToken cancellationToken)
        {
            var cake = await _context.Cakes
                             .Include(c => c.Cover)
                             .Include(c => c.Filling)
                             .FirstOrDefaultAsync(c => c.CakeId == request.CakeId);
            return cake;
        }
    }
}
