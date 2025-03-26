using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.Entities;

public class Performance
{
    protected Performance() { }

    public Guid Id { get; private set; }
    public Guid PlayId { get; private set; }
    public Play Play { get; private set; }
    public Audience Audience { get; private set; }
    public Invoice Invoice { get; private set; }

    public static Performance Create(Play play, Audience audience)
    {
        return new Performance
        {
            Id = Guid.NewGuid(),
            PlayId = play.Id,
            Play = play,
            Audience = audience
        };
    }

    public Money CalculateAmount() => Play.CalculateAmount(Audience);
    public Credits CalculateCredits() => Play.CalculateCredits(Audience);
}
