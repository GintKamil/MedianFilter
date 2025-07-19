
namespace MedianFilterTestWork
{
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

        public override string ToString() => $"({Red}, {Green}, {Blue})";
    }
}
