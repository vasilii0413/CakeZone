using CakeZone.EntityFramework.AppDBContext;
using CakeZone.EntityFramework.Entities;
using MediatR;

namespace CakeZone.Web.UI.Fillings.Commands
{
    public record AddFillingCommand(FillingModel Filling) : IRequest<FillingModel>;
    public class AddFillingHandler : IRequestHandler<AddFillingCommand, FillingModel>
    {
        private readonly AppDbContext _context;

        public AddFillingHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FillingModel> Handle(AddFillingCommand request, CancellationToken cancellationToken)
        {
            FillingModel filling = request.Filling;
            await _context.Fillings.AddAsync(filling);
            await _context.SaveChangesAsync();
            return filling;
        }
    }
}
