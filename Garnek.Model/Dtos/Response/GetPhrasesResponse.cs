namespace Garnek.Model.Dtos.Response;

public record GetPhrasesResponse(Guid GameId, IEnumerable<string> Phrases);
