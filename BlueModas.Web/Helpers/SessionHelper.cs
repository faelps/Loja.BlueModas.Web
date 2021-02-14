using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlueModas.Web.Helpers
{
    public static class SessionHelper
    {

        public static void ObterObjetoJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T ObterObjetoDoJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
