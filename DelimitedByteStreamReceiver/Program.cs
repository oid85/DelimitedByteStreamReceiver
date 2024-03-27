class Program
{
    static async Task Main()
    {
        // Создадим случайный массив длины больше, чем 255 (чтобы как минимум две части у нас были)
        byte[] bytes = GenerateRandomByteArray(100);
        byte delimiter = bytes[25]; // Разделитель - элемент массива с индексом 100
        int partNumber = 0; // Номер пакеты (для отображения результата)

        Console.WriteLine($"Input message: {String.Join(',', bytes)}");
        Console.WriteLine($"Delimiter is: {delimiter}");

        CancellationTokenSource cancelTokenSource = new();
        CancellationToken cancellationToken = cancelTokenSource.Token;

        // Читаем входной поток
        using var stream = new MemoryStream(bytes);

        var reader = new CustomStreamReader();

        // Пока не встретится конец потока
        while (stream.ReadByte() != -1 && !cancellationToken.IsCancellationRequested)
        {
            byte[] message = await reader.ReadMessageAsync(stream, delimiter, cancellationToken);

            if (message.Length == 0)
                break;

            partNumber++;

            Console.WriteLine($"Received message (part number {partNumber}): {String.Join(',', message)}");
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
