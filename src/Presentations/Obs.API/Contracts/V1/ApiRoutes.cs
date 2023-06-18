namespace Obs.API.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;
    
    public static class Identity
    {
        public const string Login = Base + "/identity/login";
        public const string Register = Base + "/identity/register";
        public const string Refresh = Base + "/identity/refresh";
    }
    
    public static class Student
    {
        public const string PostStudent = Base + "/student/";
        public const string PostExam = Base + "/student/exam";
        public const string Exams = Base + "/student/exams";
        public const string GetStudent = Base + "/student/";
        public const string PostLecture = Base + "/student/lecture";
        public const string Lectures = Base + "/student/lectures";
    }

}