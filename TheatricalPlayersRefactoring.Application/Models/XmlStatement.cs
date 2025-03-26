using System.Xml.Serialization;

namespace TheatricalPlayersRefactoring.Application.Models
{
    [XmlRoot("Statement")]
    public class XmlStatement
    {
        public string Customer { get; set; }
        [XmlArrayItem("Item")]
        public List<XmlStatementItem> Items { get; set; }
        public string AmountOwed { get; set; }
        public string EarnedCredits { get; set; }
    }

    public class XmlStatementItem
    {
        public string AmountOwed { get; set; }
        public string EarnedCredits { get; set; }
        public string Seats { get; set; }
    }
}