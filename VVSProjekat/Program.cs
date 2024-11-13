using System;
using System.Collections.Generic;

namespace VVSProjekat
{
    class Program
    {
        static void Main(string[] args)
        {
            // Kreiramo servise
            ITaskService taskService = new TaskService();
            IGoalService goalService = new GoalService();
            IReportService reportService = new ReportService();
            IReminderService reminderService = new ReminderService();
            ITaskAssignmentService taskAssignmentService = new TaskAssignmentService();
            IFileService fileService = new FileService();


            // Kreiramo osnovnu kategoriju za zadatke
            Category workCategory = new Category("Work");

            // Meni za korisnika
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Izaberite funkcionalnost:");
                Console.WriteLine("1. Kreiraj zadatak");
                Console.WriteLine("2. Prikaz zadataka");
                Console.WriteLine("3. Postavi ciljeve");
                Console.WriteLine("4. Generiši izvještaj");
                Console.WriteLine("5. Dodaj podsjetnik");
                Console.WriteLine("6. Arhiviraj završene zadatke");
                Console.WriteLine("7. Automatski dodijeli zadatak");
                Console.WriteLine("8. Oznaci zadatak kao zavrsen");
                Console.WriteLine("9. Kraj");
                Console.Write("Odaberite opciju (1-9): ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Kreiranje novog zadatka
                        Console.Write("Unesite naziv zadatka: ");
                        string title = Console.ReadLine();

                        Console.Write("Unesite opis zadatka: ");
                        string description = Console.ReadLine();

                        Console.Write("Unesite datum isteka (dd/MM/yyyy): ");
                        DateTime dueDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                        Console.WriteLine("Odaberite prioritet (0 - Low, 1 - Medium, 2 - High): ");
                        Priority priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine());

                        // Izbor kategorije
                        Console.WriteLine("Odaberite kategoriju zadatka:");
                        Console.WriteLine("0 - Posao");
                        Console.WriteLine("1 - Personalno");
                        Console.WriteLine("2 - Generalno");
                        int categoryChoice = int.Parse(Console.ReadLine());

                        Category selectedCategory;
                        switch (categoryChoice)
                        {
                            case 0:
                                selectedCategory = new Category("Posao");
                                break;
                            case 1:
                                selectedCategory = new Category("Personalno");
                                break;
                            case 2:
                                selectedCategory = new Category("Generalno");
                                break;
                            default:
                                Console.WriteLine("Nepoznata opcija. Kategorija nije odabrana.");
                                selectedCategory = new Category("Generalno"); // Default kategorija
                                break;
                        }

                        Task newTask = new Task(title, description, dueDate, priority, selectedCategory);
                        taskService.CreateTask(newTask);

                        // Spremamo zadatak u CSV
                        fileService.SaveToFile(new List<Task> { newTask }, "csv");

                        Console.WriteLine("Zadatak je kreiran i dodan u fajl bez brisanja postojećih zadataka.");
                        break;



                    case "2":
                        // Prikaz zadataka
                        Console.WriteLine("Lista zadataka:");

                        // Učitavamo zadatke iz CSV fajla
                        List<Task> loadedTasks = fileService.LoadFromFile("csv");

                        // Ispis učitanih zadataka
                        if (loadedTasks.Count == 0)
                        {
                            Console.WriteLine("Nema zadataka za prikaz.");
                        }
                        else
                        {
                            foreach (var task in loadedTasks)
                            {
                                // Prikaz osnovnih informacija o zadatku
                                Console.Write($"Zadatak: {task.Title}, Rok: {task.DueDate:dd/MM/yyyy}, Prioritet: {task.TaskPriority}, Kategorija: {task.Category.Name}, Završeno: {task.IsCompleted}");

                                // Provera da li zadatak ima postavljen podsetnik
                                if (task.ReminderTime.HasValue)
                                {
                                    Console.Write($", Podsjetnik: {task.ReminderTime.Value:dd/MM/yyyy HH:mm}");
                                }

                                Console.WriteLine(); // Novi red nakon prikaza zadatka
                            }
                        }
                        break;



                    case "3":
                        Console.Write("Unesite broj zadataka koje treba završiti mjesečno: ");
                        int monthlyGoal = int.Parse(Console.ReadLine());
                        goalService.SetMonthlyGoal(monthlyGoal);
                        goalService.TrackProgress();
                        break;

                    case "4":
                        List<Task> allTasks = fileService.LoadFromFile("csv");

                        // Unos datuma za izvještaj
                        Console.Write("Unesite početni datum izvještaja (dd/MM/yyyy): ");
                        DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                        Console.Write("Unesite završni datum izvještaja (dd/MM/yyyy): ");
                        DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                        // Generisanje izvještaja
                        var report = reportService.GenerateReport(startDate, endDate, allTasks);

                        // Prikaz grupisanih zadataka po prioritetu i kategoriji
                        Console.WriteLine("\nIzvještaj o zadacima grupisanim po prioritetima i kategorijama:");
                        reportService.DisplayTasksGroupedByPriorityAndCategory(report);
                        break;




                    case "5":
                        // Dodavanje podsjetnika
                        Console.Write("Unesite naziv zadatka za koji želite postaviti podsjetnik: ");
                        string taskName = Console.ReadLine();

                        // Učitavamo zadatke iz CSV fajla
                        List<Task> tasks = fileService.LoadFromFile("csv");

                        // Pronalazimo zadatak po nazivu
                        var taskForReminder = tasks.FirstOrDefault(t => t.Title == taskName);
                        if (taskForReminder != null)
                        {
                            Console.Write("Unesite vrijeme podsjetnika (dd/MM/yyyy HH:mm): ");
                            DateTime reminderTime = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null);

                            // Postavljamo podsjetnik za zadatak
                            taskForReminder.ReminderTime = reminderTime;
                            reminderService.SetReminder(new Reminder(taskForReminder, reminderTime));

                            // Spremamo ažurirane zadatke nazad u CSV fajl
                            fileService.SaveToFile(tasks, "csv");

                            Console.WriteLine("Podsjetnik je dodan i zadatak je ažuriran u CSV fajlu.");
                        }
                        else
                        {
                            Console.WriteLine("Zadatak nije pronađen!");
                        }
                        break;





