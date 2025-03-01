using System.Data;
using System.Globalization;
using Newtonsoft.Json;

namespace Task_Organizer;

public static class TaskManager
{
    private static List<Task> tasks;
    private static string filePath = "tasklist.json";
    
    static TaskManager()
    {
        tasks = LoadTasksFromFile();
    }
    
    public static void AddTask(Task task)
    {
        tasks.Add(task);
        SaveTasksToFile();
    }

    public static List<Task> GetTasks()
    {
        return tasks;
    }

    private static List<Task> LoadTasksFromFile()
    {
        if (File.Exists(filePath)) 
        {
            string json = File.ReadAllText(filePath);
            List<Task> tasksToLoad = JsonConvert.DeserializeObject<List<Task>>(json); 

            if (tasksToLoad == null)
            {
                tasksToLoad = new List<Task>(); 
            }
            else if (tasksToLoad.Count > 0)
            {
                int maxIdInFile = 0;
                foreach (Task task in tasksToLoad)
                {
                    if (task.GetId() > maxIdInFile)
                    {
                        maxIdInFile = task.GetId();
                    }
                }

                Task.SetNextId(maxIdInFile + 1);
            }
            return tasksToLoad;
        }
        else
        {
            return new List<Task>(); 
        }
    }
    public static void SaveTasksToFile()
    {
        string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
    public static void CreateNewTask()
    {

        Console.WriteLine("Type in the name of the task:");
        string name = Console.ReadLine();

        Console.WriteLine("Add description of the task:");
        string description = Console.ReadLine();

        Console.WriteLine("Add the creator of this task:");
        string creator = Console.ReadLine();

        Console.WriteLine("Add the performer of this task:");
        string performer = Console.ReadLine();
        
        Console.WriteLine("Set priority of this task. Choose corresponding number:");
        Console.WriteLine("1 - High, 2 - Medium, 3 - Low");
        Task.TaskPriority priority = Task.ValidatePriority();
        
        Console.WriteLine("The status is set to " + Task.TaskStatus.NotStarted + "\n");
        Task.TaskStatus status = Task.TaskStatus.NotStarted;

        Console.WriteLine("The deadline of the task (dd.mm.yyyy):");
        DateOnly deadline = Task.ValidateDeadline();

        Console.WriteLine("Add comments to this task:");
        string comment = Console.ReadLine();
        
        Task newTask = new Task(name, description, creator, performer,
            priority, status, deadline, comment );
        
        AddTask(newTask);
        Console.WriteLine("Task " + name + " is added.\n");

        Console.WriteLine("Do you want to add another task? y/n");
        bool userInputCorrect = false;
        do
        {
            string userInput = Console.ReadLine().ToLower();
            switch (userInput)
            {
                case "y":
                    CreateNewTask();
                    userInputCorrect = true;
                    break;
                case "n":
                    userInputCorrect = true;
                    break;
                default:
                    Console.WriteLine("Choose 'y' or 'n'");
                    break;
            }
        } while (!userInputCorrect);
       
    }

    public static void EditTask()
    {
        Task taskToEdit = FindTaskByName();

        bool menuExit = false;
        do
        {
            Console.WriteLine("Which field do you want to edit?:");
            Console.WriteLine("Type in the corresponding number:\n" +
                          "1 - name; 2 - description; 3 - performer; 4 - priority; 5 - status;\n" +
                          "6 - deadline; 7 - comment; 8 - Show task info\n" +
                          "Press 9 to exit menu.");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch (userChoice)
            {
                case 1:
                    Console.WriteLine("Type in the new name:");
                    string newName = Console.ReadLine();
                    taskToEdit.UpdateName(newName);
                    break;
                case 2:
                    Console.WriteLine("Type in new description:");
                    string newDescription = Console.ReadLine();
                    taskToEdit.UpdateDescription(newDescription);
                    break;
                case 3:
                    Console.WriteLine("Type in the name of new performer:");
                    string newPerformer = Console.ReadLine();
                    taskToEdit.UpdatePerformer(newPerformer);
                    break;
                case 4:
                    Console.WriteLine("Type in new priority:\n" +
                                      "1 - High, 2 - Medium, 3 - Low.");
                    taskToEdit.UpdatePriority();
                    break;
                case 5:
                    Console.WriteLine("Type in new status:\n" +
                                      "1 - Not started, 2 - In progress, 3 - Paused, 4 - Completed");
                    taskToEdit.UpdateStatusOfTask();
                    break;
                case 6:
                    Console.WriteLine("Type in the new deadline:");
                    taskToEdit.UpdateDeadline();
                    break;
                case 7:
                    Console.WriteLine("Type in the new comment:");
                    string newComment = Console.ReadLine();
                    taskToEdit.UpdateComment(newComment);
                    break;
                case 8:
                    taskToEdit.PrintTaskInfo(taskToEdit);
                    break;
                case 9:
                    menuExit = true;
                    break;
                default:
                    Console.WriteLine("Invalid input.Choose correct number from menu.");
                    break;
            }
            
        } while (!menuExit);
        SaveTasksToFile();
    }

    public static Task FindTaskByName()
    {
        Task taskToFind = null;
        bool nameIsCorrect = false;
        do
        {
            string name = Console.ReadLine();
            foreach (Task task in tasks)
            {
                if (name.ToLower() == task.GetName().ToLower())
                {
                    taskToFind = task;
                    nameIsCorrect = true;
                }
            }

            if (!nameIsCorrect)
            {
                Console.WriteLine("Task with this name doesn't exist.");
                Console.WriteLine("Try again:");
            }
        } while (!nameIsCorrect);
        return taskToFind;
    }

    public static void ShowAllTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("There are no tasks\n");
            return;
        }

