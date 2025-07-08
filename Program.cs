using System;

internal class Program {
    static void Main()
    {
        RGB[][] matrix = InputMatrix();
        
        Console.WriteLine("Исходная матрица");
        Console.WriteLine(OutputMatrix(matrix));

        MedianFilter(ref matrix);

        Console.WriteLine("Медианная фильтрация");
        Console.WriteLine(OutputMatrix(matrix));

    }

    static void MedianFilter(ref RGB[][] matrix) // Основной метод для фильтрации
    {
        List<byte> RedList = RedFilter(matrix); // Разделение красного от общего
        List<byte> GreenList = GreenFilter(matrix); // Разделение зеленого от общего
        List<byte> BlueList = BlueFilter(matrix); // Разделение синего от общего

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

    struct RGB { // Структура для пикселя
        public byte Red;
        public byte Green;
        public byte Blue;

        public RGB(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public override string ToString()
        {
            return $"({Red}, {Green}, {Blue})";
        }
    }

    static List<byte> RedFilter(RGB[][] matrix) // Получение List разделённого от общего Red
    {
        List<byte> colorList = new List<byte>();
        for (int i = 0; i < matrix.Length; i++)
            for (int j = 0; j < matrix[i].Length; j++)
                colorList.Add(matrix[i][j].Red);
        return colorList;
    }

    static List<byte> GreenFilter(RGB[][] matrix) // Получение List разделённого от общего Green
    {
        List<byte> colorList = new List<byte>();
        for (int i = 0; i < matrix.Length; i++)
            for (int j = 0; j < matrix[i].Length; j++)
                colorList.Add(matrix[i][j].Green);
        return colorList;
    }

    static List<byte> BlueFilter(RGB[][] matrix) // Получение List разделённого от общего Blue
    {
        List<byte> colorList = new List<byte>();
        for (int i = 0; i < matrix.Length; i++)
            for (int j = 0; j < matrix[i].Length; j++)
                colorList.Add(matrix[i][j].Blue);
        return colorList;
    }

    static byte[] ArrayOfAverageColorValues(List<byte> ColorList) // Метод для получения массива медиан одного из канал (Red, Green, Blue)
    {
        byte[] resultArray = new byte[ColorList.Count];

        byte[] IntermediateArray = new byte[3];

        for (int i = 0; i < ColorList.Count; i++)
        {
            if (i == 0) // Для первого и последнего элемента в выборку идут только 2 элемента
            {
                IntermediateArray[0] = ColorList[0];
                IntermediateArray[1] = ColorList[1];
            }
            else if (i == ColorList.Count - 1)
            {
                IntermediateArray[0] = ColorList[i - 1];
                IntermediateArray[1] = ColorList[i];
            }
            else
            {
                IntermediateArray[0] = ColorList[i - 1];
                IntermediateArray[1] = ColorList[i];
                IntermediateArray[2] = ColorList[i + 1];
            }
            resultArray[i] = AverageColorValue(IntermediateArray); // Определяем медиану выборки
        }
        return resultArray;
    }

    static byte AverageColorValue(byte[] ColorArray) // Метод для вычисления медианы выборки из 3 элементов
    {
        byte result;
        if (ColorArray[2] == 0) result = (byte)((ColorArray[0] + ColorArray[1]) / 2);
        else
        {
            Array.Sort(ColorArray);
            result = ColorArray[1];
        }
        return result;
    }

    static RGB[][] InputMatrix() // Ввод матрицы
    {
        Console.WriteLine("Введите размер матрицы N*N");
        int length = int.Parse(Console.ReadLine());
        Console.WriteLine("Теперь ввод данных для каждого пикселя.\nДанный для каждого вводить через пробел, без пробела в начале и в конце (Например - '10 41 87')");
        RGB[][] matrix = new RGB[length][];
        int countMatrix = 0;
        for(int i = 0; i < length; i++)
        {
            matrix[i] = new RGB[length];
            for(int j = 0; j < length; j++)
            {
                Console.WriteLine($"Введите значение для {countMatrix + 1} элемента");
                string[] str = Console.ReadLine().Split();
                matrix[i][j] = new RGB(byte.Parse(str[0]), byte.Parse(str[1]), byte.Parse(str[2]));
            }
        }
        return matrix;
    }

    static string OutputMatrix(RGB[][] matrix) // Вывод матрицы
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            Console.Write("[ ");
            for (int j = 0; j < matrix[i].Length; j++)
            {
                Console.Write(matrix[i][j].ToString() + " ");
            }
            Console.WriteLine(" ]");
        }
        return "\n";
    }
}
