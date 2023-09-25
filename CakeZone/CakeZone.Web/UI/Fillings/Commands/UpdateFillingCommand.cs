using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Fillings.Commands
{
    public record UpdateFillingCommand(FillingModel Filling) : IRequest<FillingModel>;
    public class UpdateFillingHandler : IRequestHandler<UpdateFillingCommand, FillingModel>
    {
        private readonly AppDbContext _context;

        public UpdateFillingHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FillingModel> Handle(UpdateFillingCommand request, CancellationToken cancellationToken)
        {
            FillingModel filling = request.Filling;
            _context.Fillings.Update(filling);
            await _context.SaveChangesAsync();
            return filling;
        }
    }
}