                    case "6":
                        // Arhiviranje završnih zadataka
                        taskService.ArchiveCompletedTasks();
                        break;

                    case "7":
                        // Unos naziva zadatka za automatsku dodelu
                        Console.Write("Unesite novi naziv zadatka za automatsku dodjelu: ");
                        string newTaskName = Console.ReadLine();

                        // Unos prioritet i kategorije
                        Console.WriteLine("Unesite prioritet zadatka (0 - Low, 1 - Medium, 2 - High): ");
                        if (!Enum.TryParse<Priority>(Console.ReadLine(), out Priority taskPriority))
                        {
                            Console.WriteLine("Neispravan unos za prioritet.");
                            break;
                        }

                        Console.WriteLine("Unesite kategoriju zadatka (Posao, Personalno, Generalno): ");
                        string categoryInput = Console.ReadLine().Trim();

                        // Provjera da li je unesena ispravna kategorija
                        var validCategories = new List<string> { "Posao", "Personalno", "Generalno" };
                        if (!validCategories.Contains(categoryInput))
                        {
                            Console.WriteLine("Neispravan unos za kategoriju.");
                            break;
                        }

                        // Učitavanje zadataka koji odgovaraju prioritetu i kategoriji
                        var tasksForAssignment = taskService.GetTasks()
                            .Where(t => t.TaskPriority == taskPriority && t.Category.Name.Equals(categoryInput, StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        if (tasksForAssignment.Count == 0)
                        {
                            Console.WriteLine("Nema zadatka koji ispunjava vaše uslove.");
                            break;
                        }

                        // Nasumično biranje zadatka iz filtrirane liste
                        var random = new Random();
                        var taskForAssignment = tasksForAssignment[random.Next(tasksForAssignment.Count)];

                        // Spremanje novog zadatka sa zadatim nazivom
                        var newTaska = new Task(
                            newTaskName,
                            taskForAssignment.Description,
                            taskForAssignment.DueDate,
                            taskForAssignment.TaskPriority,
                            taskForAssignment.Category
                        )
                        {
                            IsCompleted = taskForAssignment.IsCompleted,
                            ReminderTime = taskForAssignment.ReminderTime,
                            AssignedDate = DateTime.Now  // Dodjela trenutnog datuma
                        };

                        // Dodavanje zadatka u CSV fajl
                        fileService.SaveToFile(new List<Task> { newTaska }, "csv");

                        Console.WriteLine($"Novi zadatak '{newTaskName}' dodijeljen automatski sa prioritetom {taskPriority} u kategoriji {categoryInput}.");
                        break;




                    case "8":
                        Console.Write("Unesite naziv zadatka koji želite označiti kao završen: ");
                        string taskToComplete = Console.ReadLine();

                        // Učitavamo zadatke iz CSV fajla
                        List<Task> tasksFromFile = fileService.LoadFromFile("csv");

                        // Pronađi zadatak u listi učitanih zadataka
                        var taskForCompletion = tasksFromFile.FirstOrDefault(t => t.Title.ToLower().Trim() == taskToComplete.ToLower().Trim());
                        if (taskForCompletion != null)
                        {
                            // Označavamo zadatak kao završen
                            taskForCompletion.IsCompleted = true;

                            // Ažuriranje svih zadataka u CSV fajlu (zadatak je sada označen kao završen, ali nije obrisan)
                            fileService.SaveToFile(tasksFromFile, "csv");

                            Console.WriteLine("Zadatak je označen kao završen.");
                        }
                        else
                        {
                            Console.WriteLine("Zadatak nije pronađen!");
                        }
                        break;




                    case "9":
                        // Izlaz iz programa
                        Console.WriteLine("Zatvaranje aplikacije...");
                        return;

                    default:
                        Console.WriteLine("Nepoznata opcija. Pokušajte ponovo.");
                        break;
                }

                // Čekanje korisnika da pritisne tipku za nastavak
                Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
            }
        }
    }

}
