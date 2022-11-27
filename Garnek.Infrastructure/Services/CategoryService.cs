using Garnek.Application.Repositories;
using Garnek.Application.Services;
using Garnek.Model.Dtos.Response;

namespace Garnek.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var mappedCategories = categories.Select(x => new CategoryResponse(x.Id, x.Name, x.PolishLabel));
        return mappedCategories;
    }
}