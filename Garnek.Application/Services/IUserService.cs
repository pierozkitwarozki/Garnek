using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;

namespace Garnek.Application.Services;

public interface IUserService
{
    Task<InitializeGameResponse> InitializeGameWithUsersAsync(InitializeGameRequest request);
}