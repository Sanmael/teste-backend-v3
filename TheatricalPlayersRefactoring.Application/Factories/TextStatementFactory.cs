using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Application.Factories;

public class TextStatementFactory : IStatementFactory
{
    private readonly CultureInfo _cultureInfo = new("en-US");

    public string Generate(Invoice invoice)
    {
        var result = new StringBuilder();
        result.AppendFormat("Statement for {0}\n", invoice.Customer.Value);

        foreach (var performance in invoice.Performances)
        {
            var amount = performance.CalculateAmount();
            result.AppendFormat(_cultureInfo,
                "  {0}: {1:C} ({2} seats)\n",
                performance.Play.Name,
                amount.Value,
                performance.Audience.Value);
        }

        result.AppendFormat(_cultureInfo, "Amount owed is {0:C}\n",
            invoice.CalculateTotalAmount().Value);
        result.AppendFormat("You earned {0} credits\n", invoice.TotalCredits.Value);

        return result.ToString();
    }
}