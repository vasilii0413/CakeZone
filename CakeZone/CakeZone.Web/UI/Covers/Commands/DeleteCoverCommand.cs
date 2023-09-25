using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Covers.Commands
{
    public record DeleteCoverCommand(CoverModel Cover) : IRequest<CoverModel>;
    public class DeleteCoverHandler : IRequestHandler<DeleteCoverCommand, CoverModel>
    {
        private readonly AppDbContext _context;

        public DeleteCoverHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoverModel> Handle(DeleteCoverCommand request, CancellationToken cancellationToken)
        {
            CoverModel cover = request.Cover;
            _context.Covers.Remove(cover);
            await _context.SaveChangesAsync();
            return cover;
        }
    }
}
