using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayersRefactoring.Application.Models;
using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Application.Factories
{
    public class XmlStatementFactory : IStatementFactory
    {
        private readonly CultureInfo _cultureInfo = new("en-US");

        private string FormatAmount(decimal value)
        {
            string formatted = value.ToString("F1", _cultureInfo);
            return formatted.EndsWith(".0") ? formatted[..^2] : formatted;
        }

        public string Generate(Invoice invoice)
        {
            var statement = new XmlStatement
            {
                Customer = invoice.Customer.Value,
                Items = invoice.Performances.Select(p => new XmlStatementItem
                {
                    AmountOwed = FormatAmount(p.Amount),
                    EarnedCredits = FormatAmount(p.Credits),
                    Seats = FormatAmount(p.Audience.Value)
                }).ToList(),
                AmountOwed = FormatAmount(invoice.CalculateTotalAmount().Value),
                EarnedCredits = FormatAmount(invoice.TotalCredits.Value)
            };

            var serializer = new XmlSerializer(typeof(XmlStatement));

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true
            };

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, settings.Encoding);
            using var xmlWriter = XmlWriter.Create(streamWriter, settings);

            serializer.Serialize(xmlWriter, statement);
            xmlWriter.Flush();

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}