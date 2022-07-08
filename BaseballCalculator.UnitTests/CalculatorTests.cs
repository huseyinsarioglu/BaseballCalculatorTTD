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
            //When
            int actualResult = _scoreCalculator.Calculate(scores);

            //Then
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

        private static int GetRandomNumber()
        {
            const int MAX = 199;
            return new Random().Next(MAX);
        }

        [Theory]
        [InlineData(Calculator.OPERATOR_ADD)]
        [InlineData(Calculator.OPERATOR_REMOVE)]
        [InlineData(Calculator.OPERATOR_DOUBLE)]
        public void AtFirstLineOperatorShouldThrowArgumanOutOfRangeException(string score)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _scoreCalculator.Calculate(new string[] { score }));
            exception.Message.Should().Be($"{score} cannot be at first line!");
        }

        [Theory]
        [InlineData("3", Calculator.OPERATOR_ADD)]
        [InlineData("5", Calculator.OPERATOR_ADD)]
        public void AtSecondLineOperatorShouldThrowArgumanOutOfRangeException(params string[] scores)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _scoreCalculator.Calculate(scores));
            exception.Message.Should().Be($"{Calculator.OPERATOR_ADD} cannot be at second line!");
        }
    }
}