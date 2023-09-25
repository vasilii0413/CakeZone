using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Web.UI.Fillings.Queries
{
    public record GetAllFillingsQuery : IRequest<List<FillingModel>>;
    public class GetAllFillingsHandler : IRequestHandler<GetAllFillingsQuery, List<FillingModel>>
    {
        private readonly AppDbContext _context;
        public GetAllFillingsHandler(AppDbContext context) => _context = context;

        public async Task<List<FillingModel>> Handle(GetAllFillingsQuery request, CancellationToken cancellationToken)
        {
            List<FillingModel> fillings = await _context.Fillings.ToListAsync();
            return fillings;
        }
    }
}
