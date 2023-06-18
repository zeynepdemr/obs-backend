namespace Obs.Application.Services.StudentService;

public record LectureResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public IEnumerable<string> Errors { get; set; }
    public bool Success { get; set; }
}