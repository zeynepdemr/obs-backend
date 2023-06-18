using Obs.Application.Services.StudentService;

namespace Obs.API.Contracts.Responses;

public class ExamSuccessResponse
{
    public short Point { get; set; }
    public string Grade { get; set; }
    
    public LectureSuccessResponse Lecture { get; set; }
}