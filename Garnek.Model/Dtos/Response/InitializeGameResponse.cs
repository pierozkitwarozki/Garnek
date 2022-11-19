namespace Garnek.Model.Dtos.Response;

public record InitializeGameResponse(string GameId, IEnumerable<string> Urls);