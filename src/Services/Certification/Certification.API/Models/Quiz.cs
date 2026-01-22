namespace Certification.API.Models;
public class Quiz {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ResourceId { get; set; }
    public List<Question> Questions { get; set; } = new();
}

public class Question {
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}

public class UserProgress {
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int QuizId { get; set; }
    public int Score { get; set; }
    public bool Completed { get; set; }
}
