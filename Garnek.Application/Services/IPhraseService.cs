using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;

namespace Garnek.Application.Services;

public interface IPhraseService
{
    Task AddPhrasesAsync(AddPhrasesRequest request);
    Task<GetPhrasesResponse> GetPhrasesForGameAsync(Guid gameId);
}