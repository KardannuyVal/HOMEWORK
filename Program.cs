  using System;
  using System.IO;

class Program
{
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.WriteLine("Задание 6.2");

        int[,] matrix1 = {
            {1, 2, 3},
            {4, 5, 6},
        };

        int[,] matrix2 = {
            {7, 8},
            {9, 10},
            {11, 12},
        };

        Console.WriteLine("Первая матрица:");
        PrintMatrix(matrix1);

        Console.WriteLine("\nВторая матрица:");
        PrintMatrix(matrix2);

        try
        {
            int[,] resultMatrix = MultiplyMatrices(matrix1, matrix2);
            Console.WriteLine("\nРезультат умножения матриц:");
            PrintMatrix(resultMatrix);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

        Console.WriteLine("Задание 6.3");
        int[,] temperatures = GenerateRandomTemperatures();
        double[] averageTemperatures = CalculateAverageTemperatures(temperatures);

        Console.WriteLine("Средние температуры по месяцам:");
        for (int month = 0; month < 12; month++)
        {
            Console.WriteLine($"Месяц {month + 1}: {averageTemperatures[month]:F2}°C");
        }

        Array.Sort(averageTemperatures);

        Console.WriteLine("\nСредние температуры по месяцам (отсортированные):");
        foreach (var temperature in averageTemperatures)
        {
            Console.WriteLine($"{temperature:F2}°C");
        }
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
    {
        int rows1 = matrix1.GetLength(0);
        int cols1 = matrix1.GetLength(1);
        int cols2 = matrix2.GetLength(1);

        if (cols1 != matrix2.GetLength(0))
        {
            throw new ArgumentException("Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
        }

        int[,] result = new int[rows1, cols2];

        for (int i = 0; i < rows1; i++)
        {
            for (int j = 0; j < cols2; j++)
            {
                int sum = 0;
                for (int k = 0; k < cols1; k++)
                {
                    sum += matrix1[i, k] * matrix2[k, j];
                }
                result[i, j] = sum;
            }
        }

        return result;
    }

    static int[,] GenerateRandomTemperatures()
    {
        int[,] temperatures = new int[12, 30];

        for (int month = 0; month < 12; month++)
        {
            for (int day = 0; day < 30; day++)
            {
                temperatures[month, day] = random.Next(-10, 31);
            }
        }

        return temperatures;
    }

    static double[] CalculateAverageTemperatures(int[,] temperatures)
    {
        double[] averageTemperatures = new double[12];

        for (int month = 0; month < 12; month++)
        {
            int sum = 0;
            for (int day = 0; day < 30; day++)
            {
                sum += temperatures[month, day];
            }
            averageTemperatures[month] = (double)sum / 30;
        }

        return averageTemperatures;
    }
    
    
}
