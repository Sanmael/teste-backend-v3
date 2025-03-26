using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Strategies;

namespace TheatricalPlayersRefactoring.Domain.Factories;

public class BillingStrategyFactory
{
    private readonly Dictionary<PlayType, IBillingStrategy> _strategies;

    public BillingStrategyFactory()
    {
        _strategies = new Dictionary<PlayType, IBillingStrategy>
    {
        { PlayType.Comedy, new ComedyBillingStrategy() },
        { PlayType.Tragedy, new TragedyBillingStrategy() },
        { PlayType.History, new HistoryBillingStrategy() }
    };
    }

    public IBillingStrategy GetStrategy(PlayType playType)
    {
        if (_strategies.TryGetValue(playType, out var strategy))
            return strategy;

        throw new ArgumentException($"Invalid play type: {playType}");
    }
}