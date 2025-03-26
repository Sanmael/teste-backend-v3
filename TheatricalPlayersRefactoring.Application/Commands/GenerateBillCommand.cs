using MediatR;

namespace TheatricalPlayersRefactoring.Application.Commands;

public record GenerateBillCommand(
    string CustomerName,
    IEnumerable<PerformanceRequest> Performances,
    StatementFormat Format
) : IRequest<GenerateBillResult>;

public record PerformanceRequest(
    string PlayName,
    int Lines,
    string PlayType,
    int Audience
);

public enum StatementFormat
{
    Text,
    Xml
}