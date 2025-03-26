namespace TheatricalPlayersRefactoring.ApplicationTests.StatementTests
{
    public class StatementExamples
    {
        public const string ExpectedOldXml =
                @"<?xml version=""1.0"" encoding=""utf-8""?>
<Statement xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Customer>BigCo</Customer>
  <Items>
    <Item>
      <AmountOwed>650</AmountOwed>
      <EarnedCredits>25</EarnedCredits>
      <Seats>55</Seats>
    </Item>
    <Item>
      <AmountOwed>547</AmountOwed>
      <EarnedCredits>12</EarnedCredits>
      <Seats>35</Seats>
    </Item>
    <Item>
      <AmountOwed>456</AmountOwed>
      <EarnedCredits>10</EarnedCredits>
      <Seats>40</Seats>
    </Item>
    <Item>
      <AmountOwed>705.4</AmountOwed>
      <EarnedCredits>0</EarnedCredits>
      <Seats>20</Seats>
    </Item>
    <Item>
      <AmountOwed>931.6</AmountOwed>
      <EarnedCredits>9</EarnedCredits>
      <Seats>39</Seats>
    </Item>
    <Item>
      <AmountOwed>705.4</AmountOwed>
      <EarnedCredits>0</EarnedCredits>
      <Seats>20</Seats>
    </Item>
  </Items>
  <AmountOwed>3995.4</AmountOwed>
  <EarnedCredits>56</EarnedCredits>
</Statement>";
        public const string ExpectedTxt = "Statement for BigCo\n" +
                          "  Hamlet: $650.00 (55 seats)\n" +
                          "  As You Like: $547.00 (35 seats)\n" +
                          "  Othello: $456.00 (40 seats)\n" +
                          "Amount owed is $1,653.00\n" +
                          "You earned 47 credits\n";

        public const string ExpectedNewXml =
                @"<?xml version=""1.0"" encoding=""utf-8""?>
<Statement xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Customer>BigCo</Customer>
  <Items>
    <Item>
      <AmountOwed>650</AmountOwed>
      <EarnedCredits>25</EarnedCredits>
      <Seats>55</Seats>
    </Item>
    <Item>
      <AmountOwed>547</AmountOwed>
      <EarnedCredits>12</EarnedCredits>
      <Seats>35</Seats>
    </Item>
    <Item>
      <AmountOwed>456</AmountOwed>
      <EarnedCredits>10</EarnedCredits>
      <Seats>40</Seats>
    </Item>
    <Item>
      <AmountOwed>705.4</AmountOwed>
      <EarnedCredits>0</EarnedCredits>
      <Seats>20</Seats>
    </Item>
    <Item>
      <AmountOwed>931.6</AmountOwed>
      <EarnedCredits>9</EarnedCredits>
      <Seats>39</Seats>
    </Item>
    <Item>
      <AmountOwed>803.6</AmountOwed>
      <EarnedCredits>0</EarnedCredits>
      <Seats>20</Seats>
    </Item>
  </Items>
  <AmountOwed>4093.6</AmountOwed>
  <EarnedCredits>56</EarnedCredits>
</Statement>";
    }
}