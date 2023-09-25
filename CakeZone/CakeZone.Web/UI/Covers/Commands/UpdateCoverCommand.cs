using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Covers.Commands
{
    public record UpdateCoverCommand(CoverModel Cover) : IRequest<CoverModel>;
    public class UpdateCoverHandler : IRequestHandler<UpdateCoverCommand, CoverModel>
    {
        private readonly AppDbContext _context;

        public UpdateCoverHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoverModel> Handle(UpdateCoverCommand request, CancellationToken cancellationToken)
        {
            CoverModel cover = request.Cover;
            _context.Covers.Update(cover);
            await _context.SaveChangesAsync();
            return cover;
        }
    }
}
