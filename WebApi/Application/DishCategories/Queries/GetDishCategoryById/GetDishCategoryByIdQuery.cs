using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DishCategories.Queries.GetDishCategoryById
{
    public class GetDishCategoryByIdQuery : IRequest<DishCategory>
    {
        public int DishCategoryId { get; set; }
    }

    public class GetDishCategoryByIdQueryHandler : IRequestHandler<GetDishCategoryByIdQuery, DishCategory>
    {
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;

        public GetDishCategoryByIdQueryHandler(IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishCategoryRepository = dishCategoryRepository;
        }

        public async Task<DishCategory> Handle(GetDishCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            DishCategory dishCategory = await _dishCategoryRepository.GetByIdWithInclude(request.DishCategoryId, x => x.Dishes);

            if (dishCategory == null)
            {
                throw new EntityDoesNotExistException("The DishCategory does not exist");
            }

            return dishCategory;
        }
    }
}