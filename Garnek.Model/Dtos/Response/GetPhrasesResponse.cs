namespace Garnek.Model.Dtos.Response;

public record GetPhrasesResponse(string GameId, IEnumerable<string> Phrases);
