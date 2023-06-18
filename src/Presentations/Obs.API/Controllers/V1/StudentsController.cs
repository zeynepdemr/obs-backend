using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obs.API.Contracts.Requests;
using Obs.API.Contracts.Responses;
using Obs.API.Contracts.V1;
using Obs.API.Extensions;
using Obs.Application.Services.StudentService;

namespace Obs.API.Controllers.V1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StudentsController : BaseController
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost(ApiRoutes.Student.PostStudent)]
    public async Task<IActionResult> Post([FromBody] CreateStudentRequest studentRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new StudentFailedResponse
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage))
            });
        }

        var studentResponse = await _studentService.AddStudent(new StudentDto(
            studentRequest.FirstName,
            studentRequest.LastName,
            studentRequest.AcademicYear,
            studentRequest.AdmissionClass,
            studentRequest.AdmissionNumber,
            studentRequest.DateOfBirth,
            studentRequest.PhoneNumber,
            studentRequest.FatherName,
            studentRequest.MotherName,
            HttpContext.GetUserId()
        ));

        if (!studentResponse.Success)
        {
            return BadRequest(new StudentFailedResponse
            {
                Errors = studentResponse.Errors
            });
        }

        return Ok(new StudentSuccessResponse()
        {
            FirstName = studentResponse.FirstName,
            LastName = studentResponse.LastName,
            AcademicYear = studentResponse.AcademicYear,
            AdmissionClass = studentResponse.AdmissionClass,
            AdmissionNumber = studentResponse.AdmissionNumber,
            DateOfBirth = studentResponse.DateOfBirth,
            PhoneNumber = studentResponse.PhoneNumber,
            FatherName = studentResponse.FatherName,
            MotherName = studentResponse.MotherName,
            EmailAddress = studentResponse.EmailAddres
        });
    }
    
    [HttpPost(ApiRoutes.Student.PostLecture)]
    public async Task<IActionResult> CreateLecture([FromBody] CreateLectureRequest lectureRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new LectureFailedResponse()
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage))
            });
        }

        var lectureResult = await _studentService.AddLecture(new LectureDto(lectureRequest.Name));

        if (!lectureResult.Success)
        {
            return BadRequest(new LectureFailedResponse()
            {
                Errors = lectureResult.Errors
            });
        }

        return Ok(new LectureSuccessResponse()
        {
            Id = lectureResult.Id,
            Name = lectureResult.Name
        });
    }

    [HttpGet(ApiRoutes.Student.Lectures)]
    public async Task<IActionResult> GetLectures()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new LectureFailedResponse()
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage))
            });
        }

        var lectureResults = await _studentService.GetLectures();

        if (! lectureResults.All(x => x.Success))
        {
            return BadRequest(new LectureFailedResponse()
            {
                Errors = lectureResults.Select(x => string.Join(",", x.Errors))
            });
        }

        return Ok(lectureResults.Select(lectureResult => new LectureSuccessResponse()
        {
            Id = lectureResult.Id,
            Name = lectureResult.Name
        }));
    }
    
    
    [HttpPost(ApiRoutes.Student.PostExam)]
    public async Task<IActionResult> CreateExam([FromBody] CreateExamRequest createExam)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ExamFailedResponse()
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage))
            });
        }

        var examResult = await _studentService.AddExam(new ExamDto(
                createExam.LectureId,
                HttpContext.GetUserId(),
                createExam.Point
            ));

        if (!examResult.Success)
        {
            return BadRequest(new ExamFailedResponse()
            {
                Errors = examResult.Errors
            });
        }

        return Ok(new ExamSuccessResponse()
        {
            Point = examResult.Point,
            Grade = examResult.Grade,
            Lecture = new LectureSuccessResponse()
            {
                Id = examResult.Lecture.Id,
                Name = examResult.Lecture.Name
            }
        });
    }
    
    [HttpGet(ApiRoutes.Student.Exams)]
    public async Task<IActionResult> GetExams()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ExamFailedResponse()
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage))
            });
        }

        var examResults = await _studentService.GetExams(HttpContext.GetUserId());

        if (! examResults.All(x => x.Success))
        {
            return BadRequest(new LectureFailedResponse()
            {
                Errors = examResults.Select(x => string.Join(",", x.Errors))
            });
        }

        return Ok(examResults.Select(examResult => new ExamSuccessResponse()
        {
            Point = examResult.Point,
            Grade = examResult.Grade,
            Lecture = new LectureSuccessResponse()
            {
                Id = examResult.Lecture.Id,
                Name = examResult.Lecture.Name
            }
        }));
    }
    
    [HttpGet(ApiRoutes.Student.GetStudent)]
    public async Task<IActionResult> GetStudent()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new StudentFailedResponse()
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage))
            });
        }

        var studentResult = await _studentService.GetStudent(HttpContext.GetUserId());

        if (! studentResult.Success)
        {
            return BadRequest(new StudentFailedResponse()
            {
                Errors = studentResult.Errors
            });
        }

        return Ok(new StudentSuccessResponse()
            {
                FirstName = studentResult.FirstName,
                LastName = studentResult.LastName,
                AcademicYear = studentResult.AcademicYear,
                AdmissionClass = studentResult.AdmissionClass,
                AdmissionNumber = studentResult.AdmissionNumber,
                DateOfBirth = studentResult.DateOfBirth,
                PhoneNumber = studentResult.PhoneNumber,
                FatherName = studentResult.FatherName,
                MotherName = studentResult.MotherName,
                EmailAddress = studentResult.EmailAddres
            }
        );
    }

}