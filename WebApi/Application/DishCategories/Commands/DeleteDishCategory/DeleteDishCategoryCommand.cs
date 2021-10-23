using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DishCategories.Commands.DeleteDishCategory
{
	public class DeleteDishCategoryCommand : IRequest<bool>
	{
		public int DishCategoryId { get; set; }
	}

	public class DeleteDishCategoryCommandHandler : IRequestHandler<DeleteDishCategoryCommand, bool>
	{
		private readonly IGenericRepository<DishCategory> _dishCategoryRepository;

		public DeleteDishCategoryCommandHandler(IGenericRepository<DishCategory> dishCategoryRepository)
		{
			_dishCategoryRepository = dishCategoryRepository;
		}
		public async Task<bool> Handle(DeleteDishCategoryCommand request, CancellationToken cancellationToken)
		{
            DishCategory deletedDishCategory = await _dishCategoryRepository.FirstOrDefault(x => x.Id == request.DishCategoryId);

			if (deletedDishCategory != null)
			{
				await _dishCategoryRepository.Remove(deletedDishCategory);
				return true;
			}

			return false;
		}
	}
}
