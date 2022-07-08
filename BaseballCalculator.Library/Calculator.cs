namespace BaseballCalculator.Library
{
    public class Calculator
    {
        public const string OPERATOR_ADD = "+";
        public const string OPERATOR_DOUBLE = "D";
        public const string OPERATOR_REMOVE = "R";

        private readonly string[] NotAllowedAtFirstLine = new string[] { OPERATOR_ADD, OPERATOR_DOUBLE, OPERATOR_REMOVE };
        private readonly string[] NotAllowedAtSecondLine = new string[] { OPERATOR_ADD };

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
                if (NotAllowedAtFirstLine.Contains(score) && scores.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(null, $"{score} cannot be at first line!");
                }
                else if (NotAllowedAtSecondLine.Contains(score) && scores.Count == 1)
                {
                    throw new ArgumentOutOfRangeException(null, $"{score} cannot be at second line!");
                }
                else if(int.TryParse(score, out int scoreNumber))
                {
                    scores.Add(scoreNumber);
                }


                else throw new NotImplementedException();
            }

            return scores;
        }
    }
}