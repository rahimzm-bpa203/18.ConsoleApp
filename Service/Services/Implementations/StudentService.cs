using Domain.Entities;
using Repository.Repostories.Implementations;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _studentRepository;
        private int _count = 1;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }

        public Student Create(Student student)
        {
            student.Id = _count;

            _studentRepository.Create(student);

            _count++;

            return student;
        }

        public void Delete(int id)
        {
            Student student = GetById(id);

            if (student != null)
                _studentRepository.Delete(student);
        }

        public List<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student GetById(int id)
        {
            return _studentRepository.Get(s => s.Id == id);
        }

        public Student GetByName(string name)
        {
            return _studentRepository.Get(s => s.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public Student GetBySurname(string surname)
        {
            return _studentRepository.Get(s => s.Surname.Trim().ToLower() == surname.Trim().ToLower());
        }

        public List<Student> GetByGroup(string groupName)
        {
            return _studentRepository.GetAll(s => s.Group != null && s.Group.Name.Trim().ToLower() == groupName.Trim().ToLower());
        }

        public Student Update(int id, Student student)
        {
            Student dbStudent = GetById(id);

            if (dbStudent == null) return null;

            student.Id = id;
            _studentRepository.Update(student);

            return GetById(id);
        }

        public List<Student> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Student>();

            return _studentRepository.GetAll()
                .Where(s =>
                    (!string.IsNullOrEmpty(s.Name) && s.Name.Trim().ToLower()== searchText.Trim().ToLower()) ||
                    (!string.IsNullOrEmpty(s.Surname) && s.Surname.Trim().ToLower()== searchText.Trim().ToLower()) ||
                    (s.Group != null && !string.IsNullOrEmpty(s.Group.Name) && s.Group.Name.Trim().ToLower()== searchText.Trim().ToLower())
                )
                .ToList();
        }
    }
}
