using AcademySystem.Helpers;
using Domain.Entities;
using Service.Services.Implementations;

namespace AcademySystem.Controllers
{
    public class GroupController
    {
        GroupService _groupService = new();

        public void Create()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Add Group Name:");
            string groupName = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, "Add Teacher Name:");
            string teacherName = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, "Add Room Name:");
            string roomName = Console.ReadLine();

            Group group = new Group
            {
                Name = groupName,
                Teacher = teacherName,
                Room = roomName
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

            if (int.TryParse(idStr, out int id))
            {
                Group group = _groupService.GetById(id);
                if (group != null)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Group Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Group not found!");
                    goto GroupId;
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Enter a valid number!");
                goto GroupId;
            }
        }


        public void Delete()
        {
        GroupId:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Group Id to delete:");
            string idStr = Console.ReadLine();

            if (int.TryParse(idStr, out int id))
            {
                _groupService.Delete(id);
                Helper.PrintConsole(ConsoleColor.Green, $"Group with Id {id} deleted successfully!");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Enter a valid number!");
                goto GroupId;
            }
        }

        public void Update()
        {
        GroupId:
            Helper.PrintConsole(ConsoleColor.Blue, "Enter Group Id to update:");
            string idStr = Console.ReadLine();

            if (int.TryParse(idStr, out int id))
            {
                var group = _groupService.GetById(id);
                if (group == null)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Group not found!");
                    goto GroupId;
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

                Group updatedGroup = new Group
                {
                    Name = newName,
                    Teacher = newTeacher,
                    Room = newRoom
                };

                var result = _groupService.Update(id, updatedGroup);

                if (result != null)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Group updated successfully! Id: {result.Id}, Name: {result.Name}, Teacher: {result.Teacher}, Room: {result.Room}");
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Update failed!");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Enter a valid number!");
                goto GroupId;
            }
        }

        public void GetByTeacher()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter teacher name:");
            string teacherName = Console.ReadLine();

            var groups = _groupService.GetByTeacher(teacherName);

            if (groups.Count > 0)
            {
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Group Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "No groups found for this teacher!");
            }
        }

        public void GetByRoom()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter room name:");
            string roomName = Console.ReadLine();

            var groups = _groupService.GetByRoom(roomName);

            if (groups.Count > 0)
            {
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Group Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "No groups found for this room!");
            }
        }
        public void GetAll()
        {
            var groups = _groupService.GetAll();
            if (groups.Count > 0)
            {
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Group Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "No groups found! Please create a group first.");
            }
        }
        public void Search()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter group name to search:");
            string name = Console.ReadLine();

            var groups = _groupService.Search(name);

            if (groups.Count > 0)
            {
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Group Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "No groups found!");
            }
        }
    }
}
