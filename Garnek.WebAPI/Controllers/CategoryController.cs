using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace Garnek.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet(nameof(All))]
    public async Task<IActionResult> All()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return categories.Any() ? Ok(categories) : NotFound();
    }
}