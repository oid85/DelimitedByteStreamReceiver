/// <summary>
/// Класс, с методами для работы с байтовым потоком
/// </summary>
public class CustomStreamReader
{
    /// <summary>
    /// Чтение потока, пока не встретится разделитель
    /// </summary>
    /// <param name="delimiter">Байт разделитель</param>
    public async Task<byte[]> ReadMessageAsync(Stream stream, byte delimiter, CancellationToken cancellationToken)
    {
        try
        {
            using var messageBuffer = new MemoryStream();
            int currentByte;

            // Пока не встретится конец потока
            while ((currentByte = stream.ReadByte()) != -1 && !cancellationToken.IsCancellationRequested)
            {
                // Если встретился разделитель, то прерываемся
                if (currentByte == delimiter)
                    break;

                await messageBuffer.WriteAsync(new byte[] { (byte) currentByte }, cancellationToken);
            }

            var result = messageBuffer.ToArray();

            return result;
        }

        catch (Exception exception)
        {
            Console.WriteLine($"ReadMessage error: {exception.Message}");
            
            return Array.Empty<byte>();
        }        
    }
}
