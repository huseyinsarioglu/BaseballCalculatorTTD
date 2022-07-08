namespace BaseballCalculator.Library
{
    public class Calculator
    {
        public const string OperatorAdd = "+";
        public const string OperatorDouble = "D";
        public const string OperatorRemove = "R";

        private readonly string[] _notAllowedAtFirstLine = new string[] { OperatorAdd, OperatorDouble, OperatorRemove };
        private readonly string[] _notAllowedAtSecondLine = new string[] { OperatorAdd };

        public int Calculate(string[]? scoresList)
        {
            if (scoresList == null || scoresList.Length == 0)
            {
                return default;
            }

            return ParseScores(scoresList).Sum();
        }

        private List<int> ParseScores(string[] scoresList)
        {
            var scores = new List<int>(scoresList.Length);
            foreach (var score in scoresList)
            {
                if (_notAllowedAtFirstLine.Contains(score) && scores.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(null, $"{score} cannot be at first line!");
                }
                else if (_notAllowedAtSecondLine.Contains(score) && scores.Count == 1)
                {
                    throw new ArgumentOutOfRangeException(null, $"{score} cannot be at second line!");
                }
                else if (int.TryParse(score, out int scoreNumber))
                {
                    scores.Add(scoreNumber);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return scores;
        }
    }
}