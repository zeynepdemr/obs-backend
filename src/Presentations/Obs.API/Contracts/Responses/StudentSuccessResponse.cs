namespace Obs.API.Contracts.Responses;

public class StudentSuccessResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AcademicYear { get; set; }
    public string AdmissionClass { get; set; }
    public string AdmissionNumber { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string EmailAddress { get; set; }
}