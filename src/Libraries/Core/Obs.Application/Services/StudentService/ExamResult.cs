namespace Obs.Application.Services.StudentService;

public class ExamResult
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
    public short Point { get; set; }
    public string Grade { get; set; }
    
    public LectureResult Lecture { get; set; }
    
    public IEnumerable<string> Errors { get; set; }
    public bool Success { get; set; }
}