using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Obs.Domain.Models;

public class Student 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    

    public string AcademicYear { get; set; }
    public string AdmissionClass { get; set; }
    public string AdmissionNumber { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public IdentityUser User { get; set; }
    
}