class Program
{
    static void Main()
    {
        // Создадим случайный массив длины больше, чем 255 (чтобы как минимум две части у нас были)
        byte[] bytes = GenerateRandomByteArray(500);
        const byte delimiter = 127;
        int partNumber = 0;

        // Читаем входной поток
        using var stream = new MemoryStream(bytes);

        var reader = new MessageReader();

        while (true)
        {
            byte[] message = reader.ReadMessage(stream, delimiter);

            if (message.Length == 0)
                break;

            partNumber++;

            Console.WriteLine($"Received message (part number {partNumber}): {BitConverter.ToString(message)}");
        }
    }

    /// <summary>
    /// Случайный массив байт длины n
    /// </summary>
    static byte[] GenerateRandomByteArray(int n)
    {
        var rand = new Random();
        var byteArray = new byte[n];

        rand.NextBytes(byteArray);

        return byteArray;
    }
}
