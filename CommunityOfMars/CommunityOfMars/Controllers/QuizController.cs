using Microsoft.AspNetCore.Mvc;
using CommunityOfMars.Models;

namespace CommunityOfMars.Controllers
{
	public class QuizController : Controller
	{
		public Dictionary<int, string> Questions { get; set; }
		public Dictionary<int, string> Answers { get; set; }

		public QuizController()
		{
			Questions = new();
			Answers = new();
			AddQAPair(1, "The correct answer is 5", "5");
			AddQAPair(2, "The correct answer is winter", "Winter");
		}

		public IActionResult Index()
		{
			var model = LoadQuestions();
			return View(model);
		}

		[HttpPost]
		public IActionResult Index(Quizzes model)
		{
			model = LoadQuestions(model);
			model.IsCompleted = true;
			return View(model);
		}

		public bool AddQAPair(int id, string question, string answer) //Might cut this too
		{
			if (question == string.Empty || answer == string.Empty)
				return false; //Fails if question or answer is empty
			if (Questions.ContainsKey(id) || Answers.ContainsKey(id))
				return false; //Fails if key already exists
			Questions.Add(id, question);
			Answers.Add(id, answer);
			return true;
		}

		public bool RemoveQAPair(int id) //Might not need this at all, but I already made it so I'm keeping it
		{
			if (Questions.ContainsKey(id) && Answers.ContainsKey(id))
			{
				Questions.Remove(id);
				Answers.Remove(id);
				return true;
			}
			return false;
		}

		public Quizzes LoadQuestions() //Add parameter to specify what file to read from
		{
			// Temporary set of hard-coded questions
			// In the future, these will be read in from a file
			//TODO: load questions and answers into the model
			Quizzes model = new();
			model.Questions = Questions;
			model.Answers = Answers;
			foreach(var question in Questions)
				model.UserAnswers.Add(question.Key, "");
			return model;
		}

		public Quizzes LoadQuestions(Quizzes model) //For reloading questions into a completed quiz
		{
			model.Questions = Questions;
			model.Answers = Answers;
			return model;
		}
	}
}
