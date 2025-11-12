using Domain.Entities;
using Repository.Data;
using Repository.Exceptions;
using Repository.Repostories.Interfaces;

namespace Repository.Repostories.Implementations
{
    public class StudentRepository : IRepository<Student>
    {
        public void Create(Student data)
        {
            try
            {
                if (data == null)
                    throw new NotFoundException("Student data not found!");

                AppDbContext<Student>.datas.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(Student data)
        {
            AppDbContext<Student>.datas.Remove(data);
        }

        public Student Get(Predicate<Student> predicate)
        {
            return predicate != null ? AppDbContext<Student>.datas.Find(predicate) : null;
        }

        public List<Student> GetAll(Predicate<Student> predicate = null)
        {
            return predicate != null ? AppDbContext<Student>.datas.FindAll(predicate) : AppDbContext<Student>.datas;
        }

        public List<Student> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Student>();

            return AppDbContext<Student>.datas
                  .FindAll(s =>
            (!string.IsNullOrEmpty(s.Name) && s.Name.Trim().ToLower() == searchText.Trim().ToLower()) ||
            (!string.IsNullOrEmpty(s.Surname) && s.Surname.Trim().ToLower()== searchText.Trim().ToLower())
        );
        }

        public void Update(Student data)
        {
            Student dbStudent = Get(s => s.Id == data.Id);

            if (dbStudent == null) return;

            if (!string.IsNullOrEmpty(data.Name))
                dbStudent.Name = data.Name;

            if (!string.IsNullOrEmpty(data.Surname))
                dbStudent.Surname = data.Surname;

            if (data.Age > 0)
                dbStudent.Age = data.Age;

            if (data.Group != null)
                dbStudent.Group = data.Group;
        }
    }
}
