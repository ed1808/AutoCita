using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoCita.FileHandler
{
    internal static class FileHandler<T> where T : class
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public static async Task<T?> GetData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }

                using var stream = File.OpenRead(filePath);
                return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
            } catch (Exception e)
            {
                Console.WriteLine($"Ha ocurrido un error: {e.Message}");
            }

            return null;
        }

        public static async Task<bool> SaveData(string filePath, T data)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using var stream = File.Create(filePath);
                await JsonSerializer.SerializeAsync(stream, data, _jsonOptions);

                return true;
            } catch (Exception e)
            {
                Console.WriteLine($"Ha ocurrido un error: {e.Message}");
                return false;
            }
        }
    }
}
