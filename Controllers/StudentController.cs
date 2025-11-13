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

            if (!int.TryParse(groupIdStr, out int groupId) || groupId <= 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid group id!");
                goto EnterGroupId;
            }

            var group = _groupService.GetById(groupId);
            if (group == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group not found! Try again.");
                goto EnterGroupId;
            }

        StudentName:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Name:");
            string name = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(name))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Name cannot be empty!");
                goto StudentName;
            }

            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Name must contain only letters!");
                    goto StudentName;
                }
            }

        StudentSurname:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Surname:");
            string surname = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(surname))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Surname cannot be empty!");
                goto StudentSurname;
            }

         

            foreach (char c in surname)
            {
                if (!char.IsLetter(c))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Surname must contain only letters!");
                    goto StudentSurname;
                }
            }

            if (name.ToLower() == surname.ToLower())
            {
                Helper.PrintConsole(ConsoleColor.Red, "Name and Surname cannot be the same!");
                return;
            }

            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Age:");
            string ageStr = Console.ReadLine();

            if (!int.TryParse(ageStr, out int age) || age < 18 || age > 30)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Age must be between 18 and 30!");
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
                $"Student created successfully! Id: {result.Id}, Name: {result.Name} {result.Surname}, Group: {result.Group.Name}");
        }

        public void GetById()
        {
        StudentId:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Id:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id) || id <= 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid Id!");
                goto StudentId;
            }

            var student = _studentService.GetById(id);
            if (student == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Student not found!");
                goto StudentId;
            }

            Helper.PrintConsole(ConsoleColor.Green,
                $"Id: {student.Id}, Name: {student.Name} {student.Surname}, Age: {student.Age}, Group: {student.Group?.Name}");
        }

        public void Delete()
        {
        studentId:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Id to delete:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id) || id <= 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid Id!");
                goto studentId;
            }

            var student = _studentService.GetById(id);
            if (student == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Student not found!");
                goto studentId;
            }

            _studentService.Delete(id);
            Helper.PrintConsole(ConsoleColor.Green, $"Student {student.Name} {student.Surname} deleted successfully!");
        }

        public void GetByAge()
        {
        Age:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Age:");
            string ageStr = Console.ReadLine();
            if (string.IsNullOrEmpty(ageStr))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Age is not empty!");
                goto Age;
            }
            if (!int.TryParse(ageStr, out int age) || age < 18)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Age must be 18 or above!");
                goto Age;
            }

            var students = _studentService.GetByAge(age);

            if (students == null || students.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "No students found with this age.");
                goto Age;
            }

            foreach (var student in students)
            {
                Helper.PrintConsole(ConsoleColor.Green,
                    $"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Group: {student.Group?.Name}");
            }
        }

        public void Search()
        {
        Search:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter name or surname to search:");
            string searchText = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Search text cannot be empty!");
                goto Search;
            }

            var results = _studentService.Search(searchText);
            if (results == null || results.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "No matching students found.");
                goto Search;
            }

            foreach (var student in results)
            {
                if (student.Name.ToLower().Contains(searchText) ||
                    student.Surname.ToLower().Contains(searchText))
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Id: {student.Id}, Name: {student.Name} {student.Surname}, Group: {student.Group?.Name}");
                }
            }
        }
        public void GetAll()
        {
        StudentID: Helper.PrintConsole(ConsoleColor.Blue, "Enter Student ID:");
            string studentID = Console.ReadLine();
            int id;
            bool isID = int.TryParse(studentID, out id);
            if (isID)
            {
                var result = _studentService.GetById(id);
                if (result != null)
                {
                    Helper.PrintConsole(ConsoleColor.Green, $"ID: {result.Id},Name: {result.Name},Surname: {result.Surname},Age: {result.Age},Group: {result.Group.Name}");

                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Not Found Student!");
                    Helper.PrintConsole(ConsoleColor.Yellow, "Create new Student[8] or try again[9] ");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Incorrect ID type, try again.");
                goto StudentID;
            }

        }


        public void Update()
        {
        Id:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Student Id to update:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id) || id <= 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid Id!");
                goto Id;
            }

            var existingStudent = _studentService.GetById(id);
            if (existingStudent == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Student not found!");
                goto Id;
            }

            name:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter new Name:");
            string newName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(newName))
            {
                foreach (char c in newName)
                {
                    if (!char.IsLetter(c))
                    {
                        Helper.PrintConsole(ConsoleColor.Red, "Name must contain only letters!");
                        goto name;
                    }
                }
                existingStudent.Name = newName;
            }

        Surname:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter new Surname:");
            string newSurname = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(newSurname))
            {
                foreach (char c in newSurname)
                {
                    if (!char.IsLetter(c))
                    {
                        Helper.PrintConsole(ConsoleColor.Red, "Surname must contain only letters!");
                        goto Surname;
                    }
                }
                existingStudent.Surname = newSurname;
            }

        age:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter new Age:");
            string ageStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(ageStr) && int.TryParse(ageStr, out int newAge))
            {
                if (newAge < 18 || newAge > 100)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Age must be between 18 and 100!");
                    goto age;
                }
                existingStudent.Age = newAge;
            }
        

                Helper.PrintConsole(ConsoleColor.Blue, "Do you want to change Group? (yes/no):");
            string changeGroup = Console.ReadLine()?.Trim().ToLower();

            if (changeGroup == "yes")
            {
            Newid:
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
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Invalid Group Id!");
                    goto Newid;

                }
            }

            _studentService.Update(id, existingStudent);
            Helper.PrintConsole(ConsoleColor.Green, "Student updated successfully!");
        }
    }
}
