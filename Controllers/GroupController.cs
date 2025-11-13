using AcademySystem.Helpers;
using Domain.Entities;
using Service.Services.Implementations;
using System.Text.RegularExpressions;
using Group = Domain.Entities.Group;

namespace AcademySystem.Controllers
{
    public class GroupController
    {
        GroupService _groupService = new();

        public void Create()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Add Group Name:");
            string groupName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length < 25)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group name cannot be empty or longer than 50 characters!");
                return;
            }

            Helper.PrintConsole(ConsoleColor.Blue, "Add Teacher Name:");
            string teacherName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(teacherName) || !Regex.IsMatch(teacherName, @"^[A-Za-z\s]+$"))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Teacher name must contain only letters!");
                return;
            }

            Helper.PrintConsole(ConsoleColor.Blue, "Add Room Name:");
            string roomName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(roomName) || roomName.Length > 2)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Room name cannot be empty or longer than 20 characters!");
                return;
            }

            teacherName = teacherName.ToLower();
            roomName = roomName.ToLower();

            if (teacherName == roomName)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Teacher and room name cannot be the same!");
            }

            foreach (var groups in _groupService.GetAll())
            {
                if (groups.Name.Trim().ToLower() == groupName.Trim().ToLower())
                {
                    Helper.PrintConsole(ConsoleColor.Red, "A group with this name already exists!");
                    return;
                }
            }

            teacherName = char.ToUpper(teacherName[0]) + teacherName.Substring(1).ToLower();

            Group group = new Group
            {
                Name = groupName.Trim(),
                Teacher = teacherName.Trim(),
                Room = roomName.Trim()
            };

            var result = _groupService.Create(group);
            Helper.PrintConsole(ConsoleColor.Green,
                $"Group created successfully! Id: {result.Id}, Name: {result.Name}, Teacher: {result.Teacher}, Room: {result.Room}");
        }

        public void GetById()
        {
        GroupId:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Group Id:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id) || id <= 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Enter a valid positive number!");
                goto GroupId;
            }

            Group group = _groupService.GetById(id);
            if (group != null)
            {
                Helper.PrintConsole(ConsoleColor.Green,
                    $"Group Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group not found!");
            }
        }

        public void Delete()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Group Id to delete:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id) || id <= 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid group id!");
                return;
            }

            var group = _groupService.GetById(id);
            if (group == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group not found!");
                return;
            }

            _groupService.Delete(id);
            Helper.PrintConsole(ConsoleColor.Green, $"Group '{group.Name}' deleted successfully!");
        }

        public void Update()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Group Id to update:");
            string idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id) || id <= 0)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid group id!");
                return;
            }

            var group = _groupService.GetById(id);
            if (group == null)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group not found!");
                return;
            }

            Helper.PrintConsole(ConsoleColor.Blue, $"Current Name: {group.Name}. Enter new name (or press Enter to skip):");
            string newName = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, $"Current Teacher: {group.Teacher}. Enter new teacher (or press Enter to skip):");
            string newTeacher = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, $"Current Room: {group.Room}. Enter new room (or press Enter to skip):");
            string newRoom = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newName)) newName = group.Name;
            if (string.IsNullOrWhiteSpace(newTeacher)) newTeacher = group.Teacher;
            if (string.IsNullOrWhiteSpace(newRoom)) newRoom = group.Room;

            if (!Regex.IsMatch(newTeacher, @"^[A-Za-z\s]+$"))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Teacher name must contain only letters!");
                return;
            }

            if (newTeacher.ToLower() == newRoom.ToLower())
            {
                Helper.PrintConsole(ConsoleColor.Red, "Teacher and Room name cannot be the same!");
                return;
            }

            var result = _groupService.Update(id, new Group { Name = newName, Teacher = newTeacher, Room = newRoom });

            if (result != null)
                Helper.PrintConsole(ConsoleColor.Green, $"Group updated successfully!");
            else
                Helper.PrintConsole(ConsoleColor.Red, "Update failed!");
        }

        public void Search()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter group name to search:");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group name cannot be empty!");
                return;
            }

            var groups = _groupService.Search(name);

            if (groups.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "No groups found!");
                return;
            }

            foreach (var group in groups)
            {
                Helper.PrintConsole(ConsoleColor.Green,
                    $"Group Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
            }
        }
    }
}
