namespace Obs.API.Contracts.Requests;

public record CreateStudentRequest(
    string FirstName,
    string LastName,
    string AcademicYear,
    string AdmissionClass,
    string AdmissionNumber,
    DateTimeOffset DateOfBirth,
    string PhoneNumber,
    string FatherName,
    string MotherName
);