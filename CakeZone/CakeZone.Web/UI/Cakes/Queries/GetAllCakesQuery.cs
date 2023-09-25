using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Web.UI.Cakes.Queries
{
    public record GetAllCakesQuery : IRequest<List<CakeModel>>;
    public class GetAllCakesHandler : IRequestHandler<GetAllCakesQuery, List<CakeModel>>
    {
        private readonly AppDbContext _context;
        public GetAllCakesHandler(AppDbContext context) => _context = context;

        public async Task<List<CakeModel>> Handle(GetAllCakesQuery request, CancellationToken cancellationToken)
        {
            List<CakeModel> cakes = await _context.Cakes
                .Include(c => c.Cover)
                .Include(c => c.Filling)
                .ToListAsync();
            return cakes;
        }
    }
}
