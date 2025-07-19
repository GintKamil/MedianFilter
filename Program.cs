using MedianFilterTestWork;
using System;
using System.Text;

internal class Program {
    static void Main()
    {
        RGB[][] matrix = InputMatrix();
        
        Console.WriteLine("Исходная матрица");
        Console.WriteLine(OutputMatrix(matrix));

        MedianFilter(matrix);

        Console.WriteLine("Медианная фильтрация");
        Console.WriteLine(OutputMatrix(matrix));

    }

    static void MedianFilter(RGB[][] matrix) // Основной метод для фильтрации
    {
        List<byte> RedList = GetColorList(matrix, pixel => pixel.Red); // Разделение красного от общего
        List<byte> GreenList = GetColorList(matrix, pixel => pixel.Green); // Разделение зеленого от общего
        List<byte> BlueList = GetColorList(matrix, pixel => pixel.Blue); // Разделение синего от общего

        byte[] ModifiedRedArray = ArrayOfAverageColorValues(RedList); // Массив медиан красного
        byte[] ModifiedGreenArray = ArrayOfAverageColorValues(GreenList); // Массив медиан зеленого
        byte[] ModifiedBlueArray = ArrayOfAverageColorValues(BlueList); // Массив медиан синего

        int indexCount = 0;
        // Цикл для заполнения матрицы изменными значениями
        for(int i = 0; i < matrix.Length; i++)
        {
            for(int j = 0; j < matrix[i].Length; j++)
            {
                matrix[i][j] = new RGB(ModifiedRedArray[indexCount], ModifiedGreenArray[indexCount], ModifiedBlueArray[indexCount]); 
                indexCount++;
            }
        }
    }

    static List<byte> GetColorList(RGB[][] matrix, Func<RGB, byte> colorSelector) // Метод для получения list цвета
    {
        var colorList = new List<byte>();
        for (int i = 0; i < matrix.Length; i++)
            for (int j = 0; j < matrix[i].Length; j++)
                colorList.Add(colorSelector(matrix[i][j]));
        return colorList;
    }

    static byte[] ArrayOfAverageColorValues(List<byte> ColorList) // Метод для получения массива медиан одного из канала (Red, Green, Blue)
    {
        byte[] resultArray = new byte[ColorList.Count];

        for (int i = 1; i < ColorList.Count - 1; i++)
            resultArray[i] = MedianOfThree(ColorList[i - 1], ColorList[i], ColorList[i + 1]); // Определяем медиану выборки

        resultArray[0] = (byte)((ColorList[0] + ColorList[1]) / 2);
        resultArray[^1] = (byte)((ColorList[^1] + ColorList[^2]) / 2);
        return resultArray;
    }

    static byte MedianOfThree(byte a, byte b, byte c) // Метод для нахождения среднего числа из 3 элементов
    {
        if (a > b) (a, b) = (b, a);
        if (b > c) (b, c) = (c, b);
        if (a > b) (a, b) = (b, a);
        return b;
    }

    static RGB[][] InputMatrix() // Ввод матрицы
    {
        Console.WriteLine("Введите размер матрицы N*M (введите через пробел два числа)");
        string[] s = Console.ReadLine().Split();
        int n = int.Parse(s[0]), m = int.Parse(s[1]);
        Console.WriteLine("Выберите вид заполнения массива:\n1. Ручной ввод\n2. Рандомный ввод\nНапишите цифру (если будет написано число не 1 или не 2, то по умолчанию рандомный ввод)");
        int way = int.Parse(Console.ReadLine());
        RGB[][] matrix = new RGB[n][];

        return way == 1 ? InputMatrixManualEntry(n, m) : InputMatrixRandom(n, m);
    }

    static RGB[][] InputMatrixManualEntry(int n, int m) // Ручной ввод матрицы
    {
        RGB[][] matrix = new RGB[n][];
        Console.WriteLine("Теперь ввод данных для каждого пикселя.\nДанный для каждого вводить через пробел, без пробела в начале и в конце (Например - '10 41 87')");
        int countMatrix = 0;
        for (int i = 0; i < n; i++)
        {
            matrix[i] = new RGB[m];
            for (int j = 0; j < m; j++)
            {
                Console.WriteLine($"Введите значение для {countMatrix + 1} элемента");
                string[] str = Console.ReadLine().Split();
                matrix[i][j] = new RGB(byte.Parse(str[0]), byte.Parse(str[1]), byte.Parse(str[2]));
            }
        }
        return matrix;
    }

    static RGB[][] InputMatrixRandom(int n, int m) // Рандомный ввод матрицы
    {
        RGB[][] matrix = new RGB[n][];
        Random random = new Random();
        for (int i = 0; i < n; i++)
        {
            matrix[i] = new RGB[m];
            for (int j = 0; j < m; j++)
                matrix[i][j] = new RGB((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
        }
        return matrix;
    }

    static string OutputMatrix(RGB[][] matrix) // Вывод матрицы
    {
        StringBuilder sb = new();
        for (int i = 0; i < matrix.Length; i++)
        {
            sb.Append("[ ");
            for (int j = 0; j < matrix[i].Length; j++)
            {
                sb.Append(matrix[i][j].ToString() + " ");
            }
            sb.AppendLine("]");
        }
        return sb.ToString();
    }
}
