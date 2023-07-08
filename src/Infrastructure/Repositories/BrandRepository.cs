using GrowManager.Application.Interfaces.Repositories;
using GrowManager.Domain.Entities.Catalog;

namespace GrowManager.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}