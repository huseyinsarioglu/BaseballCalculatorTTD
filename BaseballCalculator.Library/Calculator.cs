﻿namespace BaseballCalculator.Library
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

        private IEnumerable<int> ParseScores(string[] scores)
        {
            var scoreList = new List<int>(scores.Length);
            foreach (var score in scores)
            {
                if (_notAllowedAtFirstLine.Contains(score) && scoreList.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(null, $"{score} cannot be at first line or after reset list with remove operator!");
                }
                else if (_notAllowedAtSecondLine.Contains(score) && scoreList.Count == 1)
                {
                    throw new ArgumentOutOfRangeException(null, $"{score} cannot be at second line!");
                }
                else if (int.TryParse(score, out int scoreNumber))
                {
                    scoreList.Add(scoreNumber);
                }
                else if (score.Equals(OperatorRemove, StringComparison.OrdinalIgnoreCase))
                {
                    scoreList.RemoveAt(scoreList.Count - 1);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(null, $"{score} operator not supported!");
                }
            }

            return scoreList;
        }
    }
}