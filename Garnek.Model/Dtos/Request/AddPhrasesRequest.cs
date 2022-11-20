namespace Garnek.Model.Dtos.Request;

public record AddPhrasesRequest(string GameId, string UserName, Dictionary<string, IEnumerable<string>> Phrases);