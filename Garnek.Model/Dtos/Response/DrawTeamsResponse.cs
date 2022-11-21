namespace Garnek.Model.Dtos.Response;

public record DrawTeamsResponse(Guid GameId, IEnumerable<TeamResponse> Teams);

public record TeamResponse(Guid TeamId, IEnumerable<string> Users);
