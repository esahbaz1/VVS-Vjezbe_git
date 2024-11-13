using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VVSProjekat
{
    public class FileService : IFileService
    {
        private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "tasks.csv");

        public void SaveToFile(List<Task> newTasks, string fileType)
        {
            if (fileType.ToLower() == "csv")
            {
                // Učitavamo sve postojeće zadatke iz CSV-a
                List<Task> existingTasks = LoadFromFile("csv");

                foreach (var newTask in newTasks)
                {
                    // Pronalazimo zadatak sa istim naslovom ako postoji
                    var existingTask = existingTasks.FirstOrDefault(t => t.Title == newTask.Title);

                    if (existingTask != null)
                    {
                        // Ako zadatak već postoji, ažuriramo njegove podatke
                        existingTask.Description = newTask.Description;
                        existingTask.DueDate = newTask.DueDate;
                        existingTask.TaskPriority = newTask.TaskPriority;
                        existingTask.IsCompleted = newTask.IsCompleted;
                        existingTask.Category = newTask.Category;
                        existingTask.ReminderTime = newTask.ReminderTime;
                    }
                    else
                    {
                        // Ako zadatak ne postoji, dodajemo ga u listu
                        existingTasks.Add(newTask);
                    }
                }

                // Prepisujemo sve zadatke u fajl
                var lines = existingTasks.Select(t => $"{t.Title},{t.Description},{t.DueDate:dd/MM/yyyy},{t.TaskPriority},{t.IsCompleted},{t.Category.Name},{t.ReminderTime:dd/MM/yyyy HH:mm}");
                File.WriteAllLines(filePath, lines);

                // Kopiramo podatke u podaci.txt
                CopyToTxtFile();
            }
        }







        public List<Task> LoadFromFile(string fileType)
        {
            List<Task> tasks = new List<Task>();

            if (fileType.ToLower() == "csv")
            {
                // Učitavamo sve zadatke iz CSV fajla
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var parts = line.Split(',');

                    string title = parts[0];
                    string description = parts[1];
                    DateTime dueDate = DateTime.ParseExact(parts[2], "dd/MM/yyyy", null);
                    Priority priority = (Priority)Enum.Parse(typeof(Priority), parts[3]);
                    bool isCompleted = bool.Parse(parts[4]);
                    string categoryName = parts[5]; // Kategorija je 6. element

                    // Pokusaj parsiranja za reminderTime
                    DateTime? reminderTime = null;
                    if (parts.Length > 6 && !string.IsNullOrEmpty(parts[6]))
                    {
                        if (DateTime.TryParseExact(parts[6], "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedReminderTime))
                        {
                            reminderTime = parsedReminderTime;
                        }
                    }

                    Category category = new Category(categoryName); // Kreiramo kategoriju na osnovu naziva

                    Task task = new Task(title, description, dueDate, priority, category)
                    {
                        IsCompleted = isCompleted,
                        ReminderTime = reminderTime
                    };

                    tasks.Add(task);
                }
            }

            return tasks;
        }





        public void DeleteCompletedTask(Task task)
        {
            var tasks = LoadFromFile("csv");
            tasks.RemoveAll(t => t.Title == task.Title && t.IsCompleted); // Uklanja zadatak koji je označen kao završen
            SaveToFile(tasks, "csv"); // Ažurira fajl nakon brisanja

            // Pozivamo CopyToTxtFile kako bi se podaci odmah prekopirali u podaci.txt
            CopyToTxtFile();
        }

        private void CopyToTxtFile()
        {
            // Učitavamo zadatke iz CSV fajla
            List<Task> tasks = LoadFromFile("csv");

            // Putanja za podaci.txt u Solution Explorer-u
            string solutionPath = Path.Combine(Directory.GetCurrentDirectory(), "podaci.txt");

            // Ako podaci.txt već postoji, brišemo ga pre nego što upišemo nove podatke
            if (File.Exists(solutionPath))
            {
                File.Delete(solutionPath);
            }

            // Priprema linija za pisanje u podaci.txt
            var lines = tasks.Select(t => $"{t.Title},{t.Description},{t.DueDate:dd/MM/yyyy},{t.TaskPriority},{t.IsCompleted},{t.Category.Name}");

            // Upisivanje u podaci.txt
            File.WriteAllLines(solutionPath, lines);

            Console.WriteLine("Podaci su automatski prekopirani u podaci.txt.");
        }


    }
}