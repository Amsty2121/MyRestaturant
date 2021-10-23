using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand : IRequest<bool>
    {
        public int DishId { get; set; }
    }

    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, bool>
    {
        private readonly IGenericRepository<Dish> _dishRepository;

        public DeleteDishCommandHandler(IGenericRepository<Dish> dishRepository)
        {
            _dishRepository = dishRepository;
        }
        public async Task<bool> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            Dish deletedDish = await _dishRepository.FirstOrDefault(x => x.Id == request.DishId);

            if (deletedDish != null)
            {
                await _dishRepository.Remove(deletedDish);
                return true;
            }

            return false;
        }
    }
}