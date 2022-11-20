namespace Garnek.Model.Dtos.Request;

public record AddPhrasesRequest(string GameId, string UserName, Dictionary<Guid, IEnumerable<string>> Phrases);