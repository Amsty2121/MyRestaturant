using System.Text.RegularExpressions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DishCategories.Commands.UpdateDishCategory
{
    public class UpdateDishCategoryCommand : IRequest<DishCategory>
    {
        public int DishCategoryId { get; set; }
        public string DishCategoryName { get; set; }
    }

    public class UpdateDishCategoryCommandHandler : IRequestHandler<UpdateDishCategoryCommand, DishCategory>
    {
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;

        public UpdateDishCategoryCommandHandler(IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishCategoryRepository = dishCategoryRepository;
        }
        public async Task<DishCategory> Handle(UpdateDishCategoryCommand request, CancellationToken cancellationToken)
        {
            DishCategory updatedDishCategory = await _dishCategoryRepository.FirstOrDefault(x => x.Id == request.DishCategoryId);

            if (updatedDishCategory == null)
            {
                throw new EntityDoesNotExistException("The DishCategory does not exist");
            }

            if (request.DishCategoryName != null && request.DishCategoryName.Length > 0)
            {
                updatedDishCategory.DishCategoryName = request.DishCategoryName;
            }

            await _dishCategoryRepository.Update(updatedDishCategory);

            return updatedDishCategory;
        }
    }
}