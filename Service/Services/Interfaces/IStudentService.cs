using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        Student Create(Student student);
        Student Update(int id, Student student);
        void Delete(int id);
        Student GetById(int id);
        Student GetByGroup(string groupName);
        List<Student> GetAll();
        List<Student> Search(string searchText); 
    }
}
