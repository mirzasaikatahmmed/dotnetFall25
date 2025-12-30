using StudentManagement.BLL.DTOs;
using StudentManagement.DAL;

namespace StudentManagement.BLL.Services
{
    public class StudentService
    {
        private readonly IRepo<Student> _repo;

        public StudentService(IRepo<Student> repo)
        {
            _repo = repo;
        }

        public List<StudentDTO> GetAll()
        {
            var students = _repo.GetAll();
            return students.Select(s => new StudentDTO
            {
                StudentId = s.StudentId,
                StudentName = s.StudentName,
                StudentEmail = s.StudentEmail
            }).ToList();
        }

        public StudentDTO? GetById(int id)
        {
            var student = _repo.GetById(id);
            if (student == null) return null;

            return new StudentDTO
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                StudentEmail = student.StudentEmail
            };
        }

        public void Create(StudentDTO studentDto)
        {
            var student = new Student
            {
                StudentName = studentDto.StudentName,
                StudentEmail = studentDto.StudentEmail
            };
            _repo.Add(student);
        }

        public void Update(StudentDTO studentDto)
        {
            var student = new Student
            {
                StudentId = studentDto.StudentId,
                StudentName = studentDto.StudentName,
                StudentEmail = studentDto.StudentEmail
            };
            _repo.Update(student);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
