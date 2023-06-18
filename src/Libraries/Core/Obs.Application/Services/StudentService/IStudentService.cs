namespace Obs.Application.Services.StudentService;

public interface IStudentService
{
    Task<StudentResult> AddStudent(StudentDto studentDto);
    
    Task<StudentResult> GetStudent(string userId);
    
    Task<IList<ExamResult>> GetExams(string userId);
    
    Task<ExamResult> AddExam(ExamDto examDto);
    Task<LectureResult> AddLecture(LectureDto lectureDto);

    Task<IList<LectureResult>> GetLectures();
}