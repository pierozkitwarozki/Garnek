using Garnek.Application.Services;
using Garnek.Model.Dtos.Response;
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

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Category
    ///
    /// </remarks>
    /// <response code="200">Categories found.</response>
    /// <response code="404">No categories found. </response>
    [HttpGet(nameof(All))]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> All()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return categories.Any() ? Ok(categories) : NotFound();
    }
}