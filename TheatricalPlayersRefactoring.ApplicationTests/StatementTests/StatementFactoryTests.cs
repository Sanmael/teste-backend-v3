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
            var expected = StatementExamples.ExpectedTxt;

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

        [Fact]
        [Description("Esse teste foi escrito errado, ele está duplicando o Henry V , porém como estava no outro projeto, foi trago também")]
        public void GenerateStatementXmlOld_WhenValuesIsCorrect_ShouldReturnExpected()
        {
            // Arrange
            var expected = StatementExamples.ExpectedOldXml;

            var _textStatementFactory = _factoryProvider.GetFactory(format: StatementFormat.Xml);
            var invoice = new Invoice(new CustomerName("BigCo"));

            invoice.AddPerformance(Performance.Create(new Play("Hamlet", new Lines(4024), PlayType.Tragedy), new Audience(55)));
            invoice.AddPerformance(Performance.Create(new Play("As You Like", new Lines(2670), PlayType.Comedy), new Audience(35)));
            invoice.AddPerformance(Performance.Create(new Play("Othello", new Lines(3560), PlayType.Tragedy), new Audience(40)));
            invoice.AddPerformance(Performance.Create(new Play("Henry V", new Lines(3227), PlayType.History), new Audience(20)));
            invoice.AddPerformance(Performance.Create(new Play("King John", new Lines(2648), PlayType.History), new Audience(39)));
            invoice.AddPerformance(Performance.Create(new Play("Henry V", new Lines(3227), PlayType.History), new Audience(20)));

            // Act
            var result = _textStatementFactory.Generate(invoice);

            // Assert            
            result.Should().Be(expected);
        }

        [Fact]
        [Description("Esse é o teste anterior escrito corretamente")]
        public void GenerateStatementXmlNew_WhenValuesIsCorrect_ShouldReturnExpected()
        {
            // Arrange
            var expected = StatementExamples.ExpectedNewXml;

            var _textStatementFactory = _factoryProvider.GetFactory(format: StatementFormat.Xml);
            var invoice = new Invoice(new CustomerName("BigCo"));

            invoice.AddPerformance(Performance.Create(new Play("Hamlet", new Lines(4024), PlayType.Tragedy), new Audience(55)));
            invoice.AddPerformance(Performance.Create(new Play("As You Like", new Lines(2670), PlayType.Comedy), new Audience(35)));
            invoice.AddPerformance(Performance.Create(new Play("Othello", new Lines(3560), PlayType.Tragedy), new Audience(40)));
            invoice.AddPerformance(Performance.Create(new Play("Henry V", new Lines(3227), PlayType.History), new Audience(20)));
            invoice.AddPerformance(Performance.Create(new Play("King John", new Lines(2648), PlayType.History), new Audience(39)));
            invoice.AddPerformance(Performance.Create(new Play("Richard III", new Lines(3718), PlayType.History), new Audience(20)));

            // Act
            var result = _textStatementFactory.Generate(invoice);

            // Assert            
            result.Should().Be(expected);
        }
    }
}