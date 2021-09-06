using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// Метод для вывода на консоль приветствия.
        /// </summary>
        public static void WriteText()
        {
            Console.WriteLine("                                              Привет!Это матричный калькулятор" + Environment.NewLine +
                Environment.NewLine + "По ходу программы тебе будут выводится подсказки.Уточнение, программа работает с интовыми значениями"
                + Environment.NewLine + "В папке с программой есть пример файла из которого мы получаем значения для матрицы." + Environment.NewLine);
            do
            {
                Console.WriteLine("Нажмите Enter для старта!" + Environment.NewLine);
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }
        /// <summary>
        /// Метод выбора операции.
        /// </summary>
        /// <param name="matrix">Многомерный массив, который представляет собой матрицу</param>
        /// <param name="height">Высота матрицы</param>
        /// <param name="length">Длина матрицы</param>
        public static void CheckConsole(int[,] matrix, int height, int length)
        {
            int num;
            Console.Clear();
            Console.WriteLine("                               Введи число, соответствующее номеру операции." + Environment.NewLine +
                "1)Умножение матрицы на число." + Environment.NewLine + "2)Нахождение следа матрицы(Только для квадратных матриц)." + Environment.NewLine +
                "3)Нахождение суммы матриц." + Environment.NewLine + "4)Нахождение разности матриц." + Environment.NewLine +
                "5)Получение определителя(Только для матриц 2х2 и 3х3)" + Environment.NewLine + "6)Транспонирование матрицы" + Environment.NewLine +
                "7)Умножение матрицы на матрицу(При вводе параметров второй матрицы учтите, что умножение происходит,когда количество строк первой матрицы," +
                "равно количеству столбцов второй)" + Environment.NewLine);
            Console.WriteLine("Номер операции:");
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > 7)
            // Использование while для выбора номера операции.
            {
                Console.WriteLine("Введи число еще раз");
            }
            Console.WriteLine(Environment.NewLine);
            if (num == 1)
            {
                MultiplicationOfMatrix(matrix);
            }
            if (num == 2)
            {
                TraceOfMatrix(matrix);
            }
            if (num == 3)
            {
                SumOfMatrix(matrix, height, length);
            }
            if (num == 4)
            {
                DifferenceOfMatrix(matrix, height, length);
            }
            if (num == 5)
            {
                GetDeterminant(matrix);
            }
            if (num == 6)
            {
                TranspositionOfMatrix(matrix);
            }
            if (num == 7)
            {
                MultiplicationMatrixOnMatrix(matrix, height, length);
            }
        }
        /// <summary>
        /// Получение и проверка параметров матрицы из консоли.
        /// </summary>
        /// <param name="nameOfParameter">Имя параметра</param>
        /// <param name="parameter">Значение параметра</param>
        public static int CheckParameters(string nameOfParameter, out int parameter)
        {
            int count = 1;
            do
            {
                if (count == 1)
                {
                    Console.WriteLine($"Введи значение {nameOfParameter}:");
                    count++;
                }
                else
                    Console.WriteLine($"Значение {nameOfParameter} было введено неверно");
            } while (!int.TryParse(Console.ReadLine(), out parameter) || parameter < 2 || parameter > 10);
            // Проверка введенных значений.
            return parameter;
        }
        /// <summary>
        /// Нахождение следа матрицы.
        /// </summary>
        /// <param name="matrix">Матрица</param>
        public static void TraceOfMatrix(int[,] matrix)
        {
            int sum = 0;
            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    sum += matrix[i, i];
                }
            }
            else
            {
                Console.WriteLine("След матрицы можно находить только для квадратных матриц!Для прямоугольных матриц определитель равен 0");
            }
            Console.WriteLine($"Cлед матрицы:{sum}");
            Console.WriteLine(Environment.NewLine);
            ViewMatrix(matrix);
        }
        /// <summary>
        /// Уможение матрицы на число.
        /// </summary>
        /// <param name="matrix">Матрица</param>
        public static void MultiplicationOfMatrix(int[,] matrix)
        {
            int numToMultiply;
            Console.WriteLine("Введи число на которое будешь умножать матрицу");
            bool temp;
            do
            {
                temp = false;
                if (!int.TryParse(Console.ReadLine(), out numToMultiply))
                {
                    Console.WriteLine("Неверный ввод. Введи число снова");
                    temp = true;
                }
            } while (temp);
            // Проверка числа на корректность.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] *= numToMultiply;
                }
            }
            Console.WriteLine(Environment.NewLine);
            ViewMatrix(matrix);
        }
        /// <summary>
        /// Заполнение матрицы числами.
        /// </summary>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        /// <returns>Заполненный многомерный массив</returns>
        public static int[,] GetMatrix(int height, int length)
        {
            int numOfOperation;
            int[,] matrix = new int[height, length];
            Console.WriteLine("                                Введи число, соответствующее выбранной операции заполнения матрицы." + Environment.NewLine +
                "1.Заполнение случайными числами" + Environment.NewLine + "2.Заполнение вводом из консоли" + Environment.NewLine +
                "3.Заполнение из файла." + Environment.NewLine +
                "Примечание:" + Environment.NewLine + "1)В файле числа матрицы должны быть расположены в одной строке и количество не должно превышать введенной размерности"
                + Environment.NewLine + "2)Формат файла должен быть строго .txt" + Environment.NewLine);
            Console.WriteLine("Номер операции:");
            while (!int.TryParse(Console.ReadLine(), out numOfOperation) || numOfOperation < 1 || numOfOperation > 3)
            {
                Console.WriteLine("Число введено неверно.");
            }
            if (numOfOperation == 1)
            {
                RandomNumbersForMatrix(matrix);
            }
            if (numOfOperation == 3)
            {
                GetMatrixOnFile(matrix);
            }
            if (numOfOperation == 2)
            {
                NumbersToMatrixFromConsole(matrix);
            }
            return matrix;
        }
        /// <summary>
        /// Метод для заполнения матрицы рандомными числами.
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <returns>Заполненный массив</returns>
        public static int[,] RandomNumbersForMatrix(int[,] matrix)
        {
            Random rand = new Random();
            // Использование метода Random для заполнения массива случайными числами.
            int numOfBeg, numOfEnd;
            bool temp;
            Console.WriteLine("Введи первое число диапазона из которого будут выбираться случайные числа");
            do
            {
                temp = false;
                if (!int.TryParse(Console.ReadLine(), out numOfBeg))
                {
                    Console.WriteLine("Неверно введенное значение. Введите число еще раз");
                    temp = true;
                }
            } while (temp);
            Console.WriteLine("Введи последнее число из диапозона ");
            do
            {
                temp = false;
                if (!int.TryParse(Console.ReadLine(), out numOfEnd))
                {
                    Console.WriteLine("Неверно введенное значение. Введите число еще раз");
                    temp = true;
                }
            } while (temp);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rand.Next(numOfBeg, numOfEnd);
                }
            }
            // Заполнение массива.
            return matrix;
        }
        /// <summary>
        /// Вычисление суммы матриц.
        /// </summary>
        /// <param name="firstMatrix">Матрица</param>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        public static void SumOfMatrix(int[,] firstMatrix, int height, int length)
        {
            Console.Clear();
            Console.WriteLine("Сейчас создадим матрицу в соответствии с размерностью которую вводили в начале программы." + Environment.NewLine);
            int[,] secondMatrix = GetMatrix(height, length);
            // Создание и заполнение нового многомерного массива чисел.
            int[,] sumOfMatrix = new int[height, length];
            for (int i = 0; i < firstMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < firstMatrix.GetLength(1); j++)
                {
                    sumOfMatrix[i, j] = firstMatrix[i, j] + secondMatrix[i, j];
                }
            }
            Console.WriteLine(Environment.NewLine + "Первая матрица:" + Environment.NewLine);
            ViewMatrix(firstMatrix);
            Console.WriteLine(Environment.NewLine + " Вторая матрица: " + Environment.NewLine);
            ViewMatrix(secondMatrix);
            Console.WriteLine(Environment.NewLine + " Сумма матриц: " + Environment.NewLine);
            ViewMatrix(sumOfMatrix);
            // Вывод всех матриц на консоль.
        }
        /// <summary>
        /// Разность матриц.
        /// </summary>
        /// <param name="firstMatrix">Матрица</param>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        public static void DifferenceOfMatrix(int[,] firstMatrix, int height, int length)
        {
            Console.Clear();
            Console.WriteLine("Сейчас создадим матрицу в соответствии с размерностью которую вводили в начале программы." + Environment.NewLine);
            int[,] secondMatrix = GetMatrix(height, length);
            int[,] difOfMatrix = new int[height, length];
            for (int i = 0; i < firstMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < firstMatrix.GetLength(1); j++)
                {
                    difOfMatrix[i, j] = firstMatrix[i, j] - secondMatrix[i, j];
                }
            }
            Console.WriteLine(Environment.NewLine + "Первая матрица:" + Environment.NewLine);
            ViewMatrix(firstMatrix);
            Console.WriteLine(Environment.NewLine + " Вторая матрица: " + Environment.NewLine);
            ViewMatrix(secondMatrix);
            Console.WriteLine(Environment.NewLine + " Разность матриц: " + Environment.NewLine);
            ViewMatrix(difOfMatrix);
        }
        /// <summary>
        /// Получение матрицы из файла.
        /// </summary>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        /// <returns>Заполненный массив из файла</returns>
        public static int[,] GetMatrixOnFile(int [,] matrix)
        {
            bool temp = true;
            string textOnFile = "";
            do
            {
                Console.WriteLine("Введи полный путь файла в соответствии с операционной системой.");
                string pathOfFile = Console.ReadLine();
                if (File.Exists(pathOfFile))
                {
                    textOnFile = File.ReadAllText(pathOfFile);
                    temp = false;
                }
                else
                {
                    Console.WriteLine("Файл не найден");
                }
            } while (temp);
            // Проверка на наличие файла.
            string[] result = textOnFile.Split(new char[] { ' ' });
            int k = 0;
            int j = 0;
            for (int i = 0; i < result.Length; i++)
            {
                matrix[k, j] = Convert.ToInt32(result[i]);
                j++;
                if (i == 2)
                {
                    k++;
                    j = 0;
                }
                else
                if (i == 5)
                {
                    k++;
                    j = 0;
                }
            }
            // Заполнение массива числами.
            return matrix;
        }
        /// <summary>
        /// Получение значений матрицы из консоли.
        /// </summary>
        /// <param name="matrix">Массив</param>
        /// <returns>Заполненный массив</returns>
        public static int[,] NumbersToMatrixFromConsole(int[,] matrix)
        {
            int num;
            int count = 1;
            bool temp = false;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    do
                    {
                        Console.WriteLine(Environment.NewLine + $"Введи число №{count}");
                        if (int.TryParse(Console.ReadLine(), out num))
                        {
                            count++;
                        }
                        else
                        {
                            Console.WriteLine($"Некорректный ввод. Введи число №{count} еще раз");
                            temp = true;
                        }
                    } while (temp);
                    matrix[i, j] += num;
                }
            }
            // Ввод чисел и присваивание определенному значению массива.
            return matrix;
        }
        /// <summary>
        /// Выбор параметра для значения.
        /// </summary>
        /// <param name="length">Длина</param>
        /// <param name="height">Высота</param>
        public static void GetParameters(ref int length, ref int height)
        {
            int count = 0;
            string name;
            for (; ; )
            {
                do
                {
                    if (count == 0)
                    {
                        Console.WriteLine("                               Введи первую букву параметра. Д-длина или В-высота" + Environment.NewLine +
                            "Параметры матрицы ограничены до 10 значений" + Environment.NewLine);
                        count++;
                    }
                    else
                    {
                        Console.WriteLine("Введи букву еще раз ");
                    }
                    Console.WriteLine("Буква:");
                    name = Console.ReadLine();
                } while (name != "Д" && name != "д" && name != "в" && name != "В");
                if (length == 0 && name == "д" || name == "Д")
                {
                    string nameOfParameter = "длины";
                    CheckParameters(nameOfParameter, out length);
                    count = 0;
                }
                if (height == 0 && name == "в" || name == "В")
                {
                    string nameOfParameter = "высоты";
                    CheckParameters(nameOfParameter, out height);
                    count = 0;
                }
                if (length != 0 && height != 0)
                // Проверка на то введены ли оба значения параметров.
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Получение определителя матрицы.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Определитель</returns>
        public static void GetDeterminant(int[,] matrix)
        {
            int let = 1;
            int j, c, dif, sum;
            int det = 0;
            int let1 = 1;
            if (matrix.GetLength(0) == matrix.GetLength(1) && matrix.GetLength(0) == 2)
            {
                j = 0;
                c = 1;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    let *= matrix[i, j];
                    j++;
                }
                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    let1 *= matrix[k, c];
                    c--;
                }
                det = let - let1;
            }
            if (matrix.GetLength(0) == matrix.GetLength(1) && matrix.GetLength(0) == 3)
            {
                j = 0;
                c = 2;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    let *= matrix[i, j];
                    j++;
                }
                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    let1 *= matrix[k, c];
                    c--;
                }
                sum = let + (matrix[0, 2] * matrix[1, 0] * matrix[2, 1]) + (matrix[0, 1] * matrix[1, 2] * matrix[2, 0]);
                dif = let1 + (matrix[0, 0] * matrix[1, 2] * matrix[2, 1]) + (matrix[0, 1] * matrix[2, 2] * matrix[1, 0]);
                det = sum - dif;
            }
            // Смог собрать определитель только для матриц 2 на 2 и 3 на 3.
            // Если не трудно не снижай за второй пункт.
            // Хоть какой-то определитель сделал).
            else
            {
                Console.WriteLine("Определитель вычисляется только для квадратных матриц.");
            }
            Console.WriteLine("Матрица:" + Environment.NewLine);
            ViewMatrix(matrix);
            Console.WriteLine(Environment.NewLine + $"Определитель матрицы:{det}");
        }
        /// <summary>
        /// Транспонирование матрицы.
        /// </summary>
        /// <param name="matrix">Массив</param>
        public static void TranspositionOfMatrix(int[,] matrix)
        {
            int[,] transpMatrix = new int[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    transpMatrix[j, i] = matrix[i, j];
                }
            }
            // Транспонирование матрицы.
            Console.WriteLine("Начальная матрица:" + Environment.NewLine);
            ViewMatrix(matrix);
            Console.WriteLine("Трапспонированная матрица:" + Environment.NewLine);
            ViewMatrix(transpMatrix);
        }
        /// <summary>
        /// Умножение матрицы на матрицу.
        /// </summary>
        /// <param name="matrix">Массив</param>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        public static void MultiplicationMatrixOnMatrix(int[,] matrix, int height, int length)
        {
            Console.Clear();
            int lengthOfSecondMatrix = 0;
            int heightOfSecondMatrix = 0;
            int num;
            int[,] newMatrix = new int[0, 0];
            Console.WriteLine("Сейчас введем параметры матрицы и создадим ее." + Environment.NewLine);
            GetParameters(ref lengthOfSecondMatrix, ref heightOfSecondMatrix);
            Console.Clear();
            int[,] secondMatrix = GetMatrix(heightOfSecondMatrix, lengthOfSecondMatrix);
            // Вызов методов получения параметров и заполнения матрицы.
            Console.Clear();
            Console.WriteLine("Введи число, соответствующее операции умножения." + Environment.NewLine +
                "1)Умножение А на В" + Environment.NewLine + "2)Умножение В на А");
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > 2)
            // Выбор варианта умножения.
            {
                Console.WriteLine("Введи число операции еще раз");
            }
            if (num == 1)
            {
                if (length == heightOfSecondMatrix)
                {
                    newMatrix = new int[height, lengthOfSecondMatrix];
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < lengthOfSecondMatrix; j++)
                        {
                            for (int k = 0; k < length; k++)
                            {
                                newMatrix[i, j] += matrix[i, k] * secondMatrix[k, j];
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Умножение невозможно. Число строк первой матрицы должно быть равно числу столбцов второй.");
                }
            }
            if (num == 2)
            {
                if (lengthOfSecondMatrix == height)
                {
                    newMatrix = new int[heightOfSecondMatrix, length];
                    for (int i = 0; i < heightOfSecondMatrix; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            newMatrix[i, j] += matrix[i, j] * secondMatrix[j, i];
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Умножение невозможно.Число строк первой матрицы должно быть равно числу столбцов второй.");
                }
            }
            Console.WriteLine(Environment.NewLine + "Первая матрица:" + Environment.NewLine);
            ViewMatrix(matrix);
            Console.WriteLine(Environment.NewLine + " Вторая матрица: " + Environment.NewLine);
            ViewMatrix(secondMatrix);
            Console.WriteLine(Environment.NewLine + " Произведение матриц: " + Environment.NewLine);
            ViewMatrix(newMatrix);
        }
        /// <summary>
        /// Вывод матрицы на консоль.
        /// </summary>
        /// <param name="matrix">Многомерный массив,который обозначает матрицу</param>
        public static void ViewMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            // Вывод матриц на консоль.
        }
        static void Main()
        {
            try
            {
                do
                {
                    Console.Clear();
                    int length = 0;
                    int height = 0;
                    WriteText();
                    Console.Clear();
                    GetParameters(ref length, ref height);
                    Console.Clear();
                    CheckConsole(GetMatrix(length, height), height, length);
                    //Вызов методов для работы калькулятора.
                    //Использование  Console.Clear() и Environment.NewLine для эстетичности работы.
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Нажмите Escape для выхода из программы." + Environment.NewLine + " Для продолжения нажмите любую клавишу");
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
                // Повтор решения.
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
