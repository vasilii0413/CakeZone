using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Web.UI.Covers.Queries
{
    public record GetAllCoversQuery : IRequest<List<CoverModel>>;
    public class GetAllCoversHandler : IRequestHandler<GetAllCoversQuery, List<CoverModel>>
    {
        private readonly AppDbContext _context;
        public GetAllCoversHandler(AppDbContext context) => _context = context;

        public async Task<List<CoverModel>> Handle(GetAllCoversQuery request, CancellationToken cancellationToken)
        {
            List<CoverModel> covers = await _context.Covers.ToListAsync();
            return covers;
        }
    }
}
