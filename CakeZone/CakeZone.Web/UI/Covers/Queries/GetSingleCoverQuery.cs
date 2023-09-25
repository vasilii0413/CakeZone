using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Covers.Queries
{
    public record GetSingleCoverQuery(byte CoverId) : IRequest<CoverModel>;

    public class GetSingleCoverHandler : IRequestHandler<GetSingleCoverQuery, CoverModel>
    {
        private readonly AppDbContext _context;
        public GetSingleCoverHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CoverModel> Handle(GetSingleCoverQuery request, CancellationToken cancellationToken)
        {
            CoverModel cover = await _context.Covers.FindAsync(request.CoverId);
            return cover;
        }
    }
}
