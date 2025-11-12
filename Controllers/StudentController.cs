using AcademySystem.Helpers;
using Domain.Entities;
using Service.Services.Implementations;


namespace AcademySystem.Controllers
{
    public class StudentController
    {
       StudentService _studentService = new();
        GroupService _groupService = new();

    
        public void Create()
        {
        EnterGroupId:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Group Id:");
            string groupIdStr = Console.ReadLine();

            if (!int.TryParse(groupIdStr, out int groupId))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid group id!");
                goto EnterGroupId;
            }

            var group = _groupService.GetById(groupId);
            if (group == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group not found! Please try again.");
                goto EnterGroupId;
            }

            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Name:");
            string name = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Surname:");
            string surname = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Age:");
            string ageStr = Console.ReadLine();

            if (!int.TryParse(ageStr, out int age))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid age!");
                return;
            }

            Student student = new Student
            {
                Name = name,
                Surname = surname,
                Age = age,
                Group = group
            };

            var result = _studentService.Create(student);

            Helper.PrintConsole(ConsoleColor.Green,
                $"Student created successfully! Id: {result.Id}, Name: {result.Name}, Group: {result.Group.Name}");
        }

        public void Update()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Id to Update:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid id!");
                return;
            }

            var existingStudent = _studentService.GetById(id);
            if (existingStudent == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Student not found!");
                return;
            }

            Helper.PrintConsole(ConsoleColor.Blue, "Enter new Name (leave empty to keep current):");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                existingStudent.Name = name;

            Helper.PrintConsole(ConsoleColor.Blue, "Enter new Surname (leave empty to keep current):");
            string surname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname))
                existingStudent.Surname = surname;

            Helper.PrintConsole(ConsoleColor.Blue, "Enter new Age (leave empty to keep current):");
            string ageStr = Console.ReadLine();
            if (int.TryParse(ageStr, out int age))
                existingStudent.Age = age;

            Helper.PrintConsole(ConsoleColor.Blue, "Do you want to change Group? (yes/no):");
            string changeGroup = Console.ReadLine().Trim().ToLower();

            if (changeGroup == "yes")
            {
                Helper.PrintConsole(ConsoleColor.Blue, "Enter new Group Id:");
                string groupIdStr = Console.ReadLine();

                if (int.TryParse(groupIdStr, out int newGroupId))
                {
                    var newGroup = _groupService.GetById(newGroupId);
                    if (newGroup != null)
                    {
                        existingStudent.Group = newGroup;
                        Helper.PrintConsole(ConsoleColor.Green, $"Group changed to: {newGroup.Name}");
                    }
                    else
                    {
                        Helper.PrintConsole(ConsoleColor.Red, "Group not found. Group not changed.");
                    }
                }
            }

            _studentService.Update(id, existingStudent);

            Helper.PrintConsole(ConsoleColor.Green, $"Student updated successfully!");
        }

        public void GetById()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Id:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid id!");
                return;
            }

            var student = _studentService.GetById(id);
            if (student == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Student not found!");
                return;
            }

            Helper.PrintConsole(ConsoleColor.Green,
                $"Id: {student.Id}, Name: {student.Name} {student.Surname}, Age: {student.Age}, Group: {student.Group?.Name}");
        }
        public void Delete()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Id to delete:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid id!");
                return;
            }

            _studentService.Delete(id);
            Helper.PrintConsole(ConsoleColor.Green, "Student deleted successfully!");
        }

        public void GetByAge()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Age:");
            string ageStr = Console.ReadLine();

            if (!int.TryParse(ageStr, out int age))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid age!");
                return;
            }

            var students = _studentService.GetByAge(age);

            if (students.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "No students found with this age.");
                return;
            }

            foreach (var student in students)
            {
                Helper.PrintConsole(ConsoleColor.Green,
                    $"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Group: {student.Group?.Name}");
            }
        }

        public void GetByGroupId()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Group Id:");
            string groupIdStr = Console.ReadLine();

            if (!int.TryParse(groupIdStr, out int groupId))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid group id!");
                return;
            }

            var students = _studentService.GetByGroupId(groupId);

            if (students.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "No students found for this group.");
                return;
            }

            foreach (var student in students)
            {
                Helper.PrintConsole(ConsoleColor.Green,
                    $"Id: {student.Id}, Name: {student.Name}, Group: {student.Group?.Name}");
            }
        }

        public void Search()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter name or surname to search:");
            string searchText = Console.ReadLine();

            var results = _studentService.Search(searchText);

            if (results.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "No matching students found.");
                return;
            }

            foreach (var student in results)
            {
                Helper.PrintConsole(ConsoleColor.Green,
                    $"Id: {student.Id}, Name: {student.Name} {student.Surname}, Group: {student.Group?.Name}");
            }
        }
    }
}
