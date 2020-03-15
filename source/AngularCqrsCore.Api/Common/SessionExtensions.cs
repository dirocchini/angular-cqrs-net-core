using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.Common
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static void InternalSet(this ISession session, string key, object value)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, value);
            session.Set(key, ms.ToArray());
        }

        public static T InternalGet<T>(this ISession session, string key)
        {
            var value = session.Get(key);
            BinaryFormatter bf = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream(value);
            object obj = bf.Deserialize(ms);

            return (T)obj;
        }
    }
}
