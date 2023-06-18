namespace Obs.Application.Services.StudentService;

public class StudentResult
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
    public string EmailAddres { get; set; }
    
    public IEnumerable<string> Errors { get; set; }
    public bool Success { get; set; }
};