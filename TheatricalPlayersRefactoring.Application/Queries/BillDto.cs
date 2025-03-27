namespace TheatricalPlayersRefactoring.Application.Queries;

public record BillDto(
    Guid Id,
    string CustomerName,
    string Content,
    string Format,
    string ContentType
);

public record FileDto(    
    string Content,    
    string Format,
    string ContentType
);