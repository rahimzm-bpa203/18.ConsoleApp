using AcademySystem.Controllers;
using AcademySystem.Helpers;

namespace AcademySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GroupController groupController = new();
            //StudentController studentController = new();

            Helper.PrintConsole(ConsoleColor.Blue, "Select one option!");
            GetMenus();

            while (true)
            {
            SelectOption:
                string selectOption = Console.ReadLine();

                if (int.TryParse(selectOption, out int selectedOption))
                {
                    switch (selectedOption)
                    {
                        case (int)Menus.CreateGroup:
                            groupController.Create();
                            break;
                        case (int)Menus.GetGroupById:
                            groupController.GetById();
                            break;
                        case (int)Menus.GetAllGroups:
                            groupController.GetAll();
                            break;
                        case (int)Menus.DeleteGroup:
                            groupController.Delete();
                            break;
                        case (int)Menus.UpdateGroup:
                            groupController.Update();
                            break;
                        case (int)Menus.SearchGroupByName:
                            groupController.Search();
                            break;
                        case (int)Menus.GetGroupByTeacher:
                            groupController.GetByTeacher();
                            break;
                        case (int)Menus.GetGroupByRoom:
                            groupController.GetByRoom();
                            break;

                        //case (int)Menus.CreateStudent:
                        //    studentController.Create();
                        //    break;
                        //case (int)Menus.GetStudent:
                        //    studentController.GetById();
                        //    break;
                        //case (int)Menus.GetAllStudents:
                        //    studentController.GetAll();
                        //    break;
                        //case (int)Menus.DeleteStudent:
                        //    studentController.Delete();
                        //    break;
                        //case (int)Menus.UpdateStudent:
                        //    studentController.Update();
                        //    break;
                        //case (int)Menus.SearchStudent:
                        //    studentController.Search();
                        //    break;

                        default:
                            Helper.PrintConsole(ConsoleColor.Red, "Option not found! Please select a valid option.");
                            break;
                    }

                    Helper.PrintConsole(ConsoleColor.Yellow, "\nSelect another option:");
                    GetMenus();
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Select correct option type!");
                    goto SelectOption;
                }
            }
        }

        private static void GetMenus()
        {
            Helper.PrintConsole(ConsoleColor.Yellow,
                "1 - Create Group\n" +
                "2 - Get Group by Id\n" +
                "3 - Get All Groups\n" +
                "4 - Delete Group\n" +
                "5 - Update Group\n" +
                "6 - Search Group\n" +
                "7 - Get Group by Teacher\n" +
                "8 - Get Group by Room\n\n" +

                "9 - Create Student\n" +
                "10 - Get Student by Id\n" +
                "11 - Get All Students\n" +
                "12 - Delete Student\n" +
                "13 - Update Student\n" +
                "14 - Search Student\n");
        }
    }
    
}
