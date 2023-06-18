namespace Obs.API.Contracts.Requests;

public record CreateExamRequest
(
    Guid LectureId,
    short Point
);