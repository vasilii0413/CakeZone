using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Fillings.Queries
{
    public record GetSingleFillingQuery(byte FillingId) : IRequest<FillingModel>;

    public class GetSingleFillingHandler : IRequestHandler<GetSingleFillingQuery, FillingModel>
    {
        private readonly AppDbContext _context;
        public GetSingleFillingHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<FillingModel> Handle(GetSingleFillingQuery request, CancellationToken cancellationToken)
        {
            FillingModel filling = await _context.Fillings.FindAsync(request.FillingId);
            return filling;
        }
    }
}
