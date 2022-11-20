using Garnek.Model.Dtos.Response;

namespace Garnek.Application.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
}