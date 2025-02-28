using System.Net.Mime;

namespace Task_Organizer;

// 1. Task creation with name, description, priority, task performer and
// creator, deadline date
// 2. Ranking system
//     1. tasks with highest priority are shown first
//     2. Tasks with similar priority are ranked by deadline date
// 3. Filtering, sorting and searching
//     1. by fields - for ex. if you want to select task by certain performer
//     2. searching tasks by name
// 4. Task management :
//     1. Status management - changing the status to «done» or «declined» etc.
//     2. Editing or deleting a task
//     3. Adding comments - for ex. comments can be attached to a task
// 5. Menu for task management


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Task Manager. Please choose one of the following options and press" +
                          "corresponding number:");
        bool menuExit = false;
        do
        {
            Console.WriteLine("1. Create new task");
            Console.WriteLine("2. Edit existing task");
            Console.WriteLine("3. Find task");
            Console.WriteLine("4. Delete task");
            Console.WriteLine("5. Filter tasks");
            Console.WriteLine("6. Sort tasks");
            Console.WriteLine("7. View all tasks");
            Console.WriteLine("8. Exit menu");
        
            int userChoice = Convert.ToInt32(Console.ReadLine());
        
            switch (userChoice)
            {
                case 1:
                    Console.WriteLine("1.Create a new task:");
                    TaskManager.CreateNewTask();
                    break;
                case 2:
                    Console.WriteLine("2.Edit existing task:");
                    Console.WriteLine("Type in name of the task");
                    TaskManager.EditTask();
                    break;
                case 3:
                    Console.WriteLine("3.Find task - Type in the name of the task:");
                    Task taskToFind = TaskManager.FindTaskByName();
                    taskToFind.PrintTaskInfo(taskToFind);
                    break;
                case 4:
                    Console.WriteLine("4.Delete task:");
                    TaskManager.DeleteTask();
                    break;
                case 5:
                    Console.WriteLine("5. Filter tasks by:");
                    TaskManager.FilterTasks();
                    break;
                case 6:
                    Console.WriteLine("6.Sort tasks by:");
                    TaskManager.SortTasks();
                    break;
                case 7:
                    Console.WriteLine("7.View all tasks:");
                    TaskManager.ShowAllTasks();
                    break;
                case 8:
                    Console.WriteLine("Exiting menu...\n");
                    menuExit = true;
                    break;
                default:
                    Console.WriteLine("Please choose a valid option\n");
                    break;
            }
            
        } while (!menuExit);

        


    }
}