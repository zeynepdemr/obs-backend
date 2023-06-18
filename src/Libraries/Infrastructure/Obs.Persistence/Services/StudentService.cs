using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Obs.Application.Services.StudentService;
using Obs.Domain.Models;
using Obs.Persistence.Data;

namespace Obs.Persistence.Services;

public class StudentService : IStudentService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _dataContext;


    public StudentService(DataContext dataContext, UserManager<IdentityUser> userManager)
    {
        _dataContext = dataContext;
        _userManager = userManager;
    }

    public async Task<StudentResult> AddStudent(StudentDto studentDto)
    {
        var user = await _userManager.FindByIdAsync(studentDto.UserId);

        if (user is null)
        {
            return new StudentResult { Errors = new[] { "User not found" } };
        }

        var student = new Student()
        {
            AcademicYear = studentDto.AcademicYear,
            UserId = studentDto.UserId,
            AdmissionClass = studentDto.AdmissionClass,
            AdmissionNumber = studentDto.AdmissionNumber,
            DateOfBirth = studentDto.DateOfBirth,
            FatherName = studentDto.FatherName,
            MotherName = studentDto.MotherName,
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            PhoneNumber = studentDto.PhoneNumber
        };


        await _dataContext.Students.AddAsync(student);
        await _dataContext.SaveChangesAsync();

        return new StudentResult
        {
            Success = true,
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            AcademicYear = studentDto.AcademicYear,
            AdmissionClass = studentDto.AdmissionClass,
            AdmissionNumber = studentDto.AdmissionNumber,
            DateOfBirth = studentDto.DateOfBirth,
            PhoneNumber = studentDto.PhoneNumber,
            FatherName = studentDto.FatherName,
            MotherName = studentDto.MotherName,
            EmailAddres = user.Email,
        };
    }

    public async Task<StudentResult> GetStudent(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return new StudentResult{ Errors = new[] { "User not found" } };
        }
        
        var student = await _dataContext.Students.FirstOrDefaultAsync(x => x.UserId == user.Id);
        
        if (student is null)
        {
            return new StudentResult{ Errors = new[] { "Student not found" } };
        }

        return new StudentResult()
        {
            FirstName = student.FirstName,
            LastName = student.LastName,
            AcademicYear = student.AcademicYear,
            AdmissionClass = student.AdmissionClass,
            AdmissionNumber = student.AdmissionNumber,
            DateOfBirth = student.DateOfBirth,
            PhoneNumber = student.PhoneNumber,
            FatherName = student.FatherName,
            MotherName = student.MotherName,
            EmailAddres = user.Email,
            Success = true
        };
    }

    public async Task<IList<ExamResult>> GetExams(string userId)
    {
        var student = await _dataContext.Students.FirstOrDefaultAsync(x => x.UserId == userId);
        
        if (student is null)
        {
            return new List<ExamResult> { new ExamResult{ Errors = new[] { "Student not found" }} };
        }

        return await _dataContext.Exams.Where(x => x.StudentId == student.Id).Include(x => x.Lecture).Select(exam => new ExamResult
        {
            Id = exam.Id,
            Point = exam.Point,
            Grade = exam.Grade,
            LectureId = exam.LectureId,
            StudentId = exam.StudentId,
            Success = true,
            Lecture = new LectureResult
            {
                Id = exam.Lecture.Id,
                Name = exam.Lecture.Name,
                Success = true
            }
            
            
        }).ToListAsync();
    }

    public async Task<ExamResult> AddExam(ExamDto examDto)
    {
        var lecture = await _dataContext.Lectures.FindAsync(examDto.LectureId);
        
        if (lecture is null)
        {
            return new ExamResult { Errors = new[] { "Lecture not found" } };
        }
        
        var student = await _dataContext.Students.FirstOrDefaultAsync(x => x.UserId == examDto.UserId);
        
        if (student is null)
        {
            return new ExamResult { Errors = new[] { "Student not found" } };
        }
        
        var exam = await _dataContext.Exams.AddAsync(new Exam()
        {
            LectureId = examDto.LectureId,
            StudentId = student.Id,
            Point = examDto.Point,
            Grade = GetGrade(examDto.Point)
        });

        await _dataContext.SaveChangesAsync();
        
        return new ExamResult()
        {
            LectureId = lecture.Id,
            Point = exam.Entity.Point,
            Grade = exam.Entity.Grade,
            StudentId = exam.Entity.StudentId,
            Success = true,
            Lecture = new LectureResult()
            {
                Id = lecture.Id,
                Name = lecture.Name,
                Success = true
            }
        };

    }

    private string GetGrade(short examDtoPoint)
    {
        switch (examDtoPoint)
        {
            case < 50:
                return "F";
            case < 60:
                return "C";
            case < 80:
                return "D";
            case < 90:
                return "B";
            case <= 100:
                return "A";
            default:
                return "F";
        }
    }

    public async Task<LectureResult> AddLecture(LectureDto lectureDto)
    {
        var lecture = await _dataContext.Lectures.AddAsync(new Lecture
        {
            Name = lectureDto.Name
        });
        await _dataContext.SaveChangesAsync();

        return new LectureResult
        {
            Id = lecture.Entity.Id,
            Name = lecture.Entity.Name,
            Success = true
        };
    }
    
    public async Task<IList<LectureResult>> GetLectures()
    {
        return await _dataContext.Lectures.Select(x => new LectureResult
        {
            Id = x.Id,
            Name = x.Name,
            Success = true
        }).ToListAsync();
    }
}