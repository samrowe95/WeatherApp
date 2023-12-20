using System.Text.Json;

namespace WeatherApp.Services
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, List<T> values)
        {
            session.SetString(key, JsonSerializer.Serialize(values));
        }

        public static IEnumerable<T>? Get<T>(this ISession session, string key)
        {
            var values = session.GetString(key);
            return values == null
           ? default
           : JsonSerializer.Deserialize<List<T>>(values);
        }
    }
}
