using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Covers.Commands
{
    public record AddCoverCommand(CoverModel Cover) : IRequest<CoverModel>;
    public class AddCoverHandler : IRequestHandler<AddCoverCommand, CoverModel>
    {
        private readonly AppDbContext _context;

        public AddCoverHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoverModel> Handle(AddCoverCommand request, CancellationToken cancellationToken)
        {
            CoverModel cover = request.Cover;
            await _context.Covers.AddAsync(cover);
            await _context.SaveChangesAsync();
            return cover;
        }
    }
}
