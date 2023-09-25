using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Fillings.Commands
{
    public record DeleteFillingCommand(FillingModel Filling) : IRequest<FillingModel>;
    public class DeleteFillingHandler : IRequestHandler<DeleteFillingCommand, FillingModel>
    {
        private readonly AppDbContext _context;

        public DeleteFillingHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FillingModel> Handle(DeleteFillingCommand request, CancellationToken cancellationToken)
        {
            FillingModel filling = request.Filling;
            _context.Fillings.Remove(filling);
            await _context.SaveChangesAsync();
            return filling;
        }
    }
}
