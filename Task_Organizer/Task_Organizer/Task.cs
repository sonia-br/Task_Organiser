using System.Data;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
namespace Task_Organizer;

public class Task
{
    private static int nextId = 1;
    [JsonProperty] private int id;
    [JsonProperty] private string name;
    [JsonProperty] private string description;
    [JsonProperty] private string creator;
    [JsonProperty] private string performer;
    [JsonProperty] private TaskPriority priority;
    [JsonProperty] private TaskStatus status;
    [JsonProperty] private DateOnly deadline;
    [JsonProperty] private string comment;

    public enum TaskPriority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
    public enum TaskStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Paused = 3,
        Finished = 4
    }

    public Task(string name, string description, string creator, string performer, TaskPriority priority,
        TaskStatus status, DateOnly deadline, string comment)//constuctor for creating a task
    {
        id = nextId++;
        this.name = name;
        this.description = description;
        this.creator = creator;
        this.performer = performer;
        this.priority = priority;
        this.status = status;
        this.deadline = deadline;
        this.comment = comment;
    }
    
    [JsonConstructor]
    public Task(int id, string name, string description, string creator, string performer, TaskPriority priority,
        TaskStatus status, DateOnly deadline, string comment) //constuctor for loading from file with existing tasks
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.creator = creator;
        this.performer = performer;
        this.priority = priority;
        this.status = status;
        this.deadline = deadline;
        this.comment = comment;
    }

    public int GetId()
    {
        return id;
    }

    public static void SetNextId(int newNextId)
    {
        nextId = newNextId;
    }
    public void UpdateName(string newName)
    {
        name = newName;
        Console.WriteLine("Name is changed to '" + name + "'.");
    }
    public string GetName()
    {
        if (name == null)
        {
            return null;
        }
        return name;
    }
    
    public void UpdateDescription(string newDescription)
    {
        description = newDescription;
    }
    public string GetDescription()
    {
        return description;
    }
    
    public void UpdatePerformer(string newPerformer)
    {
        performer = newPerformer;
    }
    public string GetPerformer()
    {
        if (performer == null)
        {
            return null;
        }
        return performer;
    }

    public static TaskPriority ValidatePriority()
    {
        int priorityToValidate = 0;
        bool correctPriorityNumber = false;
        do
        {
            int userInput = int.Parse(Console.ReadLine());
            priorityToValidate = userInput;
            if (priorityToValidate == 1 || priorityToValidate == 2 || priorityToValidate == 3)
            {
                correctPriorityNumber = true;
            }
            else
            {
                Console.WriteLine("Invalid input.Type in one of three numbers.");
            }
        } while (!correctPriorityNumber);
        return (TaskPriority)priorityToValidate;
    }
    public void UpdatePriority()
    {
        bool correctPriorityNumber = false;
        do
        {
            int newPriority = Convert.ToInt32(Console.ReadLine());
            if (newPriority == 1 || newPriority == 2 || newPriority == 3)
            {
                priority = (TaskPriority)newPriority; 
                if (this.priority == TaskPriority.Low)
                {
                    Console.WriteLine("Priority is changed to " + priority);
                }
                else if (priority == TaskPriority.Medium)
                {
                    Console.WriteLine("Priority is changed to " + priority);
                }
                else if (priority == TaskPriority.High)
                {
                    Console.WriteLine("Priority is changed to " + priority);
                }
                correctPriorityNumber = true;
            }
            else 
            {
                Console.WriteLine("Invalid input.Type in one of three numbers.");
            }
        } while (!correctPriorityNumber);
    }
    public TaskPriority GetPriority()
    {
        return priority;
    }

    
    public static TaskStatus ValidateStatus()
    {
        int statusToValidate = 0;
        bool correctStatusNumber = false;
        do
        {
            int userInput = int.Parse(Console.ReadLine());
            statusToValidate = userInput;
            if (statusToValidate == 1 || statusToValidate == 2 || statusToValidate == 3 || statusToValidate == 4)
            {
                correctStatusNumber = true;
            }
            else
            {
                Console.WriteLine("Invalid input.Type in one of four numbers.");
            }
        } while (!correctStatusNumber);
        return (TaskStatus)statusToValidate;
    }
    public void UpdateStatusOfTask()
    {
        bool correctStatusNumber = false;
        do
        {
            int newStatus = int.Parse(Console.ReadLine());
            if (newStatus == 1 || newStatus == 2 || newStatus == 3 || newStatus == 4)
            {
                this.status = (TaskStatus)newStatus;
                switch (this.status)
                {
                    case TaskStatus.NotStarted:
                        Console.WriteLine("Status is changed to " + this.status);
                        break;
                    case TaskStatus.InProgress:
                        Console.WriteLine("Status is changed to " + this.status);
                        break;
                    case TaskStatus.Paused:
                        Console.WriteLine("Status is changed to " + this.status);
                        break;
                    case TaskStatus.Finished:
                        Console.WriteLine("Status is changed to " + this.status);
                        break;
                        
                }
                correctStatusNumber = true;
            }
            else 
            {
                Console.WriteLine("Invalid input.Type in one of four numbers.");
            }
        } while (!correctStatusNumber);

    }
    
    public TaskStatus GetStatus()
    {
        return status;
    }

    public static DateOnly ValidateDeadline()
    {
        bool dateIsCorrect = false;
        DateOnly deadlineValid = default;
        do
        {
            string dateToValidate = Console.ReadLine();
            if (DateOnly.TryParse(dateToValidate, out deadlineValid))
            {
                dateIsCorrect = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again:");
            }
        } while (!dateIsCorrect);

        return deadlineValid;
    }
    public void UpdateDeadline()
    {
        bool dateIsCorrect = false;
        do
        {
            string dateToUpdate = Console.ReadLine();
            if (DateOnly.TryParse(dateToUpdate, out deadline))
            {
                this.deadline = DateOnly.Parse(dateToUpdate);
                dateIsCorrect = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again:");
            }
        } while (!dateIsCorrect);
        
    }
    public DateOnly GetDeadline()
    {
        return deadline;
    }
    
    public void UpdateComment(string newComment)
    {
        comment = newComment;
    }
    public string GetComment()
    {
        return comment;
    }

    public void PrintTaskInfo(Task task)
    {
        Console.WriteLine("---------- Task "+ id + " ----------" );
        Console.WriteLine("Task id: " + id );
        Console.WriteLine("Task name: " + name );
        Console.WriteLine("Description: " + description );
        Console.WriteLine("Creator: " + creator );
        Console.WriteLine("Performer: " + performer );
        Console.WriteLine("Priority: " + priority );
        Console.WriteLine("Status: " + status );
        Console.WriteLine("Deadline: " + deadline );
        Console.WriteLine("Comment: " + comment );
        Console.WriteLine("-----------------------------------" );
    }
    
}
