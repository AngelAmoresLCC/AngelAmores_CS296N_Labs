using CommunityOfMars.Controllers;
using CommunityOfMars.Models;

namespace MarsTests
{
	public class QuizTests
	{
		[Fact]
		public void TestAddQAPair()
		{
			var controller = new QuizController();
			Assert.True(controller.AddQAPair(5, "Test question", "Test answer"));
			Assert.False(controller.AddQAPair(5, "Fail question", "Fail answer"));
			Assert.False(controller.AddQAPair(6, "", "No Question"));
			Assert.False(controller.AddQAPair(6, "No Answer", ""));
			Assert.True(controller.AddQAPair(6, "ID Available", "Not taken"));
		}

		[Fact]
		public void TestRemoveQAPair()
		{
			var controller = new QuizController();
			controller.AddQAPair(5, "Test question", "Test answer");
			Assert.True(controller.RemoveQAPair(5));
			Assert.False(controller.RemoveQAPair(5));
		}

		[Fact]
		public void TestCheckAnswers()
		{
			var controller = new QuizController();
			Quizzes quiz = null;
			controller.AddQAPair(4, "Unanswered question", "Test answer");
			controller.AddQAPair(5, "Test question", "Test answer");
			controller.AddQAPair(6, "Test question", "Test answer");
			quiz = controller.LoadQuestions();
			quiz.UserAnswers = new Dictionary<int, string>
			{
				{ 5, "Wrong answer" },
				{ 6, "Test answer" }
			};
			var quizResults = quiz.CheckAnswers();
			foreach (var result in quizResults)
			{
				if (result.Key == 4)
					Assert.False(result.Value);
				if (result.Key == 5)
					Assert.False(result.Value);
				if (result.Key == 6)
					Assert.True(result.Value);
			}
		}

		[Fact]
		public void TestLoadQuestions()
		{
			//Arrange
			var controller = new QuizController();

			//Act
			//controller.AddQAPair(1, "The correct answer is 5", "5");
			//controller.AddQAPair(2, "The correct answer is winter", "Winter");
			var loadedModel = controller.LoadQuestions();

			//Assert
			Assert.NotNull(loadedModel.Questions);
			Assert.NotEmpty(loadedModel.Questions);
			Assert.NotNull(loadedModel.Answers);
			Assert.NotEmpty(loadedModel.Answers);
			Assert.Equal(controller.Questions, loadedModel.Questions);
			Assert.Equal(controller.Answers, loadedModel.Answers);
		}
	}
}