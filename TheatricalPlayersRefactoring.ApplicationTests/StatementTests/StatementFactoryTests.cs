using FluentAssertions;
using System.ComponentModel;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Application.Factories;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.ApplicationTests.StatementTests
{
    public class StatementFactoryTests
    {
        private readonly IStatementFactoryProvider _factoryProvider;

        public StatementFactoryTests()
        {
            _factoryProvider = new StatementFactoryProvider();
        }

        [Fact]
        [Description("Teste usado no projeto TheatricalPlayersRefactoringKata.Tests e refatorado")]
        public void GenerateStatementTxt_WhenValuesIsCorrect_ShouldReturnExpected()
        {
            // Arrange
            var expected = "Statement for BigCo\n" +
                          "  Hamlet: $650.00 (55 seats)\n" +
                          "  As You Like: $547.00 (35 seats)\n" +
                          "  Othello: $456.00 (40 seats)\n" +
                          "Amount owed is $1,653.00\n" +
                          "You earned 47 credits\n";

            var _textStatementFactory = _factoryProvider.GetFactory(format: StatementFormat.Text);
            var invoice = new Invoice(new CustomerName("BigCo"));

            invoice.AddPerformance(Performance.Create(new Play("Hamlet", new Lines(4024), PlayType.Tragedy), new Audience(55)));
            invoice.AddPerformance(Performance.Create(new Play("As You Like", new Lines(2670), PlayType.Comedy), new Audience(35)));
            invoice.AddPerformance(Performance.Create(new Play("Othello", new Lines(3560), PlayType.Tragedy), new Audience(40)));

            // Act
            var result = _textStatementFactory.Generate(invoice);

            // Assert            
            result.Should().Be(expected);
        }
    }
}