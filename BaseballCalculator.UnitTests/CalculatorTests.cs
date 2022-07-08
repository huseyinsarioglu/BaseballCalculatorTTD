using BaseballCalculator.Library;

namespace BaseballCalculator.UnitTests
{
    public class CalculatorTests
    {
        private readonly Calculator _scoreCalculator;

        public CalculatorTests()
        {
            _scoreCalculator = new Calculator();
        }

        [Theory]
        [MemberData(nameof(NullAndEmptyData))]
        public void NullOrEmptyScoresShouldReturnZero(params string[]? scores)
        {
            // When
            int actualResult = _scoreCalculator.Calculate(scores);

            // Then
            actualResult.Should().Be(default);
        }

        private static IEnumerable<object?[]> NullAndEmptyData()
        {
            yield return new object?[] { null };
            yield return new object[] { Array.Empty<string>() };
        }

        [Theory]
        [MemberData(nameof(SingleIntData))]
        public void OneScoreShouldReturnItself(int score)
        {
            var result = _scoreCalculator.Calculate(new string[] { score.ToString() });

            result.Should().Be(score);
        }

        private static IEnumerable<object[]> SingleIntData()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new object[] { GetRandomNumber() };
            }
        }

        private static int GetRandomNumber()
        {
            const int MAX = 199;
            return new Random().Next(MAX);
        }

        [Theory]
        [MemberData(nameof(OnlyNumbersArray))]
        public void OnlyNumberShouldBeAdded(params int[] scores)
        {
            var scoresString = scores.Select(s => s.ToString()).ToArray();
            var result = _scoreCalculator.Calculate(scoresString);
            result.Should().Be(scores.Sum());
        }

        private static IEnumerable<object[]> OnlyNumbersArray()
        {
            const int NumberToProduce = 4;
            for (int i = 0; i < NumberToProduce; i++)
            {
                var twoDimArray = new int[] { GetRandomNumber(), GetRandomNumber() };
                yield return new object[] { twoDimArray };
            }

            for (int i = 0; i < NumberToProduce; i++)
            {
                var threeDimArray = new int[] { GetRandomNumber(), GetRandomNumber(), GetRandomNumber() };
                yield return new object[] { threeDimArray };
            }
        }

        [Theory]
        [InlineData(Calculator.OperatorAdd)]
        [InlineData(Calculator.OperatorRemove)]
        [InlineData(Calculator.OperatorDouble)]
        [InlineData("30", Calculator.OperatorRemove, Calculator.OperatorDouble)]
        public void AtFirstLineOperatorShouldThrowArgumanOutOfRangeException(params string[] score)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _scoreCalculator.Calculate(score));
            exception.Message.Should().Be($"{score[^1]} cannot be at first line or after reset list with remove operator!");
        }

        [Theory]
        [InlineData("3", Calculator.OperatorAdd)]
        [InlineData("5", Calculator.OperatorAdd)]
        public void AtSecondLineOperatorShouldThrowArgumanOutOfRangeException(params string[] scores)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _scoreCalculator.Calculate(scores));
            exception.Message.Should().Be($"{Calculator.OperatorAdd} cannot be at second line!");
        }

        [Theory]
        [InlineData("3", Calculator.OperatorRemove)]
        [InlineData("5", Calculator.OperatorRemove)]
        public void ANumberAndRemoveShouldReturnZero(params string[] scores)
        {
            // When
            int actualResult = _scoreCalculator.Calculate(scores);

            // Then
            actualResult.Should().Be(default);
        }
    }
}