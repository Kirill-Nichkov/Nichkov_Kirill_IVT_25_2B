using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5
{
    public class Program
    {
        private static readonly Random random = new Random();

        private static readonly string[] samples = new string[]
        {
            "абвг деё",
            "жзий клмн",
            "опрс туф"
        };

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            int[,] matrix = null;
            int[][] jagged = null;
            string text = null;

            bool isRunning = true;
            while (isRunning)
            {
                PrintMenu(
                    "1. Двумерные массивы",
                    "2. Рваные массивы",
                    "3. Строки",
                    "4. Выход");

                int choice = ReadInt("Выберите пункт (1-4): ", 1, 4);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        RunMatrixMenu(ref matrix);
                        break;
                    case 2:
                        RunJaggedMenu(ref jagged);
                        break;
                    case 3:
                        RunStringMenu(ref text);
                        break;
                    case 4:
                        isRunning = false;
                        Console.WriteLine("Программа завершена.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void RunMatrixMenu(ref int[,] matrix)
        {
            bool isBack = false;
            while (!isBack)
            {
                PrintMenu(
                    "1. Создать случайно (ДСЧ)",
                    "2. Создать вручную",
                    "3. Напечатать массив",
                    "4. Удалить строки с K1 по K2",
                    "5. Назад");

                int choice = ReadInt("Выберите пункт: ", 1, 5);
                Console.WriteLine();

                if (choice == 1 || choice == 2)
                {
                    int rows = ReadInt("Число строк (1-20): ", 1, 20);
                    int cols = ReadInt("Число столбцов (1-20): ", 1, 20);
                    matrix = CreateMatrix(rows, cols, choice == 1);
                    Console.WriteLine("Массив создан:");
                    PrintArray(matrix);
                }
                else if (choice == 3)
                {
                    PrintArray(matrix);
                }
                else if (choice == 4)
                {
                    if (matrix == null)
                    {
                        Console.WriteLine("Ошибка: массив пуст!");
                    }
                    else
                    {
                        int rows = matrix.GetLength(0);
                        int k1 = ReadInt($"Номер начальной строки K1 (1-{rows}): ", 1, rows);
                        int k2 = ReadInt($"Номер конечной строки K2 ({k1}-{rows}): ", k1, rows);
                        DeleteRows(ref matrix, k1, k2);
                        PrintArray(matrix);
                    }
                }
                else
                {
                    isBack = true;
                }

                Console.WriteLine();
            }
        }

        private static void RunJaggedMenu(ref int[][] jagged)
        {
            bool isBack = false;
            while (!isBack)
            {
                PrintMenu(
                    "1. Создать случайно (ДСЧ)",
                    "2. Создать вручную",
                    "3. Напечатать массив",
                    "4. Добавить строку",
                    "5. Назад");

                int choice = ReadInt("Выберите пункт: ", 1, 5);
                Console.WriteLine();

                if (choice == 1 || choice == 2)
                {
                    int rows = ReadInt("Число строк (1-20): ", 1, 20);
                    jagged = CreateJagged(rows, choice == 1);
                    Console.WriteLine("Массив создан:");
                    PrintArray(jagged);
                }
                else if (choice == 3)
                {
                    PrintArray(jagged);
                }
                else if (choice == 4)
                {
                    int rows = (jagged == null) ? 0 : jagged.Length;
                    int pos = ReadInt($"Позиция для вставки (1-{rows + 1}): ", 1, rows + 1);
                    int len = ReadInt("Длина новой строки (1-20): ", 1, 20);
                    int mode = ReadInt("Заполнение (1 - случайно, 2 - вручную): ", 1, 2);
                    int[] newRow = CreateRow(len, mode == 1);

                    AddRow(ref jagged, pos, newRow);
                    PrintArray(jagged);
                }
                else
                {
                    isBack = true;
                }

                Console.WriteLine();
            }
        }

        private static void RunStringMenu(ref string text)
        {
            bool isBack = false;
            while (!isBack)
            {
                PrintMenu(
                    "1. Ввести с клавиатуры",
                    "2. Выбрать готовую строку",
                    "3. Напечатать текущую строку",
                    "4. Перевернуть слова и отсортировать",
                    "5. Назад");

                int choice = ReadInt("Выберите пункт: ", 1, 5);
                Console.WriteLine();

                if (choice == 1)
                {
                    Console.Write("Введите строку: ");
                    text = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        text = null;
                        Console.WriteLine("Введена пустая строка.");
                    }
                }
                else if (choice == 2)
                {
                    for (int i = 0; i < samples.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {samples[i]}");
                    }

                    int index = ReadInt($"Выберите номер (1-{samples.Length}): ", 1, samples.Length);
                    text = samples[index - 1];
                    Console.WriteLine("Выбрано: " + text);
                }
                else if (choice == 3)
                {
                    Console.WriteLine(string.IsNullOrEmpty(text) ? "Строка пуста." : "Текущая строка: " + text);
                }
                else if (choice == 4)
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        Console.WriteLine("Ошибка: строка пуста!");
                    }
                    else
                    {
                        Console.WriteLine("Исходная строка: " + text);
                        Console.WriteLine("Результат:       " + ProcessText(text));
                    }
                }
                else
                {
                    isBack = true;
                }

                Console.WriteLine();
            }
        }

        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введено не число!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: число слишком большое!");
                }
            }
        }

        public static int ReadInt(string message, int min, int max)
        {
            while (true)
            {
                int value = ReadInt(message);
                if (value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Ошибка: введите число от {min} до {max}!");
            }
        }

        public static void PrintMenu(params string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine(items[i]);
            }
        }

        public static int[,] CreateMatrix(int rows, int cols, bool isRandom = true)
        {
            int[,] matrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = isRandom ? random.Next(-50, 51) : ReadInt($"[{i + 1}, {j + 1}]: ");
                }
            }

            return matrix;
        }

        public static void DeleteRows(ref int[,] matrix, int k1, int k2)
        {
            if (matrix == null)
            {
                Console.WriteLine("Ошибка: массив пуст!");
                return;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int deleteCount = k2 - k1 + 1;

            if (deleteCount >= rows)
            {
                matrix = null;
                Console.WriteLine("Все строки удалены. Массив стал пустым.");
                return;
            }

            int[,] newMatrix = new int[rows - deleteCount, cols];
            int newRow = 0;

            for (int i = 0; i < rows; i++)
            {
                int rowNumber = i + 1;
                if (rowNumber < k1 || rowNumber > k2)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        newMatrix[newRow, j] = matrix[i, j];
                    }

                    newRow++;
                }
            }

            matrix = newMatrix;
            Console.WriteLine($"Строки с {k1} по {k2} удалены.");
        }

        public static int[] CreateRow(int length, bool isRandom = true)
        {
            int[] row = new int[length];
            for (int i = 0; i < length; i++)
            {
                row[i] = isRandom ? random.Next(-50, 51) : ReadInt($"[{i + 1}]: ");
            }

            return row;
        }

        public static int[][] CreateJagged(int rows, bool isRandom = true)
        {
            int[][] jagged = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                int len = ReadInt($"Длина строки {i + 1} (1-20): ", 1, 20);
                jagged[i] = CreateRow(len, isRandom);
            }

            return jagged;
        }

        public static void AddRow(ref int[][] jagged, int pos, int[] newRow)
        {
            int rows = (jagged == null) ? 0 : jagged.Length;
            int[][] newJagged = new int[rows + 1][];
            int insertIndex = pos - 1;

            for (int i = 0; i < insertIndex; i++)
            {
                newJagged[i] = jagged[i];
            }

            newJagged[insertIndex] = newRow;

            for (int i = insertIndex; i < rows; i++)
            {
                newJagged[i + 1] = jagged[i];
            }

            jagged = newJagged;
            Console.WriteLine($"Строка добавлена на позицию {pos}.");
        }

        public static void PrintArray(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Console.WriteLine($"Двумерный массив ({rows}x{cols}):");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write("{0, 6}", matrix[i, j]);
                }

                Console.WriteLine();
            }
        }

        public static void PrintArray(int[][] jagged)
        {
            if (jagged == null || jagged.Length == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }

            Console.WriteLine($"Рваный массив ({jagged.Length} строк):");
            for (int i = 0; i < jagged.Length; i++)
            {
                Console.Write($"Строка {i + 1} ({jagged[i].Length} элементов): ");
                for (int j = 0; j < jagged[i].Length; j++)
                {
                    Console.Write("{0, 5}", jagged[i][j]);
                }

                Console.WriteLine();
            }
        }

        public static string ProcessText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "";
            }

            MatchCollection matches = Regex.Matches(text, @"([^.!?]+)([.!?]?)");
            string[] sentences = new string[matches.Count];
            int count = 0;

            foreach (Match match in matches)
            {
                string body = match.Groups[1].Value.Trim();
                string mark = match.Groups[2].Value;

                if (body.Length > 0)
                {
                    sentences[count] = ProcessSentence(body, mark);
                    count++;
                }
            }

            string[] result = new string[count];
            Array.Copy(sentences, result, count);
            return string.Join(" ", result);
        }

        public static string ProcessSentence(string body, string mark)
        {
            MatchCollection wordMatches = Regex.Matches(body, @"\b[\wА-Яа-яЁё0-9]+\b");
            if (wordMatches.Count == 0)
            {
                return body + mark;
            }

            string[] words = new string[wordMatches.Count];
            for (int i = 0; i < wordMatches.Count; i++)
            {
                char[] chars = wordMatches[i].Value.ToLower().ToCharArray();
                Array.Reverse(chars);
                words[i] = new string(chars);
            }

            Array.Sort(words);
            words[0] = char.ToUpper(words[0][0]) + words[0].Substring(1);

            return string.Join(" ", words) + mark;
        }
    }
}