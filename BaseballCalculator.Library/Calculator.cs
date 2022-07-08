namespace BaseballCalculator.Library
{
    public class Calculator
    {
        public const string OperatorAdd = "+";
        public const string OperatorDouble = "D";
        public const string OperatorCancel = "C";

        private readonly string[] _notAllowedAtFirstLine = new string[] { OperatorAdd, OperatorDouble, OperatorCancel };
        private readonly string[] _notAllowedAtSecondLine = new string[] { OperatorAdd };

        public int Calculate(string[]? scoresList)
        {
            if (scoresList == null || scoresList.Length == 0)
            {
                return default;
            }

            return ParseScores(scoresList).Sum();
        }

        private IEnumerable<int> ParseScores(string[] scores)
        {
            var scoreList = new List<int>(scores.Length);
            foreach (var score in scores)
            {
                ValidateOperatorLine(scoreList, score);
                ParseSingleScore(scoreList, score);
            }

            return scoreList;
        }

        private void ParseSingleScore(List<int> scoreList, string score)
        {
            switch (score)
            {
                case OperatorCancel:
                    scoreList.RemoveAt(scoreList.Count - 1);
                    break;
                case OperatorDouble:
                    scoreList.Add(scoreList[^1] * 2);
                    break;
                case OperatorAdd:
                    scoreList.Add(scoreList[^1] + scoreList[^2]);
                    break;
                default:
                    if (!int.TryParse(score, out int number))
                    {
                        throw new ArgumentOutOfRangeException(null, $"{score} operator not supported!");
                    }
                    scoreList.Add(number);
                    break;
            }
        }

        private void ValidateOperatorLine(List<int> scoreList, string score)
        {
            if (_notAllowedAtFirstLine.Contains(score) && scoreList.Count == 0)
            {
                throw new ArgumentOutOfRangeException(null, $"{score} cannot be at first line or after reset list with remove operator!");
            }

            if (_notAllowedAtSecondLine.Contains(score) && scoreList.Count == 1)
            {
                throw new ArgumentOutOfRangeException(null, $"{score} cannot be at second line!");
            }
        }
    }
}