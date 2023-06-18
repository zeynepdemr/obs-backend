namespace Obs.API.Contracts.Responses;

public class LectureFailedResponse
{
    public IEnumerable<string> Errors { get; set; }
}