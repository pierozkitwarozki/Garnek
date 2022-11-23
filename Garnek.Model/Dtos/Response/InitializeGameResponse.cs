namespace Garnek.Model.Dtos.Response;

public record InitializeGameResponse(Guid GameId, IEnumerable<string> Urls);