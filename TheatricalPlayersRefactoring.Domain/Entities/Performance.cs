namespace TheatricalPlayersRefactoring.Domain.Entities;

public class Performance
{
    public Guid Id { get; private set; }
    public Guid PlayId { get; private set; }
    public Play Play { get; private set; }
    public Invoice Invoice { get; private set; }

    public static Performance Create(Play play)
    {
        return new Performance
        {
            Id = Guid.NewGuid(),
            PlayId = play.Id,
            Play = play,
        };
    }
}