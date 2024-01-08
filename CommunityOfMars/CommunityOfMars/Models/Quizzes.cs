namespace CommunityOfMars.Models
{
	public class Quizzes
	{
		public Dictionary<int, string> Questions { get; set; } = new();
		public Dictionary<int, string> Answers { get; set; } = new();
		public Dictionary<int, string> UserAnswers { get; set; } = new();
		public bool IsCompleted { get; set; } = false;

		public Dictionary<int, bool> CheckAnswers()
		{
			Dictionary<int, bool> results = new();
			foreach (var question in Questions)
			{
				int id = question.Key;
				if (UserAnswers.ContainsKey(id))
				{
					if (UserAnswers[id].ToLower() == Answers[id].ToLower())
						results[id] = true;
					else
						results[id] = false;
				}
				else
					results[id] = false;
			}
			return results;
		}
	}
}
