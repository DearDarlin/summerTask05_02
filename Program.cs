public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed,
    Deferred
}

public class TaskItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }

    public TaskItem(string title, string description)
    {
        Title = title;
        Description = description;
        Status = TaskStatus.NotStarted;
    }

    public void ChangeStatus(TaskStatus newStatus)
    {
        Status = newStatus;
    }

    public void Display()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine(new string('-', 30));
    }
}


class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- TO-DO LIST ---");
            Console.WriteLine("1. Add a task");
            Console.WriteLine("2. Change task status");
            Console.WriteLine("3. Show all tasks");
            Console.WriteLine("4. Show tasks by status");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ChangeTaskStatus();
                    break;
                case "3":
                    ShowAllTasks();
                    break;
                case "4":
                    ShowTasksByStatus();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter a task name: ");
        string title = Console.ReadLine();
        Console.Write("Enter a task description: ");
        string desc = Console.ReadLine();

        tasks.Add(new TaskItem(title, desc));
        Console.WriteLine("Task added!");
    }

    static void ChangeTaskStatus()
    {
        ShowAllTasks();
        Console.Write("Enter the task number to change the status: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
        {
            Console.WriteLine("Choose a new status:");
            foreach (var status in Enum.GetValues(typeof(TaskStatus)))
            {
                Console.WriteLine($"{(int)status}. {status}");
            }

            if (int.TryParse(Console.ReadLine(), out int statusValue) &&
                Enum.IsDefined(typeof(TaskStatus), statusValue))
            {
                tasks[index - 1].ChangeStatus((TaskStatus)statusValue);
                Console.WriteLine("Status changed.");
            }
            else
            {
                Console.WriteLine("Invalid status.");
            }
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void ShowAllTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Task list is empty..");
            return;
        }

        Console.WriteLine("\nAll tasks:");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"Task #{i + 1}");
            tasks[i].Display();
        }
    }

    static void ShowTasksByStatus()
    {
        Console.WriteLine("Select a status to filter by:");
        foreach (var status in Enum.GetValues(typeof(TaskStatus)))
        {
            Console.WriteLine($"{(int)status}. {status}");
        }

        if (int.TryParse(Console.ReadLine(), out int statusValue) &&
            Enum.IsDefined(typeof(TaskStatus), statusValue))
        {
            var filtered = tasks.FindAll(t => t.Status == (TaskStatus)statusValue);

            if (filtered.Count == 0)
                Console.WriteLine("There are no tasks with this status..");
            else
            {
                Console.WriteLine($"\nTasks with status {(TaskStatus)statusValue}:");
                foreach (var task in filtered)
                    task.Display();
            }
        }
        else
        {
            Console.WriteLine("Invalid status.");
        }
    }
}