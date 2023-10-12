using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

class Program
{
    static List<Student> students = new List<Student>();
    static void Main(string[] args)
    {
        // Создаем список для хранения картинок
        List<string> images = new List<string>();

        // Загружаем 32 пары картинок (замените URL на свои картинки)
        for (int i = 0; i < 32; i++)
        {
            images.Add("https://avatars.mds.yandex.net/i?id=9fb4b6f54b898b31c0f10c5a4c20241751c95d89-10915107-images-thumbs&n=13");
            images.Add("https://avatars.mds.yandex.net/i?id=211993b263ee0eefabad6d99c650bfddb1f8d312-10952374-images-thumbs&n=13");
        }

        // Перемешиваем список
        Random rng = new Random();
        int n = images.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string temp = images[k];
            images[k] = images[n];
            images[n] = temp;
        }

        // Выводим изначальный список
        Console.WriteLine("Изначальный список:");
        for (int i = 0; i < images.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {images[i]}");
        }

        // Создаем копию перемешанного списка
        List<string> shuffledImages = images.ToList();

        // Перемешиваем копию списка
        n = shuffledImages.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string temp = shuffledImages[k];
            shuffledImages[k] = shuffledImages[n];
            shuffledImages[n] = temp;
        }

        // Выводим перемешанный список
        Console.WriteLine("\nПолученный список:");
        for (int i = 0; i < shuffledImages.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {shuffledImages[i]}");
        }
        List<Student> students = new List<Student>();
        LoadStudentsFromFile("students.txt");

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("a. Новый студент");
            Console.WriteLine("b. Удалить");
            Console.WriteLine("c. Сортировать");
            Console.WriteLine("q. Выйти");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "a":
                    AddStudent();
                    break;
                case "b":
                    RemoveStudent();
                    break;
                case "c":
                    SortStudentsByScore();
                    break;
                case "q":
                    SaveStudentsToFile("students.txt");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Console.WriteLine("Введите фамилию студента:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Введите имя студента:");
        string firstName = Console.ReadLine();

        Console.WriteLine("Введите год рождения студента:");
        if (!int.TryParse(Console.ReadLine(), out int birthYear))
        {
            Console.WriteLine("Некорректный год рождения. Введите число.");
            return;
        }

        Console.WriteLine("Введите экзамен, по которому поступил студент:");
        string entranceExam = Console.ReadLine();

        Console.WriteLine("Введите баллы студента:");
        if (!int.TryParse(Console.ReadLine(), out int score))
        {
            Console.WriteLine("Некорректные баллы. Введите число.");
            return;
        }

        Student newStudent = new Student
        {
            LastName = lastName,
            FirstName = firstName,
            BirthYear = birthYear,
            EntranceExam = entranceExam,
            Score = score
        };

        students.Add(newStudent);
        Console.WriteLine("Студент добавлен.");
    }

    static void RemoveStudent()
    {
        Console.WriteLine("Введите фамилию студента для удаления:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Введите имя студента для удаления:");
        string firstName = Console.ReadLine();

        Student studentToRemove = students.FirstOrDefault(s => s.LastName == lastName && s.FirstName == firstName);
        if (studentToRemove != null)
        {
            students.Remove(studentToRemove);
            Console.WriteLine("Студент удален.");
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }

    static void SortStudentsByScore()
    {
        students = students.OrderBy(s => s.Score).ToList();
        Console.WriteLine("Студенты отсортированы по баллам.");
    }

    static void LoadStudentsFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 5)
                {
                    Student student = new Student
                    {
                        LastName = parts[0].Trim(),
                        FirstName = parts[1].Trim(),
                        BirthYear = int.Parse(parts[2]),
                        EntranceExam = parts[3].Trim(),
                        Score = int.Parse(parts[4])
                    };
                    students.Add(student);
                }
            }
        }
    }

    static void SaveStudentsToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Student student in students)
            {
                writer.WriteLine(student);
            }
        }
    }
class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int BirthYear { get; set; }
    public string EntranceExam { get; set; }
    public int Score { get; set;}

    public override string ToString()
     {
        return $"{LastName}, {FirstName}, {BirthYear}, {EntranceExam}, {Score}";
    }
 }
}


