/// <summary>
/// Класс, с методами для работы с байтовым потоком
/// </summary>
public class MessageReader
{
    /// <summary>
    /// Чтение потока, пока не встретится разделитель
    /// </summary>
    /// <param name="delimiter">Байт разделитель</param>
    public byte[] ReadMessage(Stream stream, byte delimiter)
    {
        try
        {
            using var messageBuffer = new MemoryStream();
            int currentByte;

            // Пока не встретится конец потока
            while ((currentByte = stream.ReadByte()) != -1)
            {
                // Если встретился разделитель, то прерываемся
                if (currentByte == delimiter)
                    break;

                messageBuffer.WriteByte((byte)currentByte);
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
