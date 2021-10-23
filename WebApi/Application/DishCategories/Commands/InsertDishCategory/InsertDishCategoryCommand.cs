using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.DishCategories;

namespace Application.DishCategories.Commands.InsertDishCategory
{
    public class InsertDishCategoryCommand : IRequest<DishCategory>
    {
        public InsertDishCategoryDto Dto { get; set; }
    }
    
    public class InsertDishCategoryCommandHandler : IRequestHandler<InsertDishCategoryCommand, DishCategory>
    {
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;

        public InsertDishCategoryCommandHandler(IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishCategoryRepository = dishCategoryRepository;
        }

        public async Task<DishCategory> Handle(InsertDishCategoryCommand request, CancellationToken cancellationToken)
        {
            DishCategory dishCategory = await _dishCategoryRepository.FirstOrDefault(x => x.DishCategoryName == request.Dto.DishCategoryName);
            if (dishCategory != null)
            {
                throw new EntityAlreadyExistsException("This DishCategory already exists");
            }

            dishCategory = new DishCategory()
            {
                DishCategoryName = request.Dto.DishCategoryName
            };

            await _dishCategoryRepository.Add(dishCategory);

            return dishCategory;
        }
    }
}