        Console.WriteLine("All created tasks:");
        foreach (Task task in tasks)
        {
            task.PrintTaskInfo(task);
        }
    }
    

    public static void DeleteTask()
    {
        bool nameIsCorrect = false;
        do
        {
            Console.WriteLine("Type in the name of the task to delete:");
            Task taskToDelete = FindTaskByName();
            if (taskToDelete != null)
            {
                tasks.Remove(taskToDelete);
                Console.WriteLine("The task " + taskToDelete.GetName() + " is deleted.");
                nameIsCorrect = true;
            }
            else
            {
                Console.WriteLine("Try again.");
            }
            
        } while (!nameIsCorrect);
        SaveTasksToFile();
    }

    public static void FilterTasks()
    {
        bool menuExit = false;
        do
        {
            Console.WriteLine("Choose filter:");
            Console.WriteLine("1 - Performer; 2 - Deadline; 3 - Priority ; 4 - Status; 5 - Go back to main menu");
            int userChoice = int.Parse(Console.ReadLine());

            switch (userChoice)
            {
                case 1:
                    Console.WriteLine("Enter the name of performer:");
                    string performer = Console.ReadLine();
                    List<Task> tasksByPerformer = FindTaskByPerformer(performer);
                    foreach (Task task in tasksByPerformer)
                    {
                        task.PrintTaskInfo(task);
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the deadline:");
                    DateOnly deadline = DateOnly.Parse(Console.ReadLine());
                    List<Task> tasksByDeadline = FindTaskByDeadline(deadline);
                    foreach (Task task in tasksByDeadline)
                    {
                        task.PrintTaskInfo(task);
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter the priority:");
                    Console.WriteLine("1 - High, 2 - Medium, 3 - Low");
                    Task.TaskPriority userPriority = Task.ValidatePriority();
                    List<Task> tasksByPriority = FindTaskByPriority(userPriority);
                    foreach (Task task in tasksByPriority)
                    {
                        task.PrintTaskInfo(task);
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter the status:");
                    Console.WriteLine("1 - Not started, 2 - In progress, 3 - Paused, 4 - Completed");
                    Task.TaskStatus userStatus = Task.ValidateStatus();
                    List<Task> tasksByStatus = FindTaskByStatus(userStatus);
                    foreach (Task task in tasksByStatus)
                    {
                        task.PrintTaskInfo(task);
                    }
                    break;
                case 5:
                    menuExit = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Choose one of the options.");
                    break;
            }
            
        } while (!menuExit);
        
    }
    public static List<Task> FindTaskByPerformer(string performer)
    {
        List<Task> matchingTasks = new List<Task>();
        
        foreach (Task task in tasks)
        {
            if (performer.ToLower() == task.GetPerformer().ToLower())
            {
                matchingTasks.Add(task);
            }
        }

        if (matchingTasks.Count == 0)
        {
            Console.WriteLine("Task with this performer doesn't exist.");
        }
        return matchingTasks;
    }
    public static List<Task> FindTaskByDeadline(DateOnly deadline)
    {
        List<Task> matchingTasks = new List<Task>();
        foreach (Task task in tasks)
        {
            if (deadline == task.GetDeadline())
            {
                matchingTasks.Add(task);
            }
        }

        if (matchingTasks.Count == 0)
        {
            Console.WriteLine("Task with this deadline doesn't exist.");
        }
        return matchingTasks;
    }
    public static List<Task> FindTaskByPriority(Task.TaskPriority priority)
    {
        List<Task> matchingTasks = new List<Task>();
        foreach (Task task in tasks)
        {
            if (priority == task.GetPriority())
            {
                matchingTasks.Add(task);
            }
        }

        if (matchingTasks.Count == 0)
        {
            Console.WriteLine("Task with this priority doesn't exist.");
        }
        return matchingTasks;
    }
    public static List<Task> FindTaskByStatus(Task.TaskStatus status)
    {
        List<Task> matchingTasks = new List<Task>();
        foreach (Task task in tasks)
        {
            if (status == task.GetStatus())
            {
                matchingTasks.Add(task);
            }
        }

        if (matchingTasks.Count == 0)
        {
            Console.WriteLine("Task with this status doesn't exist.");
        }
        return matchingTasks;
    }

    public static void SortTasks()
    {
        bool menuExit = false;
        do
        {
            Console.WriteLine("1 - tasks with higher priority first; 2 - tasks with closer deadline first;" +
                              "3 - highest priority and closest date; 4 - exit to main menu");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch (userChoice)
            {
                case 1:
                    SortByPriority();
                    break;
                case 2:
                    SortByDeadline();
                    break;
                case 3:
                    SortByPriorityAndDeadline();
                    break;
                case 4:
                    menuExit = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Choose one of four numbers.");
                    break;
            }
        } while (!menuExit);
    }
    
    public static void SortByPriority()
    {
        var sortedTasks = tasks
            .OrderBy(p => p.GetPriority());

        foreach (Task task in sortedTasks)
        {
            task.PrintTaskInfo(task);
        }
    }
    public static void SortByDeadline()
    {
        var sortedTasks = tasks
            .OrderBy(p => p.GetDeadline());

        foreach (Task task in sortedTasks)
        {
            task.PrintTaskInfo(task);
        }
        
    }

     public static void SortByPriorityAndDeadline()
     {
         var sortedTasks = tasks
                 .OrderBy(p => p.GetPriority())
                 .ThenBy(p => p.GetDeadline());

         foreach (Task task in sortedTasks)
         {
             task.PrintTaskInfo(task);
         }
     }
}