namespace Obs.Application.Services.StudentService;

public record StudentDto(
    string FirstName,
    string LastName,
    string AcademicYear,
    string AdmissionClass,
    string AdmissionNumber,
    DateTimeOffset DateOfBirth,
    string PhoneNumber,
    string FatherName,
    string MotherName,
    string UserId);